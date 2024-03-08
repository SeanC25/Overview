using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Data.Entities
{
    public class Supplier
    {

        public int Id { get; set; }
        public DateTime DateAdded { get; set; }

        [Required()]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string MainContactName { get; set; } // who do we deal with in this company most.

        [Required()]
        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(20)]
        public string PostCode { get; set; }

        [MaxLength(20)]
        public string Website { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        // Relationship 1-N Supplier - N Glass: One Supplier can provide many types of glass
        public IList<Glass> Glasses { get; set; } = new List<Glass>();

    }
}
