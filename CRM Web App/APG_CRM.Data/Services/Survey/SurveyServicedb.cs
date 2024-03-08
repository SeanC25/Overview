using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace APG_CRM.Data.Services
{
    public class SurveyServiceDb : ISurveyService
    {
        private readonly DatabaseContext db;    //the service is public but the access to this db is private readonly within this service.  //Dependency Injection: injecting it through the constructor. This will make unit testing easier and is a better practice for maintainability and scalability.

        public SurveyServiceDb(DatabaseContext context)
        {
            this.db = context;
        }

        public void Initialise()
        {
            db.Initialise(); // recreate database
        }

        //*** =================== Survey Entity Services Management CRUD operations (Create, Read, Update, Delete),=========================
        public Survey CreateSurvey(Survey sur)
        {
            // Check if a survey with the same street address and ScheduleDate already exists
            if (GetSurveyByStreetAndDate(sur.Street, sur.ScheduleDate) != null)
            {
                return null; // stops duplication- only one survey can be allocated so hopfully stops duplicate survey bookings.
            }

            // Create a new Surveys object and initialize it with the passed Survey properties.
            var survey = new Survey
            {
                RequestDate = sur.RequestDate,
                ScheduleDate = sur.ScheduleDate,
                PreSurveyNotes = sur.PreSurveyNotes,
                RequiresLadders = sur.RequiresLadders,
                RequiresScissorStairs = sur.RequiresScissorStairs,
                RequiresStreetBullards = sur.RequiresStreetBullards,
                NumStaffRequired = sur.NumStaffRequired,
                Street = sur.Street,
                City = sur.City,
                County = sur.County,
                PostCode = sur.PostCode,
                SurveyPhone = sur.SurveyPhone,
                Status = sur.Status,
                Reviewed = sur.Reviewed,
                CompletedByWho = sur.CompletedByWho,
                Description = sur.Description,
                RiskAssessment = sur.RiskAssessment,
                CustomerId = sur.CustomerId,
            };

            db.Surveys.Add(sur); // Persists the new Survey to the database.
            db.SaveChanges(); // Saves changes
            return sur; // Return the persisted Survey with its generated ID.
        }

        public List<Survey> GetAllSurveys()
        {
            return db.Surveys.ToList();
        }

        public Survey GetSurveyById(int id)
        {
            return db.Surveys.FirstOrDefault(sur => sur.Id == id);
        }

        public Survey UpdateSurvey(Survey update)
        {
            var survey = GetSurveyById(update.Id);
            if (survey == null) return null; // Checks that the survey exists

            // Check if a survey with the same street address and ScheduleDate already exists
            // wont work because this method does not check if the duplicate has same id as survey being updated
            if (GetSurveyByStreetAndDate(update.Street, update.ScheduleDate, update.Id) != null)
            {
                return null; // Stops duplication - only one survey can be allocated to avoid duplicate survey bookings.
            }

            // Update 
            survey.RequestDate = update.RequestDate;
            survey.ScheduleDate = update.ScheduleDate;
            survey.PreSurveyNotes = update.PreSurveyNotes;
            survey.RequiresLadders = update.RequiresLadders;
            survey.RequiresScissorStairs = update.RequiresScissorStairs;
            survey.RequiresStreetBullards = update.RequiresStreetBullards;
            survey.NumStaffRequired = update.NumStaffRequired;
            survey.Street = update.Street;
            survey.City = update.City;
            survey.County = update.County;
            survey.PostCode = update.PostCode;
            survey.SurveyPhone = update.SurveyPhone;
            survey.Status = update.Status;
            survey.Reviewed = update.Reviewed;
            survey.CompletedByWho = update.CompletedByWho;
            survey.Description = update.Description;
            survey.RiskAssessment = update.RiskAssessment;

            db.SaveChanges(); // Save update
            return survey; // Return updated survey
        }

        public bool DeleteSurvey(int id)
        {
            var survey = GetSurveyById(id);// finds the survey with this Id
            if (survey == null) return false;// checks if none fund returns null 

            db.Surveys.Remove(survey);// if there is one found of this id, it will delete it.
            db.SaveChanges(); // saves changes made to Db.
            return true;// returns success bool. 
        }//Deletesurvey

        public IQueryable<Survey> Surveys => db.Surveys;

        //returns the Surveys DbSet from your DatabaseContext as an IQueryable<Survey>, allowing you to perform further queries on it as needed.
        //used in pageing the survey data. 


        // ==================== survey Entity Services Search Functions=========================

        // helper method 


        // helper method 

        ///method will work with both CreateSurvey and UpdateSurvey methods. It will prevent duplication in the creation process and allow updates to the same survey without incorrectly flagging it as a duplicate.
        public Survey GetSurveyByStreetAndDate(string street, DateTime scheduleDate, int id = 0)
        {
            var survey = db.Surveys.FirstOrDefault(s => s.Street == street && s.ScheduleDate.Date == scheduleDate.Date);

            // Return null if another survey with the same street and date is found, and it's not the survey being updated
            if (survey != null && survey.Id != id)
            {
                return null;
            }

            // Return the found survey (could be the same survey being updated or null if no conflicting survey is found)
            return survey;
        }

        

        public IList<Survey> SearchSurveys(string scan)
        {
            scan = (scan ?? "").ToLower();  //if the scanner bar is left empty it shows all Survey

            // otherwise this is where the search takes place

            return db.Surveys
                     .Where(sur => sur.PostCode.ToLower().Contains(scan) || sur.County.ToLower().Contains(scan))
                     .ToList();
            // this scans the database for Survey, post code and city, so that managment could check if there are any surveys in the same area- to help manage good scheduling.  
        }

        public Survey GetSurveyByCustomerName(string name)
        {
            return db.Surveys.Include(sur => sur.Customer) // Including related Customer entity to get CustomerName
                            .FirstOrDefault(sur => sur.Customer.Name == name);
        }

        public Survey GetSurveyByPostCode(string postCode)
        {
            return db.Surveys.FirstOrDefault(sur => sur.PostCode == postCode);
        }

        public IList<Survey> GetPendingSurveys()
        {
            return db.Surveys.Where(sur => sur.Status == SurveyStatus.Pending).ToList();
        }

        public IList<Survey> GetCompletedSurveysNotReviewed()
        {
            return db.Surveys.Where(sur => sur.Status == SurveyStatus.Completed && sur.Reviewed == false).ToList();
        }


        public Paged<Survey> GetSurveys(int page = 1, int size = 10, string orderBy = "id", string direction = "asc")
        {
            var query = (orderBy.ToLower(), direction.ToLower()) switch
            {
                ("id", "asc") => db.Surveys.OrderBy(s => s.Id),
                ("id", "desc") => db.Surveys.OrderByDescending(s => s.Id),

                ("requestdate", "asc") => db.Surveys.OrderBy(s => s.RequestDate),
                ("requestdate", "desc") => db.Surveys.OrderByDescending(s => s.RequestDate),

                ("scheduledate", "asc") => db.Surveys.OrderBy(s => s.ScheduleDate),
                ("scheduledate", "desc") => db.Surveys.OrderByDescending(s => s.ScheduleDate),

                ("preSurveyNotes", "asc") => db.Surveys.OrderBy(s => s.PreSurveyNotes),
                ("preSurveyNotes", "desc") => db.Surveys.OrderByDescending(s => s.PreSurveyNotes),

                ("requiresladders", "asc") => db.Surveys.OrderBy(s => s.RequiresLadders),
                ("requiresladders", "desc") => db.Surveys.OrderByDescending(s => s.RequiresLadders),

                ("requiresscissorstairs", "asc") => db.Surveys.OrderBy(s => s.RequiresScissorStairs),
                ("requiresscissorstairs", "desc") => db.Surveys.OrderByDescending(s => s.RequiresScissorStairs),

                ("requiresstreetbullards", "asc") => db.Surveys.OrderBy(s => s.RequiresStreetBullards),
                ("requiresstreetbullards", "desc") => db.Surveys.OrderByDescending(s => s.RequiresStreetBullards),

                ("NumStaffRequired", "asc") => db.Surveys.OrderBy(s => s.NumStaffRequired),
                ("NumStaffRequired", "desc") => db.Surveys.OrderByDescending(s => s.NumStaffRequired),

                ("street", "asc") => db.Surveys.OrderBy(s => s.Street),
                ("street", "desc") => db.Surveys.OrderByDescending(s => s.Street),

                ("city", "asc") => db.Surveys.OrderBy(s => s.City),
                ("city", "desc") => db.Surveys.OrderByDescending(s => s.City),

                ("county", "asc") => db.Surveys.OrderBy(s => s.County),
                ("county", "desc") => db.Surveys.OrderByDescending(s => s.County),

                ("postcode", "asc") => db.Surveys.OrderBy(s => s.PostCode),
                ("postcode", "desc") => db.Surveys.OrderByDescending(s => s.PostCode),

                ("surveyphone", "asc") => db.Surveys.OrderBy(s => s.SurveyPhone),
                ("surveyphone", "desc") => db.Surveys.OrderByDescending(s => s.SurveyPhone),

                ("Status", "asc") => db.Surveys.OrderBy(s => s.Status),
                ("Status", "desc") => db.Surveys.OrderByDescending(s => s.Status),

                ("Reviewed", "asc") => db.Surveys.OrderBy(s => s.Reviewed),
                ("Reviewed", "desc") => db.Surveys.OrderByDescending(s => s.Reviewed),

                ("completedbywho", "asc") => db.Surveys.OrderBy(s => s.CompletedByWho),
                ("completedbywho", "desc") => db.Surveys.OrderByDescending(s => s.CompletedByWho),

                ("description", "asc") => db.Surveys.OrderBy(s => s.Description),
                ("description", "desc") => db.Surveys.OrderByDescending(s => s.Description),

                ("riskassessment", "asc") => db.Surveys.OrderBy(s => s.RiskAssessment),
                ("riskassessment", "desc") => db.Surveys.OrderByDescending(s => s.RiskAssessment),

                // Add more sorting criteria if needed
                _ => db.Surveys.OrderBy(s => s.Id)
            };

            return query.ToPaged(page, size, orderBy, direction);
        }

        public IList<Survey> GetUsersQuery(Func<Survey, bool> q)
        {
            return db.Surveys.Where(q).ToList();
        }

        // ==================== Survey Entity Services Search Functions=========================
    }
}
