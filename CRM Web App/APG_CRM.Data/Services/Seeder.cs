using System;
using System.Linq;
using APG_CRM.Data.Entities;
namespace APG_CRM.Data.Services;
public static class Seeder
{
    // use this class to seed the database with dummy test data using an IUserService 
    public static void Seed(IUserService svc)
    {
        svc.Initialise();  // seeder destroys and recreates the database - this will only be done once in production to create dummy data.

        svc.Register("admin", "admin@mail.com", "password", Role.admin);
        svc.Register("admin1", "admin1@mail.com", "password", Role.admin);
        svc.Register("administrator", "administrator@mail.com", "password", Role.admin);
        svc.Register("manager", "manager@mail.com", "password", Role.manager);
        svc.Register("Sean Coyle", "sean@mail.com", "password", Role.manager);
        svc.Register("customer1", "customer1@mail.com", "password", Role.customer);
        svc.Register("customer2", "customer2@mail.com", "password", Role.customer);
        svc.Register("survey1", "survey1@mail.com", "password", Role.survey);
        svc.Register("survey2", "survey2@mail.com", "password", Role.survey);
    }

    public static void SeedData(ICustomerService csvc, IQuotationService qsvc, ISurveyService ssvc, ISupplierService spsvc, IGlassService gsvc) // This method seeds the database with dummy  data.
    {

        //***======================= Customer seeder ============================================================================//
        // Seeding customers

        var cu1 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 02),
            Name = "Bridget Coyle",
            Street = "123 KillKenny Road",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT48 6JB",
            Phone = "078262923",
            Email = "Bridget@mail.com",
            Description = "long established customer often buys mirrors",
            Type = "Individual",
            PaymentTerms = "Repeat Customer Terms"
        });

        var cu2 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2024, 04, 12),
            Name = "Mary Kerr",
            Street = "2 Garvagh Rd, Dungiven, Londonderry ",
            City = "Dungiven",
            County = "County Derry",
            PostCode = "BT47 4LT",
            Phone = "0798762923",
            Email = "maryk25@hotmail.com",
            Description = "long established customer has all her DGU changed in 2018",
            Type = "Individual",
            PaymentTerms = "Repeat Customer Terms"
        });

        var cu3 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 03),
            Name = "Philip Coyle",
            Street = "123 Armagh Road",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT48 6EF",
            Phone = "0799262923",
            Email = "Philip@mail.com",
            Description = "Regular Customer, orders about 3 times a year but big orders",
            Type = "Individual",
            PaymentTerms = "Repeat Customer Terms"
        });

        var cu4 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 14),
            Name = "Sinead Mallon",
            Street = "123 Galway Road",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT48 8JG",
            Phone = "0789262923",
            Email = "sinead@mail.com",
            Description = "Supply of cut glass sheets about once every 3 mothns since 2021",
            Type = "Individual",
            PaymentTerms = "Repeat Customer Terms"
        });

        var cu5 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 14),
            Name = "Dennis Mallon",
            Street = "123 Killybeg Road",
            City = "Derry City",
            County = "County Donegal",
            PostCode = "BT46 8JG",
            Phone = "0789262423",
            Email = "Dennis@mail.com",
            Description = "Supply of cut glass sheets",
            Type = "Individual",
            PaymentTerms = "Individual Terms"
        });

        var cu6 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 14),
            Name = "Mary Mallon",
            Street = "123 Fintra Road",
            City = "Killybegs",
            County = "County Donegal",
            PostCode = "BT46 8JG",
            Phone = "0789262423",
            Email = "MaryMallon@mail.com",
            Description = "Supply of cut glass sheets",
            Type = "Individual",
            PaymentTerms = "Individual Terms"
        });

        var cu7 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 14),
            Name = "Mary Doherty",
            Street = "123 Finvola Park",
            City = "Dunfanaghy",
            County = "County Donegal",
            PostCode = "BT48 8JT",
            Phone = "0789262473",
            Email = "MaryDoherty@mail.com",
            Description = "Supply of cut glass sheets",
            Type = "Individual",
            PaymentTerms = "Individual Terms"
        });

        var cu8 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 14),
            Name = "Tony Barry",
            Street = "123 Petter Park",
            City = "Omagh",
            County = "County Tyrone",
            PostCode = "BT78 1EE",
            Phone = "0789262473",
            Email = "tonyb@mail.com",
            Description = "Supply of decourative front door desgined 45",
            Type = "Individual",
            PaymentTerms = "Individual Terms"
        });

        var cu9 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 14),
            Name = "Gary Balck",
            Street = "123 Fountain Street",
            City = "Strabane",
            County = "County Tyrone",
            PostCode = "BT82 8JG",
            Phone = "0789262473",
            Email = "gbalck@mail.com",
            Description = "Gary owns a gym and we haved Supply and install of Gym Mirror for him",
            Type = "Individual",
            PaymentTerms = "Individual Terms"
        });

        var cu10 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 14),
            Name = "Sarah Lynch",
            Street = "8 Greystone Rd",
            City = "Limavady",
            County = "County Tyrone",
            PostCode = "BT49 0ND",
            Phone = "0789962473",
            Email = "lynch23@mail.com",
            Description = "Sarah bought stove glass from us in the past, but alos needs a price for DGU",
            Type = "Individual",
            PaymentTerms = "Individual Terms"
        });

        var cu11 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 01),
            Name = "Sean Coyle",
            Street = "123 Donegal street",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT48 7NF",
            Phone = "0788262923",
            Email = "seancoylesc@hotmail.com",
            Description = "New Customer",
            Type = "Individual",
            PaymentTerms = "Individual Terms"
        });

        //company customers = co
        var co1 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(1985, 07, 01),
            Name = "St Pauls Primary School",
            Street = "12 St Paul street",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT47 6WN",
            Phone = "0788262923",
            Email = "enquires@stpauls.com",
            Description = "Long Establised company, main contact there is Terry Creggan the maintenace Manager his mobile is 07888262923",
            Type = "Company",
            PaymentTerms = "Company on Accounts"
        });

        var co2 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 16),
            Name = "McDaid Builders",
            Street = "123 Kerry Road",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT47 3JG",
            Phone = "0789262923",
            Email = "Paul@mail.com",
            Description = "long established building company works all over derry city and portrush area mostly buys replacement double glazing",
            Type = "Company",
            PaymentTerms = "Company on account"
        });

        var co3 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 16),
            Name = "Town Council",
            Street = "123 Belfast Road",
            City = "Belfast ",
            County = "Belfast",
            PostCode = "HT47 8JG",
            Phone = "0786262923",
            Email = "council@mail.com",
            Description = "Local government council",
            Type = "Company",
            PaymentTerms = "Company on account"
        });

        var co4 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 16),
            Name = "Insurance Provider",
            Street = "123 Glasgow Road",
            City = "Belfast ",
            County = "Belfast",
            PostCode = "BT47 4UG",
            Phone = "0786262923",
            Email = "Insurance@mail.com",
            Description = "International Insurance Company",
            Type = "Company",
            PaymentTerms = "Company on account"
        });

        var co5 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 16),
            Name = "Five Star Bathrooms ",
            Street = "Gortrush Industrial Estate",
            City = "Omagh",
            County = "County Tyrone",
            PostCode = "BT47 4UG",
            Phone = "07862632923",
            Email = "5StarBathrooms@mail.com",
            Description = "Bathroom installers, usually buy Mirrors and Shower screen glass",
            Type = "Company",
            PaymentTerms = "Company not on account"
        });

        var co6 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 14),
            Name = "ABC Maintenance",
            Street = "123 Galway Road",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT47 8PG",
            Phone = "02871357444",
            Email = "info@abcmaintenance.com",
            Description = "Company Customer",
            Type = "Company",
            PaymentTerms = "Company on account"
        });

        var co7 = csvc.AddCustomer(new Customer
        {
            DateEstablished = new DateTime(2023, 07, 14),
            Name = "Peter White Butcher Shop,",
            Street = " 1 Colmans Street",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT47 6BG",
            Phone = "02871357444",
            Email = "info@pwbutchers.com",
            Description = "Company Customer",
            Type = "Company",
            PaymentTerms = "Company on account"
        });

        //***======================= surveys seeder ============================================================================//

        // create surveys
        var sur1 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now,
            ScheduleDate = DateTime.Now.AddDays(2),
            PreSurveyNotes = "Shop Front of house glass display cabinet is broken, need to take sizes of replacement pannel",
            RequiresLadders = false,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 1,
            Street = "Peter White Butchers, 1 Colmans Street",
            City = "Derry City",
            County = "Derry",
            PostCode = "BT47 6BG",
            SurveyPhone = "02871357444",
            Status = SurveyStatus.Completed,
            Reviewed = true,
            CompletedByWho = "Edward Bradley",
            Description = "Broken Glass removed, sizes taken for new screen pannel, needs to be a 10mm toughen with holes- see diagram attached, spoke with Kerry in the Butcher shop she said they might want to get a company logo design on the new front of shop glass, i took some picture of their design and she asked for a price of a etched glass design with no colour aswell as this they want a glass splash back for behind the counter, we will need to return to take accurate sizes as there are four wall plugs, but the approx size is 5000 x 660",
            RiskAssessment = "No Issues",
            CustomerId = co7.Id,
        });

        var sur2 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now.AddDays(-10),
            ScheduleDate = DateTime.Now.AddDays(5),
            PreSurveyNotes = "Emergency glazing- make safe- and get sizes, Two man job, front of street access, broken shop front glazing, need replacement safety wooden panneling to make safe- Speak to johny in the shop",
            RequiresLadders = false,
            RequiresScissorStairs = false,
            RequiresStreetBullards = true,
            NumStaffRequired = 2,
            Street = "456 Another St.",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT48 6RG",
            SurveyPhone = "02871 123456",
            Status = SurveyStatus.Pending,
            Reviewed = false,
            CompletedByWho = "Edward Bradly and Declan Duffy",
            Description = "arrived at job at 9am, two 10.8 laminate glass smashed, made safe boarded up and sizes taken, return to factory for new glass and scheuled re-fit tomorrow.",
            RiskAssessment = "Medium risk",
            CustomerId = cu3.Id
        });

        var sur3 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now.AddDays(-10),
            ScheduleDate = DateTime.Now.AddDays(5),
            PreSurveyNotes = "Measure up Gym for new Mirrors, speak to David when you arrive",
            RequiresLadders = false,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 1,
            Street = "Big Muscle Gym, 89 Mount Vernon",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT48 6HW",
            SurveyPhone = "02871 1239996",
            Status = SurveyStatus.Canceled,
            Reviewed = false,
            CompletedByWho = "Declan Duffy",
            Description = " Arrived on Site 9AM, spoke to David, he changes his mind and does not want any mirriors",
            RiskAssessment = "No Issue",
            CustomerId = cu3.Id
        });

        var sur4 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now.AddDays(-5),
            ScheduleDate = DateTime.Now.AddDays(2),
            PreSurveyNotes = "Front door glass broken, sizes and template new glass",
            RequiresLadders = false,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 1,
            Street = "Petter smith, 5 Broadway road.",
            City = "Derry City",
            County = "Derry",
            PostCode = "BT48 6HQ",
            SurveyPhone = "02871 88555",
            Status = SurveyStatus.Completed,
            Reviewed = false,
            CompletedByWho = "Kieran Doherty",
            Description = " door made save boarded up, sizes taken client wants a decourative design and will come to office to look at samples",
            RiskAssessment = "No Issues",
            CustomerId = cu2.Id,
        });

        var sur5 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now.AddDays(-5),
            ScheduleDate = DateTime.Now.AddDays(2),
            PreSurveyNotes = "Window on the 10th floor has broken DGU pannel",
            RequiresLadders = false,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 1,
            Street = "Hotel 5 Star.",
            City = " Lisburn",
            County = "Lisburn",
            PostCode = "BT28 2GW",
            SurveyPhone = "02871 88555",
            Status = SurveyStatus.Completed,
            Reviewed = false,
            CompletedByWho = "Kieran Doherty",
            Description = "door made save boarded up, sizes taken client wants a decourative design and will come to office to look at samples",
            RiskAssessment = "No Issues",
            CustomerId = cu2.Id,
        });

        var sur6 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now.AddDays(-10),
            ScheduleDate = DateTime.Now.AddDays(5),
            PreSurveyNotes = "Two man job, of broken stair case glass- need replacement safety wooden panneling to infill the gap in stairs.",
            RequiresLadders = false,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 2,
            Street = "456 Another St.",
            City = "Omagh",
            County = "County Tyrone",
            PostCode = "BT78 1AE",
            SurveyPhone = "02871 123456",
            Status = SurveyStatus.Canceled,
            Reviewed = false,
            CompletedByWho = "Edward Bradly and Declan Duffy",
            Description = "removal of broken glass, stairs made safe,- 2nr 1m x 2.5m glass 10mm toughen low-3 clear Par Staire case glass, client sarah wants refit ASAP",
            RiskAssessment = "Low risk",
            CustomerId = cu3.Id
        });

        var sur7 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now.AddDays(-5),
            ScheduleDate = DateTime.Now.AddDays(2),
            PreSurveyNotes = "Key for the house is left under the plant pot at the front door, will need ladders to access the window on the 1st floow landing",
            RequiresLadders = true,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 1,
            Street = "Foyle Dental, 3 St Micheals St.",
            City = "Derry City",
            County = "Derry",
            PostCode = "BT48 6XY",
            SurveyPhone = "02871 555555",
            Status = SurveyStatus.Pending,
            Reviewed = false,
            CompletedByWho = "Kieran Doherty",
            Description = "Broken glass in window on the staircase landing, single glazing replace glass on site",
            RiskAssessment = "Low risk",
            CustomerId = cu2.Id,
        });

        var sur8 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = new DateTime(2023, 8, 5),
            ScheduleDate = new DateTime(2023, 8, 10),
            PreSurveyNotes = " Survey carried out of 6 doors technology department, sizes 6nr 400 x 900 wired safety glass rough cast",
            RequiresLadders = true,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 1,
            Street = "st pauls, Primary school.",
            City = "Derry City",
            County = "Derry",
            PostCode = "BT48 6XY",
            SurveyPhone = "02871 555555",
            Status = SurveyStatus.Pending,
            Reviewed = false,
            CompletedByWho = "Kieran Doherty",
            Description = "Broken glass in window on the staircase landing, single glazing replace glass on site",
            RiskAssessment = "Low risk",
            CustomerId = co1.Id,
        });


        var sur9 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now.AddDays(-10),
            ScheduleDate = DateTime.Now.AddDays(5),
            PreSurveyNotes = "double check the size of the 4nr Gym for new Mirrors, speak to David when you arrive",
            RequiresLadders = true,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 1,
            Street = "Big Muscle Gym, 89 Mount Vernon",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT48 6HW",
            SurveyPhone = "02871 1239996",
            Status = SurveyStatus.Completed,
            Reviewed = true,
            CompletedByWho = "Declan Duffy",
            Description = "Arrived on Site 9AM, spoke to David, he wants these mirros to have 4nr holes and safety backing, one man job to install but fitted above 3M so need the ladders to install",
            RiskAssessment = "working at height",
            CustomerId = cu3.Id
        });


        var sur10 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now.AddDays(-10),
            ScheduleDate = DateTime.Now.AddDays(5),
            PreSurveyNotes = "speak to David, make template of their reception desk for a Toughen table top",
            RequiresLadders = false,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 2,
            Street = "Big Muscle Gym, 89 Mount Vernon",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT48 6HW",
            SurveyPhone = "02871 1239996",
            Status = SurveyStatus.Completed,
            Reviewed = true,
            CompletedByWho = "Declan Duffy",
            Description = "1nr 6mm Toughen table top- clear 1nr 800 X 140- Shaped-tempate made, if we are fitting this then its need 2nr Men for lifting",
            RiskAssessment = "low risk",
            CustomerId = cu3.Id
        });

        var sur11 = ssvc.CreateSurvey(new Survey
        {
            RequestDate = DateTime.Now.AddDays(-10),
            ScheduleDate = DateTime.Now.AddDays(5),
            PreSurveyNotes = " needs the back door glass measured up, 3nr windows in the kitcehen have missed up DGUs",
            RequiresLadders = false,
            RequiresScissorStairs = false,
            RequiresStreetBullards = false,
            NumStaffRequired = 1,
            Street = "123 Armagh Road",
            City = "Derry City",
            County = "County Derry",
            PostCode = "BT48 6HW",
            SurveyPhone = "02871 1239996",
            Status = SurveyStatus.Completed,
            Reviewed = true,
            CompletedByWho = "Declan Duffy",
            Description = "1nr 6mm Toughen table top- clear 1nr 800 X 140- Shaped-tempate made, if we are fitting this then its need 2nr Men for lifting",
            RiskAssessment = "low risk",
            CustomerId = cu3.Id
        });




        //  //***======================= Quotation seeder ============================================================================//
        var q1 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 8, 5),
            Title = "Pauls Primary School for Terry",
            ContactName = "Terry McShane",
            ContactPhone = "0123456789",
            ContactEmail = "terry@stpauls.com",
            Description = "Collection of 6nr 100X500 Polished Mirrors X4 holes",
            RequiresSurvey = false,     //No Survey needed.
            WorkType = "Mirrors",
            DeliveryRequired = false,
            DeliveryAddress = "N/A",
            Price = 1850,
            QuoteSentDate = new DateTime(2023, 8, 5),
            QuoteFollowDate = new DateTime(2023, 8, 13),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 2,
            Accepted = false,
            CustomerId = co1.Id
        });

        var q2 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 8, 5),
            Title = "st Pauls Security Door glass",
            ContactName = "Terry Creggan",
            ContactPhone = "0123456789",
            ContactEmail = "Jerry@stpauls.com",
            Description = "supply and install wired safety glass for the doors in the technology department",
            RequiresSurvey = true,     //Survey needed.
            //Survey = sur8.Id,
            WorkType = "supply and install",
            DeliveryRequired = true,
            DeliveryAddress = " st Pauls, 10 Brook street, BT48 7NH -Technology Department",
            Price = 1320,
            QuoteSentDate = new DateTime(2023, 8, 5),
            QuoteFollowDate = new DateTime(2023, 8, 13),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 3,
            Accepted = false,
            CustomerId = co1.Id
        });

        var q3 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 8, 5),
            Title = "st Peter Primary Schoool ",
            ContactName = "Becky Bradley",
            ContactPhone = "0123456789",
            ContactEmail = "bbradly@stpeterb.com",
            Description = "Toughen glass 1nr 800X1000",
            RequiresSurvey = false,     //No Survey needed.
            WorkType = "Toughen glass Table top",
            DeliveryRequired = false,
            DeliveryAddress = "N/A",
            Price = 140,
            QuoteSentDate = new DateTime(2023, 8, 5),
            QuoteFollowDate = new DateTime(2023, 8, 13),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 5,
            Accepted = false,
            CustomerId = cu1.Id
        });

        var q4 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 9, 5),
            Title = "Peter White Butchers",
            ContactName = "Peter White",
            ContactPhone = "0123468789",
            ContactEmail = "Peter o@pwbutchers.com",
            Description = "Shop Front emergency glazing and glass splash back supply and install",
            RequiresSurvey = true,
            SurveyId = sur1.Id, //Survey Id needed as- RequiresSurvey = true
            WorkType = "Emergency glazing and supply in splash back",
            DeliveryRequired = false,
            Price = 1800,
            QuoteSentDate = new DateTime(2023, 12, 5),
            QuoteFollowDate = new DateTime(2023, 12, 13),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 5,
            Accepted = false,
            CustomerId = co7.Id
        });

        var q5 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 9, 5),
            Title = "regular schdule- stove glass",
            ContactName = "Jane Doe",
            ContactPhone = "0123456789",
            ContactEmail = "janey@example.com",
            Description = " 10nr 360x40 stove glass",
            RequiresSurvey = true,
            SurveyId = sur1.Id, //Survey Id needed as- RequiresSurvey = true
            WorkType = "Collection-stove glass",
            DeliveryRequired = false,
            Price = 100,
            QuoteSentDate = new DateTime(2023, 10, 4),
            QuoteFollowDate = new DateTime(2023, 10, 10),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 5,
            Accepted = false,
            CustomerId = cu1.Id
        });
        //***======================= extra quotes-  

        var q6 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 9, 5),
            Title = "Blue bell woodland nursey",
            ContactName = "David Green",
            ContactPhone = "0123456789",
            ContactEmail = "david@bluebellnursey.com",
            Description = "60nr 2X4 green house glass",
            RequiresSurvey = false,
            //SurveyId = surxxx.Id, //Survey Id needed as- RequiresSurvey = true
            WorkType = "Collection",
            DeliveryRequired = false,
            Price = 100,
            QuoteSentDate = new DateTime(2023, 10, 5),
            QuoteFollowDate = new DateTime(2023, 10, 13),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 5,
            Accepted = false,
            CustomerId = cu1.Id
        });

        var q7 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 9, 12),
            Title = " Gary Balck Qutation",
            ContactName = " Gary Balck",
            ContactPhone = "0789262473",
            ContactEmail = "gbalck@mail.com",
            Description = "Supply and install 8 1000 x 2000 6mm Mirror",
            RequiresSurvey = true,
            SurveyId = sur2.Id, //Survey Id needed as- RequiresSurvey = true
            WorkType = "Supply and install",
            DeliveryRequired = false,
            DeliveryAddress = "123 Fountain Street, Strabane, County Tyrone",
            Price = 2500,
            QuoteSentDate = new DateTime(2023, 9, 12),
            QuoteFollowDate = new DateTime(2023, 9, 12),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 3,
            Accepted = false,
            CustomerId = cu9.Id
        });

        var q8 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 9, 5),
            Title = " Gary Balck ",
            ContactName = " Gary Balck",
            ContactPhone = "0789262473",
            ContactEmail = "gbalck@mail.com",
            Description = "Supply and install 3 1000 x 2000 6mm Mirror",
            RequiresSurvey = true,
            SurveyId = sur4.Id, //Survey Id needed as- RequiresSurvey = true
            WorkType = "Supply and install",
            DeliveryRequired = false,
            DeliveryAddress = "123 Fountain Street, Strabane, County Tyrone",
            Price = 2500,
            QuoteSentDate = new DateTime(2023, 10, 8),
            QuoteFollowDate = new DateTime(2023, 10, 16),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 3,
            Accepted = false,
            CustomerId = cu9.Id
        });

        var q9 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 9, 5),
            Title = "Philip Coyle Qutation",
            ContactName = " David",
            ContactPhone = "0799262923",
            ContactEmail = "Philip@mail.com",
            Description = "Supply and install 8 1000 x 2000 6mm Mirror",
            RequiresSurvey = true,
            SurveyId = sur3.Id, //Survey Id needed as- RequiresSurvey = true
            WorkType = "Supply and install",
            DeliveryRequired = false,
            DeliveryAddress = "Big Muscle Gym, 89 Mount, Derry City",
            Price = 2500,
            QuoteSentDate = new DateTime(2023, 10, 5),
            QuoteFollowDate = new DateTime(2023, 10, 13),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 3,
            Accepted = false,
            CustomerId = cu3.Id
        });

        var q10 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 9, 5),
            Title = "Mary Kerr- Hotel 10th floor",
            ContactName = "Mary Kerr",
            ContactPhone = "0799262923",
            ContactEmail = "maryk25@hotmail.com",
            Description = "Supply and install 1nr DGU- 600 X 986- clear safety ",
            RequiresSurvey = true,
            SurveyId = sur5.Id, //Survey Id needed as- RequiresSurvey = true
            WorkType = "Supply and install",
            DeliveryRequired = true,
            DeliveryAddress = "2 Garvagh Rd, Dungiven, Londonderry",
            Price = 2500,
            QuoteSentDate = new DateTime(2023, 10, 5),
            QuoteFollowDate = new DateTime(2023, 10, 13),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 3,
            Accepted = false,
            CustomerId = cu2.Id
        });

        var q11 = qsvc.AddQuotation(new Quotation
        {
            Date = new DateTime(2023, 9, 5),
            Title = "Mary Kerr new glass",
            ContactName = "Mary Kerr",
            ContactPhone = "0799262923",
            ContactEmail = "maryk25@hotmail.com",
            Description = "Supply and install 1nr DGU- 600 X 986- clear safety ",
            RequiresSurvey = false,
            //SurveyId = sur9.Id, //Survey Id needed as- RequiresSurvey = true
            WorkType = "Supply and install",
            DeliveryRequired = false,
            DeliveryAddress = "2 Garvagh Rd, Dungiven, Londonderry",
            Price = 800,
            QuoteSentDate = new DateTime(2023, 10, 5),
            QuoteFollowDate = new DateTime(2023, 10, 13),
            Status = Quotestatus.Pending,
            Response = "Awaiting response",
            Urgency = 3,
            Accepted = false,
            CustomerId = cu2.Id
        });


        // var q12 = qsvc.AddQuotation(new Quotation
        // {
        //     Date = new DateTime(2023, 10, 5),
        //     Title = "Philip Coyle more mirrors",
        //     ContactName = "David",
        //     ContactPhone = "0799262923",
        //     ContactEmail = "Philip@mail.com",
        //     Description = "Supply and install 4 600 x 2000 6mm Mirror",
        //     RequiresSurvey = true,
        //     SurveyId = sur9.Id, //Survey Id needed as- RequiresSurvey = true
        //     WorkType = "Supply and install",
        //     DeliveryRequired = true,
        //     DeliveryAddress = "Big Muscle Gym, 89 Mount Vernon, Derry City",
        //     Price = 1200,
        //     QuoteSentDate = new DateTime(2023, 10, 6),
        //     QuoteFollowDate = new DateTime(2023, 10, 14),
        //     Status = Quotestatus.Pending,
        //     Response = "Purchase confirmed",
        //     Urgency = 4,
        //     Accepted = true,
        //     CustomerId = cu3.Id
        // });

        // var q13 = qsvc.AddQuotation(new Quotation
        // {
        //     Date = new DateTime(2023, 9, 5),
        //     Title = "Philip Coyle more mirrors",
        //     ContactName = "David",
        //     ContactPhone = "0799262923",
        //     ContactEmail = "Philip@mail.com",
        //     Description = "Supply 6mm Toughen table top- clear 1nr 800 X 140- Shaped-tempate needed, for their reception desk- ",
        //     RequiresSurvey = true,
        //     SurveyId = sur10.Id, //Survey Id needed as- RequiresSurvey = true
        //     WorkType = "Supply and install",
        //     DeliveryRequired = false,
        //     DeliveryAddress = "Big Muscle Gym, 89 Mount Vernon, Derry City",
        //     Price = 600,
        //     QuoteSentDate = new DateTime(2023, 10, 5),
        //     QuoteFollowDate = new DateTime(2023, 10, 13),
        //     Status = Quotestatus.Pending,
        //     Response = "Purchase confirmed",
        //     Urgency = 4,
        //     Accepted = true,
        //     CustomerId = cu3.Id
        // });


        // var q14 = qsvc.AddQuotation(new Quotation
        // {
        //     Date = new DateTime(2023, 9, 5),
        //     Title = "Philip Coyle- House Back door",
        //     ContactName = "David",
        //     ContactPhone = "0799262923",
        //     ContactEmail = "Philip@mail.com",
        //     Description = "Back foor DGU- SAFETY obscure 1nr- 400 X600 3nr-DGU- 600 x 400 CLEAR",
        //     RequiresSurvey = true,
        //     SurveyId = sur11.Id, //Survey Id needed as- RequiresSurvey = true
        //     WorkType = "Supply and install",
        //     DeliveryRequired = true,
        //     DeliveryAddress = "123 Armagh Road, Derry City",
        //     Price = 600,
        //     QuoteSentDate = new DateTime(2023, 10, 5),
        //     QuoteFollowDate = new DateTime(2023, 10, 13),
        //     Status = Quotestatus.Pending,
        //     Response = "Purchase confirmed",
        //     Urgency = 4,
        //     Accepted = true,
        //     CustomerId = cu3.Id
        // });



        // //***======================= suppliers seeder ============================================================================//
        // create suppliers
        var sp1 = spsvc.AddSupplier(new Supplier
        {
            DateAdded = new DateTime(2023, 07, 14),
            Name = "Pillkys Glass",
            MainContactName = "joe Gallagher",
            Address = "453 Wigan Way",
            City = "Wigan",
            PostCode = "WN5 0UZ",
            Website = "Pillkys.com",
            Phone = "01952246934",
            Email = "mail@Pillkys.com",
            Notes = "supplier of Pillys made glass- Uk leading supplier."
        });
        var sp2 = spsvc.AddSupplier(new Supplier
        {
            DateAdded = new DateTime(2023, 07, 14),
            Name = "5 star Glass",
            MainContactName = "joe Gallagher",
            Address = "2 Birmingham Way",
            City = "Birmingham",
            PostCode = "BR1 0UZ",
            Website = "5StarGlass.com",
            Phone = "01952246934",
            Email = "mail@5StarGlass.com",
            Notes = "supplier of specialist Fire glass- Uk leading supplier."
        });

        var sp3 = spsvc.AddSupplier(new Supplier
        {
            DateAdded = new DateTime(2020, 06, 11),
            Name = "Keane Glass Distribution",
            MainContactName = "Davide Keane",
            Address = "Unit 5 Slaney Road, Dublin Industrial Estate",
            City = "Dublin",
            PostCode = "BR1 0UZ",
            Website = "KeaneGlass.ie",
            Phone = "0035316382700",
            Email = "orders@KeaneGlass.ie",
            Notes = "supplier of European made glass- Irish leading supplier."
        });


        //***========================Glass seeder ============================================================================//

        // create Glass
        var G1 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "4mm Clear Float",
            Thickness = 4.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 40,
            ImageUrl = "/images/GlassIMG/22.png",
            SupplierId = sp1.Id,
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/18.png" },
                new() { Url = "/images/GlassIMG/24.png" },
                new() { Url = "/images/GlassIMG/17.png" },
                new() { Url = "/images/GlassIMG/22.png" },
            }

        });

        var G2 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "6mm Clear Float",
            Thickness = 4.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 40,
            ImageUrl = "/images/GlassIMG/18.png",
            SupplierId = sp1.Id,
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/17.png" },
                new() { Url = "/images/GlassIMG/22.png" },
                new() { Url = "/images/GlassIMG/24.png" },
                new() { Url = "/images/GlassIMG/17.png" },
            }
        });

        var G3 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "6mm Ultra",
            Thickness = 4.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 40,
            ImageUrl = "/images/GlassIMG/17.png",
            SupplierId = sp1.Id,
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/18.png" },
                new() { Url = "/images/GlassIMG/24.png" },
                new() { Url = "/images/GlassIMG/17.png" },
            }
        });

        var G4 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.FIRE,
            Name = "Pyrodur Half Hour",
            Thickness = 4.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 40,
            ImageUrl = "/images/GlassIMG/10.png",
            SupplierId = sp2.Id,
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/11.png" },
                new() { Url = "/images/GlassIMG/1.png" },
                new() { Url = "/images/GlassIMG/2.png" },
            }
        });

        var G5 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.FIRE,
            Name = "Pyrodur 1Hour",
            Thickness = 4.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 40,
            ImageUrl = "/images/GlassIMG/11.png",
            SupplierId = sp2.Id,
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/10.png" },
                new() { Url = "/images/GlassIMG/2.png" },
                new() { Url = "/images/GlassIMG/17.png" },
            }
        });

        var G6 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.MIRROR,
            Name = "Standard Silver",
            Thickness = 4.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 40,
            ImageUrl = "/images/GlassIMG/9.png",
            SupplierId = sp3.Id,
            Images = new List<GlassImage>
            {

            }
        });

        var G7 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.MIRROR,
            Name = "1 Way Mirror",
            Thickness = 6.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 3600,
            BoxSheetQuantity = 40,
            SupplierId = sp3.Id,
            ImageUrl = "/images/GlassIMG/33.png",
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/9.png" },

            }
        });

        var G8 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.SAFETY,
            Name = "Acoustic Laminated",
            Thickness = 10.8,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 3200,
            BoxSheetQuantity = 40,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/30.png",
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/31.png" },

            }
        });

        var G9 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.SAFETY,
            Name = "Laminated Diffused",
            Thickness = 6.8,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 40,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/31.png",
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/30.png" },

            }
        });

        var G10 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.SAFETY,
            Name = "Laminated Clear",
            Thickness = 6.8,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1850,
            BoxSheetQuantity = 40,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/30.png",
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/31.png" },

            }
        });


        var G11 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.SAFETY,
            Name = "Wired Rough Cast",
            Thickness = 7.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1600,
            BoxSheetQuantity = 40,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/29.png",
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/26.png" },
                new() { Url = "/images/GlassIMG/27.png" },
            }
        });

        var G12 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.SAFETY,
            Name = "Wired Safety",
            Thickness = 6.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 1600,
            BoxSheetQuantity = 40,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/28.png",
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/31.png" },
            }
        });

        var G13 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "4mm Tinted Blue",
            Thickness = 4.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 3300,
            BoxSheetQuantity = 40,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/7.png",
        });

        var G14 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "4mm Tinted Grey",
            Thickness = 4.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 2800,
            BoxSheetQuantity = 40,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/25.png",
            Images = new List<GlassImage>
            {
                new() { Url = "/images/GlassIMG/6.png" },
                new() { Url = "/images/GlassIMG/25.png" },
            }
        });

        var G15 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "4mm Low-E",
            Thickness = 4.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 2300,
            BoxSheetQuantity = 40,
            SupplierId = sp3.Id,
            ImageUrl = "/images/GlassIMG/24.png",
        });

        var G16 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.MIRROR,
            Name = "Antique Silver",
            Thickness = 6.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 2100,
            BoxSheetQuantity = 40,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/23.png",
        });

        var G17 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "6mm Low Iron",
            Thickness = 6.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 2500,
            BoxSheetQuantity = 40,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/22.png",
        });

        var G18 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "8mm Clear",
            Thickness = 8.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 2150,
            BoxSheetQuantity = 45,
            SupplierId = sp3.Id,
            ImageUrl = "/images/GlassIMG/21.png",
        });

        var G19 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "10mm Clear",
            Thickness = 10.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 2150,
            BoxSheetQuantity = 45,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/21.png",
        });

        var G20 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "12mm Clear",
            Thickness = 12.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 2200,
            BoxSheetQuantity = 30,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/21.png",
        });

        var G21 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "15mm Clear",
            Thickness = 15.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 2600,
            BoxSheetQuantity = 30,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/21.png",
        });

        var G22 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "Stove Glass",
            Thickness = 5.0,
            SheetSizeL = 2000,
            SheetSizeH = 1250,
            PricePerSheet = 3600,
            BoxSheetQuantity = 30,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/20.png",

        });

        var G23 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "Horticultural",
            Thickness = 3.0,
            SheetSizeL = 457,
            SheetSizeH = 610,
            PricePerSheet = 1600,
            BoxSheetQuantity = 380,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/19.png",
        });

        var G24 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "Picture Frame",
            Thickness = 2.0,
            SheetSizeL = 2000,
            SheetSizeH = 1250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 300,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/19.png",
        });

        var G25 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.STANDARD,
            Name = "Picture Frame Diffused",
            Thickness = 2.0,
            SheetSizeL = 2000,
            SheetSizeH = 1250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 300,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/19.png",
        });

        var G26 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.TOUGHENED,
            Name = "4mm Toughen Clear",
            Thickness = 4.0,
            SheetSizeL = 1000,
            SheetSizeH = 1000,
            PricePerSheet = 230,
            BoxSheetQuantity = 1,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/18.png",
        });

        var G27 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.TOUGHENED,
            Name = "10mm Toughen Clear",
            Thickness = 10.0,
            SheetSizeL = 1000,
            SheetSizeH = 1000,
            PricePerSheet = 280,
            BoxSheetQuantity = 1,
            SupplierId = sp2.Id,
            ImageUrl = "/images/GlassIMG/17.png",
        });

        var G28 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.TEXTURED,
            Name = "Cotswold",
            Thickness = 2.0,
            SheetSizeL = 2000,
            SheetSizeH = 1250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 300,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/16.png",
        });

        var G29 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.TEXTURED,
            Name = "Flemish",
            Thickness = 4.0,
            SheetSizeL = 2000,
            SheetSizeH = 1250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 300,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/15.png",
        });

        var G30 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.TEXTURED,
            Name = "Taffeta",
            Thickness = 4.0,
            SheetSizeL = 2000,
            SheetSizeH = 1250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 300,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/14.png",
        });

        var G31 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.TEXTURED,
            Name = "Minster",
            Thickness = 4.0,
            SheetSizeL = 2000,
            SheetSizeH = 1250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 300,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/13.png",
        });


        var G32 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.TEXTURED,
            Name = "Acid Etched",
            Thickness = 4.0,
            SheetSizeL = 2000,
            SheetSizeH = 1250,
            PricePerSheet = 1200,
            BoxSheetQuantity = 300,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/12.png",
        });

        var G33 = gsvc.AddGlass(new Glass
        {
            Type = GlassType.MIRROR,
            Name = "Antique Gold",
            Thickness = 6.0,
            SheetSizeL = 3210,
            SheetSizeH = 2250,
            PricePerSheet = 2500,
            BoxSheetQuantity = 40,
            SupplierId = sp1.Id,
            ImageUrl = "/images/GlassIMG/8.png",
        });

    }

}
