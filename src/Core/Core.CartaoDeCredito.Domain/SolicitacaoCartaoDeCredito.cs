using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Core.CartaoDeCredito.Domain
{
    public class SolicitacaoCartaoDeCredito
    {
        public ValidationResult ValidationResult { get; }
        public string Nome { get; }
        public string Cpf { get; }
        public string Rg { get; }
        public string Profissao { get; }
        public decimal Renda { get; }
        public string NomeNoCartao { get; }
        public ETipoCartao? TipoCartaoDisponivel { get; }

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
            TipoCartaoDisponivel = TipoDeCartaoPorRenda(renda);

            ValidationResult = new SolicitacaoCartaoDeCreditoValidation().Validate(this);
        }

        private ETipoCartao? TipoDeCartaoPorRenda(decimal renda)
        {
            if (renda >= 2500m)
                return ETipoCartao.Platinum;
            else if (renda >= 800m)
                return ETipoCartao.Gold;

            throw new Exception("Renda abaixo de R$:800,00");
        }
    }

    public class SolicitacaoCartaoDeCreditoValidation : AbstractValidator<SolicitacaoCartaoDeCredito>
    {
        private static IReadOnlyDictionary<string, string> _erroMsg => new Dictionary<string, string>()
        {
            {"erro_nome", "Insira um nome válido"},
            {"erro_cpf", "Insira um cpf válido"},
            {"erro_rg", "Insira um RG válido"},
            {"erro_profissao", "Insira uma profissão válida"},
            {"erro_renda", "Insira uma renda válida"},
            {"erro_nome_cartao", "Insira um nome no cartão válido"},
        };

        public static IReadOnlyDictionary<string, string> Erro_Msg => _erroMsg;

        public SolicitacaoCartaoDeCreditoValidation()
        {
            RuleFor(s => s.Nome)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_nome"]);

            RuleFor(s => s.Cpf)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_cpf"]);

            RuleFor(s => s.Rg)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_rg"]);

            RuleFor(s => s.Profissao)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_profissao"]);

            RuleFor(s => s.Renda)
                .GreaterThan(0)
                .WithMessage(Erro_Msg["erro_renda"]);

            RuleFor(s => s.NomeNoCartao)
                .NotEmpty()
                .WithMessage(Erro_Msg["erro_nome_cartao"]);
        }
    }
}
