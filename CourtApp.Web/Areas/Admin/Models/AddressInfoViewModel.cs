namespace CourtApp.Web.Areas.Admin.Models
{
    public class AddressInfoViewModel
    {
        public int StateId { get; set; }
        public string State { get; set; }
        public int DistrictId { get; set; }
        public string District { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
    }
}
