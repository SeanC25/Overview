using System.ComponentModel.DataAnnotations;
using APG_CRM.CustomValidations;
using APG_CRM.Data.Entities; // for SurveyStatus enum

namespace APG_CRM.Web.Models
{
    public class SurveyViewModel
    {
        public int CustomerId { get; set; }

        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; } = null!;

        public int Id { get; set; }

        [Required(ErrorMessage = "Request date is required.")]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "Schedule date is required.")]
        [DataType(DataType.Date)]
        public DateTime ScheduleDate { get; set; }

        [Required(ErrorMessage = "Pre-survey notes are required- needs to state if ladders are needed.")]
        [MaxLength(200, ErrorMessage = "Pre-survey notes cannot exceed 200 characters.")]
        public string PreSurveyNotes { get; set; }

        public bool RequiresLadders { get; set; }
        public bool RequiresScissorStairs { get; set; }
        public bool RequiresStreetBullards { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of staff required must be at least 1.")]
        public int NumStaffRequired { get; set; }

        [Required(ErrorMessage = "Street address is required.")]
        [MaxLength(200, ErrorMessage = "Street address cannot exceed 200 characters.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "County is required.")]
        [MaxLength(100, ErrorMessage = "County cannot exceed 100 characters.")]
        public string County { get; set; }

        [Required(ErrorMessage = "Postcode is required.")]
        [MaxLength(20, ErrorMessage = "Postcode cannot exceed 20 characters.")]
        public string PostCode { get; set; }

        [StringLength(14, ErrorMessage = "Survey phone number cannot exceed 14 characters.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must be numeric.")]
        public string SurveyPhone { get; set; }


        public SurveyStatus Status { get; set; }
        public bool? Reviewed { get; set; }
        [Required(ErrorMessage = "The survey needs state who carried out the survey.")]
        public string CompletedByWho { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Required(ErrorMessage = "The survey needs a full description of all required details.")]
        public string Description { get; set; }

        [MaxLength(100, ErrorMessage = "Risk assessment cannot exceed 100 characters.")]
        [Required(ErrorMessage = "Risk Assessment details are  required.")]
        public string RiskAssessment { get; set; }

        public int? PreviousSurveyId { get; set; }
        public int? NextSurveyId { get; set; }
    }

    public enum SurveyStatus
    {
        Pending, Completed, Canceled
    }
}
