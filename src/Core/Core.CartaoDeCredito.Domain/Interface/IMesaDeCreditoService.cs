using Core.CartaoDeCredito.Domain.Request;

namespace Core.CartaoDeCredito.Domain.Interface
{
    public interface IMesaDeCreditoService
    {
        bool EnviarParaMesaDeCredito(MesaDeCreditoRequest solicitacaoCartaoDeCredito);
    }
}
