using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.Drafting
{
    [Table("cpet_injury", Schema = "drafting")]
    public class ClaimPetitionInjuryEntity : AcciedentPetitionBaseEntity
    {
        /// <summary>
        /// Nature of injury with % disablity.
        /// </summary>
        public int PerDisability { get; set; }

        /// <summary>
        /// Towards medical expenses.
        /// </summary>
        public float MedlExp{ get; set; }
        /// <summary>
        /// Towards loss of partial income due to disablity.
        /// </summary>
        public float LPrtInc{ get; set; }
        /// <summary>
        /// Amount towards general damages.
        /// </summary>
        public float GenDamageAmt{ get; set; }
        /// <summary>
        /// Towards Pain & Suffering
        /// </summary>
        public float AmtPainSuff { get; set; }

        /// <summary>
        /// Towards special diet
        /// </summary>
        public float DietAmt { get; set; }

        /// <summary>
        /// Toward Transportation Amount
        /// </summary>
        public float TransAmt { get; set; }

        /// <summary>
        /// Others amount
        /// </summary>
        public float OtherAmt { get; set; }

        /// <summary>
        /// Is Insurance Company Exponerated?
        /// </summary>
        public bool IsInsurorExp { get; set; }

        /// <summary>
        /// If Yes, mentioned the ground?
        /// </summary>
        public string ExpoGround { get; set; }
    }
}
