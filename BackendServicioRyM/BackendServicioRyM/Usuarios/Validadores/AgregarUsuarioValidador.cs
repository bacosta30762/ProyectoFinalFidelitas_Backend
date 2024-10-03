using Aplicacion.Usuarios.Dtos;
using FluentValidation;

namespace Aplicacion.Usuarios.Validadores
{
    public class AgregarUsuarioValidador : AbstractValidator<AgregarUsuarioDto>
    {
        public AgregarUsuarioValidador()
        {
            RuleFor(v => v.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(35).WithMessage("El nombre no puede ser mayor a 35 carácteres");
            RuleFor(v => v.Correo)
                .EmailAddress().WithMessage("Debe de ser un correo válido");
            RuleFor(v => v.Cedula)
                .NotEmpty().WithMessage("La cédula es requerida").MaximumLength(10);
        }
    }
}
