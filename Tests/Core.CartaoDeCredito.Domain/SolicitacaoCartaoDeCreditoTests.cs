﻿using Core.CartaoDeCredito.Domain;
using Xunit;

namespace Tests.CartaoDeCredito.Domain
{
    public class SolicitacaoCartaoDeCreditoTests
    {
        [Fact(DisplayName = "Solicitar cartão com dados válidos")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarCartaoComDadosValidos_DeveCriarSolicitacaoComSucesso()
        {
            //Arrange
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito("Teste Teste", 
                "01234567890", 
                "1234567890", 
                "Analista de Sistemas", 
                2000m,
                "Teste Plástico");

            //Act, Assert
            Assert.True(solicitacaoCartaoDeCredito.validationResult.IsValid);
        }

        [Fact(DisplayName = "Solicitar cartão com dados inválidos")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarCartaoComDadosInvalidos_NaoDeveCriarSolicitacaoComSucesso()
        {
            //Arrange
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito(default,
                default,
                default,
                default,
                default,
                default);

            //Act, Assert
            Assert.False(solicitacaoCartaoDeCredito.validationResult.IsValid);
        }

    }
}