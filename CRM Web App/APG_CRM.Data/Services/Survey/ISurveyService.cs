using APG_CRM.Data.Entities;


namespace APG_CRM.Data.Services;

// This interface describes the functions that a ContactService class will implement
public interface ISurveyService
{
    void Initialise();

    // ==================== Survey Entity Services Management- CRUD (Create, Read, Update, Delete) operations =========================
    Survey CreateSurvey(Survey sur); // Create method
    List<Survey> GetAllSurveys();  // retrieve list of surveys / read method
    Survey GetSurveyById(int id);  // retrieve  survey by its id / read method
    Survey UpdateSurvey(Survey update); //Update method
    bool DeleteSurvey(int id); // Delete  method

    IQueryable<Survey> Surveys { get; } //used to make the inex paged. 

    // ==================== Survey Entity Services Search Functions=========================

    IList<Survey> SearchSurveys(string scan);
    Survey GetSurveyByCustomerName(string CustomerName);
    Survey GetSurveyByPostCode(string PostCode);
    IList<Survey> GetPendingSurveys();
    IList<Survey> GetCompletedSurveysNotReviewed();

    // retrieve paged list of Surveys- Pagination 
    Paged<Survey> GetSurveys(int page = 1, int size = 20, string orderBy = "id", string direction = "asc");

    IList<Survey> GetUsersQuery(Func<Survey, bool> q);

}
