using APG_CRM.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APG_CRM.Web.Models
{
    public class PagedSurveyViewModel : Paged<SurveyViewModel>
    {
        // inherit properties from SurveyViewModel- PagedSurveyViewModel to be a paginated list of SurveyViewModel- from Paged<T> in the SurveynController
        
    }
}