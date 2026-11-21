using System.Windows;
using Projeto_2biPOO.Control;

namespace Projeto_2biPOO
{
    public partial class SuaConta : Window
    {
        private int _mesaId;
        private readonly int _pedidoId;
        private readonly bool _veioDePedidos;
        private readonly PedidoController _pedidoController = new PedidoController();

        public SuaConta() : this(0) { }

        // Construtor chamado quando vem de NovaConta ou Pagamento (recebe o Id da Mesa)
        public SuaConta(int mesaId)
        {
            InitializeComponent();
            _mesaId = mesaId;
            _veioDePedidos = false;
            CarregarContaPorMesa();
        }

        // Construtor chamado quando vem do duplo clique na tela Pedidos (recebe o Id do Pedido)
        public SuaConta(int pedidoId, bool veioDePedidos)
        {
            InitializeComponent();
            _pedidoId = pedidoId;
            _veioDePedidos = veioDePedidos;
            CarregarContaPorPedidoId();
        }

        private void CarregarContaPorMesa()
        {
            dgItensConta.ItemsSource = _pedidoController.ListarItensDaContaFormatados(_mesaId, out double total, out int numeroMesa);
            lblTotal.Content = $"Total: R$ {total:F2}";
            lblTituloPedidos.Content = numeroMesa > 0 ? $"Seus Pedidos: (Mesa {numeroMesa})" : "Seus Pedidos:";
        }

        private void CarregarContaPorPedidoId()
        {
            dgItensConta.ItemsSource = _pedidoController.ListarItensPorPedidoIdFormatados(
                _pedidoId,
                out double total,
                out int numeroMesa,
                out int mesaId,
                out bool pedidoAberto
            );

            _mesaId = mesaId;
            lblTotal.Content = $"Total: R$ {total:F2}";
            lblTituloPedidos.Content = numeroMesa > 0 ? $"Seus Pedidos: (Mesa {numeroMesa})" : "Seus Pedidos:";

            // Se o pedido consultado pelo histórico já foi pago, oculta o botão de ir para pagamento
            if (!pedidoAberto)
            {
                btnPagamento.Visibility = Visibility.Hidden;
            }
        }

        private void BtnPagamento_Click(object sender, RoutedEventArgs e)
        {
            Pagamento telaPagamento = new Pagamento(_mesaId);
            telaPagamento.Show();
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            if (_veioDePedidos)
            {
                Pedidos telaPedidos = new Pedidos();
                telaPedidos.Show();
                this.Close();
            }
            else
            {
                NovaConta telaNovaConta = new NovaConta(_mesaId);
                telaNovaConta.Show();
                this.Close();
            }
        }
    }
}