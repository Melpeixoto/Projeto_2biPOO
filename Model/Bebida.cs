using System;

namespace Projeto_2biPOO.Entities
{
    internal class Bebida : Produto
    {
        public int VolumeMl { get; set; }
        public bool Gelada { get; set; }

        public Bebida() : base()
        {
            VolumeMl = 0;
            Gelada = false;
        }

        public Bebida(string nome, string descricao, double precoBase, int volumeMl, bool gelada)
            : base(nome, descricao, precoBase)
        {
            VolumeMl = volumeMl;
            Gelada = gelada;
        }

        public override string ExibirDetalhes()
        {
            string temp = Gelada ? "Gelada" : "Natural";
            return $"{Descricao}\n\nVolume: {VolumeMl}ml ({temp})";
        }
    }
}