using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_2biPOO.Entities
{
    internal class Prato : Produto, IPreparacao
    {
        public string IngredientesStr { get; set; }
        public int TempoPreparoMinutos { get; set; }
        public string TamanhoPorcao { get; set; }

        public Prato() : base()
        {
            IngredientesStr = "";
            TempoPreparoMinutos = 0;
            TamanhoPorcao = "";
        }

        public Prato(string nome, string descricao, double precoBase, string ingredientesStr, int tempoPreparoMinutos, string tamanhoPorcao)
            : base(nome, descricao, precoBase)
        {
            IngredientesStr = ingredientesStr;
            TempoPreparoMinutos = tempoPreparoMinutos;
            TamanhoPorcao = tamanhoPorcao;
        }

        public List<string> ObterListaIngredientes()
        {
            if (string.IsNullOrWhiteSpace(IngredientesStr))
                return new List<string>();

            return IngredientesStr
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim())
                .ToList();
        }

        public void Preparar() { }

        public override string ExibirDetalhes()
        {
            return $"{Descricao}\n\nIngredientes: {IngredientesStr}\nPorção: {TamanhoPorcao} | Tempo de preparo: {TempoPreparoMinutos} min";
        }
    }
}