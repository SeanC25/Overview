using System.ComponentModel.DataAnnotations;
using APG_CRM.CustomValidations;
using Microsoft.AspNetCore.Mvc.Rendering;
using APG_CRM.Data.Entities;
using System.Collections.Generic; // Ensure this namespace is included for List<T>
using System;
using System.Linq;

namespace APG_CRM.Web.Models;

public class QuotationViewModel
{

    public int Id { get; set; }

    [Display(Name = "Quote Date")]
    public DateTime Date { get; set; } = DateTime.Now.Date;

    public string Title { get; set; }

    [Required(ErrorMessage = "Contact name is required and must be 1st name and last name.")]
    [MultiWordName(ErrorMessage = "Name must consist of multiple words.")]
    public string ContactName { get; set; } // the name of the person asking for the quote.

    [StringLength(14, ErrorMessage = "Phone number cannot be longer than 14 characters.")]  
    [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must be numeric.")]
    public string ContactPhone { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string ContactEmail { get; set; }
    
    [Required(ErrorMessage = "Please provide a description  of the work undertaken for the quotation ")]
    [MaxLength(500)]
    public string Description { get; set; }

    [Display(Name = "Requires Survey")]
    public bool RequiresSurvey { get; set; } = false;

    public string WorkType { get; set; } // what type of work is being carried out
    public bool DeliveryRequired { get; set; }
    public string DeliveryAddress { get; set; }

    [Required(ErrorMessage = "Please provide a price of the work undertaken for the quotation ")]
    [Display(Name = "Quote Price")]
    public double Price { get; set; }


    [Display(Name = "Sent Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? QuoteSentDate { get; set; } = DateTime.Now;

    [Display(Name = "Follow-up Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? QuoteFollowDate { get; set; } = DateTime.Now.AddDays(3);

    public Quotestatus Status { get; set; } = Quotestatus.Pending;

    [Required]
    [StringLength(1000, MinimumLength = 5)]
    public string Response { get; set; }

    public bool? Accepted { get; set; } = null;   //similar to an "active" property.

    [Range(0, 10)]
    public int Urgency { get; set; }

    public List<SelectListItem> UrgencyOptions { get; set; }


    public string UrgencyClassification => Classify();

    public enum Quotestatus { Pending, Approved, Declined, }

    public int? PreviousId { get; set; }   //used in the views to navigate
    public int? NextId { get; set; }


    public List<Quotation> Quotations { get; set; }

    public int SurveyId { get; set; }
    public SelectList Surveys { get; set; } // Add this property

    public CustomerViewModel Customer { get; set; } // Add this

    public SelectList Items { get; set; }
    public SelectList Customers { set; get; }

    [Required(ErrorMessage = "Please select a Customer")]
    [Display(Name = "Select Customer")]
    public int CustomerId { get; set; }

    public class CustomerViewModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Address fields
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }

    }

    private string Classify()
    {
        if (Urgency < 2) return "Standard Enquiry";
        if (Urgency <= 5) return "Strong Prospect";
        if (Urgency < 8) return "Returning Customer";
        return "Emergency Glazing";
    }

    public IEnumerable<Quotation> Survey { get; set; }

}


