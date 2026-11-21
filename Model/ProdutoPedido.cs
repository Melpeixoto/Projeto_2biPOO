using System;

namespace Projeto_2biPOO.Entities
{
    internal class ProdutoPedido
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public double PrecoUnitario { get; set; }
        public string ObservacoesItem { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;

        public ProdutoPedido()
        {
            Quantidade = 0;
            PrecoUnitario = 0.0;
            ObservacoesItem = "";
        }

        public ProdutoPedido(int quantidade, double precoUnitario, string observacoesItem, int produtoId, int pedidoId)
        {
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            ObservacoesItem = observacoesItem;
            ProdutoId = produtoId;
            PedidoId = pedidoId;
        }

        public double CalcularSubtotal()
        {
            return Quantidade * PrecoUnitario;
        }

        public void AlterarQuantidade(int novaQtd)
        {
            if (novaQtd >= 0)
            {
                Quantidade = novaQtd;
            }
        }

        public void DefinirObservacao(string obs)
        {
            ObservacoesItem = obs ?? "";
        }
    }
}