using FluentValidation;
using Service.Command;

namespace Service.Validators
{
    public class LoginValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginValidator() 
        {
            RuleFor(m => m.UserName)
                .NotEmpty()
                .MaximumLength(200)
                .MinimumLength(3)
                .WithMessage("Username length must between 3 to 200 !!! ");
            RuleFor(m => m.Password)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200)
                .WithMessage("Password length must between 3 and 200 !!! ");
            // test add account
        }
    }
}
