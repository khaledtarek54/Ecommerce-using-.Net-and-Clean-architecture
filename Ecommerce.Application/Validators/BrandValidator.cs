using Ecommerce.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Validators
{
    public class BrandValidator : AbstractValidator<BrandDto>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(b => b.Description)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(b => b.LogoUrl)
                .NotEmpty()
                .MaximumLength(200);

        }
    }
}
