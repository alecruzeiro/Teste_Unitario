using FluentValidation;

namespace Core.CartaoDeCredito.Domain
{
    public class CartaoDeCredito
    {
        public string NomeNoCartao { get; }
        public string NumeroCartaoVirtual { get; }
        public string Cvv { get; }
        public string DataDeValidade { get; }
        public string Cpf { get; }

        public CartaoDeCredito(string cpf, string cvv, string dataDeValidade, string nomeNoCartao, string numeroCartaoVirtual)
        {
            NomeNoCartao = nomeNoCartao;
            NumeroCartaoVirtual = numeroCartaoVirtual;
            Cvv = cvv;
            DataDeValidade = dataDeValidade;
            Cpf = cpf;
        }
    }

    public class CartaoDeCreditoValidator : AbstractValidator<CartaoDeCredito>
    {
        public CartaoDeCreditoValidator()
        {
            RuleFor(c => c.NomeNoCartao)
                .NotEmpty();

            RuleFor(c => c.NumeroCartaoVirtual)
                .NotEmpty();

            RuleFor(c => c.Cvv)
                .NotEmpty();

            RuleFor(c => c.DataDeValidade)
                .NotEmpty();

            RuleFor(c => c.Cpf)
                .NotEmpty();
        }
    }
}
