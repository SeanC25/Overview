using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace APG_CRM.Data.Entities
{
    public class GlassImage   ///setting up for image carousel
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int GlassId { get; set; } // Foreign key
    public Glass Glass { get; set; } // Navigation property
}

}

