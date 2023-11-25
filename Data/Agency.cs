using System.ComponentModel.DataAnnotations;

namespace InsuranceManagement.API
{
    /// <summary>
    /// Represents information about insurance agency branches
    /// </summary>
    public class Agency
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string? Address { get; set; }

        public int IdState { get; set; } //Foreign key property

        //Navigation property to represent the relationship
        public State State { get; set; }

        //Navigation property to represent the relationship
        public ICollection<ApplicationUser> Users { get; set; }

    }
}
