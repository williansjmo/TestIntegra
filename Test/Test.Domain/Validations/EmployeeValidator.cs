using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Test.Domain.Entities;

namespace Test.Domain.Validations
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(r=> r.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Debe de introducir el nombre del empleado.");
            RuleFor(r => r.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Debe de introducir el apellido del empleado.");
            RuleFor(r => r.Phone)
                .NotEmpty()
                .NotNull()
                .WithMessage("Debe de introducir el número de teléfono del empleado.");
            RuleFor(r => r.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Debe de introducir el correo del empleado.")
                .EmailAddress()
                .WithMessage("Se requiere un coreo válido");
            //RuleFor(r => r.Photo)
            //    .NotEmpty()
            //    .WithMessage("Debe seleccionar una foto del empleado.");
            RuleFor(r => r.HiringDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Debe seleccionar la fecha de contratació del empleado.");
        }
    }
}
