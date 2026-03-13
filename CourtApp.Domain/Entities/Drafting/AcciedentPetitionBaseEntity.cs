using AuditTrail.Abstrations;
using System;
namespace CourtApp.Domain.Entities.Drafting
{
    public abstract class AcciedentPetitionBaseEntity: AuditableEntity    
    {
        /// <summary>
        /// This is associated case Id
        /// </summary>
        public Guid CaseId { get; set; }
        /// <summary>
        /// Gender of the injured or death 
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// Injured or death person name
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// Age of Person
        /// </summary>
        public int  PersonAge { get; set; }

        /// <summary>
        /// Age of person as per MLR or PMR
        /// </summary>
        public int AgeAsPer { get; set; }

        /// <summary>
        /// Nature of work or Occupation of person
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// Income of Person
        /// </summary>
        public float InCome { get; set; }
        /// <summary>
        /// Date of Acciedent
        /// </summary>
        public DateTime AcciedentDate { get; set; }

        /// <summary>
        /// Place of acciedent
        /// </summary>
        public string PlaceOfAcciedent { get; set; }

        /// <summary>
        /// Acciendent vehical type
        /// </summary>
        public string VehicalType{ get; set; }

        /// <summary>
        /// Involved vehical number
        /// </summary>
        public string VehicalNumber{ get; set; }

        /// <summary>
        /// Name of the insurance company
        /// </summary>
        public string InsuranceCompany{ get; set; }

        /// <summary>
        /// Total amount claimed by the person family
        /// </summary>
        public float  AmountClaim{ get; set; }

        /// <summary>
        /// Fir number
        /// </summary>
        public string FirNumber { get; set; }

        /// <summary>
        /// Fir Date
        /// </summary>
        public DateTime FirDate { get; set; }
    }
}
