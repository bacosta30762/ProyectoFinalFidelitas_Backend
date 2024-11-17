using Aplicacion.Usuarios.Dtos;
using FluentValidation;

namespace Aplicacion.Usuarios.Validadores
{
    public class ActualizarUsuarioValidador : AbstractValidator<ActualizarUsuarioDto>
    {
        public ActualizarUsuarioValidador()
        {
            RuleFor(x => x.Cedula)
                .NotEmpty().WithMessage("La cédula es obligatoria.");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Apellidos)
                .NotEmpty().WithMessage("Los apellidos son obligatorios.");
            RuleFor(x => x.Correo)
                .EmailAddress().WithMessage("Debe ser un correo electrónico válido.");
        }
    }
}
