using Core.CartaoDeCredito.Domain.Request;

namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface ISolicitacaoCartaoDeCreditoService
    {
        public SolicitacaoCartaoDeCreditoResponse SolicitarCartao(SolicitacaoCartaoDeCreditoRequest solicitacaoCartaoDeCreditoRequest);
        public bool VerificarCpf(string cpf);
    }
}
