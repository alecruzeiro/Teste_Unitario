using FluentValidation.Results;
using Newtonsoft.Json;
using System;

namespace Core.CartaoDeCredito.Domain.Request
{
    public class SolicitacaoCartaoDeCreditoRequest
    {
        public string Nome { get; }
        public string Cpf { get; }
        public string Rg { get; }
        public string Profissao { get; }
        public decimal Renda { get; }
        public string NomeNoCartao { get; }

        [JsonConstructor]
        public SolicitacaoCartaoDeCreditoRequest(string nome, string cpf, string rg, string profissao, decimal renda, string nomeNoCartao)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Profissao = profissao;
            Renda = renda;
            NomeNoCartao = nomeNoCartao;
        }

    }

    public class SolicitacaoCartaoDeCreditoResponse
    {
        public Guid? Id { get; }
        public ValidationResult Validation { get; }

        public SolicitacaoCartaoDeCreditoResponse(Guid? id, ValidationResult validation)
        {
            Id = id;
            Validation = validation;
        }

    }

    public static class SolicitacaoCartaoDeCreditoRequestResponseMappingExtension
    {
        public static SolicitacaoCartaoDeCredito ToDomain(this SolicitacaoCartaoDeCreditoRequest solicitacaoCartaoDeCredito)
        {
            return new SolicitacaoCartaoDeCredito(
                    solicitacaoCartaoDeCredito.Nome,
                    solicitacaoCartaoDeCredito.Cpf,
                    solicitacaoCartaoDeCredito.Rg,
                    solicitacaoCartaoDeCredito.Profissao,
                    solicitacaoCartaoDeCredito.Renda,
                    solicitacaoCartaoDeCredito.NomeNoCartao
               );
        }

        public static SolicitacaoCartaoDeCreditoResponse ToResponse(this SolicitacaoCartaoDeCredito solicitacaoCartaoDeCredito)
        {
            return new SolicitacaoCartaoDeCreditoResponse(
                    solicitacaoCartaoDeCredito.Id,
                    solicitacaoCartaoDeCredito.ValidationResult
                );
        }
    }
}
