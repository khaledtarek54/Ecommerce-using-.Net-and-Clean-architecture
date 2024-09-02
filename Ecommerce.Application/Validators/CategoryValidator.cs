using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(c =>  c.Name)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(c => c.Description)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(c => c.CategoryId)
            .InclusiveBetween(0, int.MaxValue);

        }
    }
}
