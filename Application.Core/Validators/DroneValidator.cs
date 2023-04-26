using Application.Command.RegisterDrone;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Validators
{
    public class DroneValidator : AbstractValidator<RegisterDroneCommand>
    {
        public DroneValidator()
        {
            RuleFor(d => d.SerialNumber)
                .NotEmpty().WithMessage("Serial number is required.")
                .MaximumLength(100).WithMessage("Serial number cannot be longer than 100 characters.");

            RuleFor(d => d.Model)
                .NotEmpty().WithMessage("Model is required.")
                .Must(m => Enum.IsDefined(typeof(DroneModel), m)).WithMessage("Invalid model.");

            RuleFor(d => d.WeightLimit)
                .GreaterThan(0).WithMessage("Weight limit must be greater than 0.")
                .LessThanOrEqualTo(500).WithMessage("Weight limit cannot be greater than 500 grams.");

            RuleFor(d => d.BatteryCapacity)
                .InclusiveBetween(0, 100).WithMessage("Battery capacity must be between 0 and 100.");

            RuleFor(d => d.State)
                .Must(m => Enum.IsDefined(typeof(DroneState), m)).WithMessage("Invalid state.");
        }
    }
}
