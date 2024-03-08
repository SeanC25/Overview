using APG_CRM.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Data.Entities;

public class Invoice
{
    public int Id { get; set; }

    public string Notes { get; set; } = "";

    public decimal TotalValue { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public DateTime PaymentDate { get; set; } 
    public DateTime ChaseDate { get; set; }

    [Required(ErrorMessage = "How is this contact paying for their service?")]
    [MaxLength(20, ErrorMessage = "PaymentMethod cannot exceed 20 characters.")]
    [AllowedPaymentMethods(ErrorMessage = "Invalid payment method. Allowed methods: Cash, Card, On Account, Business Terms.")]
    public string PaymentMethod { get; set; }
        
  
    // one-to-one relationship, where an invoice is associated with an order
    //public int OrderId { get; set; } // Required foreign key property
    //public Order Order { get; set; } = null!; // Required reference navigation to principal

}
