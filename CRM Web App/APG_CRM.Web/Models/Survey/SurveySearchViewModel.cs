using APG_CRM.Data.Entities;

public class SurveySearchViewModel
{
    public string SearchTerm { get; set; }
    public List<string> AvailableStatuses { get; set; } = new List<string>(); // Populate from your DB or enum
    public string SelectedStatus { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public SurveyStatus Status { get; set; } = SurveyStatus.Pending;
}