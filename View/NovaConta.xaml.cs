using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Projeto_2biPOO.Control;
using Projeto_2biPOO.Entities;

namespace Projeto_2biPOO
{
    public partial class NovaConta : Window
    {
        private readonly int _mesaId;
        private readonly ProdutoController _produtoController = new ProdutoController();
        private readonly PedidoController _pedidoController = new PedidoController();

        public NovaConta() : this(0) { }

        public NovaConta(int mesaId)
        {
            InitializeComponent();
            _mesaId = mesaId;
            CarregarCardapio();
        }

        private void CarregarCardapio()
        {
            dgComidas.ItemsSource = _produtoController.ListarPratos();
            dgBebidas.ItemsSource = _produtoController.ListarBebidas();
        }

        private void DgProduto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid grid && grid.SelectedItem is Produto produtoSelecionado)
            {
                Prato telaPrato = new Prato(produtoSelecionado.Id, _mesaId);
                telaPrato.Show();
                this.Close();
            }
        }

        private void BtnCancelarConta_Click(object sender, RoutedEventArgs e)
        {
            _pedidoController.CancelarContaDaMesa(_mesaId);

            Mesas telaMesas = new Mesas();
            telaMesas.Show();
            this.Close();
        }

        private void BtnVisualizarConta_Click(object sender, RoutedEventArgs e)
        {
            SuaConta telaSuaConta = new SuaConta(_mesaId);
            telaSuaConta.Show();
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Mesas telaMesas = new Mesas();
            telaMesas.Show();
            this.Close();
        }
    }
}