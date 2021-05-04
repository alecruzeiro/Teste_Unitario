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


        public SolicitacaoCartaoDeCreditoResponse SolicitarCartao(SolicitacaoCartaoDeCreditoRequest solicitacaoCartaoDeCreditoRequest)
        {
            var solicitacaoCartaoDeCredito = solicitacaoCartaoDeCreditoRequest.ToDomain();

            solicitacaoCartaoDeCredito.FoiEnviadoParaMesaDeCredito(_mesaDeCreditoService.EnviarParaMesaDeCredito(new MesaDeCreditoRequest(solicitacaoCartaoDeCredito)));

            _solicitacaoCartaoDeCreditoRepository.CriarSolicitacao(solicitacaoCartaoDeCredito);

            return solicitacaoCartaoDeCredito.ToResponse();
        }

        public bool VerificarCpf(string cpf)
        {
            throw new System.NotImplementedException();
        }
    }
}
