using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace InsuranceManagement.API
{
    /// <summary>
    /// districts (daïras) 
    /// </summary>
    public class District
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        public int IdState { get; set; } //Foreign key property

        //Navigation property to represent the relationship
        public ICollection<Municipality> Municipalities { get; set; }

        //Navigation property to represent the relationship
        public State State { get; set; }

    }
}
