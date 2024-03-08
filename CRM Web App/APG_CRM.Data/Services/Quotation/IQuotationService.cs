using APG_CRM.Data.Entities;

namespace APG_CRM.Data.Services;

// This interface describes the functions that a ContactService class will implement
public interface IQuotationService
{
    void Initialise();

    // ***==================== Quotation Entity Services Management CRUD operations (Create, Read, Update, Delete), =========================

    Quotation AddQuotation(Quotation q); //Create method- used frot he seeder
    Quotation GetQuotationById(int id); //read method
    Quotation UpdateQuotation(Quotation update); //update method
    bool DeleteQuotation(int id); //Delete method

    // Quotation CreateQuotation(int customerId, DateTime quoteDate, string quoteDescription, bool requiresSurvey, string worktype, decimal quotePrice, DateTime? quoteSentDate, DateTime? quoteFollowDate, Quotestatus status, string quoteResponse, bool? quoteAccepted, int urgency, bool deliveryRequired, string deliveryAddress);


    // ***====================Quotation Entity Services  helpter methods/Search Functions =========================

    List<Quotation> GetAllQuotations();

    Quotation GetQuotationByTitle(string Title);

    Quotation GetQuotationByNameAndDate(string contactName, DateTime quoteDate);
    Quotation GetQuotationBySentDateAndUpdateFollowUp(DateTime quoteSentDate);

    Quotation GetQuotationByStatusPastSentDate(Quotestatus Status, DateTime quoteSentDate);

    IList<Quotation> SearchQuotations(string searchTerm);


    //Pagination
    Paged<Quotation> GetQuotationsPaged(string searchTerm = "", string sortBy = "Id", string direction = "asc", int page = 1, int size = 20);

    IQueryable<Quotation> GetQuotations();

    ///Creating a Quotation connected to a customer
    Quotation CreateQuotation(int customerId, DateTime date, string title, string contactName, string contactPhone, string contactEmail, string description, bool requiresSurvey, string workType, bool deliveryRequired, string deliveryAddress, double price, DateTime? quoteSentDate, DateTime? quoteFollowDate, Quotestatus status, string response, bool? accepted, int urgency);

     Paged<Survey> GetSurveysForQuotation(int quotationId);

}
