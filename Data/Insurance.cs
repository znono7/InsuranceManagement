using System.ComponentModel.DataAnnotations;

namespace InsuranceManagement.API
{
    /// <summary>
    /// Represents insurance information
    /// </summary>
    public class Insurance
    {
        [Key]
        public int Id { get; set; }

        public string IdCustomer { get; set; }//Foreign key property

        public Customer Customer { get; set; }//Navigation property to represent the relationship
     
        [MaxLength(128)]
        public string VehicleModel { get; set; }

        public DateTime YearVehicleManufacture {  get; set; }

        [MaxLength(64)]
        public string VehicleIdentificationNumber { get; set; }

        [MaxLength(64)]
        public string VehicleRegistrationNumber { get; set;}

        [MaxLength(64)]
        public string VehicleType { get;set; }

        [MaxLength(64)]
        public string GrayCardNumber { get; set; }

        public DateTime GrayCardIssuanceDate { get; set; }

        public DateTime GrayCardExpirationDate { get; set; }

        [MaxLength(64)]
        public string TypeInsuranceCoverage {  get; set; }

        public DateTime DocumentStartDate { get; set; }

        public DateTime DocumentEndDate { get; set; }


    }
}
