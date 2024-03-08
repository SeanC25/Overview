using System;
using Xunit;
using Moq;
using APG_CRM.Data.Entities;
using APG_CRM.Data.Services;
using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;


namespace APG_CRM.Test
{
    [Collection("Sequential")]
    public class SurveyServiceTests
    {
        private readonly ISurveyService service;
        private readonly ICustomerService customerService;

        public SurveyServiceTests()
        {
            // configure the data context options to use sqlite for testing
            var options = DatabaseContext.OptionsBuilder
                            .UseSqlite("Filename=test.db")
                            //.LogTo(Console.WriteLine)
                            .Options;

            // create service with new context
            service = new SurveyServiceDb(new DatabaseContext(options));
            customerService = new CustomerServiceDb(new DatabaseContext(options));
            service.Initialise();
        }// test enviroment

        // ==================== Test: CreateSurvey(Survey sur) =========================

        [Fact]
        public void CreateSurvey_AddNewSurvey_SurveyAdded()
        {
            // Arrange- Survey needs a customer for the relationship
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2023, 07, 01),
                Name = "Sean Coyle",
                Street = "123 Donegal street",
                City = "Derry City",
                County = "County Derry",
                PostCode = " BT48 7NF",
                Phone = "0788262923",
                Email = "seancoylesc@hotmail.com",
                Description = "New Customer",
                Type = "Individual",
                PaymentTerms = "standard terms"
            });

            var survey = new Survey
            {
                RequestDate = DateTime.Now,
                ScheduleDate = DateTime.Now.AddDays(1),
                PreSurveyNotes = "ground floor window to survey",
                RequiresLadders = false,
                RequiresScissorStairs = false,
                RequiresStreetBullards = false,
                NumStaffRequired = 2,
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
            };

            // Act
            var result = service.CreateSurvey(survey);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("123 Test Street", result.Street);
        }

        [Fact]
        public void CreateSurvey_AddDuplicateSurvey_ReturnsNull()
        {
            // Arrange
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2023, 07, 01),
                Name = "Sean Coyle",
                Street = "123 Donegal street",
                City = "Derry City",
                County = "County Derry",
                PostCode = " BT48 7NF",
                Phone = "0788262923",
                Email = "seancoylesc@hotmail.com",
                Description = "New Customer",
                Type = "Individual",
                PaymentTerms = ""
            });
            var survey1 = new Survey
            {
                RequestDate = DateTime.Now,
                ScheduleDate = DateTime.Now.AddDays(2),
                PreSurveyNotes = "None",
                RequiresLadders = false,
                RequiresScissorStairs = false,
                RequiresStreetBullards = false,
                NumStaffRequired = 2,
                Street = "123 Duplicate Street",
                City = "TestCity",
                County = "TestCounty",
                PostCode = "BT488SE",
                SurveyPhone = "1234567890",
                Status = SurveyStatus.Pending,
                Reviewed = false,
                CompletedByWho = "Joe",
                Description = "Test survey",
                RiskAssessment = "Test",
                CustomerId = customer.Id,
            };

            var survey2 = new Survey
            {
                RequestDate = DateTime.Now,
                ScheduleDate = DateTime.Now.AddDays(2),
                PreSurveyNotes = "None",
                RequiresLadders = false,
                RequiresScissorStairs = false,
                RequiresStreetBullards = false,
                NumStaffRequired = 2,
                Street = "123 Duplicate Streett",
                City = "TestCity",
                County = "TestCounty",
                PostCode = "BT488SE",
                SurveyPhone = "1234567890",
                Status = SurveyStatus.Pending,
                Reviewed = false,
                CompletedByWho = "Joe",
                Description = "Test survey",
                RiskAssessment = "Test",
                CustomerId = customer.Id,
            };

            service.CreateSurvey(survey1);

            // Act
            var result = service.CreateSurvey(survey2);

            // Assert
            Assert.Null(result);
        }


        // =============== GetGlassById(int id); //read method Test =========
        [Fact]
        public void GetSurveyById_ReturnsTheSurveysWithCorrectId()
        {
            // Arrange- Survey needs a customer for the relationship
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2023, 07, 01),
                Name = "Sean Coyle",
                Street = "123 Donegal street",
                City = "Derry City",
                County = "County Derry",
                PostCode = " BT48 7NF",
                Phone = "0788262923",
                Email = "seancoylesc@hotmail.com",
                Description = "New Customer",
                Type = "Individual",
                PaymentTerms = "standard terms"
            });

            var survey = new Survey
            {
                RequestDate = DateTime.Now,
                ScheduleDate = DateTime.Now.AddDays(1),
                PreSurveyNotes = "ground floor window to survey",
                RequiresLadders = false,
                RequiresScissorStairs = false,
                RequiresStreetBullards = false,
                NumStaffRequired = 2,
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
            };

            service.CreateSurvey(survey); // Creates the survey. Now, 'survey' should have its ID populated.

            // Act
            var testSurvey = service.GetSurveyById(survey.Id);
            Assert.True(survey.Id > 0); // Ensure Supplier has an Id.

            // Assert
            Assert.NotNull(testSurvey); // Ensures the survey was retrieved.
            Assert.Equal(survey.Id, testSurvey.Id); // Compares the IDs.
            Assert.Equal("Low", testSurvey.RiskAssessment); // Ensures the risk assessment matches.
        }

        [Fact]
        public void GetSurvey_WhenNoneExist_ExpectedNull()
        {
            // Arrange: Not needed 

            // Act: Trying to fetch a survey with an ID does not exist.
            var testSurvey = service.GetSurveyById(999999);
            // Assert -ID shouldn't exist.
            Assert.Null(testSurvey);
        }

        [Fact]
        public void GetSurvey_WhenAdded_ShouldReturnSurvey()
        {
            //Arrange- don't need to arrange anything as  testing for a non-existing survey.
            // Act
            var testSurvey = service.GetSurveyById(1); // Trying to fetch a survey with ID=1 which shouldn't exist.

            // Assert
            Assert.Null(testSurvey); // Expecting null as the survey with ID=1 shouldn't exist.
        }

        //==================== Test: UpdateSurvey(Survey update)=========================

        [Fact]
        public void UpdateSurvey_WithValidInput_ReturnsUpdatedSurvey()
        {
            // Arrange
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2023, 07, 01),
                Name = "Sean Coyle",
                Street = "123 Donegal street",
                City = "Derry City",
                County = "County Derry",
                PostCode = " BT48 7NF",
                Phone = "0788262923",
                Email = "seancoylesc@hotmail.com",
                Description = "New Customer",
                Type = "Individual",
                PaymentTerms = ""
            });

            var survey1 = service.CreateSurvey(new Survey
            {
                RequestDate = DateTime.Now,
                ScheduleDate = DateTime.Now.AddDays(2),
                PreSurveyNotes = "None",
                RequiresLadders = false,
                RequiresScissorStairs = false,
                RequiresStreetBullards = false,
                NumStaffRequired = 2,
                Street = "123 Duplicate Street",
                City = "TestCity",
                County = "TestCounty",
                PostCode = "BT488SE",
                SurveyPhone = "1234567890",
                Status = SurveyStatus.Pending,
                Reviewed = false,
                CompletedByWho = null,
                Description = "Test survey",
                RiskAssessment = "Test",
                CustomerId = customer.Id,
            });

            // Act       
            survey1.RequestDate = DateTime.Now;
            survey1.ScheduleDate = DateTime.Now.AddDays(2);
            survey1.PreSurveyNotes = "Updated-None";
            survey1.RequiresLadders = false;
            survey1.RequiresScissorStairs = false;
            survey1.RequiresStreetBullards = false;
            survey1.NumStaffRequired = 2;
            survey1.Street = "456 Updated Duplicate Street";
            survey1.City = "Updated-TestCity";
            survey1.County = "Updated-TestCounty";
            survey1.PostCode = "BT488SA";
            survey1.SurveyPhone = "9876543210";
            survey1.Status = SurveyStatus.Pending;
            survey1.Reviewed = true;
            survey1.CompletedByWho = "Sean Coyle";
            survey1.Description = "Updated-Test survey";
            survey1.RiskAssessment = "Updated-Test";

            var result = service.UpdateSurvey(survey1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("456 Updated Duplicate Street", result.Street);
        }

        [Fact]
        public void UpdateSurvey_ToBeDuplicate_ReturnsNull()
        {
            // Arrange
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2023, 07, 01),
                Name = "Sean Coyle",
                Street = "123 Donegal street",
                City = "Derry City",
                County = "County Derry",
                PostCode = " BT48 7NF",
                Phone = "0788262923",
                Email = "seancoylesc@hotmail.com",
                Description = "New Customer",
                Type = "Individual",
                PaymentTerms = ""
            });

            var survey1 = service.CreateSurvey(new Survey
            {
                // assuming Id property is available and is the primary key
                RequestDate = DateTime.Now,
                ScheduleDate = DateTime.Now.AddDays(2),
                PreSurveyNotes = "None",
                RequiresLadders = false,
                RequiresScissorStairs = false,
                RequiresStreetBullards = false,
                NumStaffRequired = 2,
                Street = "123 Duplicate Street",
                City = "TestCity",
                County = "TestCounty",
                PostCode = "BT488SE",
                SurveyPhone = "1234567890",
                Status = SurveyStatus.Pending,
                Reviewed = false,
                CompletedByWho = null,
                Description = "Test survey",
                RiskAssessment = "Test",
                CustomerId = customer.Id,
            });

            // Act       
            var survey2 = service.CreateSurvey(new Survey
            {
                // assuming Id property is available and is the primary key
                RequestDate = DateTime.Now,
                ScheduleDate = DateTime.Now.AddDays(2),
                PreSurveyNotes = "None",
                RequiresLadders = false,
                RequiresScissorStairs = false,
                RequiresStreetBullards = false,
                NumStaffRequired = 2,
                Street = "123 Duplicate Street",
                City = "TestCity",
                County = "TestCounty",
                PostCode = "BT488SE",
                SurveyPhone = "1234567890",
                Status = SurveyStatus.Pending,
                Reviewed = false,
                CompletedByWho = null,
                Description = "Test survey",
                RiskAssessment = "Test",
                CustomerId = customer.Id,
            });

            // Assert
            Assert.NotNull(survey1);
            Assert.Null(survey2);
        }

        //==================== Test:  public bool DeleteSurvey(int id)=========================

        [Fact]
        public void DeleteSurvey_Nonexisting_ReturnsFalse()
        {
            //act 
            var del = service.DeleteSurvey(0);
            // assert
            Assert.False(del);
        }

        [Fact]
        public void DeleteSurvey_WithValidId_DeletesSurvey()
        {
            // Arrange
            var customer = customerService.AddCustomer(new Customer
            {
                DateEstablished = new DateTime(2023, 07, 01),
                Name = "Sean Coyle",
                Street = "123 Donegal street",
                City = "Derry City",
                County = "County Derry",
                PostCode = " BT48 7NF",
                Phone = "0788262923",
                Email = "seancoylesc@hotmail.com",
                Description = "New Customer",
                Type = "Individual",
                PaymentTerms = ""
            });

            var survey = service.CreateSurvey(new Survey
            {
                // assuming Id property is available and is the primary key
                RequestDate = DateTime.Now,
                ScheduleDate = DateTime.Now.AddDays(2),
                PreSurveyNotes = "None",
                RequiresLadders = false,
                RequiresScissorStairs = false,
                RequiresStreetBullards = false,
                NumStaffRequired = 2,
                Street = "123 Duplicate Street",
                City = "TestCity",
                County = "TestCounty",
                PostCode = "BT488SE",
                SurveyPhone = "1234567890",
                Status = SurveyStatus.Pending,
                Reviewed = false,
                CompletedByWho = null,
                Description = "Test survey",
                RiskAssessment = "Test",
                CustomerId = customer.Id,
            });

            // Act
            bool deleteResult = service.DeleteSurvey(survey.Id);

            var deletedSurvey = service.GetSurveyById(survey.Id);

            // Assert
            Assert.True(deleteResult); // checks if the delete method returned true
            Assert.Null(deletedSurvey); // checks if the survey can still be found
        }

    }
}
