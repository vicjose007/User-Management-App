using FluentValidation;
using UserManagementApp.Application.Phones.Dtos;

namespace UserManagementApp.Application.Phones.Validators;

public class PhoneValidator : AbstractValidator<CreatePhone>
{
    public PhoneValidator()
    {

        RuleFor(phone => phone.Number)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
            .Matches(@"^\d+$").WithMessage("El número de teléfono solo debe contener dígitos.")
            .Length(7, 15).WithMessage("El número de teléfono debe tener entre 7 y 15 dígitos.");

        RuleFor(phone => phone.CityCode)
            .NotEmpty().WithMessage("El código de ciudad es obligatorio.")
            .Matches(@"^\d+$").WithMessage("El código de ciudad solo debe contener dígitos.")
            .Length(1, 5).WithMessage("El código de ciudad debe tener entre 1 y 5 dígitos.");

        RuleFor(phone => phone.ContryCode)
            .NotEmpty().WithMessage("El código de país es obligatorio.")
            .Matches(@"^\d+$").WithMessage("El código de país solo debe contener dígitos.")
            .Length(1, 5).WithMessage("El código de país debe tener entre 1 y 5 dígitos.");
    }
}

public class UserPhoneValidator : AbstractValidator<CreateUserPhone>
{
    public UserPhoneValidator()
    {
        RuleFor(phone => phone.UserId)
            .NotEmpty().WithMessage("Es obligatorio especificar al usuario");

        RuleFor(phone => phone.Number)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
            .Matches(@"^\d+$").WithMessage("El número de teléfono solo debe contener dígitos.")
            .Length(7, 15).WithMessage("El número de teléfono debe tener entre 7 y 15 dígitos.");

        RuleFor(phone => phone.CityCode)
            .NotEmpty().WithMessage("El código de ciudad es obligatorio.")
            .Matches(@"^\d+$").WithMessage("El código de ciudad solo debe contener dígitos.")
            .Length(1, 5).WithMessage("El código de ciudad debe tener entre 1 y 5 dígitos.");

        RuleFor(phone => phone.ContryCode)
            .NotEmpty().WithMessage("El código de país es obligatorio.")
            .Matches(@"^\d+$").WithMessage("El código de país solo debe contener dígitos.")
            .Length(1, 5).WithMessage("El código de país debe tener entre 1 y 5 dígitos.");
    }
}

public class PhoneUpdateValidator : AbstractValidator<UpdatePhone>
{
    public PhoneUpdateValidator()
    {

        RuleFor(phone => phone.Number)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
            .Matches(@"^\d+$").WithMessage("El número de teléfono solo debe contener dígitos.")
            .Length(7, 15).WithMessage("El número de teléfono debe tener entre 7 y 15 dígitos.");

        RuleFor(phone => phone.CityCode)
            .NotEmpty().WithMessage("El código de ciudad es obligatorio.")
            .Matches(@"^\d+$").WithMessage("El código de ciudad solo debe contener dígitos.")
            .Length(1, 5).WithMessage("El código de ciudad debe tener entre 1 y 5 dígitos.");

        RuleFor(phone => phone.ContryCode)
            .NotEmpty().WithMessage("El código de país es obligatorio.")
            .Matches(@"^\d+$").WithMessage("El código de país solo debe contener dígitos.")
            .Length(1, 5).WithMessage("El código de país debe tener entre 1 y 5 dígitos.");
    }
}

