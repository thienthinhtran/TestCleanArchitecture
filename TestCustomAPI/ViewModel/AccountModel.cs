using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace TestCustomAPI.ViewModel
{
    public class AccountModel
    {
        [Required]
        public string Username {  get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginValidator : AbstractValidator<AccountModel>
    {
        public LoginValidator() 
        {
            RuleFor(m => m.Username)
                .NotEmpty()
                .MaximumLength(200)
                .MinimumLength(3)
                .WithMessage("Username length must between 5 to 200 !!! ");
            RuleFor(m => m.Password)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Maximum password length is 200 !!! ");
            // test add account
        }
    }
}
