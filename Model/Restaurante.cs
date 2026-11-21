using System.Collections.Generic;
using System.Linq;

namespace Projeto_2biPOO.Entities
{
    internal class Restaurante
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<Produto> Cardapio { get; set; } = new List<Produto>();
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public Restaurante()
        {
            Nome = "";
        }

        public Restaurante(string nome)
        {
            Nome = nome;
        }

        public void AdicionarProdutoCardapio(Produto p)
        {
            if (p != null)
            {
                p.RestauranteId = Id;
                Cardapio.Add(p);
            }
        }

        public void RemoverProdutoCardapio(int id)
        {
            var prod = Cardapio.FirstOrDefault(p => p.Id == id);
            if (prod != null)
            {
                Cardapio.Remove(prod);
            }
        }

        public void RegistrarPedido(Pedido pedido)
        {
            if (pedido != null)
            {
                Pedidos.Add(pedido);
            }
        }

        public void CancelarPedido(int idPedido)
        {
            var pedido = Pedidos.FirstOrDefault(p => p.Id == idPedido);
            if (pedido != null)
            {
                pedido.CancelarPedido();
            }
        }
    }
}