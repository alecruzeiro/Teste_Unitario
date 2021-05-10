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
            return new TransacaoCartaoDeCredito(new CartaoDeCredito(solicitacaoCartaoDeCredito.CartaoDeCredito.Cpf,
                solicitacaoCartaoDeCredito.CartaoDeCredito.Cvv,
                solicitacaoCartaoDeCredito.CartaoDeCredito.DataDeValidade,
                solicitacaoCartaoDeCredito.CartaoDeCredito.NomeNoCartao,
                solicitacaoCartaoDeCredito.CartaoDeCredito.NumeroCartaoVirtual) ,solicitacaoCartaoDeCredito.ValorTotal);
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
