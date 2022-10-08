using BpInterface.Core;
using BpInterface.Core.Models.Dto;
using FluentValidation;

namespace BpInterface.Infrastructure.Validations
{
    public class ClienteRequestForUpdateValidator : AbstractValidator<ClienteRequestForUpdate>
    {
        public ClienteRequestForUpdateValidator()
        {
            var requiredMessage = "El campo {PropertyName} es requerido.";
            RuleFor(p => p.Nombres).NotEmpty().WithMessage(requiredMessage);
            RuleFor(p => p.Identificacion).NotEmpty().WithMessage(requiredMessage);
            RuleFor(p => p.Genero).NotEmpty().WithMessage(requiredMessage).Must(t=>t.Equals(GeneroPersona.Masculino) || t.Equals(GeneroPersona.Femenino));
            RuleFor(p => p.Edad).NotEmpty().WithMessage(requiredMessage).GreaterThan(17).WithMessage("Cliente debe ser mayor de edad");
        }
    }
}
