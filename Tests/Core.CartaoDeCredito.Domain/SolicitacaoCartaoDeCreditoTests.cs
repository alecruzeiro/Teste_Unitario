using Core.CartaoDeCredito.Domain;
using System.Linq;
using Xunit;

namespace Tests.CartaoDeCredito.Domain
{
    public class SolicitacaoCartaoDeCreditoTests
    {
        [Fact(DisplayName = "Solicitar cartão com dados válidos")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarCartaoComDadosValidos_DeveCriarSolicitacaoComSucesso()
        {
            //Arrange, Act
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                2000m,
                "Teste Plástico");

            //Assert
            Assert.True(solicitacaoCartaoDeCredito.validationResult.IsValid);
        }

        [Theory(DisplayName = "Solicitar cartão com dados inválidos")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        [InlineData("", "01234567890", "123456789", "Analista de Sistemas", 2000, "Teste Plástico", "erro_nome")]
        [InlineData("Teste Nome", "", "123456789", "Analista de Sistemas", 2000, "Teste Plástico", "erro_cpf")]
        [InlineData("Teste Nome", "01234567890", "", "Analista de Sistemas", 2000, "Teste Plástico", "erro_rg")]
        [InlineData("Teste Nome", "01234567890", "123456789", "", 2000, "Teste Plástico", "erro_profissao")]
        [InlineData("Teste Nome", "01234567890", "123456789", "Analista de Sistemas", 0, "Teste Plástico", "erro_renda")]
        [InlineData("Teste Nome", "01234567890", "123456789", "Analista de Sistemas", 2000, "", "erro_nome_cartao")]
        public void CartaoDeCredito_SolicitarCartaoComDadosInvalidos_NaoDeveCriarSolicitacaoComSucesso(string nome,
            string cpf,
            string rg,
            string profissao,
            decimal renda,
            string nomeNoCartao,
            string mensagem)
        {
            //Arrange, Act
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito(nome,
                cpf,
                rg,
                profissao,
                renda,
                nomeNoCartao);

            //Assert
            Assert.False(solicitacaoCartaoDeCredito.validationResult.IsValid);
            Assert.Contains(SolicitacaoCartaoDeCreditoValidation.Erro_Msg[mensagem], solicitacaoCartaoDeCredito.validationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact(DisplayName = "Solicitar cartão renda entre 800 e 2500")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarCartaoComRendaDeGold_DeveRetornarSolicitacaoGold()
        {
            //Arrange, Act
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                2000m,
                "Teste Plástico");

            //Assert
            Assert.True(solicitacaoCartaoDeCredito.TipoCartaoDisponivel == ETipoCartao.Gold);
        }

        [Fact(DisplayName = "Solicitar cartão renda maior ou igual a 2500")]
        [Trait("Categoria", "Cartão de Crédito - Solicitação")]
        public void CartaoDeCredito_SolicitarCartaoComRendaDeGold_DeveRetornarSolicitacaoPlatinum()
        {
            //Arrange, Act
            var solicitacaoCartaoDeCredito = new SolicitacaoCartaoDeCredito("Teste Teste",
                "01234567890",
                "1234567890",
                "Analista de Sistemas",
                2600m,
                "Teste Plástico");

            //Assert
            Assert.True(solicitacaoCartaoDeCredito.TipoCartaoDisponivel == ETipoCartao.Platinum);
        }

    }
}
