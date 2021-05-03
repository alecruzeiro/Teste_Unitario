namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface ISolicitacaoCartaoDeCreditoRepository
    {
        public bool VerificarCpf(string cpf);
        public void CriarSolicitacao(SolicitacaoCartaoDeCredito solicitacaoCartaoDeCredito);
    }
}
