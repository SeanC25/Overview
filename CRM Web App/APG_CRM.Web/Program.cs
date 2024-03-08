using APG_CRM.Web;
using APG_CRM.Data.Services;
using APG_CRM.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Authentication / Authorisation via extension methods 
builder.Services.AddCookieAuthentication();
//builder.Services.AddPolicyAuthorisation();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    // Configure connection string for selected database in appsettings.json
    //options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));

     // Configure connection string for selected database in appsettings.json
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"),
        b => b.MigrationsAssembly("APG_CRM.Data")); // Specify the assembly name containing the DbContext

});

// Add UserService to DI   
builder.Services.AddTransient<IUserService, UserServiceDb>();
builder.Services.AddTransient<IMailService, SmtpMailService>();

// Register services with DI
builder.Services.AddTransient<ICustomerService, CustomerServiceDb>();
builder.Services.AddTransient<IQuotationService, QuotationServiceDb>();
builder.Services.AddTransient<ISurveyService, SurveyServiceDb>();

builder.Services.AddTransient<IGlassService, GlassServiceDb>();
builder.Services.AddTransient<ISupplierService, SupplierServiceDb>();
// builder.Services.AddTransient<IStockService, StockServiceDb>();

// ** Required to enable asp-authorize Taghelper **            
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // seed in development mode - using service provider to get services from DI
    using var scope = app.Services.CreateScope();
    var userService = scope.ServiceProvider.GetService<IUserService>();
    var customerService = scope.ServiceProvider.GetService<ICustomerService>();
    var quotationService = scope.ServiceProvider.GetService<IQuotationService>();
    var surveyService = scope.ServiceProvider.GetService<ISurveyService>();
    var supplierService = scope.ServiceProvider.GetService<ISupplierService>();
    var glassservice = scope.ServiceProvider.GetService<IGlassService>();


    // in development mode seed the database each time the application starts
    Seeder.Seed(userService);
    Seeder.SeedData(customerService, quotationService, surveyService, supplierService, glassservice);
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ** configure cors to allow full cross origin access to any webapi end points **
//app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// ** turn on authentication/authorisation **
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();