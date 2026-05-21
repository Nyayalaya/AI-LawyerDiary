using System;

namespace CourtApp.Infrastructure.Email.Templates
{
    public static class RegistrationEmailTemplate
    {
        public static string GetApprovalConfirmedTemplate(string name)
        {
            throw new NotImplementedException();
        }

        public static string GetApprovalPendingTemplate(string fullName)
        {
            throw new NotImplementedException();
        }

        public static string GetApprovalPendingWithFreePlanTemplate(string fullName, DateTime? expiryDate)
        {
            var expiryDateFormatted = expiryDate?.ToString("MMMM dd, yyyy") ?? "One month from today";

            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; background: #f9f9f9; }}
                        .header {{ background: linear-gradient(135deg, #007bff 0%, #0056b3 100%); color: white; padding: 30px; text-align: center; border-radius: 5px 5px 0 0; }}
                        .header h1 {{ margin: 0; font-size: 28px; }}
                        .content {{ background: white; padding: 30px; border-radius: 0 0 5px 5px; }}
                        .section {{ margin: 20px 0; padding: 15px; background: #f0f8ff; border-left: 4px solid #007bff; border-radius: 3px; }}
                        .section h3 {{ margin: 0 0 10px 0; color: #007bff; }}
                        .features {{ list-style: none; padding: 0; }}
                        .features li {{ padding: 8px 0; }}
                        .features li:before {{ content: '✓ '; color: #28a745; font-weight: bold; margin-right: 10px; }}
                        .cta-button {{ display: inline-block; padding: 12px 30px; background: #007bff; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
                        .important {{ background: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; border-radius: 3px; margin: 20px 0; }}
                        .important strong {{ color: #856404; }}
                        .footer {{ text-align: center; padding: 20px; color: #666; font-size: 12px; border-top: 1px solid #eee; margin-top: 20px; }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <div class=""header"">
                            <h1>Welcome to Court App!</h1>
                            <p>Your registration is successful</p>
                        </div>
                        <div class=""content"">
                            <p>Dear <strong>{fullName}</strong>,</p>

                            <p>Thank you for registering with <strong>Court App</strong>. We're excited to have you onboard!</p>

                            <div class=""section"">
                                <h3>🎉 Free Plan Activated</h3>
                                <p>Your account has been created with our <strong>Free Plan</strong>, which includes:</p>
                                <ul class=""features"">
                                    <li>Full access to all features</li>
                                    <li>Unlimited case management</li>
                                    <li>Document upload and storage</li>
                                    <li>Email notifications</li>
                                    <li>Professional dashboard</li>
                                </ul>
                            </div>

                            <div class=""important"">
                                <strong>⏰ Important Notice:</strong><br/>
                                Your free plan is valid for <strong>1 month</strong> and will expire on <strong>{expiryDateFormatted}</strong>. 
                                After this date, you will need to subscribe to a paid plan to continue using Court App.
                            </div>

                            <div class=""section"">
                                <h3>Next Steps</h3>
                                <p>1. Your registration is pending admin approval</p>
                                <p>2. You will receive a confirmation email once approved</p>
                                <p>3. After approval, you can log in and start using all features</p>
                                <p>4. Upgrade your plan anytime before expiry to continue without interruption</p>
                            </div>

                            <p>If you have any questions about your account or our subscription plans, please don't hesitate to contact our support team.</p>

                            <p>Best regards,<br/><strong>Court App Team</strong></p>
                        </div>
                        <div class=""footer"">
                            <p>&copy; 2024 Court App. All rights reserved.</p>
                            <p>This is an automated email. Please do not reply directly to this message.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        public static string GetRejectionTemplate(string name, string reason)
        {
            throw new NotImplementedException();
        }

        public static string GetTemplate(string userName, string fullName, string verificationUrl)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; background: #f9f9f9; }}
                        .header {{ background: #007bff; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                        .content {{ background: white; padding: 30px; border-radius: 0 0 5px 5px; }}
                        .button {{ display: inline-block; padding: 12px 30px; background: #007bff; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
                        .footer {{ text-align: center; padding: 20px; color: #666; font-size: 12px; }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <div class=""header"">
                            <h1>Welcome to Court App!</h1>
                        </div>
                        <div class=""content"">
                            <p>Dear {fullName},</p>
                            <p>Thank you for registering with Court App. Your account has been created successfully.</p>
                            <p><strong>Username:</strong> {userName}</p>
                            <p>To complete your registration and activate your account, please click the button below:</p>
                            <a href=""{verificationUrl}"" class=""button"">Verify Email Address</a>
                            <p>Or copy and paste this link in your browser:</p>
                            <p><small>{verificationUrl}</small></p>
                            <p>This verification link will expire in 24 hours.</p>
                            <p>If you did not create this account, please ignore this email.</p>
                            <p>Best regards,<br/>Court App Team</p>
                        </div>
                        <div class=""footer"">
                            <p>&copy; 2024 Court App. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }
    }
}