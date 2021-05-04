using Core.CartaoDeCredito.Domain.Dto;

namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface ISolicitacaoCartaoDeCreditoRepository
    {
        public bool VerificarCpf(string cpf);
        public CriarSolicitacaoAdquirenteResponse CriarSolicitacaoAdquirente(SolicitacaoCartaoDeCredito solicitacaoCartaoDeCredito);
    }
}
