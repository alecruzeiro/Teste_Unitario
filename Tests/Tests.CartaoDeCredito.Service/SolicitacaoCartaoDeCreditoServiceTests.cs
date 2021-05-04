using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Interface;
using Core.CartaoDeCredito.Domain.Dto;
using Core.CartaoDeCredito.Service;
using Moq;
using System.Linq;
using Xunit;
using System;
using FluentValidation;

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

            _solicitacaoCartaoDeCreditoRepository.Setup(s => s.VerificarCpfJaCadastrado("01234567890"))
                                                .Returns(false);
            _mesaDeCreditoService.Setup(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()))
                                .Returns(true);

            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);
            ISolicitacaoCartaoDeCreditoService solicitacaoCartaoDeCreditoService = new SolicitacaoCartaoDeCreditoService(_solicitacaoCartaoDeCreditoRepository.Object, _mesaDeCreditoService.Object, _solicitacaoCartaoDeCreditoValidator);

            //Act
            var response = solicitacaoCartaoDeCreditoService.SolicitarCartao(solicitacaoCartaoDeCredito);

            //Assert
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
            
            _solicitacaoCartaoDeCreditoRepository.Setup(s => s.VerificarCpfJaCadastrado(It.Is<string>(x => x == "01234567890")))
                                              .Returns(false);
            _mesaDeCreditoService.Setup(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()))
                                .Returns(false);

            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);
            ISolicitacaoCartaoDeCreditoService solicitacaoCartaoDeCreditoService = new SolicitacaoCartaoDeCreditoService(_solicitacaoCartaoDeCreditoRepository.Object, _mesaDeCreditoService.Object, _solicitacaoCartaoDeCreditoValidator);

            //Act
            var response = solicitacaoCartaoDeCreditoService.SolicitarCartao(solicitacaoCartaoDeCredito);

            //Assert
            _solicitacaoCartaoDeCreditoRepository.Verify(s => s.CriarSolicitacaoAdquirente(It.IsAny<SolicitacaoCartaoDeCredito>()), Times.Never);
            _mesaDeCreditoService.Verify(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()), Times.Never);
            Assert.Contains(SolicitacaoCartaoDeCreditoValidator.Erro_Msg[mensagem], response.Validation.Errors.Select(e => e.ErrorMessage));
            Assert.Equal(default, response.NumeroDoCartao);
            Assert.Equal(default, response.Cvv);
            Assert.Equal(default, response.DataDeValidade);
            Assert.Equal(default, response.NomeNoCartao);
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
            var dataValidade = DateTime.Now.AddYears(5);
            CriarSolicitacaoAdquirenteResponse solicitacaoAdquirenteResponse = new CriarSolicitacaoAdquirenteResponse("12345", "123", dataValidade, "Teste Teste");

            _mesaDeCreditoService.Setup(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()))
                                .Returns(true);
            _solicitacaoCartaoDeCreditoRepository.Setup(s => s.CriarSolicitacaoAdquirente(It.IsAny<SolicitacaoCartaoDeCredito>()))
                                                .Returns(solicitacaoAdquirenteResponse);
            _solicitacaoCartaoDeCreditoRepository.Setup(s => s.VerificarCpfJaCadastrado(It.Is<string>(x => x == "01234567890")))
                                              .Returns(false);

            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);

            ISolicitacaoCartaoDeCreditoService solicitacaoCartaoDeCreditoService = new SolicitacaoCartaoDeCreditoService(_solicitacaoCartaoDeCreditoRepository.Object, _mesaDeCreditoService.Object, _solicitacaoCartaoDeCreditoValidator);

            //Act
            var response = solicitacaoCartaoDeCreditoService.SolicitarCartao(solicitacaoCartaoDeCredito);

            //Assert
            _solicitacaoCartaoDeCreditoRepository.Verify(s => s.CriarSolicitacaoAdquirente(It.IsAny<SolicitacaoCartaoDeCredito>()), Times.Once);
            _mesaDeCreditoService.Verify(m => m.EnviarParaMesaDeCredito(It.IsAny<MesaDeCreditoRequest>()), Times.Once);
            Assert.Equal(solicitacaoAdquirenteResponse.NumeroDoCartao, response.NumeroDoCartao);
            Assert.Equal(solicitacaoAdquirenteResponse.Cvv, response.Cvv);
            Assert.Equal($"{solicitacaoAdquirenteResponse.DataDeValidade?.ToString("MM")}/{solicitacaoAdquirenteResponse.DataDeValidade?.ToString("yy")}", response.DataDeValidade);
            Assert.Equal(solicitacaoAdquirenteResponse.NomeNoCartao, response.NomeNoCartao);
        }

        [Fact(DisplayName = "Caso o cpf já esteja cadastrado na base deverá retornar Cpf já cadastrado")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarCpfJaCadastrado_DeveRetornarMensagemCpfCadastrado()
        {
            //Arrange
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCreditoRequest("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                900m,
                "Teste Plástico");
            _solicitacaoCartaoDeCreditoRepository.Setup(s => s.VerificarCpfJaCadastrado("01234567890"))
                                                .Returns(true);

            IValidator<SolicitacaoCartaoDeCredito> _solicitacaoCartaoDeCreditoValidator = new SolicitacaoCartaoDeCreditoValidator(_solicitacaoCartaoDeCreditoRepository.Object);

            ISolicitacaoCartaoDeCreditoService solicitacaoCartaoDeCreditoService = new SolicitacaoCartaoDeCreditoService(_solicitacaoCartaoDeCreditoRepository.Object, _mesaDeCreditoService.Object, _solicitacaoCartaoDeCreditoValidator);

            //Act
            var response = solicitacaoCartaoDeCreditoService.SolicitarCartao(solicitacaoCartaoDeCredito);

            //Assert
            _solicitacaoCartaoDeCreditoRepository.Verify(s => s.VerificarCpfJaCadastrado(It.IsAny<string>()), Times.Once);
            Assert.Contains(SolicitacaoCartaoDeCreditoValidator.Erro_Msg["erro_cpf_cadastrado"], response.Validation.Errors.Select(e => e.ErrorMessage));
        }
    }
}