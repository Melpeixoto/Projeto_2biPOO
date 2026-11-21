using System.Windows;
using System.Windows.Input;
using Projeto_2biPOO.Control;

namespace Projeto_2biPOO
{
    public partial class Pedidos : Window
    {
        private readonly PedidoController _pedidoController = new PedidoController();

        public Pedidos()
        {
            InitializeComponent();
            CarregarPedidos();
        }

        private void CarregarPedidos()
        {
            dgPedidos.ItemsSource = _pedidoController.ListarTodosPedidosFormatados();
        }

        private void DgPedidos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgPedidos.SelectedItem is PedidoResumo pedidoSelecionado)
            {
                SuaConta telaSuaConta = new SuaConta(pedidoSelecionado.Id, veioDePedidos: true);
                telaSuaConta.Show();
                this.Close();
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Mesas telaMesas = new Mesas();
            telaMesas.Show();
            this.Close();
        }
    }
}