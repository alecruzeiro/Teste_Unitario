using Core.CartaoDeCredito.Domain;
using Core.CartaoDeCredito.Domain.Dto;
using Core.CartaoDeCredito.Domain.Interface;
using Core.CartaoDeCredito.Service;
using FluentValidation;
using Moq;
using System.Linq;
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
            Assert.True(transacaoResponse.StatusTransacao == StatusTransacao.Aprovada);
        }

        [Theory(DisplayName = "Se os dados do cartão forem inválidos, deve retornar mensagem de erro")]
        [Trait("Categoria", "Cartão de Crédito - Transação")]
        [InlineData("", "123", "123456789", "12/2026", "Teste Plástico", 100, "CPF inválido")]
        [InlineData("01234567890", "", "123456789", "12/2026", "Teste Plástico", 100, "Número do cvv inválido")]
        [InlineData("01234567890", "123", "", "12/2026", "Teste Plástico", 100, "Número do cartão inválido")]
        [InlineData("01234567890", "123", "123", "", "Teste Plástico", 100, "Data de validade inválida")]
        [InlineData("01234567890", "123", "123", "12/2026", "", 100, "Nome no Cartão inválido")]
        [InlineData("01234567890", "123", "123", "12/2026", "Teste Plástico", 0, "Valor total inválido")]
        [InlineData("01234567890", "123", "123", "04/2021", "Teste Plástico", 10, "Cartão vencido")]
        public void TransacaoCartaoDeCredito_TransacaoComDadosInvalidosNoCartao_NaoDeveTransacionar(string cpf, 
            string cvv,
            string numeroCartaoVirtual,
            string dataDeValidade,
            string nomeNoCartao,
            decimal valorTotal,
            string mensagem)
        {
            //Arrange
            var transacao = new TransacaoCartaoDeCreditoRequest()
            {
                CartaoDeCredito = new CartaoDeCreditoRequest()
                {
                    Cpf = cpf,
                    Cvv = cvv,
                    NumeroCartaoVirtual = numeroCartaoVirtual,
                    DataDeValidade = dataDeValidade,
                    NomeNoCartao = nomeNoCartao
                },
                ValorTotal = valorTotal
            };

            _transacaoCartaoDeCreditoRepository.Setup(c => c.Criar(It.IsAny<TransacaoCartaoDeCredito>()))
                                            .Returns(true);
            //Act
            var transacaoResponse = _transacaoCartaoDeCreditoService.Criar(transacao);

            //Assert
            _transacaoCartaoDeCreditoRepository.Verify(s => s.Criar(It.IsAny<TransacaoCartaoDeCredito>()), Times.Never);
            Assert.True(transacaoResponse.StatusTransacao == StatusTransacao.Reprovada);
            Assert.Contains(mensagem, transacaoResponse.Validation.Errors.Select(e => e.ErrorMessage));
        }
    }
}
