using System.Windows;
using System.Windows.Input;
using Projeto_2biPOO.Control;
using Projeto_2biPOO.Entities;

namespace Projeto_2biPOO
{
    public partial class Mesas : Window
    {
        private readonly MesaController _mesaController = new MesaController();

        public Mesas()
        {
            InitializeComponent();
            _mesaController.InicializarBanco();
            CarregarMesas();
        }

        private void CarregarMesas()
        {
            dgMesas.ItemsSource = _mesaController.ListarMesas();
        }

        private void DgMesas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgMesas.SelectedItem is Mesa mesaSelecionada)
            {
                NovaConta telaNovaConta = new NovaConta(mesaSelecionada.Id);
                telaNovaConta.Show();
                this.Close();
            }
        }

        private void BtnTodosPedidos_Click(object sender, RoutedEventArgs e)
        {
            Pedidos telaPedidos = new Pedidos();
            telaPedidos.Show();
            this.Close();
        }

        private void BtnVoltarOpcoes_Click(object sender, RoutedEventArgs e)
        {
            Opcoes telaOpcoes = new Opcoes();
            telaOpcoes.Show();
            this.Close();
        }
    }
}