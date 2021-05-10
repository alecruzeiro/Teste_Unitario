using FluentValidation;
using FluentValidation.Results;

namespace Core.CartaoDeCredito.Domain
{
    public class TransacaoCartaoDeCredito
    {
        public CartaoDeCredito CartaoDeCredito { get; set; }
        public decimal ValorTotal { get; set; }
        public bool TransacaoRealizadaComSucesso { get; set; }
        public ValidationResult ValidationResult { get; set; }

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
            throw new System.Exception();
        }
    }
}
