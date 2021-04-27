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
            RuleFor(s => s.Nome)
                .NotEmpty()
                .WithMessage("Insira um nome válido");

            RuleFor(s => s.Cpf)
                .NotEmpty()
                .WithMessage("Insira um cpf válido");

            RuleFor(s => s.Rg)
                .NotEmpty()
                .WithMessage("Insira um RG válido");

            RuleFor(s => s.Profissao)
                .NotEmpty()
                .WithMessage("Insira uma profissão válida");

            RuleFor(s => s.Renda)
                .GreaterThan(0)
                .WithMessage("Insira uma renda válida");
        }
    }
}
