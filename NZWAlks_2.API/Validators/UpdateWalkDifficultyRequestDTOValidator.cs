using FluentValidation;
using NZWAlks_2.API.Models.DTO;

namespace NZWAlks_2.API.Validators
{
    public class AddWalkDifficultyDTOValidator26:AbstractValidator<UpdateWalkDifficultyRequestDTO>
    {
        public AddWalkDifficultyDTOValidator26()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
