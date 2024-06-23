using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class BookAppointmentDtoValidator : AbstractValidator<BookAppointmentDto>
    {
        public BookAppointmentDtoValidator()
        {

            RuleFor(b => b.AppointmentDate)
                .NotEmpty().WithMessage("Appointment date is required.");

            RuleFor(b => b.AppointmentTime)
                .NotEmpty().WithMessage("Appointment time is required.");

            RuleFor(b => b.Notes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.");
        }
    }
}
