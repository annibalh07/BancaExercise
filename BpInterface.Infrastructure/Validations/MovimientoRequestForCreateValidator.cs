using BpInterface.Core;
using BpInterface.Core.Models.Dto;
using FluentValidation;

namespace BpInterface.Infrastructure.Validations
{
    public class MovimientoRequestForCreateValidator : AbstractValidator<MovimientoRequestForCreate>
    {
        public MovimientoRequestForCreateValidator()
        {
            var requiredMessage = "El campo {PropertyName} es requerido.";
            RuleFor(t => t.NumeroCuenta).NotEmpty().WithMessage(requiredMessage);
            RuleFor(t => t.Fecha).NotEmpty().WithMessage(requiredMessage).GreaterThan(DateTime.MinValue);
            RuleFor(t => t.TipoMovimiento).NotEmpty().WithMessage(requiredMessage).Must(t=>t.Equals(TipoMovimiento.Retiro) || t.Equals(TipoMovimiento.Deposito));
            RuleFor(t => t.Valor).NotEmpty().WithMessage(requiredMessage).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor que 0");
        }
    }
}
