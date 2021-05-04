using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Interface;
using Core.CartaoDeCredito.Domain.Dto;
using Core.CartaoDeCredito.Service;
using Moq;
using System.Linq;
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
            _solicitacaoCartaoDeCreditoRepository.Verify(s => s.CriarSolicitacaoAdquirente(It.IsAny<SolicitacaoCartaoDeCredito>()), Times.Once);
            _mesaDeCreditoService.Verify(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()), Times.Once);
        }

        [Theory(DisplayName = "Cartão de crédito inválido não deve ser enviado à mesa de crédito")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        [InlineData("", "01234567890", "123456789", "Analista de Sistemas", "Teste Plástico", "erro_nome")]
        [InlineData("Teste Nome", "", "123456789", "Analista de Sistemas", "Teste Plástico", "erro_cpf")]
        [InlineData("Teste Nome", "01234567890", "", "Analista de Sistemas", "Teste Plástico", "erro_rg")]
        [InlineData("Teste Nome", "01234567890", "123456789", "", "Teste Plástico", "erro_profissao")]
        [InlineData("Teste Nome", "01234567890", "123456789", "Analista de Sistemas", "", "erro_nome_cartao")]
        public void CartaoDeCredito_AoSolicitarInvalido_NaoDeveSerCadastradoEEnviadoAMesaDeCredito(string nome,
            string cpf,
            string rg,
            string profissao,
            string nomeNoCartao,
            string mensagem)
        {
            //Arrange
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCreditoRequest(nome,
                cpf,
                rg,
                profissao,
                900m,
                nomeNoCartao);

            _mesaDeCreditoService.Setup(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()))
                                .Returns(true);

            ISolicitacaoCartaoDeCreditoService solicitacaoCartaoDeCreditoService = new SolicitacaoCartaoDeCreditoService(_solicitacaoCartaoDeCreditoRepository.Object, _mesaDeCreditoService.Object);

            //Act
            var response = solicitacaoCartaoDeCreditoService.SolicitarCartao(solicitacaoCartaoDeCredito);

            //Assert
            Assert.Null(response.Id);
            _solicitacaoCartaoDeCreditoRepository.Verify(s => s.CriarSolicitacaoAdquirente(It.IsAny<SolicitacaoCartaoDeCredito>()), Times.Never);
            _mesaDeCreditoService.Verify(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()), Times.Never);
            Assert.Contains(SolicitacaoCartaoDeCreditoValidation.Erro_Msg[mensagem], response.Validation.Errors.Select(e => e.ErrorMessage));
        }

        [Fact(DisplayName = "Pedido enviado para mesa deve ter dados do cartão retornados pelo adquirente")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarValido_DeveRetornarCartaoAdquirente()
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
            _solicitacaoCartaoDeCreditoRepository.Verify(s => s.CriarSolicitacaoAdquirente(It.IsAny<SolicitacaoCartaoDeCredito>()), Times.Once);
            _mesaDeCreditoService.Verify(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()), Times.Once);
            Assert.NotNull(response.NumeroDoCartao);
        }
    }
}