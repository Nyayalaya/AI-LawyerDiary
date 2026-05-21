# Database Migration Guide

## Prerequisites
- .NET 9 SDK installed
- Entity Framework Core tools installed
- Database connection string configured

## Step 1: Create Migration

Run this command from the solution root directory:

```powershell
cd "D:\EnterpriseApps\DairyApps\LawyerDiary- AI\Service"

dotnet ef migrations add SystemUserEntity -p CourtApp.Infrastructure -s CourtApp.Api
```

**Expected Output:**
```
Build started...
Build succeeded.
An operation was scaffolded and has been written to file at [path]/CourtApp.Infrastructure/Migrations/[timestamp]_SystemUserEntity.cs
To undo this action, use 'ef migrations remove'
```

## Step 2: Review Migration

Check the generated migration file at:
```
CourtApp.Infrastructure/Migrations/[timestamp]_SystemUserEntity.cs
```

It should create:
- `SystemUsers` table with columns:
  - `Id` (Guid, PK)
  - `UserId` (string, FK to Users.Id, unique)
  - `UserType` (int)
  - `FirstName` (varchar(100), nullable)
  - `LastName` (varchar(100), nullable)
  - `Email` (varchar(256), indexed)
  - `CompanyName` (varchar(500), nullable)
  - `Subscription` (int, indexed)
  - `SubscriptionExpiryDate` (timestamp, nullable)
  - `SubscriptionStartDate` (timestamp, nullable)
  - `Status` (int, indexed)
  - `StatusReason` (varchar(500), nullable)
  - `RegisteredDate` (timestamp, indexed)
  - `LastLoginDate` (timestamp, nullable)
  - `IsEmailVerified` (bool)
  - `CreatedAt` (timestamp)
  - `UpdatedAt` (timestamp, nullable)

## Step 3: Apply Migration

Update the database with the new migration:

```powershell
dotnet ef database update -p CourtApp.Infrastructure -s CourtApp.Api
```

**Expected Output:**
```
Build started...
Build succeeded.
Applying migration '[timestamp]_SystemUserEntity'.
Done.
```

## Step 4: Verify

Check that the table was created in your database. For PostgreSQL:

```sql
\dt public.system_users

-- Or view columns:
\d+ public.system_users

-- Check indexes:
\di public.system_users*

-- View table structure:
SELECT * FROM information_schema.tables 
WHERE table_name = 'system_users';
```

For SQL Server:

```sql
SELECT * FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME = 'SystemUsers'

-- View columns
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'SystemUsers'
```

## Troubleshooting

### Issue: "Multiple DbContext types were found"
**Solution:** Specify both `-p` (project) and `-s` (startup project):
```powershell
dotnet ef migrations add SystemUserEntity -p CourtApp.Infrastructure -s CourtApp.Api
```

### Issue: "Unable to create an instance of DbContext"
**Solution:** Ensure connection string is in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your_Connection_String_Here"
  }
}
```

### Issue: "The migration 'SystemUserEntity' has already been applied"
**Solution:** The migration already exists. If you need to update it:
```powershell
# Remove the migration
dotnet ef migrations remove -p CourtApp.Infrastructure -s CourtApp.Api

# Re-create it
dotnet ef migrations add SystemUserEntity -p CourtApp.Infrastructure -s CourtApp.Api

# Apply
dotnet ef database update -p CourtApp.Infrastructure -s CourtApp.Api
```

### Issue: Foreign key constraint fails
**Solution:** Make sure all ApplicationUsers exist before SystemUsers. The service creates them in correct order:
1. First: ApplicationUser (via UserManager)
2. Then: SystemUser (with UserId FK)

## Rollback

If you need to rollback the migration:

```powershell
# Remove last applied migration from database
dotnet ef database update [previous_migration_name] -p CourtApp.Infrastructure -s CourtApp.Api

# Or rollback to initial state
dotnet ef database update 0 -p CourtApp.Infrastructure -s CourtApp.Api

# Remove migration file from project
dotnet ef migrations remove -p CourtApp.Infrastructure -s CourtApp.Api
```

## Verify Schema After Migration

### Query SystemUsers table
```sql
SELECT * FROM system_users LIMIT 10;
```

### Check Foreign Keys
```sql
SELECT constraint_name, table_name, column_name, referenced_table_name, referenced_column_name
FROM information_schema.referential_constraints
WHERE table_name = 'system_users';
```

### Verify Indexes
```sql
SELECT indexname FROM pg_indexes 
WHERE tablename = 'system_users';
```

Expected indexes:
- `system_users_pkey` - Primary key on Id
- `ix_system_users_userid` - Unique index on UserId
- `ix_system_users_email` - Index on Email
- `ix_system_users_usertype` - Index on UserType
- `ix_system_users_status` - Index on Status
- `ix_system_users_subscription` - Index on Subscription
- `ix_system_users_registereddate` - Index on RegisteredDate

## Testing the Migration

After applying the migration, test registration:

```bash
# Register a new user
curl -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Password123!",
    "userType": 0,
    "firstName": "Test",
    "lastName": "User",
    "phoneNumber": "+1234567890"
  }'
```

Then verify data in both tables:

```sql
-- Check ApplicationUser was created
SELECT id, email, user_type, is_active FROM users 
WHERE email = 'test@example.com';

-- Check SystemUser was created
SELECT id, user_id, subscription, subscription_expiry_date, status 
FROM system_users 
WHERE user_id = (SELECT id FROM users WHERE email = 'test@example.com');
```

Both records should exist and have matching user_id/id.

## Performance Optimization

After migration, run these for better performance:

```sql
-- Analyze statistics (PostgreSQL)
ANALYZE system_users;

-- Or for SQL Server
UPDATE STATISTICS system_users;
```

## Backup Before Migration

Always backup your database before running migrations:

```bash
# PostgreSQL
pg_dump database_name > backup_before_migration.sql

# SQL Server
sqlcmd -S server_name -U user -P password -Q "BACKUP DATABASE database_name TO DISK = 'backup_before_migration.bak'"
```

## Next Steps

After successful migration:

1. ✅ Test registration flow (creates ApplicationUser + SystemUser)
2. ✅ Test GetAllRegisteredUsers query
3. ✅ Test UpdateSystemUser (change subscription/status)
4. ✅ Set up background job for free plan expiry
5. ✅ Configure admin approval workflow

## Commands Reference

```powershell
# Create migration
dotnet ef migrations add <name> -p CourtApp.Infrastructure -s CourtApp.Api

# List migrations
dotnet ef migrations list -p CourtApp.Infrastructure -s CourtApp.Api

# Update database
dotnet ef database update -p CourtApp.Infrastructure -s CourtApp.Api

# Rollback last migration
dotnet ef database update <previous_migration> -p CourtApp.Infrastructure -s CourtApp.Api

# Remove migration
dotnet ef migrations remove -p CourtApp.Infrastructure -s CourtApp.Api

# Generate SQL script
dotnet ef migrations script -p CourtApp.Infrastructure -s CourtApp.Api -o migration_script.sql

# Update to specific migration
dotnet ef database update <target_migration> -p CourtApp.Infrastructure -s CourtApp.Api
```
