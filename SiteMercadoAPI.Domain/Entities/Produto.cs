using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;

namespace SiteMercadoAPI.Domain.Entities
{
    public class Produto : Notifiable, IValidatable
    {

        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public decimal Valor { get; private set; }
        public string Imagem { get; private set; }

        public Produto()
        {
            Id = Guid.NewGuid();
        }

        public Produto(string nome, decimal valor, string imagem)
        {
            Nome = nome;
            Valor = valor;
            Imagem = imagem;
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public void AlterarValor(decimal valor)
        {
            Valor = valor;
        }

        public void AlterarImagem(string imagem)
        {
            Imagem = imagem;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .IsNotNullOrEmpty(Nome, "Nome", "O nome deve ser preenchido")
                .IsGreaterThan(Valor,0, "Valor", "O valor deve ser maior que zero")
            );
        }
    }
}
