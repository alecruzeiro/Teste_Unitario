using FluentValidation;

namespace Core.CartaoDeCredito.Domain
{
    public class CartaoDeCredito
    {
        public string NomeNoCartao { get; set; }
        public string NumeroCartaoVirtual { get; set; }
        public string Cvv { get; set; }
        public string DataDeValidade { get; set; }
        public string Cpf { get; set; }
    }

    public class CartaoDeCreditoValidator : AbstractValidator<CartaoDeCredito>
    {
        public CartaoDeCreditoValidator()
        {
            
        }
    }
}
