using FluentValidation;
using NZWAlks_2.API.Models.DTO;

namespace NZWAlks_2.API.Validators
{
    public class UpdateWalkRequestDTOValidator : AbstractValidator<UpdateWalkRequestDTO>
    {
        public UpdateWalkRequestDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
