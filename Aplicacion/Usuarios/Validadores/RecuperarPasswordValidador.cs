using Aplicacion.Usuarios.Dtos;
using FluentValidation;

namespace Aplicacion.Usuarios.Validadores
{
    public class RecuperarPasswordValidador : AbstractValidator<RecuperarPasswordDto>
    {
        public RecuperarPasswordValidador()
        {

            RuleFor(v => v.Correo)
                .EmailAddress().WithMessage("Debe de ser un correo válido");
        }
    }
}

