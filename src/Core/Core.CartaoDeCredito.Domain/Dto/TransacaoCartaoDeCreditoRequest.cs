namespace Core.CartaoDeCredito.Domain.Dto
{
    public class TransacaoCartaoDeCreditoRequest
    {
        public CartaoDeCreditoRequest CartaoDeCredito { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class TransacaoCartaoDeCreditoResponse
    {
        public bool TransacaoRealizadaComSucesso { get; set; }
    }

    public class CartaoDeCreditoRequest
    {
        public string NomeNoCartao { get; set; }
        public string NumeroCartaoVirtual { get; set; }
        public string Cvv { get; set; }
        public string DataDeValidade { get; set; }
        public string Cpf { get; set; }
    }


    public static class TransacaoCartaoDeCreditoResponseMappingExtension
    {
        public static TransacaoCartaoDeCredito ToDomain(this TransacaoCartaoDeCreditoRequest solicitacaoCartaoDeCredito)
        {
            return new TransacaoCartaoDeCredito()
            {
                CartaoDeCredito = new CartaoDeCredito()
                {
                    Cpf = solicitacaoCartaoDeCredito.CartaoDeCredito.Cpf,
                    Cvv = solicitacaoCartaoDeCredito.CartaoDeCredito.Cvv,
                    DataDeValidade = solicitacaoCartaoDeCredito.CartaoDeCredito.DataDeValidade,
                    NomeNoCartao = solicitacaoCartaoDeCredito.CartaoDeCredito.NomeNoCartao,
                    NumeroCartaoVirtual = solicitacaoCartaoDeCredito.CartaoDeCredito.NumeroCartaoVirtual
                },
                ValorTotal = solicitacaoCartaoDeCredito.ValorTotal
            };
        }

        public static TransacaoCartaoDeCreditoResponse ToResponse(this TransacaoCartaoDeCredito solicitacaoCartaoDeCredito)
        {
            return new TransacaoCartaoDeCreditoResponse()
            {
                TransacaoRealizadaComSucesso = solicitacaoCartaoDeCredito.TransacaoRealizadaComSucesso
            };
        }
    }
}
