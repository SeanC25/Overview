using System;
using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Data.Entities
{
    public class Survey
    {
        public int Id { get; set; }

        // General survey details needed to request a survey.
        [Required]
        public DateTime RequestDate { get; set; } = DateTime.Now.Date;  //pre-set as default for when new survey is made.

        [Required]
        public DateTime ScheduleDate { get; set; } 
        
        [Required]
        [MaxLength(800)]
        public string PreSurveyNotes { get; set; }

        //Prerequisites items//
        public bool RequiresLadders { get; set; }
        public bool RequiresScissorStairs { get; set; }
        public bool RequiresStreetBullards { get; set; }
        public int NumStaffRequired { get; set; }

        // Address components-  Survet location can be different from the Customer address. - will have these grouped togeather in the view
        [Required]
        [MaxLength(200)]
        [Display(Name = "Address")]
        public string Street { get; set; }

        [Required()]
        [MaxLength(100)]
        public string City { get; set; }

        [Required()]
        [MaxLength(100)]
        public string County { get; set; }

        [Required()]
        [MaxLength(20)]
        public string PostCode { get; set; }

        [StringLength(50)]
        public string SurveyPhone { get; set; } = string.Empty; // somtimes survey will have a different telephone number for the person who gives access to the work.

        // Survey completion details- following survery attendance- the staff will edit the survey to add these details. so all information is in one place.

        public SurveyStatus Status { get; set; } = SurveyStatus.Pending;

        public bool? Reviewed { get; set; }

        public String CompletedByWho { get; set; } = string.Empty;

        // Survey descriptions and assessments
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;


        [MaxLength(1000)]
        public string RiskAssessment { get; set; } = string.Empty;

        public int? PreviousSurveyId { get; set; }   //used in the views to navigate
        public int? NextSurveyId { get; set; }

        // Relationship Navigation properties    
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? QuotationId { get; set; }
        public Quotation Quotation { get; set; } //= null!;

    }

    public enum SurveyStatus
    {
        Pending, Completed, Canceled
    }
}
