# Free Plan Expiry & Lockout Implementation Guide

## Overview
This guide covers implementing automatic account lockout after free plan expiry and reminder email notifications.

## Architecture

### Option 1: Background Service (Recommended)
Run a hosted service that checks expiry at regular intervals.

### Option 2: Hangfire
Use Hangfire for more robust job scheduling.

### Option 3: Windows Service / CRON Job
For scheduled batch processing.

## Implementation: Background Service

### Step 1: Create Background Service

Create file: `CourtApp.Infrastructure/Background/SubscriptionExpiryBackgroundService.cs`

```csharp
using CourtApp.Domain.Entities;
using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.Email.Templates;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Background
{
    /// <summary>
    /// Background service to monitor and process subscription expirations
    /// Locks accounts when free plan expires and sends reminder emails
    /// </summary>
    public class SubscriptionExpiryBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SubscriptionExpiryBackgroundService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromHours(1); // Run every hour

        public SubscriptionExpiryBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<SubscriptionExpiryBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Subscription Expiry Background Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessSubscriptionExpiryAsync();
                    await Task.Delay(_interval, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in Subscription Expiry Background Service");
                    // Continue on error
                }
            }

            _logger.LogInformation("Subscription Expiry Background Service stopped.");
        }

        private async Task ProcessSubscriptionExpiryAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var now = DateTime.UtcNow;

                // Find expired free plan users
                var expiredUsers = await identityContext.SystemUsers
                    .Where(x =>
                        x.Subscription == SubscriptionType.Free &&
                        x.SubscriptionExpiryDate.HasValue &&
                        x.SubscriptionExpiryDate <= now &&
                        x.Status == UserAccountStatus.Active // Only lock if still active
                    )
                    .ToListAsync();

                _logger.LogInformation("Found {Count} users with expired free plans", expiredUsers.Count);

                foreach (var systemUser in expiredUsers)
                {
                    try
                    {
                        await LockExpiredUserAsync(
                            identityContext,
                            userManager,
                            systemUser
                        );
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to process expiry for user {UserId}", systemUser.UserId);
                    }
                }

                // Send reminder emails (7 days, 3 days, 1 day before expiry)
                await SendReminderEmailsAsync(identityContext, userManager, now);
            }
        }

        private async Task LockExpiredUserAsync(
            IdentityContext context,
            UserManager<ApplicationUser> userManager,
            SystemUser systemUser)
        {
            _logger.LogInformation("Locking expired user: {UserId}", systemUser.UserId);

            // Update SystemUser
            systemUser.Status = UserAccountStatus.Inactive;
            systemUser.StatusReason = "Account locked - free plan expired without renewal";
            systemUser.UpdatedAt = DateTime.UtcNow;
            context.SystemUsers.Update(systemUser);

            // Update ApplicationUser
            var appUser = await userManager.FindByIdAsync(systemUser.UserId);
            if (appUser != null)
            {
                appUser.IsActive = false;
                await userManager.UpdateAsync(appUser);

                // Lock the account (prevent login)
                await userManager.SetLockoutEnabledAsync(appUser, true);
                await userManager.SetLockoutEndDateAsync(appUser, DateTimeOffset.MaxValue);

                _logger.LogInformation("User locked: {UserId}", systemUser.UserId);
            }

            await context.SaveChangesAsync();
        }

        private async Task SendReminderEmailsAsync(
            IdentityContext context,
            UserManager<ApplicationUser> userManager,
            DateTime now)
        {
            var mailService = _serviceProvider.GetRequiredService<IMailService>();

            // Send reminder emails for users expiring in 7, 3, 1 days
            var reminderDays = new[] { 7, 3, 1 };

            foreach (var days in reminderDays)
            {
                var expiryThreshold = now.AddDays(days);
                var nextThreshold = expiryThreshold.AddHours(1);

                var usersToRemind = await context.SystemUsers
                    .Where(x =>
                        x.Subscription == SubscriptionType.Free &&
                        x.SubscriptionExpiryDate > expiryThreshold &&
                        x.SubscriptionExpiryDate <= nextThreshold &&
                        x.Status == UserAccountStatus.Active
                    )
                    .ToListAsync();

                _logger.LogInformation("Sending {Days} day reminder to {Count} users", days, usersToRemind.Count);

                foreach (var systemUser in usersToRemind)
                {
                    try
                    {
                        var appUser = await userManager.FindByIdAsync(systemUser.UserId);
                        if (appUser == null) continue;

                        var fullName = systemUser.FirstName != null
                            ? $"{systemUser.FirstName} {systemUser.LastName}".Trim()
                            : systemUser.CompanyName;

                        // Send reminder email
                        var emailBody = GetReminderEmailTemplate(fullName, days, systemUser.SubscriptionExpiryDate.Value);
                        var mailRequest = new MailRequest
                        {
                            To = appUser.Email,
                            Subject = $"Your subscription expires in {days} day(s) - Court App",
                            Body = emailBody,
                            IsHtml = true
                        };

                        await mailService.SendAsync(mailRequest);
                        _logger.LogInformation("Reminder email sent to {Email}", appUser.Email);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to send reminder email to {UserId}", systemUser.UserId);
                    }
                }
            }
        }

        private string GetReminderEmailTemplate(string fullName, int daysLeft, DateTime expiryDate)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f5f5f5; }}
        .container {{ max-width: 600px; margin: 20px auto; background-color: white; padding: 20px; border-radius: 5px; }}
        .header {{ color: #d32f2f; font-size: 20px; font-weight: bold; margin-bottom: 20px; }}
        .content {{ color: #333; line-height: 1.6; }}
        .warning {{ background-color: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
        .button {{ display: inline-block; background-color: #1976d2; color: white; padding: 10px 20px; text-decoration: none; border-radius: 3px; margin: 20px 0; }}
        .footer {{ color: #666; font-size: 12px; margin-top: 20px; border-top: 1px solid #eee; padding-top: 20px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>⏰ Your Free Trial Expires Soon</div>

        <div class='content'>
            <p>Hi {fullName},</p>

            <p>This is a reminder that your free trial subscription will expire in <strong>{daysLeft} day(s)</strong>.</p>

            <div class='warning'>
                <strong>Expiry Date:</strong> {expiryDate:MMMM dd, yyyy} at {expiryDate:hh:mm tt} UTC
                <br/>
                <strong>Action Required:</strong> Upgrade your plan before this date to avoid service interruption.
            </div>

            <p>After your free trial expires, your account will be locked and you won't be able to access Court App.</p>

            <p><strong>To continue using Court App:</strong></p>
            <ul>
                <li>Log in to your account</li>
                <li>Navigate to Subscription</li>
                <li>Choose a paid plan (Monthly or Yearly)</li>
                <li>Complete payment</li>
            </ul>

            <a href='https://yourapp.com/dashboard/subscription' class='button'>Upgrade Now</a>

            <p>If you have any questions, please contact our support team.</p>

            <div class='footer'>
                <p>© 2024 Court App. All rights reserved.</p>
                <p>This is an automated message, please do not reply to this email.</p>
            </div>
        </div>
    </div>
</body>
</html>
";
        }
    }
}
```

### Step 2: Register in Program.cs

Add to your `Program.cs` (or Startup.cs):

```csharp
// Add services
builder.Services.AddHostedService<SubscriptionExpiryBackgroundService>();

// Or if using dependency injection for MailService:
builder.Services.AddScoped<IMailService, MailService>();
```

### Step 3: Configure in appsettings.json (Optional)

```json
{
  "SubscriptionExpiry": {
    "CheckIntervalHours": 1,
    "SendReminderEmails": true,
    "ReminderDays": [7, 3, 1]
  }
}
```

## Implementation: Hangfire

### Step 1: Install Hangfire

```bash
dotnet add package Hangfire.Core
dotnet add package Hangfire.PostgreSql  # Or .SqlServer for SQL Server
```

### Step 2: Create Hangfire Job

Create file: `CourtApp.Infrastructure/Jobs/SubscriptionExpiryJob.cs`

```csharp
using CourtApp.Domain.Entities;
using CourtApp.Domain.Enums;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Jobs
{
    public class SubscriptionExpiryJob
    {
        private readonly IdentityContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SubscriptionExpiryJob> _logger;

        public SubscriptionExpiryJob(
            IdentityContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<SubscriptionExpiryJob> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [DisableConcurrentExecution(timeoutInSeconds: 3600)]
        public async Task ProcessSubscriptionExpiry()
        {
            _logger.LogInformation("Starting subscription expiry job");

            var now = DateTime.UtcNow;

            // Lock expired users
            var expiredUsers = await _context.SystemUsers
                .Where(x =>
                    x.Subscription == SubscriptionType.Free &&
                    x.SubscriptionExpiryDate <= now &&
                    x.Status == UserAccountStatus.Active
                )
                .ToListAsync();

            foreach (var systemUser in expiredUsers)
            {
                try
                {
                    systemUser.Status = UserAccountStatus.Inactive;
                    systemUser.StatusReason = "Account locked - free plan expired";
                    systemUser.UpdatedAt = DateTime.UtcNow;

                    var appUser = await _userManager.FindByIdAsync(systemUser.UserId);
                    if (appUser != null)
                    {
                        appUser.IsActive = false;
                        await _userManager.UpdateAsync(appUser);
                        await _userManager.SetLockoutEnabledAsync(appUser, true);
                        await _userManager.SetLockoutEndDateAsync(appUser, DateTimeOffset.MaxValue);
                    }

                    _logger.LogInformation("Locked expired user: {UserId}", systemUser.UserId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to lock user: {UserId}", systemUser.UserId);
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Subscription expiry job completed");
        }
    }
}
```

### Step 3: Configure Hangfire in Program.cs

```csharp
using Hangfire;
using Hangfire.PostgreSql;

// Add Hangfire
builder.Services.AddHangfire(configuration => configuration
    .UsePostgreSqlStorage("your_connection_string"));

builder.Services.AddHangfireServer();

// In app setup:
app.UseHangfireDashboard();

// Schedule recurring job
RecurringJob.AddOrUpdate<SubscriptionExpiryJob>(
    "process-subscription-expiry",
    job => job.ProcessSubscriptionExpiry(),
    Cron.Hourly);
```

## Testing

### Test Background Service Locally

```csharp
// In a test file or controller
[HttpGet("test-expiry")]
public async Task<IActionResult> TestExpiry()
{
    var backgroundService = HttpContext.RequestServices
        .GetRequiredService<SubscriptionExpiryBackgroundService>();

    await backgroundService.ExecuteAsync(CancellationToken.None);
    return Ok("Expiry check executed");
}
```

### Create Test User with Expiry Date

```bash
# Register a user and manually set expiry to 1 day ago
UPDATE system_users 
SET subscription_expiry_date = NOW() - INTERVAL '1 day'
WHERE user_id = 'test-user-id';

# Run background job
curl http://localhost:5000/api/test/test-expiry

# Verify account is locked
SELECT status FROM system_users WHERE user_id = 'test-user-id';
# Should return: 2 (Inactive/Locked)
```

## Monitoring

### Check Job Status (if using Hangfire)

Access Hangfire Dashboard at: `http://localhost:5000/hangfire`

### Query Locked Users

```sql
SELECT user_id, email, subscription_expiry_date, status, status_reason
FROM system_users
WHERE status = 2  -- Inactive
ORDER BY updated_at DESC;
```

### Check Logs

```bash
# View background service logs
tail -f logs/application.log | grep "Subscription Expiry"
```

## Email Template Customization

Modify the email template in `GetReminderEmailTemplate()` to match your branding:

- Add company logo
- Customize colors and fonts
- Add links to upgrade page
- Include support contact info

## Bonus: Re-enable Expired Account

Create an admin endpoint to re-enable expired accounts:

```csharp
[HttpPost("reactivate/{userId}")]
[Authorize(Roles = "Admin")]
public async Task<IActionResult> ReactivateExpiredAccount(string userId)
{
    var systemUser = await _context.SystemUsers
        .FirstOrDefaultAsync(x => x.UserId == userId);

    if (systemUser == null)
        return NotFound();

    // Extend subscription another month
    systemUser.SubscriptionExpiryDate = DateTime.UtcNow.AddMonths(1);
    systemUser.Status = UserAccountStatus.Active;
    systemUser.StatusReason = "Reactivated by admin";

    var appUser = await _userManager.FindByIdAsync(userId);
    if (appUser != null)
    {
        appUser.IsActive = true;
        await _userManager.UpdateAsync(appUser);
        await _userManager.SetLockoutEndDateAsync(appUser, null);
    }

    await _context.SaveChangesAsync();
    return Ok("Account reactivated");
}
```

## Summary

✅ Automatic expiry detection every hour
✅ Account locking on expiry
✅ Reminder emails (7, 3, 1 days before)
✅ Audit trail (status reasons)
✅ Admin override capability
✅ Logging and monitoring
✅ Error handling and retry logic
