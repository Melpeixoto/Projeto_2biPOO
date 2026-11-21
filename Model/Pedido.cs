using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_2biPOO.Entities
{
    internal class Pedido : IDesconto
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; }
        public double Desconto { get; set; }
        public string ObservacaoGeral { get; set; }
        public double ValorTotal { get; set; }

        public int MesaId { get; set; }
        public Mesa Mesa { get; set; } = null!;

        public int? RestauranteId { get; set; }

        public List<ProdutoPedido> Itens { get; set; } = new List<ProdutoPedido>();

        public Pedido()
        {
            DataHora = DateTime.Now;
            Status = "";
            Desconto = 0.0;
            ObservacaoGeral = "";
            ValorTotal = 0.0;
        }

        public Pedido(DateTime dataHora, string status, double desconto, string observacaoGeral, double valorTotal)
        {
            DataHora = dataHora;
            Status = status;
            Desconto = desconto;
            ObservacaoGeral = observacaoGeral;
            ValorTotal = valorTotal;
        }

        public void AdicionarItem(ProdutoPedido item)
        {
            if (item == null) return;
            Itens.Add(item);
            CalcularTotal();
        }

        public void RemoverItem(ProdutoPedido item)
        {
            if (item == null) return;
            Itens.Remove(item);
            CalcularTotal();
        }

        public double CalcularDescontoPorFaixa(double subtotal)
        {
            if (subtotal <= 55.00)
                return 0.0;
            if (subtotal <= 75.00)
                return 5.00;
            if (subtotal <= 115.00)
                return 10.00;
            if (subtotal <= 150.00)
                return Math.Round(subtotal * 0.10, 2);

            return Math.Round(subtotal * 0.15, 2);
        }

        public double CalcularTotal()
        {
            double subtotal = Itens.Sum(i => i.CalcularSubtotal());
            double descontoCalculado = CalcularDescontoPorFaixa(subtotal);
            AplicarDesconto(descontoCalculado);
            ValorTotal = Math.Round(Math.Max(0.0, subtotal - Desconto), 2);
            return ValorTotal;
        }

        public void CancelarPedido()
        {
            Status = "Cancelado";
            Desconto = 0.0;
            ValorTotal = 0.0;
        }

        public void AplicarDesconto(double valorOuPercentual)
        {
            if (valorOuPercentual >= 0)
            {
                Desconto = Math.Round(valorOuPercentual, 2);
            }
        }
    }
}