using System.ComponentModel.DataAnnotations;

namespace InsuranceManagement.API
{
    /// <summary>
    /// municipality , Commune (baladiyahs)
    /// </summary>
    public class Municipality
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        public int? IdDistrict { get; set; } //Foreign key property

        //Navigation property to represent the relationship
        public District District { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

    }
}
