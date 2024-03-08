using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Web.Models.User;
public class ForgotPasswordViewModel
{
    [Required]
    public string Email { get; set; }
    
}
