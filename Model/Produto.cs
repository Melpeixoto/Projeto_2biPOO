using System;

namespace Projeto_2biPOO.Entities
{
    internal abstract class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double PrecoBase { get; set; }

        public int RestauranteId { get; set; }
        public Restaurante Restaurante { get; set; } = null!;

        public Produto()
        {
            Nome = "";
            Descricao = "";
            PrecoBase = 0.0;
        }

        public Produto(string nome, string descricao, double precoBase)
        {
            Nome = nome;
            Descricao = descricao;
            PrecoBase = precoBase;
        }

        public double CalcularPrecoComDesconto(double percentual)
        {
            if (percentual < 0) percentual = 0;
            if (percentual > 100) percentual = 100;
            return PrecoBase - (PrecoBase * (percentual / 100.0));
        }

        public virtual string ExibirDetalhes()
        {
            return $"{Nome} - R$ {PrecoBase:F2}\n{Descricao}";
        }
    }
}