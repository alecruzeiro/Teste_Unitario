using FluentValidation;
using FluentValidation.Results;

namespace Core.CartaoDeCredito.Domain
{
    public class TransacaoCartaoDeCredito
    {
        public CartaoDeCredito CartaoDeCredito { get; }
        public decimal ValorTotal { get; }
        public bool TransacaoRealizadaComSucesso { get; private set; }
        public ValidationResult ValidationResult { get; private set; }

        public TransacaoCartaoDeCredito(CartaoDeCredito cartaoDeCredito, decimal valorTotal)
        {
            CartaoDeCredito = cartaoDeCredito;
            ValorTotal = valorTotal;
        }

        public ValidationResult Validate(IValidator<TransacaoCartaoDeCredito> transacaoCartaoDeCreditoValidator)
        {
            ValidationResult = transacaoCartaoDeCreditoValidator.Validate(this);

            return ValidationResult;
        }
    }

    public class TransacaoCartaoDeCreditoValidator : AbstractValidator<TransacaoCartaoDeCredito>
    {
        public TransacaoCartaoDeCreditoValidator()
        {
            RuleFor(t => t.ValorTotal)
                .GreaterThan(0);

            RuleFor(c => c.CartaoDeCredito)
                .SetValidator(new CartaoDeCreditoValidator());
        }
    }
}
