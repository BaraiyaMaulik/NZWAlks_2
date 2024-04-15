using FluentValidation;
using FluentValidation.AspNetCore;
using NZWAlks_2.API.Models.DTO;

namespace NZWAlks_2.API.Validators
{
    public class LoginRequestDTOValidator:AbstractValidator<LoginRequestDTO>
    {
        public LoginRequestDTOValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
