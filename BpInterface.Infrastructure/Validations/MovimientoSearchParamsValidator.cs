using BpInterface.Core.Models.Dto;
using FluentValidation;

namespace BpInterface.Infrastructure.Validations
{
    public class MovimientoSearchParamsValidator : AbstractValidator<MovimientoSearchParams>
    {
        public MovimientoSearchParamsValidator()
        {
            var requiredMessage = "El campo {PropertyName} es requerido.";
            RuleFor(p => p.FechaInicio).NotEmpty().WithMessage(requiredMessage).GreaterThan(DateTime.MinValue);
            RuleFor(p => p.FechaFin).NotEmpty().WithMessage(requiredMessage).GreaterThan(DateTime.MinValue);
        }
    }
}
