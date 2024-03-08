using Xunit;
using APG_CRM.Data.Entities;
using APG_CRM.Data.Services;
using Microsoft.EntityFrameworkCore;
using APG_CRM.Data.Repositories;

namespace APG_CRM.Test
{
    [Collection("Sequential")]
    public class CustomerServiceTests
    {
        private readonly ICustomerService service;

        public CustomerServiceTests()
        {
            // configure the data context options to use sqlite for testing
            var options = DatabaseContext.OptionsBuilder
                            .UseSqlite("Filename=test.db")
                            //.LogTo(Console.WriteLine)
                            .Options;

            // create service with new context
            service = new CustomerServiceDb(new DatabaseContext(options));
            service.Initialise();
        }

        // ==================== AddCustomer Test =========================
        [Fact]
        public void Can_Create_Customer()
        {
            // arrange
            var data = service.AddCustomer(new Customer
                {
                    Name = "Joe Bloggs",
                    Street = "Main St",
                    City = "Nowhere",
                    County = "Somewhere",
                    PostCode = "BT67134",
                    Phone = "03455665666",
                    Email = "joe@mail.com",
                    Description = "Blah....."
                });

            //act
            var customer = service.GetCustomerById(data.Id);

            //assert
            Assert.NotNull(customer);
            Assert.Equal("Joe Bloggs", customer.Name);
        }


        [Fact]
        public void AddCustomer_DuplicateCustomerEmail_ShouldReturnNull()
        {
            // arrange
            var data1 = service.AddCustomer(
                new Customer
                {
                    Name = "Joe Bloggs",
                    Street = "Main St",
                    City = "Nowhere",
                    County = "Somewhere",
                    PostCode = "BT67134",
                    Phone = "03455665666",
                    Email = "joe@mail.com",
                    Description = "Blah....."
                });

            // act
            var data2 = service.AddCustomer(
                new Customer
                {
                    Name = "Jane Bloggs",
                    Street = "Back St",
                    City = "Upside",
                    County = "Roundabout",
                    PostCode = "BT87134",
                    Phone = "05455665666",
                    Email = "joe@mail.com",
                    Description = "Bonkers....."
                });
            // assert
            Assert.NotNull(data1);
            Assert.Null(data2);
        }

        // ==================== GetCustomer Tests =========================
        [Fact]
        public void GetCustomer_WhenNoneExistExpectedNull()
        {
            // act
            var customer = service.GetCustomerById(1); //this Customer is not there
            // assert
            Assert.Null(customer);
        }

        [Fact]
        public void GetCustomer_WhenAdded_ShouldReturnCustomer()
        {
            // arrange
            var cu3 = service.AddCustomer(new Customer
            {
                Name = "Jane Bloggs",
                Street = "Main St",
                City = "Nowhere",
                County = "Somewhere",
                PostCode = "BT67134",
                Phone = "03455665666",
                Email = "joe@mail.com",
                Description = "Blah....."
            });

            // act
            var cu3test = service.GetCustomerById(cu3.Id);
            // assert
            Assert.NotNull(cu3test);
            Assert.Equal(cu3test.Id, cu3.Id);
        }

        // ==================== GetAllCustomers Tests =========================

        [Fact]
        public void GetAllCustomers_WhenThereAreNone_ShoulReturnZero()
        {
            // act
            var Customers = service.GetAllCustomers();
            var count = Customers.Count;

            // assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void GetAllCustomers_WhenThereAreTwo_ShoulReturnCount2()
        {
            // arrange
            var cu1 = service.AddCustomer(new Customer
            {
                Name = "Jane Bloggs",
                Street = "Main St",
                City = "Nowhere",
                County = "Somewhere",
                PostCode = "BT67134",
                Phone = "03455665666",
                Email = "joe@mail.com",
                Description = "Blah....."
            });

            var cu2 = service.AddCustomer(new Customer
            {
                Name = "Joe Bloggs",
                Street = "Main St",
                City = "Nowhere",
                County = "Somewhere",
                PostCode = "BT67134",
                Phone = "02455665666",
                Email = "jane@mail.com",
                Description = "Blah....."
            });

            // act
            var Customers = service.GetAllCustomers();
            var count = Customers.Count;

            // assert
            Assert.Equal(2, count);
        }

        // ====================  UpdateCustomer Tests =========================
        [Fact]
        public void UpdateCustomer_ThatExists_ShouldSetAllProperties()
        {
            // Arrange
            var c = service.AddCustomer(new Customer
            {
                Name = "Jane Bloggs",
                Street = "Main St",
                City = "Nowhere",
                County = "Somewhere",
                PostCode = "BT67134",
                Phone = "03455665666",
                Email = "joe@mail.com",
                Description = "Blah....."
            });

            // Act
            var update = service.UpdateCustomer(new Customer
            {
                Id = c.Id,
                Name = "Jimmy Bloggs",
                Street = "Lower St",
                City = "NowhereElse",
                County = "SomewhereDiffernet",
                PostCode = "DD67134",
                Phone = "12355665666",
                Email = "jIMMY@mail.com",
                Description = "DingDong....."
            });

            var uc = service.GetCustomerById(c.Id);

            // Assert
            Assert.NotNull(uc);

            Assert.Equal(update.DateEstablished, uc.DateEstablished);
            Assert.Equal(update.Type, uc.Type);
            Assert.Equal(update.Name, uc.Name);
            Assert.Equal(update.Street, uc.Street);
            Assert.Equal(update.City, uc.City);
            Assert.Equal(update.County, uc.County);
            Assert.Equal(update.PostCode, uc.PostCode);
            Assert.Equal(update.Phone, uc.Phone);
            Assert.Equal(update.Email, uc.Email);
            Assert.Equal(update.Description, uc.Description);
            Assert.Equal(update.PaymentTerms, uc.PaymentTerms);
        }


        // ====================  DeleteCustomer Tests =========================

        [Fact]
        public void DeleteCustomer_Nonexisting_ReturnsFalse()
        {
            //act 
            var del = service.DeleteCustomer(0);
            // assert
            Assert.False(del);
        }

        [Fact]
        public void DeleteCustomer_Existing_ReturnsTrue()
        {
            // arrange
            var DelC = service.AddCustomer(new Customer
            {
                Name = "Jane Bloggs",
                Street = "Main St",
                City = "Nowhere",
                County = "Somewhere",
                PostCode = "BT67134",
                Phone = "03455665666",
                Email = "joe@mail.com",
                Description = "Blah....."
            });

            //act 
            var del = service.DeleteCustomer(DelC.Id);

            // make an attempt to retrieve the deleted Customer
            var checkDel = service.GetCustomerById(DelC.Id);

            //assert
            Assert.True(del); //if the Customer has been deleted it should return true
            Assert.Null(checkDel); //the check should be null because it was deleted
        }
    }
}
