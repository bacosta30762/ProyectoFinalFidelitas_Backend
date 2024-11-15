using Aplicacion.Usuarios.Dtos;
using FluentValidation;

namespace Aplicacion.Usuarios.Validadores
{
    public class RestablecerPasswordValidador : AbstractValidator<RestablecerPasswordDto>
    {
        public RestablecerPasswordValidador()
        {
            RuleFor(v => v.Correo)
                .EmailAddress().WithMessage("Debe de ser un correo válido");
            RuleFor(v => v.Token)
                .NotEmpty();
            RuleFor(x => x.NuevaPassword)
                .NotEmpty().MinimumLength(6).Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula")
                                                     .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula")
                                                     .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número")
                                                     .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial");
        }
    }
}
