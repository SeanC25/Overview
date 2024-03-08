using System.ComponentModel.DataAnnotations;
using APG_CRM.CustomValidations;
using System;
using System.Collections.Generic;


namespace APG_CRM.Data.Entities
{
    public enum Quotestatus { Pending, Approved, Declined, WithCustomer, InProduction, AwaitingDelivery, ReadyForCollection }

    public class Quotation
    {
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

        public bool DeliveryRequired { get; set; } = false;

        public string DeliveryAddress { get; set; }


        [Display(Name = "Quote Price")]
        public double Price { get; set; }

        [Display(Name = "Sent Date")]
        public DateTime? QuoteSentDate { get; set; } = null;

        [Display(Name = "Follow-up Date")]
        public DateTime? QuoteFollowDate { get; set; } = null;

        public Quotestatus Status { get; set; } = Quotestatus.Pending;

        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string Response { get; set; } //any feedback or client responce to the Quote- Will they want to make it an order?
        public bool? Accepted { get; set; } = null;   //similar to an "active" property.

        [Range(0, 10)]
        public int Urgency { get; set; }

        public string UrgencyClassification => Classify();

        //private method to calculate the Quoate urgency classifcation
        private string Classify()
        {
            if (Urgency < 2) return "Standard Enquiry";
            if (Urgency <= 5) return "Strong Prospect";
            if (Urgency < 8) return "Returning Customer";
            return "Emergency Glazing";
        }


        // customer relationship
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    
        // Surveys relationship - One Quotation can have multiple Surveys
        public int? SurveyId { get; set; }   //set to nullable as we can provide quotations which do not need a survy to happen before we quote. 
        public Survey Survey { get; set; }

        // navigation for details view 
        public int? PreviousQuotationId { get; set; }
        public int? NextQuotationId { get; set; }


        //Pagination
        public IList<Quotation> Quotations { get; set; } = new List<Quotation>();

        public IList<Survey> Surveys { get; set; } = new List<Survey>();
        //public ICollection<Survey> Surveys { get; set; } = new List<Survey>()

        // list of quotation items to link to QoutaionItems
        public IList<QuotationItem> Items { get; set; } = new List<QuotationItem>();

        public int? PreviousId { get; set; }   //used in the views to navigate
        public int? NextId { get; set; }


        // notes: should i add? 
        // public string LabourRate { get; set; }
        // public string LabourTime { get; set; }
        // public string LabourCost { get; set; }
    }
}
