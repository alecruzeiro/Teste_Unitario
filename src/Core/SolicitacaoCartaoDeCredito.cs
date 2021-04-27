using FluentValidation;
using FluentValidation.Results;
using System;

namespace Core.CartaoDeCredito.Domain
{
    public class SolicitacaoCartaoDeCredito
    {
        public ValidationResult validationResult { get; }
        public string Nome { get; }
        public string Cpf { get; }
        public string Rg { get; }
        public string Profissao { get; }
        public decimal Renda { get; }
        public string NomeNoCartao { get; }


        public SolicitacaoCartaoDeCredito(string nome,
            string cpf,
            string rg,
            string profissao,
            decimal renda,
            string nomeNoCartao)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Profissao = profissao;
            Renda = renda;
            NomeNoCartao = nomeNoCartao;

            validationResult = new SolicitacaoCartaoDeCreditoValidation().Validate(this);
        }
    }

    internal class SolicitacaoCartaoDeCreditoValidation : AbstractValidator<SolicitacaoCartaoDeCredito>
    {


        public SolicitacaoCartaoDeCreditoValidation()
        {
        }
    }
}
