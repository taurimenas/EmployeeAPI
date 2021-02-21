using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Contracts.V1.Requests;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace EmployeeAPI.Validators
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .Must((x, y) => y != x.LastName);
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.BirthDate)
                .NotNull()
                .NotEmpty()
                .GreaterThan(DateTime.Now.AddYears(-Constants.MaxLegalWorkingAge))
                .LessThan(DateTime.Now.AddYears(-Constants.MinLegalWorkingAge));
            RuleFor(x => x.EmploymentDate)
                .NotNull()
                .NotEmpty()
                .GreaterThan(Constants.MinEmploymentDate)
                .LessThan(DateTime.Now);
            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Salary)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Role)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.BossId)
                .Must((x, y) =>
                {
                    if (x.Role != "CEO")
                    {
                        return y > 0;
                    }
                    return true;
                });
        }
    }
}
