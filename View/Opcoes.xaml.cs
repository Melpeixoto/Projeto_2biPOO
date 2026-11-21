using System.Windows;
using Projeto_2biPOO.Control;

namespace Projeto_2biPOO
{
    public partial class Opcoes : Window
    {
        private readonly MesaController _mesaController = new MesaController();

        public Opcoes()
        {
            InitializeComponent();
        }

        private void BtnMesas_Click(object sender, RoutedEventArgs e)
        {
            Mesas telaMesas = new Mesas();
            telaMesas.Show();
            this.Close();
        }

        private void BtnAdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            AdicionarProduto telaAdd = new AdicionarProduto();
            telaAdd.Show();
            this.Close();
        }

        private void BtnAdicionarMesa_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumeroMesa.Text) || !int.TryParse(txtNumeroMesa.Text.Trim(), out int numeroMesa) || numeroMesa <= 0)
            {
                MessageBox.Show("Informe um número de mesa válido.");
                return;
            }

            if (_mesaController.CadastrarMesa(numeroMesa, out string mensagem))
            {
                MessageBox.Show(mensagem);
                txtNumeroMesa.Clear();
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }
    }
}