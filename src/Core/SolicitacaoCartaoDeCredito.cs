using FluentValidation;
using FluentValidation.Results;
using System;

namespace Core.CartaoDeCredito.Domain
{
    public class SolicitacaoCartaoDeCredito
    {
        public readonly ValidationResult validationResult;
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Profissao { get; set; }
        public decimal Renda { get; set; }
        public string NomeNoCartao { get; set; }


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
            throw new NotImplementedException();
        }
    }
}
