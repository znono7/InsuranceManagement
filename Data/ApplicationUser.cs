using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagement.API
{
    /// <summary>
    /// The User data and account for the application
    /// </summary>
    public class ApplicationUser : IdentityUser
    {

        [MaxLength(128)]    
        public string? FirstName { get; set; }
            
        [MaxLength(128)]
        public string? LastName { get; set; }
        
        public DateTime? DateOfBirth { get; set; }

        public int StateOfBirth { get; set; } //Foreign key property for State of birth 

        [MaxLength(16)]
        public string? Phone { get; set; }

        [MaxLength(32)]
        public string? NIdentification { get; set; }

        [MaxLength(32)]
        public string? CCPpayment { get; set; }

        public int IdAgency { get; set; } //Foreign key property for Agency

        public int? IdCommune { get; set; } //Foreign key property for Commune

        //License Information:

        [MaxLength(32)]
        public string? LicenseNumber { get; set; }

        public int LicenseIssuingState { get; set; } //Foreign key property for License Issuing State  "wilaya" 

        public DateTime? LicenseExpiryDate { get; set; }

        //Navigation property to represent the relationship for Agency
        public Agency Agency { get; set; }

        //Navigation property to represent the relationship for State of birth
        public State State { get; set; }

        //Navigation property to represent the relationship for Commune , Geographic area of work
        public Municipality? Municipality { get; set; }
        
        public ICollection<Insurance>? Insurances { get; set; }
    }
}
