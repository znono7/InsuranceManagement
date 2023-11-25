using System.ComponentModel.DataAnnotations;

namespace InsuranceManagement.API
{
    /// <summary>
    /// State (Wilaya)
    /// </summary>
    public class State
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(16)]
        public string Code { get; set; }

        //Navigation property to represent the relationship
        public ICollection<District> Districts { get; set; }
        public ICollection<Agency> Agencies  { get; set; }
        public ICollection<ApplicationUser> Users { get; set; } 

    }
}
