using Application.Command.CreateMedication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Validators
{
    public class CreateMedicationCommandValidator : AbstractValidator<CreateMedicationCommand>
    {
        public CreateMedicationCommandValidator()
        {
            RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Matches("^[a-zA-Z0-9-_]+$").WithMessage("Name can only contain letters, numbers, '-', and '_'.");

            RuleFor(m => m.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");

            RuleFor(m => m.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Matches("^[A-Z0-9_]+$").WithMessage("Code can only contain upper case letters, numbers, and '_'.");

        }
    }
}
