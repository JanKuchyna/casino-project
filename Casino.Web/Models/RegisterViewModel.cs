using System.ComponentModel.DataAnnotations;

namespace Casino.Web.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Zadejte jméno.")]
    [StringLength(100, ErrorMessage = "Jméno může mít nejvýše 100 znaků.")]
    [Display(Name = "Jméno")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Zadejte příjmení.")]
    [StringLength(100, ErrorMessage = "Příjmení může mít nejvýše 100 znaků.")]
    [Display(Name = "Příjmení")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Zadejte e-mail.")]
    [EmailAddress(ErrorMessage = "Neplatný formát e-mailu.")]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Zadejte datum narození.")]
    [DataType(DataType.Date)]
    [Display(Name = "Datum narození")]
    public DateTime? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Zadejte heslo.")]
    [DataType(DataType.Password)]
    [Display(Name = "Heslo")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Zopakujte heslo.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Hesla se neshodují.")]
    [Display(Name = "Heslo znovu")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
