using System.Windows;
using System.Windows.Controls;
using Projeto_2biPOO.Control;

namespace Projeto_2biPOO
{
    public partial class Pagamento : Window
    {
        private readonly int _mesaId;
        private readonly PedidoController _pedidoController = new PedidoController();

        public Pagamento() : this(0) { }

        public Pagamento(int mesaId)
        {
            InitializeComponent();
            _mesaId = mesaId;
            CarregarTotal();
        }

        private void CarregarTotal()
        {
            var pedido = _pedidoController.ObterPedidoAbertoPorMesa(_mesaId);
            if (pedido != null)
            {
                lblTotal.Content = $"Total: R$ {pedido.ValorTotal:F2}";
            }
        }

        private void BtnConfirmarPagamento_Click(object sender, RoutedEventArgs e)
        {
            string formaPagamento = (sender as Button)?.Content?.ToString() ?? "Pago";
            bool pago = _pedidoController.FinalizarPagamento(_mesaId, formaPagamento);

            if (pago)
            {
                MessageBox.Show("Pagamento realizado com sucesso!");
            }

            Mesas telaMesas = new Mesas();
            telaMesas.Show();
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            SuaConta telaSuaConta = new SuaConta(_mesaId);
            telaSuaConta.Show();
            this.Close();
        }
    }
}