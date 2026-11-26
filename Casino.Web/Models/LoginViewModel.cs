using System.ComponentModel.DataAnnotations;

namespace Casino.Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Zadejte e-mail.")]
    [EmailAddress(ErrorMessage = "Neplatný formát e-mailu.")]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Zadejte heslo.")]
    [DataType(DataType.Password)]
    [Display(Name = "Heslo")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Zapamatovat si mě")]
    public bool RememberMe { get; set; }
}
