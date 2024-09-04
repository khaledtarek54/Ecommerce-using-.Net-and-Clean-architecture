using Ecommerce.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator() { 
            RuleFor(p =>  p.Name)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(p => p.Description)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(p => p.SKU)
                .NotEmpty().WithMessage("SKU is required.")
                .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters.");

            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");

            RuleFor(p => p.BrandId)
                .NotEmpty().WithMessage("BrandId is required.");
        }
    }
}
