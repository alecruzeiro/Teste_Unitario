using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Interface;
using Core.CartaoDeCredito.Domain.Dto;
using Core.CartaoDeCredito.Service;
using Moq;
using System.Linq;
using Xunit;
using System;

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

        [Fact(DisplayName = "Cart�o de cr�dito v�lido deve ser cadastrado e enviado � mesa de cr�dito")]
        [Trait("Categoria", "Cart�o de Cr�dito - Solicita��o")]
        public void CartaoDeCredito_SolicitarValido_DeveSerCadastradoEEnviadoAMesaDeCredito()
        {
            //Arrange
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCreditoRequest("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                900m,
                "Teste Pl�stico");

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

        [Theory(DisplayName = "Cart�o de cr�dito inv�lido n�o deve ser enviado � mesa de cr�dito")]
        [Trait("Categoria", "Cart�o de Cr�dito - Solicita��o")]
        [InlineData("", "01234567890", "123456789", "Analista de Sistemas", "Teste Pl�stico", "erro_nome")]
        [InlineData("Teste Nome", "", "123456789", "Analista de Sistemas", "Teste Pl�stico", "erro_cpf")]
        [InlineData("Teste Nome", "01234567890", "", "Analista de Sistemas", "Teste Pl�stico", "erro_rg")]
        [InlineData("Teste Nome", "01234567890", "123456789", "", "Teste Pl�stico", "erro_profissao")]
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
            Assert.Equal(default, response.NumeroDoCartao);
            Assert.Equal(default, response.Cvv);
            Assert.Equal(default, response.DataDeValidade);
            Assert.Equal(default, response.NomeNoCartao);
        }

        [Fact(DisplayName = "Pedido enviado para mesa deve ter dados do cart�o retornados pelo adquirente")]
        [Trait("Categoria", "Cart�o de Cr�dito - Solicita��o")]
        public void CartaoDeCredito_SolicitarValido_DeveRetornarCartaoAdquirente()
        {
            //Arrange
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCreditoRequest("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                900m,
                "Teste Pl�stico");
            var dataValidade = DateTime.Now.AddYears(5);
            CriarSolicitacaoAdquirenteResponse solicitacaoAdquirenteResponse = new CriarSolicitacaoAdquirenteResponse("12345", "123", dataValidade, "Teste Teste");

            _mesaDeCreditoService.Setup(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()))
                                .Returns(true);
            _solicitacaoCartaoDeCreditoRepository.Setup(s => s.CriarSolicitacaoAdquirente(It.IsAny<SolicitacaoCartaoDeCredito>()))
                                                .Returns(solicitacaoAdquirenteResponse);

            ISolicitacaoCartaoDeCreditoService solicitacaoCartaoDeCreditoService = new SolicitacaoCartaoDeCreditoService(_solicitacaoCartaoDeCreditoRepository.Object, _mesaDeCreditoService.Object);

            //Act
            var response = solicitacaoCartaoDeCreditoService.SolicitarCartao(solicitacaoCartaoDeCredito);

            //Assert
            Assert.NotNull(response.Id);
            _solicitacaoCartaoDeCreditoRepository.Verify(s => s.CriarSolicitacaoAdquirente(It.IsAny<SolicitacaoCartaoDeCredito>()), Times.Once);
            _mesaDeCreditoService.Verify(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()), Times.Once);
            Assert.Equal(solicitacaoAdquirenteResponse.NumeroDoCartao, response.NumeroDoCartao);
            Assert.Equal(solicitacaoAdquirenteResponse.Cvv, response.Cvv);
            Assert.Equal($"{solicitacaoAdquirenteResponse.DataDeValidade?.ToString("MM")}/{solicitacaoAdquirenteResponse.DataDeValidade?.ToString("yy")}", response.DataDeValidade);
            Assert.Equal(solicitacaoAdquirenteResponse.NomeNoCartao, response.NomeNoCartao);
        }
    }
}