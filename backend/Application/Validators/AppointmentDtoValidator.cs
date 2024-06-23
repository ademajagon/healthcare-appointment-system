using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class AppointmentDtoValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentDtoValidator()
        { 
            RuleFor(a => a.AppointmentDate)
                .NotEmpty().WithMessage("Appointment date is required.");

            RuleFor(a => a.AppointmentTime)
                .NotEmpty().WithMessage("Appointment time is required.");

            RuleFor(a => a.Notes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.");
        }
    }
}
