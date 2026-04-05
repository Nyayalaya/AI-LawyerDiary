using CourtApp.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CourtApp.Application.Constants
{
    public static class Permissions
    {
        public static readonly Dictionary<string, Dictionary<string, List<string>>> Modules = new()
        {
            { "Admin", new Dictionary<string, List<string>>
                {
                    { "Menu", new()
                        {
                            "AdminPanel.Menu.canAccessAdminPanel",
                            "AdminPanel.Menu.User",
                            "AdminPanel.Menu.Role",
                            "AdminPanel.Menu.FormBuilder",
                            "AdminPanel.Menu.LawyerDirectory",
                            "AdminPanel.Menu.Client",
                            "AdminPanel.Menu.Associate",
                            "AdminPanel.Menu.Template",
                            "AdminPanel.Menu.CaseForm"
                        }
                    },
                    { "User", new() { "AdminPanel.User.Create", "AdminPanel.User.View", "AdminPanel.User.Edit", "AdminPanel.User.Delete" } },
                    { "Role", new() { "AdminPanel.Role.Create", "AdminPanel.Role.View", "AdminPanel.Role.Edit", "AdminPanel.Role.Delete" } },
                }
            },

            { "Case", new Dictionary<string, List<string>>
                {
                    { "Menu", new()
                        {
                            "CasePanel.Menu.canAccessCasePanel",
                            "CasePanel.Menu.ManageCase",
                            "CasePanel.Menu.CompleteTitle",
                            "CasePanel.Menu.TodayHearing",
                            "CasePanel.Menu.CaseSearch"
                        }
                    },
                    { "ManageCase", new()
                        {
                            "CasePanel.ManageCase.Create",
                            "CasePanel.ManageCase.View",
                            "CasePanel.ManageCase.Edit",
                            "CasePanel.ManageCase.Delete",
                            "CasePanel.ManageCase.DocUpload",
                            "CasePanel.ManageCase.Detail"
                        }
                    },
                    { "CompleteTitle", new()
                        {
                            "CasePanel.CompleteTitle.Create",
                            "CasePanel.CompleteTitle.View",
                            "CasePanel.CompleteTitle.Edit",
                            "CasePanel.CompleteTitle.Delete"
                        }
                    }
                }
            },

            { "Register", new Dictionary<string, List<string>>
                {
                    { "Menu", new()
                        {
                            "Register.Menu.canAccessCasePanel",
                            "Register.Menu.Disposal",
                            "Register.Menu.Copying"
                        }
                    }
                }
            },

            { "Accounting", new Dictionary<string, List<string>>
                {
                    { "Menu", new()
                        {
                            "AccountingPanel.Menu.canAccessAccountingPanel",
                            "AccountingPanel.Menu.Billing"
                        }
                    },
                    { "Billing", new()
                        {
                            "AccountingPanel.Billing.Create",
                            "AccountingPanel.Billing.View",
                            "AccountingPanel.Billing.Edit",
                            "AccountingPanel.Billing.Delete"
                        }
                    }
                }
            },

            { "Tools", new Dictionary<string, List<string>>
                {
                    { "Menu", new()
                        {
                            "Menu.Tools.canAccess",
                            "Menu.CourtFeeCalculator.canAccess",
                            "Menu.InterestCalculator.canAccess"
                        }
                    }
                }
            }
        };

        // ✅ Get All Permissions
        public static List<string> GetAllPermissions() =>
            Modules.Values.SelectMany(m => m.Values)
                          .SelectMany(p => p)
                          .Distinct()
                          .ToList();

        // ✅ Get Module Permissions
        public static List<string> GetPermissionsForModule(string module) =>
            Modules.TryGetValue(module, out var perms)
                ? perms.Values.SelectMany(p => p).ToList()
                : new List<string>();

        // =========================
        // ✅ ROLE PERMISSIONS
        // =========================

        public static List<string> LawyerPermissions()
        {
            return GetPermissionsForModule("Case")
                .Union(GetPermissionsForModule("Tools"))
                .Distinct()
                .ToList();
        }

        public static List<string> CorporatePermissions()
        {
            return new List<string>()
                .Union(GetPermissionsForModule("Case"))
                .Where(p => p.Contains(".View"))
                .Distinct()
                .ToList();
        }

        public static List<string> AssociatePermissions()
        {
            return GetPermissionsForModule("Case")
                .Where(p => p.Contains("View") || p.Contains("Edit"))
                .Distinct()
                .ToList();
        }

        public static List<string> ClerkPermissions()
        {
            return GetPermissionsForModule("Case")
                .Union(GetPermissionsForModule("Register"))
                .Where(p => !p.Contains("Delete"))
                .Distinct()
                .ToList();
        }

        public static List<string> ClientPermissions()
        {
            return new List<string>
            {
                "CasePanel.Menu.canAccessCasePanel",
                "CasePanel.Menu.CaseSearch",
                "CasePanel.ManageCase.View"
            };
        }

        // ✅ FINAL MAPPING (Seeder Ready)
        public static Dictionary<RegisterType, List<string>> GetRolePermissions()
        {
            return new Dictionary<RegisterType, List<string>>
            {
                { RegisterType.SuperAdmin, GetAllPermissions() },
                { RegisterType.Lawyer, LawyerPermissions() },
                { RegisterType.Corporate, CorporatePermissions() },
                { RegisterType.Associate, AssociatePermissions() },
                { RegisterType.Clerk, ClerkPermissions() },
                { RegisterType.Client, ClientPermissions() }
            };
        }
    }
}
