using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Interface;
using Core.CartaoDeCredito.Domain.Request;
using Core.CartaoDeCredito.Service;
using Moq;
using Xunit;

namespace Tests.CartaoDeCredito.Service
{
    public class SolicitacaoCartaoDeCreditoServiceTests
    {
        private readonly Mock<ISolicitacaoCartaoDeCreditoRepository> _solicitacaoCartaoDeCreditoRepository;
        private readonly Mock<IMesaDeCreditoService> _mesaDeCreditoService;

        public SolicitacaoCartaoDeCreditoServiceTests()
        {
            _solicitacaoCartaoDeCreditoRepository = new Mock<ISolicitacaoCartaoDeCreditoRepository>();
            _mesaDeCreditoService = new Mock<IMesaDeCreditoService>();
        }

        [Fact(DisplayName = "Cartão de crédito válido deve ser cadastrado e enviado à mesa de crédito")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarValido_DeveSerCadastradoEEnviadoAMesaDeCredito()
        {
            //Arrange
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCreditoRequest("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                900m,
                "Teste Plástico");

            _mesaDeCreditoService.Setup(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()))
                                .Returns(true);
                                    
            ISolicitacaoCartaoDeCreditoService solicitacaoCartaoDeCreditoService = new SolicitacaoCartaoDeCreditoService(_solicitacaoCartaoDeCreditoRepository.Object, _mesaDeCreditoService.Object);

            //Act
            var response = solicitacaoCartaoDeCreditoService.SolicitarCartao(solicitacaoCartaoDeCredito);

            //Assert
            Assert.NotNull(response.Id);
            _solicitacaoCartaoDeCreditoRepository.Verify(s => s.CriarSolicitacao(It.IsAny<SolicitacaoCartaoDeCredito>()), Times.Once);
            _mesaDeCreditoService.Verify(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()), Times.Once);
        }

        [Fact(DisplayName = "Cartão de crédito inválido não deve ser enviado à mesa de crédito")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_AoSolicitarInvlido_NaoDeveSerCadastradoEEnviadoAMesaDeCredito()
        {
            //Arrange
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCreditoRequest("",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                900m,
                "Teste Plástico");

            _mesaDeCreditoService.Setup(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()))
                                .Returns(true);

            ISolicitacaoCartaoDeCreditoService solicitacaoCartaoDeCreditoService = new SolicitacaoCartaoDeCreditoService(_solicitacaoCartaoDeCreditoRepository.Object, _mesaDeCreditoService.Object);

            //Act
            var response = solicitacaoCartaoDeCreditoService.SolicitarCartao(solicitacaoCartaoDeCredito);

            //Assert
            Assert.Null(response.Id);
            _solicitacaoCartaoDeCreditoRepository.Verify(s => s.CriarSolicitacao(It.IsAny<SolicitacaoCartaoDeCredito>()), Times.Never);
            _mesaDeCreditoService.Verify(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()), Times.Never);
        }
    }
}