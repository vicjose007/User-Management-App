using FluentValidation;
using UserManagementApp.Application.Phones.Validators;
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
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
            .MaximumLength(20).WithMessage("La contraseña no debe superar los 20 caracteres.") 
            .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.") 
            .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número.") 
            .Matches(@"[\W_]").WithMessage("La contraseña debe contener al menos un carácter especial (como @, #, $, etc.).") 
            .Matches(@"^[^\s]+$").WithMessage("La contraseña no debe contener espacios.") 
            .NotEqual(x => x.Name).WithMessage("La contraseña no puede ser igual al nombre de usuario.") 
            .NotEqual(x => x.Email).WithMessage("La contraseña no puede ser igual al correo electrónico.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("El rol especificado no es válido.");

        RuleFor(x => x.Phones)
            .NotEmpty().WithMessage("La lista de teléfonos no puede estar vacía.")
            .Must(phones => phones != null && phones.Any()).WithMessage("Debe haber al menos un teléfono registrado.")
            .ForEach(phone => phone.SetValidator(new PhoneValidator()));
    }
}

