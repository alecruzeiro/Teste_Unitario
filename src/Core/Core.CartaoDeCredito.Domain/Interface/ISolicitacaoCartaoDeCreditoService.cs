namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface ISolicitacaoCartaoDeCreditoService
    {
        public void SolicitarCartao();
        public bool VerificarCpf(string cpf);
    }
}
