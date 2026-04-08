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