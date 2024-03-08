using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using APG_CRM.Data.Repositories;
using APG_CRM.Data.Entities;
using APG_CRM.Web.Models;
using APG_CRM.Data.Services;
using System.Globalization;
using APG_CRM.Web.Views.Shared.Components;
using System.Linq;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using APG_CRM.Data.Security;


namespace APG_CRM.Web.Controllers;

public class SurveyController : BaseController
{
    private ICustomerService _svc;
    private IQuotationService _svcQ;
    private ISurveyService _ssvc;

    private readonly DatabaseContext dbContext;

    private readonly ILogger<SurveyController> _logger;

    public SurveyController(DatabaseContext dbContext, ILogger<SurveyController> logger, ICustomerService CustomerService, IQuotationService QuotationService, ISurveyService _ssvc)
    {
        this.dbContext = dbContext;
        _logger = logger;
        this._svc = CustomerService;
        this._svcQ = QuotationService;
        this._ssvc = _ssvc;// Initialize svcS
    }

    //===================Page the Data in the Db Ffor the view =====================

    // HTTP GET - Display Paged List of surveys
    [Authorize]
    public ActionResult Index(int page = 1, int size = 20, string order = "id", string direction = "asc")
    {
        var paged = _ssvc.GetSurveys(page, size, order, direction);
        return View(paged);
    }



    // GET: survey/Create
    [Authorize(Roles = "admin, manager, survey")] //this means only admin, manager and survey team, users can create, not customer. 
    public IActionResult Create()
    {
        return View();  // display the blank form used to create a survey object to Db.
    }

    // POST: survey/Create
    [HttpPost]
    [Authorize(Roles = "admin, manager, survey")]
    [ValidateAntiForgeryToken]
    //public IActionResult Create(Survey survey)
    public IActionResult Create([Bind("RequestDate, ScheduleDate, PreSurveyNotes, RequiresLadders, RequiresScissorStairs, RequiresStreetBullards, NumStaffRequired, Street, City, County, PostCode, SurveyPhone, Status, Reviewed, CompletedByWho, Description, RiskAssessment, PreviousSurveyId, NextSurveyId, CustomerId, QuotationId ")] Survey sur)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _ssvc.CreateSurvey(sur);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating Survey.");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
            }
        }

        // If we reach this point, there was a problem, and we should redisplay the form.
        return View(sur);
    }

    //     // POST: survey/Create
    //     [HttpPost]
    // [Authorize(Roles = "admin, manager, survey")]
    // [ValidateAntiForgeryToken]
    // public IActionResult Create([Bind("RequestDate, ScheduleDate, PreSurveyNotes, RequiresLadders, RequiresScissorStairs, RequiresStreetBullards, NumStaffRequired, Street, City, County, PostCode, SurveyPhone, Status, Reviewed, CompletedByWho, Description, RiskAssessment, PreviousSurveyId, NextSurveyId, CustomerId, QuotationId ")] Survey sur)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         try
    //         {
    //             var createdSurvey = _ssvc.CreateSurvey(sur);

    //             if (createdSurvey == null)
    //             {
    //                 // Handle the case where a duplicate survey was not created
    //                 ModelState.AddModelError("", "A survey with the same properties already exists.");
    //                 return View(sur);
    //             }

    //             // Redirect to the Index action if the survey was successfully created
    //             return RedirectToAction(nameof(Index));
    //         }
    //         catch (InvalidOperationException ex)
    //         {
    //             ModelState.AddModelError("", ex.Message);
    //         }
    //         catch (Exception ex)
    //         {
    //             _logger.LogError(ex, "An unexpected error occurred while creating Survey.");
    //             ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
    //         }
    //     }

    //     // If we reach this point, there was a problem, and we should redisplay the form.
    //     return View(sur);
    // }

    public IActionResult Details(int id)
    {
        var survey = _ssvc.GetSurveyById(id);
        if (survey == null)
        {
            Alert("survey not found", AlertType.warning);
            return RedirectToAction(nameof(Index));
        }

        // Get Previous and Next survey IDs
        survey.PreviousSurveyId = dbContext.Surveys
            .Where(sur => sur.Id < id)
            .OrderByDescending(sur => sur.Id)
            .Select(sur => (int?)sur.Id)
            .FirstOrDefault();

        survey.NextSurveyId = dbContext.Surveys
             .Where(sur => sur.Id > id)
             .OrderBy(sur => sur.Id)
             .Select(sur => (int?)sur.Id)
             .FirstOrDefault();

        return View(survey);
    }


    // GET: survey/Edit/
    public IActionResult Edit(int id)
    {
        var sur = _ssvc.GetSurveyById(id);
        if (sur == null)
        {
            return NotFound();
        }
        return View(sur);
    }

    // POST: survey/Edit/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind(" Id, RequestDate, ScheduleDate, PreSurveyNotes, RequiresLadders, RequiresScissorStairs, RequiresStreetBullards, NumStaffRequired, Street, City, County, PostCode, SurveyPhone, Status, Reviewed, CompletedByWho, Description, RiskAssessment, PreviousSurveyId, NextSurveyId, CustomerId, QuotationId ")] Survey sur)
    {
        if (id != sur.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            _ssvc.UpdateSurvey(sur);
            return RedirectToAction(nameof(Index));
        }
        return View(sur);
    }

    // GET: survey/Delete/
    public IActionResult Delete(int id)
    {
        var sur = _ssvc.GetSurveyById(id);
        if (sur == null)
        {
            return NotFound();
        }
        return View(sur);
    }

    // POST: survey/Delete/
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _ssvc.DeleteSurvey(id);

        TempData["isDeleted"] = true; // Set this to indicate a successful deletion to the next GET request.

        return RedirectToAction(nameof(Index));
    }

    public IActionResult SurveySearch()
    {
        var viewModel = new SurveySearchViewModel();
        return PartialView("_SurveySearch", viewModel);
    }


    // public IActionResult Index(int page = 1, int size = 10, string orderBy = "RequestDate", string direction = "asc")
    // {
    //     var surveysList = _ssvc.GetAllSurveys();
    //     var surveysQuery = surveysList.AsQueryable();

    //     // ... sorting logic ...

    //     var pagedSurveysEntity = surveysQuery.ToPaged(page, size, orderBy, direction);

    //     var pagedSurveysViewModel = new PagedSurveyViewModel
    //     {
    //         Data = pagedSurveysEntity.Data.Select(MapToViewModel).ToList(),
    //         TotalRows = pagedSurveysEntity.TotalRows,
    //         CurrentPage = pagedSurveysEntity.CurrentPage,
    //         PageSize = pagedSurveysEntity.PageSize,
    //         OrderBy = pagedSurveysEntity.OrderBy,
    //         Direction = pagedSurveysEntity.Direction
    //     };
    //     return View(pagedSurveysViewModel);
    // }

    // ///Mapping Logic: a method  to map a Survey entity to a SurveyViewModel. this is for Pagination of survey data. 
    // private SurveyViewModel MapToViewModel(Survey survey)
    // {
    //     return new SurveyViewModel
    //     {
    //         Id = survey.Id,
    //         RequestDate = survey.RequestDate,
    //         ScheduleDate = survey.ScheduleDate,
    //         Prerequisites = survey.Prerequisites,
    //         Street = survey.Street,
    //         City = survey.City,
    //         County = survey.County,
    //         PostCode = survey.PostCode,
    //         SurveyPhone = survey.SurveyPhone,
    //         Completed = survey.Completed,
    //         CompletedByWho = survey.CompletedByWho,
    //         Description = survey.Description,
    //         RiskAssessment = survey.RiskAssessment,
    //     };
    // }

    //=========  SurveyScanViewModel =======
    //=========  CRUD =======
}