using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class DoctorDtoValidator : AbstractValidator<DoctorDto>
    {
        public DoctorDtoValidator()
        {
            RuleFor(d => d.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(d => d.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(d => d.Specialization)
                .NotEmpty().WithMessage("Specialization is required.")
                .MaximumLength(100).WithMessage("Specialization must not exceed 100 characters.");

            RuleFor(d => d.Biography)
                .MaximumLength(500).WithMessage("Biography must not exceed 500 characters.");

            RuleFor(d => d.ImageUrl)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("ImageUrl must be a valid URL.")
                .When(d => !string.IsNullOrEmpty(d.ImageUrl));
        }
    }
}
