using FluentValidation;
using NZWAlks_2.API.Models.DTO;

namespace NZWAlks_2.API.Validators
{
    public class AddWalkDifficultyDTOValidator:AbstractValidator<AddWalkDifficultyDTO>
    {
        public AddWalkDifficultyDTOValidator()
        {
                RuleFor(x=>x.Code).NotEmpty();
        }
    }
}
