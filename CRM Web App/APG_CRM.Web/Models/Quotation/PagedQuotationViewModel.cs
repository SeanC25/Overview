using APG_CRM.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using APG_CRM.CustomValidations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace APG_CRM.Web.Models
{
    public class PagedQuotationViewModel

    {
        //Notes:- not sure if i need to add the relationships to the view model...

        // list of quotation items
        public SelectList Items { get; set; }

        public SelectList Customers { set; get; }

        // customer relationship
        //public int CustomerId { get; set; }
        //public Customer Customer { get; set; }

        //=================================Properties============

        [Required(ErrorMessage = "Please select a Customer")]
        [Display(Name = "Select Customer")]
        public int CustomerId { get; set; }

        // Survey relationship
        public int SurveyId { get; set; }
        //public Survey Survey { get; set; }

        public int Id { get; set; }

        [Display(Name = "Quote Date")]
        public DateTime Date { get; set; } = DateTime.Now.Date;

        public string Title { get; set; }

        [MultiWordName(ErrorMessage = "Name must consist of multiple words.")]
        public string ContactName { get; set; } // the name of the person asking for the quote. 
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Requires Survey")]
        public bool RequiresSurvey { get; set; } = false;

        //Quote pricing structure
        public string WorkType { get; set; } // what type of work is being carried out

        public bool DeliveryRequired { get; set; }

        public string DeliveryAddress { get; set; }

        [Display(Name = "Quote Price")]
        public double Price { get; set; }
        public double QuotationTotal { get; set; }

        [Display(Name = "Sent Date")]
        public DateTime? QuoteSentDate { get; set; } = null;

        [Display(Name = "Follow-up Date")]
        public DateTime? QuoteFollowDate { get; set; } = null;

        public Quotestatus Status { get; set; } = Quotestatus.Pending;

        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string Response { get; set; }

        public bool? Accepted { get; set; } = null;   //similar to an "active" property.

        [Range(0, 10)]
        public int Urgency { get; set; }
        public string UrgencyClassification => Classify();

        public enum Quotestatus { Pending, Approved, Declined, WithCustomer, InProduction, AwaitingDelivery, ReadyForCollection }

        //private method to calculate the Quoate urgency classifcation
        public string Classify()
        {
            if (Urgency < 2) return "Standard Enquiry";
            if (Urgency <= 5) return "Strong Prospect";
            if (Urgency < 8) return "Returning Customer";
            return "Emergency Glazing";
        }

        public List<Quotation> Quotations { get; set; }
        ///Pagination 

        // Inherits properties from QuotationViewModel to be a paginated list of QuotationViewModel from Paged<T> in the QuotationController

        public string SearchTerm { get; set; }
        public QuotationScanViewModel QuotationScanViewModel { get; set; }

        public int TotalRows { get; set; } // Ensure this is only declared once
        public int CurrentPage { get; set; }
        public int PageSize { get; set; } // Ensure this is only declared once
        public string OrderBy { get; set; }
        public string Direction { get; set; }

        // Calculate TotalPages based on TotalRows and PageSize
        public int TotalPages => (int)Math.Ceiling((double)TotalRows / PageSize);

        public SelectList SurveySelectList { get; set; } // used to help the partial view show the survey for quotations

        public List<QuotationViewModel> Data { get; set; } // Collection of individual quotations

    }
}
