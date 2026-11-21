using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Projeto_2biPOO.Control
{
    using Projeto_2biPOO.Entities;

    public class PedidoResumo
    {
        public int Id { get; set; }
        public int Mesa { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; } = "";
        public double Desconto { get; set; }
        public double ValorTotal { get; set; }
    }

    internal class PedidoController
    {
        public Pedido? ObterPedidoAbertoPorMesa(int mesaId)
        {
            using var context = new SistemaDeliveryDbContext();
            return context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefault(p => p.MesaId == mesaId && p.Status == "Aberto");
        }

        public Pedido? ObterPedidoPorId(int pedidoId)
        {
            using var context = new SistemaDeliveryDbContext();
            return context.Pedidos
                .Include(p => p.Mesa)
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefault(p => p.Id == pedidoId);
        }

        public bool AdicionarProdutoAoPedido(int mesaId, int produtoId, int quantidade = 1, string observacao = "")
        {
            using var context = new SistemaDeliveryDbContext();
            var produto = context.Produtos.Find(produtoId);
            var mesa = context.Mesas.Find(mesaId);

            if (produto == null || mesa == null)
                return false;

            if (produto is IPreparacao preparavel)
            {
                preparavel.Preparar();
            }

            var pedido = context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefault(p => p.MesaId == mesaId && p.Status == "Aberto");

            if (pedido == null)
            {
                pedido = new Pedido(DateTime.Now, "Aberto", 0.0, "Pedido no salão", 0.0)
                {
                    MesaId = mesaId,
                    RestauranteId = produto.RestauranteId
                };
                context.Pedidos.Add(pedido);
                context.SaveChanges();
            }

            var itemExistente = pedido.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (itemExistente != null)
            {
                itemExistente.AlterarQuantidade(itemExistente.Quantidade + quantidade);
                if (!string.IsNullOrWhiteSpace(observacao))
                {
                    itemExistente.DefinirObservacao(observacao);
                }
            }
            else
            {
                var novoItem = new ProdutoPedido(quantidade, produto.PrecoBase, observacao, produto.Id, pedido.Id);
                pedido.AdicionarItem(novoItem);
                context.ProdutoPedidos.Add(novoItem);
            }

            pedido.CalcularTotal();
            mesa.AlocarMesa();

            context.SaveChanges();
            return true;
        }

        public void CancelarContaDaMesa(int mesaId)
        {
            using var context = new SistemaDeliveryDbContext();
            var pedidoAberto = context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefault(p => p.MesaId == mesaId && p.Status == "Aberto");

            if (pedidoAberto != null)
            {
                pedidoAberto.CancelarPedido();
                context.Pedidos.Remove(pedidoAberto);
            }

            var mesa = context.Mesas.Find(mesaId);
            if (mesa != null)
            {
                mesa.LiberarMesa();
            }

            context.SaveChanges();
        }

        public bool FinalizarPagamento(int mesaId, string formaPagamento)
        {
            using var context = new SistemaDeliveryDbContext();
            var pedido = context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefault(p => p.MesaId == mesaId && p.Status == "Aberto");

            var mesa = context.Mesas.Find(mesaId);

            if (pedido == null)
                return false;

            pedido.CalcularTotal();
            pedido.Status = $"Pago ({formaPagamento})";

            if (mesa != null)
            {
                mesa.LiberarMesa();
            }

            context.SaveChanges();
            return true;
        }

        public List<PedidoResumo> ListarTodosPedidosFormatados()
        {
            using var context = new SistemaDeliveryDbContext();
            return context.Pedidos
                .Include(p => p.Mesa)
                .OrderByDescending(p => p.DataHora)
                .Select(p => new PedidoResumo
                {
                    Id = p.Id,
                    Mesa = p.Mesa.Numero,
                    DataHora = p.DataHora,
                    Status = p.Status,
                    Desconto = p.Desconto,
                    ValorTotal = p.ValorTotal
                })
                .ToList();
        }

        public IList ListarItensDaContaFormatados(int mesaId, out double valorTotal, out int numeroMesa)
        {
            valorTotal = 0.0;
            numeroMesa = 0;

            using (var context = new SistemaDeliveryDbContext())
            {
                var mesa = context.Mesas.Find(mesaId);
                if (mesa != null)
                {
                    numeroMesa = mesa.Numero;
                }
            }

            var pedido = ObterPedidoAbertoPorMesa(mesaId);
            if (pedido == null)
                return Array.Empty<object>();

            valorTotal = pedido.ValorTotal;
            return pedido.Itens.Select(i => new
            {
                Produto = i.Produto.Nome,
                i.Quantidade,
                PrecoUnitario = i.PrecoUnitario,
                Subtotal = i.CalcularSubtotal()
            }).ToList();
        }

        public IList ListarItensPorPedidoIdFormatados(int pedidoId, out double valorTotal, out int numeroMesa, out int mesaId, out bool pedidoAberto)
        {
            valorTotal = 0.0;
            numeroMesa = 0;
            mesaId = 0;
            pedidoAberto = false;

            var pedido = ObterPedidoPorId(pedidoId);
            if (pedido == null)
                return Array.Empty<object>();

            valorTotal = pedido.ValorTotal;
            numeroMesa = pedido.Mesa?.Numero ?? 0;
            mesaId = pedido.MesaId;
            pedidoAberto = pedido.Status == "Aberto";

            return pedido.Itens.Select(i => new
            {
                Produto = i.Produto.Nome,
                i.Quantidade,
                PrecoUnitario = i.PrecoUnitario,
                Subtotal = i.CalcularSubtotal()
            }).ToList();
        }
    }
}