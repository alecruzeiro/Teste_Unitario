namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface ISolicitacaoCartaoDeCreditoService
    {
        public void SolicitarCartao(SolicitacaoCartaoDeCredito solicitacaoCartaoDeCredito);
        public bool VerificarCpf(string cpf);
    }
}
