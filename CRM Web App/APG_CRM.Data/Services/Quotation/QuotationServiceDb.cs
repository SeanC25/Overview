using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;


namespace APG_CRM.Data.Services;
public class QuotationServiceDb : IQuotationService
{

    private readonly DatabaseContext db;

    public QuotationServiceDb(DatabaseContext context)
    {
        db = context;
    }

    public void Initialise()
    {
        db.Initialise(); // recreate database
    }

    public Quotation AddQuotation(Quotation q) //Create method
    {
        if (q == null)
        {
            throw new ArgumentNullException(nameof(q), "Quotation cannot be null");
        }

        // Validate the Quotation entity
        var validationContext = new ValidationContext(q);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(q, validationContext, validationResults, true))
        {
            // Join the validation error messages and throw an exception with the detailed messages.
            var errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
            throw new InvalidOperationException($"The Quotation entity is not valid. Errors: {errorMessages}");
        }

        // Check if a Quotation with the same Title already exists
        if (GetQuotationByTitle(q.Title) != null)
        {
            throw new InvalidOperationException("A Quotation with the same name already exists.");
        }

        try
        {
            // Directly add Quotation 'q' object to the database- instead of calling properities invividually.
            db.Quotations.Add(q);
            db.SaveChanges();

            // Return the added entity with its Id. 
            // The entity 'q' should have its Id populated after the save.
            return q;
        }

        catch (Exception ex)
        {
            // Log the exception or handle it appropriately.
            throw new InvalidOperationException("An error occurred while adding this Quotation.", ex);
        }
    } //create method


    public Quotation GetQuotationById(int id)  //read method
    {
        return db.Quotations.FirstOrDefault(q => q.Id == id);
    }//read method

    public Quotation UpdateQuotation(Quotation update)
    {
        var quotation = GetQuotationById(update.Id);   // Check if the quotation exists
        if (quotation == null)
        {
            return null;  // Return null if not present.
        }
        quotation.Date = update.Date;
        quotation.Title = update.Title;
        quotation.ContactName = update.ContactName;
        quotation.ContactPhone = update.ContactPhone;
        quotation.ContactEmail = update.ContactEmail;
        quotation.Description = update.Description;
        quotation.RequiresSurvey = update.RequiresSurvey;
        quotation.WorkType = update.WorkType;
        quotation.DeliveryRequired = update.DeliveryRequired;
        quotation.DeliveryAddress = update.DeliveryAddress;
        quotation.Price = update.Price;
        quotation.QuoteSentDate = update.QuoteSentDate;
        quotation.QuoteFollowDate = update.QuoteFollowDate;
        quotation.Status = update.Status;
        quotation.Response = update.Response;
        quotation.Accepted = update.Accepted;
        quotation.Urgency = update.Urgency;
        db.SaveChanges();

        return quotation;
    }//UpdateQuotation

    public bool DeleteQuotation(int id) //Delete method
    {
        var quotation = db.Quotations.FirstOrDefault(q => q.Id == id);  // find quotation
        if (quotation == null) return false; // if none exist/ not found turn false

        db.Quotations.Remove(quotation); // remove quotation
        db.SaveChanges();// save changes 
        return true; // return true to indicate a successful method
    } //Delete method


    public Quotation CreateQuotation(int customerId, DateTime date, string title, string contactName, string contactPhone, string contactEmail, string description, bool requiresSurvey, string workType, bool deliveryRequired, string deliveryAddress, double price, DateTime? quoteSentDate, DateTime? quoteFollowDate, Quotestatus status, string response, bool? accepted, int urgency)
    {
        var customer = db.Customers.Find(customerId);

        if (customer == null)
        {
            throw new ArgumentException($"Customer with ID {customerId} does not exist.");
        }

        var quotation = new Quotation
        {
            CustomerId = customerId,
            Date = date,
            Title = title,
            ContactName = contactName,
            ContactPhone = contactPhone,
            ContactEmail = contactEmail,
            Description = description,
            RequiresSurvey = requiresSurvey,
            WorkType = workType,
            DeliveryRequired = deliveryRequired,
            DeliveryAddress = deliveryAddress,
            Price = price,
            QuoteSentDate = quoteSentDate,
            QuoteFollowDate = quoteFollowDate,
            Status = status,
            Response = response,
            Accepted = accepted,
            Urgency = urgency,

        };

        db.Quotations.Add(quotation);// Persents the newQuotation to the database
        db.SaveChanges();// saves
        return quotation; // Return new quotation
    }


    // ***====================Quotation Entity Services  helpter methods/Search Functions =========================

    public List<Quotation> GetAllQuotations() => db.Quotations.ToList() ?? new List<Quotation>();

    public Quotation GetQuotationByTitle(string Title)
    {
        return db.Quotations
            .Where(q => q.Title == Title)
            .FirstOrDefault();
    }

    public Quotation GetQuotationByNameAndDate(string contactName, DateTime quoteDate)
    {
        return db.Quotations
            .Where(q => q.ContactName == contactName && q.Date == quoteDate)
            .FirstOrDefault();
    }

    public Quotation GetQuotationBySentDateAndUpdateFollowUp(DateTime quoteSentDate)
    {
        var quotation = db.Quotations
                .FirstOrDefault(q => q.QuoteSentDate == quoteSentDate);

        if (quotation != null)
        {
            quotation.QuoteFollowDate = quoteSentDate.AddDays(10);
            db.SaveChanges();
        }
        return quotation;
    }
    public Quotation GetQuotationByStatusPastSentDate(Quotestatus status, DateTime quoteSentDate)
    {
        // If you want to filter by both status and sent date:
        var quotation = db.Quotations
                .FirstOrDefault(q => q.Status == status && q.QuoteSentDate <= quoteSentDate);
        return quotation;
    }

    //Search function -uses partial matching. 
    public IList<Quotation> SearchQuotations(string searchTerm)
    {
        searchTerm = searchTerm.ToLower(); // Convert search term to lowercase for case-insensitive comparison

        var query = db.Quotations
            .Where(q =>
                q.Title.ToLower().Contains(searchTerm) ||
                q.ContactName.ToLower().Contains(searchTerm) ||
                q.ContactPhone.ToLower().Contains(searchTerm) ||
                q.ContactEmail.ToLower().Contains(searchTerm) ||
                q.Description.ToLower().Contains(searchTerm) ||
                q.WorkType.ToLower().Contains(searchTerm) ||
                q.DeliveryAddress.ToLower().Contains(searchTerm) ||
                q.Response.ToLower().Contains(searchTerm)
            );

        return query.ToList();
    }

    //Pagination
    public Paged<Quotation> GetQuotationsPaged(string searchTerm = "", string sortBy = "Id", string direction = "asc", int page = 1, int size = 20)
    {
        var query = db.Quotations.AsQueryable();

        // Filtering
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(q => q.Title.Contains(searchTerm) || q.ContactName.Contains(searchTerm));
            // You can add more fields to filter by in the above condition if needed.
        }

        // Sorting
        query = (sortBy.ToLower(), direction.ToLower()) switch
        {
            ("id", "asc") => query.OrderBy(q => q.Id),
            ("id", "desc") => query.OrderByDescending(q => q.Id),
            ("date", "asc") => query.OrderBy(q => q.Date),
            ("date", "desc") => query.OrderByDescending(q => q.Date),
            ("title", "asc") => query.OrderBy(q => q.Title),
            ("title", "desc") => query.OrderByDescending(q => q.Title),
            ("contactname", "asc") => query.OrderBy(q => q.ContactName),
            ("contactname", "desc") => query.OrderByDescending(q => q.ContactName),
            ("contactphone", "asc") => query.OrderBy(q => q.ContactPhone),
            ("contactphone", "desc") => query.OrderByDescending(q => q.ContactPhone),
            ("contactemail", "asc") => query.OrderBy(q => q.ContactEmail),
            ("contactemail", "desc") => query.OrderByDescending(q => q.ContactEmail),
            ("description", "asc") => query.OrderBy(q => q.Description),
            ("description", "desc") => query.OrderByDescending(q => q.Description),
            ("worktype", "asc") => query.OrderBy(q => q.WorkType),
            ("worktype", "desc") => query.OrderByDescending(q => q.WorkType),
            ("price", "asc") => query.OrderBy(q => q.Price),
            ("price", "desc") => query.OrderByDescending(q => q.Price),
            ("quotesentdate", "asc") => query.OrderBy(q => q.QuoteSentDate),
            ("quotesentdate", "desc") => query.OrderByDescending(q => q.QuoteSentDate),
            ("quotefollowdate", "asc") => query.OrderBy(q => q.QuoteFollowDate),
            ("quotefollowdate", "desc") => query.OrderByDescending(q => q.QuoteFollowDate),
            ("status", "asc") => query.OrderBy(q => q.Status),
            ("status", "desc") => query.OrderByDescending(q => q.Status),
            ("urgency", "asc") => query.OrderBy(q => q.Urgency),
            ("urgency", "desc") => query.OrderByDescending(q => q.Urgency),
            _ => query.OrderBy(q => q.Id) // Default sorting
        };

        return query.ToPaged(page, size, sortBy, direction);
    }

    //checks what quotations are connected to which customer. 
    public IQueryable<Quotation> GetQuotations()
    {
        return db.Quotations.Include(cu => cu.Customer);

    }


    public Paged<Survey> GetSurveysForQuotation(int quotationId)
    {
        // Replace this with your actual logic to get surveys for a quotation
        var surveys = db.Surveys
            .Where(s => s.QuotationId == quotationId)
            .OrderBy(s => s.RequestDate)
            .ToPaged(); // Assuming you have a ToPaged() extension method

        return surveys;
    }

}