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
            

        [Fact(DisplayName = "Transação com dados validos")]
        [Trait("Categoria", "Cartão de Crédito - Transação")]
        public void TransacaoCartaoDeCredito_TransacaoComDadosValidosNoCartao_DeveTransacionarComSucesso()
        {
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

            _transacaoCartaoDeCreditoService.Criar(transacao);
        }
    }
}
