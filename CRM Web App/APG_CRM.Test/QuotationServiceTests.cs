using System;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Entities;
using APG_CRM.Data.Services;
using APG_CRM.Data.Repositories;

namespace APG_CRM.Test
{
    [Collection("Sequential")]
    public class QuotationServiceTests
    {
        private readonly IQuotationService service;
        private readonly ICustomerService customerService;
        private readonly ISurveyService surveyService;  // Assuming you have this defined elsewhere in your code

        public QuotationServiceTests()
        {
            // configure the data context options to use sqlite for testing
            var options = DatabaseContext.OptionsBuilder
                            .UseSqlite("Filename=test.db")
                            //.LogTo(Console.WriteLine)
                            .Options;

            // create service with new context
            service = new QuotationServiceDb(new DatabaseContext(options));
            customerService = new CustomerServiceDb(new DatabaseContext(options));
            surveyService = new SurveyServiceDb(new DatabaseContext(options)); // Initialize surveyService
            service.Initialise();
        }// test enviroment


        // ==================== AddQuotation(Quotation q); // create method Test =========================

        [Fact]
        public void AddQuotation_WithNoSurvey()
        {
            // Arrange
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2022, 07, 01),
                Name = "Primary Schoool in Derry",
                Street = "458 Derry Ave",
                City = "Derry City",
                County = "Derry County",
                PostCode = "AB12 CDE",
                Phone = "0143456789",
                Email = "PrimarySchoool@example.com",
                Description = "New Customer",
                Type = "Company",
                PaymentTerms = "Standard payment terms"
            });

            var quotationToAdd = new Quotation
            {
                Date = new DateTime(2023, 8, 5),
                Title = "Primary Schoool newQuotation",
                ContactName = "Terry ShaneDoe",
                ContactPhone = "0123456789",
                ContactEmail = "terryPrimarySchoool@example.com",
                Description = "This is a new quotation",
                RequiresSurvey = false,     //No Survey needed.
                WorkType = "supply mirrors",
                DeliveryRequired = false,
                DeliveryAddress = "N/A",
                Price = 140,
                QuoteSentDate = new DateTime(2023, 8, 5),
                QuoteFollowDate = new DateTime(2023, 8, 13),
                Status = Quotestatus.Pending,
                Response = "Awaiting response",
                Urgency = 5,
                CustomerId = customer.Id
            };

            // Act
            var addedQuotation = service.AddQuotation(quotationToAdd);
            var retrievedQuotation = service.GetQuotationById(addedQuotation.Id);

            // Assert
            Assert.NotNull(retrievedQuotation); // Check that the retrievedQuotation is not null
            Assert.False(retrievedQuotation.RequiresSurvey); // Check that no survey is required
            Assert.Equal(quotationToAdd.Date, retrievedQuotation.Date);
            Assert.Equal(quotationToAdd.Title, retrievedQuotation.Title);
            Assert.Equal(quotationToAdd.ContactName, retrievedQuotation.ContactName);
            Assert.Equal(quotationToAdd.ContactPhone, retrievedQuotation.ContactPhone);
            Assert.Equal(quotationToAdd.ContactEmail, retrievedQuotation.ContactEmail);
            Assert.Equal(quotationToAdd.Description, retrievedQuotation.Description);
            Assert.Equal(quotationToAdd.WorkType, retrievedQuotation.WorkType);
            Assert.False(retrievedQuotation.DeliveryRequired);
            Assert.Equal(quotationToAdd.Price, retrievedQuotation.Price);
            Assert.Equal(quotationToAdd.Status, retrievedQuotation.Status);
            Assert.Equal(quotationToAdd.Response, retrievedQuotation.Response);
            Assert.Equal(quotationToAdd.Urgency, retrievedQuotation.Urgency);
            Assert.Equal(customer.Id, retrievedQuotation.CustomerId);
        }

        [Fact]
        public void AddQuotation_WithSurveyIsRequired()
        {
            // Arrange
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2023, 07, 01),
                Name = "Jimmy Doherty Duilders",
                Street = "458 Derry Ave",
                City = "DerryCity",
                County = "Derry County",
                PostCode = "AB12 CDE",
                Phone = "0123456789",
                Email = "jim@example.com",
                Description = "New Customer",
                Type = "Individual",
                PaymentTerms = "Standard payment terms"
            });

            var survey = surveyService.CreateSurvey(new Survey
            {
                RequestDate = new DateTime(2023, 9, 5),                    //Issued in test with using enity of date being  new DateTime.now as there was a conflic with test creating both dates for Quotations and Survey at the same time. - so a time is set. 
                ScheduleDate = new DateTime(2023, 9, 5).AddDays(1),
                PreSurveyNotes = "ground floor window to survey",
                Street = "123 Test Street",
                City = "TestCity",
                County = "TestCounty",
                PostCode = "BT488SE",
                SurveyPhone = "1234567890",
                Status = SurveyStatus.Pending,
                Reviewed = false,
                CompletedByWho = "Joe",
                Description = "Test Sean Coyle survey",
                RiskAssessment = "Low",
                CustomerId = customer.Id,
            });

            var quotationToAdd = new Quotation
            {
                Date = new DateTime(2023, 9, 5),
                Title = "Test newQuotation",
                ContactName = "Jane Doe",
                ContactPhone = "0123456789",
                ContactEmail = "janey@example.com",
                Description = "This is a new quotation",
                RequiresSurvey = true,
                SurveyId = survey.Id, //Survey Id needed as- RequiresSurvey = true
                WorkType = "Test new work type",
                DeliveryRequired = false,
                Price = 100,
                Status = Quotestatus.Pending,
                Response = "Awaiting response",
                Urgency = 5,
                CustomerId = customer.Id
            };

            // Act
            var addedQuotation = service.AddQuotation(quotationToAdd);
            var retrievedQuotation = service.GetQuotationById(addedQuotation.Id);

            // Assert
            Assert.NotNull(retrievedQuotation);
            Assert.True(retrievedQuotation.RequiresSurvey);
            Assert.Equal(survey.Id, retrievedQuotation.SurveyId);
            Assert.NotNull(retrievedQuotation);
            Assert.Equal(new DateTime(2023, 9, 5), retrievedQuotation.Date);
            Assert.Equal("Test newQuotation", retrievedQuotation.Title);
            Assert.Equal("Jane Doe", retrievedQuotation.ContactName);
            Assert.Equal("0123456789", retrievedQuotation.ContactPhone);
            Assert.Equal("janey@example.com", retrievedQuotation.ContactEmail);
            Assert.Equal("This is a new quotation", retrievedQuotation.Description);
            Assert.True(retrievedQuotation.RequiresSurvey);
            Assert.Equal("Test new work type", retrievedQuotation.WorkType);
            Assert.False(retrievedQuotation.DeliveryRequired);
            Assert.Equal(100, retrievedQuotation.Price);
            Assert.Equal(Quotestatus.Pending, retrievedQuotation.Status);
            Assert.Equal("Awaiting response", retrievedQuotation.Response);
            Assert.Equal(5, retrievedQuotation.Urgency);
            Assert.Equal(customer.Id, retrievedQuotation.CustomerId);
        }


        // ===========  Quotation GetQuotation(int id); //read method Test ================

        [Fact]
        public void GetQuotationById_ReturnsTheQuotationWithCorrectId()
        {
            // Arrange
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2023, 07, 01),
                Name = "david Doherty Duilders",
                Street = "6 Derry Ave",
                City = "DerryCity",
                County = "Derry County",
                PostCode = "AB12 CDE",
                Phone = "0123456789",
                Email = "david@example.com",
                Description = "New Customer",
                Type = "Individual",
                PaymentTerms = "Standard payment terms"
            });

            var survey = surveyService.CreateSurvey(new Survey
            {
                RequestDate = new DateTime(2023, 4, 5),                    //Issued in test with using enity of date being  new DateTime.now as there was a conflic with test creating both dates for Quotations and Survey at the same time. - so a time is set. 
                ScheduleDate = new DateTime(2023, 9, 5).AddDays(1),
                PreSurveyNotes = "ground floor window to survey",
                Street = "654 Test Street",
                City = "TestCity",
                County = "TestCounty",
                PostCode = "BT488SE",
                SurveyPhone = "1234567890",
                Status = SurveyStatus.Pending,
                Reviewed = false,
                CompletedByWho = "Joe",
                Description = "Test Sean Coyle survey",
                RiskAssessment = "Low",
                CustomerId = customer.Id,
            });

            var quotation = new Quotation
            {
                Date = new DateTime(2023, 9, 5),
                Title = "Test ID check",
                ContactName = "Joey Smith",
                ContactPhone = "0123456789",
                ContactEmail = "Joeyjjs@example.com",
                Description = "Description",
                RequiresSurvey = true,
                SurveyId = survey.Id,   //Survey Id needed as- RequiresSurvey = true- we want the serarch to seach everything.
                WorkType = "Test new work type",
                DeliveryRequired = false,
                Price = 100,
                Status = Quotestatus.Pending,
                Response = "Awaiting response",
                Urgency = 5,
                CustomerId = customer.Id
            };

            // Assuming you have a method to add or save a quotation.
            service.AddQuotation(quotation);

            // Act
            var testQuotation = service.GetQuotationById(quotation.Id);

            // Assert
            Assert.NotNull(testQuotation);
            Assert.Equal(quotation.Id, testQuotation.Id);
            Assert.Equal(quotation.Title, testQuotation.Title);
        }


        // ===========  Quotation UpdateQuotation(Quotation update); //uodate method Test =========
        [Fact]
        public void UpdateQuotation_ShouldUpdateQuotaion()
        {
            // Arrange
            // Adding a customer as required before creating a Quotation
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2022, 07, 01),
                Name = "Secondary School in Derry",
                Street = "458 Derry Ave",
                City = "Derry City",
                County = "Derry County",
                PostCode = "AB12 CDE",
                Phone = "0143456789",
                Email = "Secondary@example.com",
                Description = "New Customer",
                Type = "Company",
                PaymentTerms = "Standard payment terms"
            });
            Assert.NotNull(customer);  // Ensure the customer was created

            // Create an original Quotation
            var originalQuotation = new Quotation
            {
                Date = new DateTime(2023, 8, 5),
                Title = "Primary Schoool newQuotation",
                ContactName = "Terry ShaneDoe",
                ContactPhone = "0123456789",
                ContactEmail = "terryPrimarySchoool@example.com",
                Description = "This is a new quotation",
                RequiresSurvey = false,     //No Survey needed.
                WorkType = "supply mirrors",
                DeliveryRequired = false,
                DeliveryAddress = "N/A",
                Price = 140,
                QuoteSentDate = new DateTime(2023, 8, 5),
                QuoteFollowDate = new DateTime(2023, 8, 13),
                Status = Quotestatus.Pending,
                Response = "Awaiting response",
                Urgency = 5,
                CustomerId = customer.Id
            };

            service.AddQuotation(originalQuotation);
            Assert.True(originalQuotation.Id > 0);  // Ensure Quotation has an Id.

            // Modify properties for the update
            var updatedProperties = new Quotation
            {
                Id = originalQuotation.Id,
                Date = new DateTime(2023, 08, 10),
                Title = "Secondary School gym",
                ContactName = "Jane Doe",
                RequiresSurvey = false,
                WorkType = "supply glass for doors",
                DeliveryRequired = false,
                Price = 120,
                Status = Quotestatus.Approved,
                Response = "response Received, will not go ahead with the work",
                Urgency = 7,
                CustomerId = customer.Id
            };

            // === ACT ===

            // Update the quotation with modified properties
            var updatedQuotation = service.UpdateQuotation(updatedProperties);
            // Retrieve the updated quotation to verify changes

            var retrievedQuotation = service.GetQuotationById(updatedQuotation.Id);

            // === ASSERT ===

            // Check the ID matches and it's the same quotation
            Assert.Equal(updatedProperties.Id, retrievedQuotation.Id);

            // Check individual properties of the updated quotation
            Assert.Equal(updatedProperties.Date, retrievedQuotation.Date);
            Assert.Equal(updatedProperties.Title, retrievedQuotation.Title);
            Assert.Equal(updatedProperties.ContactName, retrievedQuotation.ContactName);
            Assert.Equal(updatedProperties.WorkType, retrievedQuotation.WorkType);
            Assert.Equal(updatedProperties.DeliveryRequired, retrievedQuotation.DeliveryRequired);
            Assert.Equal(updatedProperties.Price, retrievedQuotation.Price);
            Assert.Equal(updatedProperties.Status, retrievedQuotation.Status);
            Assert.Equal(updatedProperties.Response, retrievedQuotation.Response);
            Assert.Equal(updatedProperties.Urgency, retrievedQuotation.Urgency);
            Assert.Equal(updatedProperties.CustomerId, retrievedQuotation.CustomerId);
        }

        // ==================== bool DeleteQuotation(int id); -delete method Test =========================

        [Fact]
        public void DeleteQuotation_Test()
        {
            // Arrange
            // Adding a customer as required before creating a Quotation
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2022, 07, 01),
                Name = "Secondary School in Derry",
                Street = "458 Derry Ave",
                City = "Derry City",
                County = "Derry County",
                PostCode = "AB12 CDE",
                Phone = "0143456789",
                Email = "Secondary@example.com",
                Description = "New Customer",
                Type = "Company",
                PaymentTerms = "Standard payment terms"
            });
            Assert.NotNull(customer);  // Ensure the customer was created

            // Create an original Quotation
            var quotation = new Quotation
            {
                Date = new DateTime(2023, 8, 5),
                Title = "Secondary School Quotation",
                ContactName = "Terry Shane",
                ContactPhone = "0123456789",
                ContactEmail = "terryPrimarySchool@example.com",
                Description = "This is a new quotation",
                RequiresSurvey = false,
                WorkType = "supply mirrors",
                DeliveryRequired = false,
                Price = 140,
                Status = Quotestatus.Pending,
                Response = "Awaiting response",
                Urgency = 5,
                CustomerId = customer.Id
            };

            var addedQuotation = service.AddQuotation(quotation);
            Assert.NotNull(addedQuotation);  // Ensure the quotation was added and the service returned it
            Assert.True(addedQuotation.Id > 0); // Verifying the Glass has been added

            // Act
            var result = service.DeleteQuotation(addedQuotation.Id);

            // Assert
            Assert.True(result); // Ensuring the deletion was successful
            Assert.Null(service.GetQuotationById(addedQuotation.Id)); // Confirm the quotation no longer exists in the database
        }

        // ==================== SearchQuotations(string searchTerm); ; - search method Test =========================
        [Fact]
        public void CanSearchQuotations()
        {
            // Arrange
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2023, 07, 01),
                Name = "david Doherty Duilders",
                Street = "6 Derry Ave",
                City = "DerryCity",
                County = "Derry County",
                PostCode = "AB12 CDE",
                Phone = "0123456789",
                Email = "david@example.com",
                Description = "New Customer",
                Type = "Individual",
                PaymentTerms = "Standard payment terms"
            });

            var survey = surveyService.CreateSurvey(new Survey
            {
                RequestDate = new DateTime(2023, 4, 5),                    //Issued in test with using enity of date being  new DateTime.now as there was a conflic with test creating both dates for Quotations and Survey at the same time. - so a time is set. 
                ScheduleDate = new DateTime(2023, 9, 5).AddDays(1),
                PreSurveyNotes = "ground floor window to survey",
                Street = "654 Test Street",
                City = "TestCity",
                County = "TestCounty",
                PostCode = "BT488SE",
                SurveyPhone = "1234567890",
                Status = SurveyStatus.Pending,
                Reviewed = false,
                CompletedByWho = "Joe",
                Description = "Test Sean Coyle survey",
                RiskAssessment = "Low",
                CustomerId = customer.Id,
            });

            var quotation = new Quotation
            {
                Date = new DateTime(2023, 9, 5),
                Title = "Test search",
                ContactName = "Joey Joe Joe Shabadooo",
                ContactPhone = "0123456789",
                ContactEmail = "Joeyjjs@example.com",
                Description = "Description",
                RequiresSurvey = true,
                SurveyId = survey.Id,   //Survey Id needed as- RequiresSurvey = true- we want the serarch to seach everything.
                WorkType = "Test new work type",
                DeliveryRequired = false,
                Price = 100,
                Status = Quotestatus.Pending,
                Response = "Awaiting response",
                Urgency = 5,
                CustomerId = customer.Id
            };

            service.AddQuotation(quotation);

            // Act
            var searchResults = service.SearchQuotations("Joey Joe Joe Shabadooo");

            // Assert
            Assert.NotEmpty(searchResults); // Ensure results are returned

            var expectedQuotation = searchResults.FirstOrDefault(q => q.ContactName == "Joey Joe Joe Shabadooo");
            Assert.Contains(expectedQuotation, searchResults);
        }

        [Fact]
        public void SearchQuotationsReturnsEmptyWithNoMatch()
        {
            // Act
            var searchResults = service.SearchQuotations("NonexistentTerm");

            // Assert
            Assert.Empty(searchResults); // Ensure no results are returned
        }

    }
}