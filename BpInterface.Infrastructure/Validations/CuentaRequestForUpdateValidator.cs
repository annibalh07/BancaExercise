using BpInterface.Core;
using BpInterface.Core.Models.Dto;
using FluentValidation;

namespace BpInterface.Infrastructure.Validations
{
    public class CuentaRequestForUpdateValidator : AbstractValidator<CuentaRequestForUpdate>
    {
        public CuentaRequestForUpdateValidator()
        {
            var requiredMessage = "El campo {PropertyName} es requerido.";
            RuleFor(t => t.SaldoInicial).NotEmpty().WithMessage(requiredMessage);
            RuleFor(t => t.TipoCuenta).NotEmpty().WithMessage(requiredMessage).Must(x => x.Equals(TipoCuentas.Ahorros) || x.Equals(TipoCuentas.Corriente));
            RuleFor(t => t.Estado).NotEmpty().WithMessage(requiredMessage);
            RuleFor(t => t.NumeroCuenta).NotEmpty().WithMessage(requiredMessage);
            RuleFor(t => t.LimiteDiario).NotEmpty().WithMessage(requiredMessage).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor que 0");
        }
    }
}
