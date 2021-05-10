using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Dto;
using Core.CartaoDeCredito.Domain.Interface;
using Core.CartaoDeCredito.Service;
using FluentValidation;
using Moq;
using Xunit;

namespace Tests.CartaoDeCredito.Service
{
    public class TransacaoCartaoDeCreditoServiceTest
    {
        private readonly ITransacaoCartaoDeCreditoService _transacaoCartaoDeCreditoService;
        private readonly Mock<ITransacaoCartaoDeCreditoRepository> _transacaoCartaoDeCreditoRepository;
        private readonly IValidator<TransacaoCartaoDeCredito> _transacaoCartaoDeCredito;

        public TransacaoCartaoDeCreditoServiceTest()
        {
            _transacaoCartaoDeCredito = new TransacaoCartaoDeCreditoValidator();
            _transacaoCartaoDeCreditoRepository = new Mock<ITransacaoCartaoDeCreditoRepository>();
            _transacaoCartaoDeCreditoService = new TransacaoCartaoDeCreditoService(_transacaoCartaoDeCreditoRepository.Object, _transacaoCartaoDeCredito);
        }
            

        [Fact(DisplayName = "Transação com dados validos deve criar transação")]
        [Trait("Categoria", "Cartão de Crédito - Transação")]
        public void TransacaoCartaoDeCredito_TransacaoComDadosValidosNoCartao_DeveTransacionarComSucesso()
        {
            //Arrange
            var transacao = new TransacaoCartaoDeCreditoRequest()
            {
                CartaoDeCredito = new CartaoDeCreditoRequest()
                {
                    Cpf = "01234567890",
                    Cvv = "123",
                    NumeroCartaoVirtual = "1234567890",
                    DataDeValidade = "12/2026",
                    NomeNoCartao = "Teste do Teste"
                },
                ValorTotal = 100m
            };

            _transacaoCartaoDeCreditoRepository.Setup(c => c.Criar(It.IsAny<TransacaoCartaoDeCredito>()))
                                            .Returns(true);
            //Act
            var transacaoResponse = _transacaoCartaoDeCreditoService.Criar(transacao);

            //Assert
            _transacaoCartaoDeCreditoRepository.Verify(s => s.Criar(It.IsAny<TransacaoCartaoDeCredito>()), Times.Once);
            Assert.True(transacaoResponse.TransacaoRealizadaComSucesso);
        }

        [Fact(DisplayName = "Se os dados do cartão forem inválidos, deve retornar mensagem de erro")]
        [Trait("Categoria", "Cartão de Crédito - Transação")]
        public void TransacaoCartaoDeCredito_TransacaoComDadosInvalidosNoCartao_NaoDeveTransacionar()
        {
            //Arrange
            var transacao = new TransacaoCartaoDeCreditoRequest()
            {
                CartaoDeCredito = new CartaoDeCreditoRequest()
                {
                    Cpf = "",
                    Cvv = "",
                    NumeroCartaoVirtual = "",
                    DataDeValidade = "",
                    NomeNoCartao = ""
                },
                ValorTotal = 0m
            };

            _transacaoCartaoDeCreditoRepository.Setup(c => c.Criar(It.IsAny<TransacaoCartaoDeCredito>()))
                                            .Returns(true);
            //Act
            var transacaoResponse = _transacaoCartaoDeCreditoService.Criar(transacao);

            //Assert
            _transacaoCartaoDeCreditoRepository.Verify(s => s.Criar(It.IsAny<TransacaoCartaoDeCredito>()), Times.Never);
            Assert.False(transacaoResponse.TransacaoRealizadaComSucesso);
        }
    }
}
