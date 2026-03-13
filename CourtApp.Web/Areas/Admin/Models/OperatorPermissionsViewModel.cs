using System.Collections.Generic;

namespace CourtApp.Web.Areas.Admin.Models
{
    public class OperatorPermissionsViewModel
    {
        public string OperatorId { get; set; }
        public List<string> AvailablePermissions { get; set; } = new List<string>();
        public List<string> AssignedPermissions { get; set; } = new List<string>();
    }
}
