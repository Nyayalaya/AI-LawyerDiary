namespace CourtApp.Web.Areas.Drafting.Models
{
    public class DeathViewModel: AccBaseViewModel
    {
        /// <summary>
        /// Amount awarded by MACT
        /// </summary>
        public float MACTAmt { get; set; }
        /// <summary>
        /// Relation of claimant with decesed
        /// </summary>
        public string ClaimantRelation { get; set; }
        /// <summary>
        /// Multiplier
        /// </summary>
        public float Multiplier { get; set; }
        /// <summary>
        /// Dependency Determined by MACT
        /// </summary>
        public string Dependency { get; set; }

        /// <summary>
        /// Amount of Consortium awarded by MACT
        /// </summary>
        public float ConsAmt { get; set; }
        /// <summary>
        /// Amount towards love and affection
        /// </summary>
        public float AmtAffection { get; set; }

        /// <summary>
        /// Future prospects
        /// </summary>
        public string FProspects { get; set; }

        /// <summary>
        /// Is Insurance Company Exponerated?
        /// </summary>
        public bool IsInsurorExp { get; set; }

        /// <summary>
        /// If Yes, mentioned the ground?
        /// </summary>
        public string ExpoGround { get; set; }

        /// <summary>
        /// Name of doctor who was looks the deases.
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// Towards medical expenses.
        /// </summary>
        public float MedlExp { get; set; }

        /// <summary>
        /// Toward Transportation Amount
        /// </summary>
        public float TransAmt { get; set; }

        /// <summary>
        /// Funeral Amount
        /// </summary>
        public float FuneralAmt { get; set; }
    }
}
