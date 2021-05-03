using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Interface;
using Core.CartaoDeCredito.Domain.Request;

namespace Core.CartaoDeCredito.Service
{
    public class SolicitacaoCartaoDeCreditoService : ISolicitacaoCartaoDeCreditoService
    {
        private readonly ISolicitacaoCartaoDeCreditoRepository _solicitacaoCartaoDeCreditoRepository;
        private readonly IMesaDeCreditoService _mesaDeCreditoService;

        public SolicitacaoCartaoDeCreditoService(ISolicitacaoCartaoDeCreditoRepository solicitacaoCartaoDeCreditoRepository, IMesaDeCreditoService mesaDeCreditoService)
        {
            _solicitacaoCartaoDeCreditoRepository = solicitacaoCartaoDeCreditoRepository;
            _mesaDeCreditoService = mesaDeCreditoService;
        }


        public void SolicitarCartao(SolicitacaoCartaoDeCreditoRequest solicitacaoCartaoDeCreditoRequest)
        {
            var solicitacaoCartaoDeCredito = solicitacaoCartaoDeCreditoRequest.ToDomain();
            _solicitacaoCartaoDeCreditoRepository.CriarSolicitacao(solicitacaoCartaoDeCredito);

            var mesaDeCreditoRequest = new MesaDeCreditoRequest(solicitacaoCartaoDeCredito);
            _mesaDeCreditoService.EnviarParaMesaDeCredito(mesaDeCreditoRequest);
        }

        public bool VerificarCpf(string cpf)
        {
            throw new System.NotImplementedException();
        }
    }
}
