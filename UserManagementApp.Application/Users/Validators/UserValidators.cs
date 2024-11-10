using FluentValidation;
using UserManagementApp.Application.Users.Dtos;

namespace UserManagementApp.Application.Users.Validators;

public class UserValidator : AbstractValidator<CreateUser>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre no puede ser demasiado corto.")
            .MaximumLength(50).WithMessage("El nombre no puede ser demasiado largo.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("El rol especificado no es válido.");
    }
}

