using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Projeto_2biPOO.Control;

namespace Projeto_2biPOO
{
    public partial class AdicionarProduto : Window
    {
        private readonly ProdutoController _produtoController = new ProdutoController();
        private string? _caminhoImagemSelecionada;

        public AdicionarProduto()
        {
            InitializeComponent();
            AtualizarCamposPorCategoria();
        }

        private void CmbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AtualizarCamposPorCategoria();
        }

        private void AtualizarCamposPorCategoria()
        {
            if (txtIngredientes == null || txtTempo == null || txtTamanho == null || chkGelada == null)
                return;

            string categoria = (cmbCategoria.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Prato";

            if (categoria == "Prato")
            {
                txtIngredientes.IsEnabled = true;
                txtTempo.IsEnabled = true;

                chkGelada.IsChecked = false;
                chkGelada.IsEnabled = false;
            }
            else
            {
                txtIngredientes.Clear();
                txtIngredientes.IsEnabled = false;

                txtTempo.Clear();
                txtTempo.IsEnabled = false;

                chkGelada.IsEnabled = true;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void BtnEscolherImagem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Selecione a imagem do produto",
                Filter = "Imagens (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                _caminhoImagemSelecionada = dialog.FileName;
                if (txtCaminhoImagem != null)
                {
                    txtCaminhoImagem.Text = Path.GetFileName(_caminhoImagemSelecionada);
                }
            }
        }

        private void BtnSalvarProduto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                MessageBox.Show("Preencha pelo menos o nome e o preço do produto.");
                return;
            }

            if (!double.TryParse(txtPreco.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double preco) || preco < 0)
            {
                MessageBox.Show("Informe um preço numérico válido.");
                return;
            }

            string categoria = (cmbCategoria.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Prato";
            string ingredientes = txtIngredientes.Text.Trim();
            string tamanho = string.IsNullOrWhiteSpace(txtTamanho.Text) ? "Individual" : txtTamanho.Text.Trim();
            bool gelada = chkGelada.IsChecked == true;

            int tempoPreparo = 20;
            if (categoria == "Prato" && !string.IsNullOrWhiteSpace(txtTempo.Text))
            {
                if (!int.TryParse(txtTempo.Text.Trim(), out tempoPreparo) || tempoPreparo < 0)
                {
                    MessageBox.Show("Informe um tempo de preparo válido (em minutos).");
                    return;
                }
            }

            bool sucesso = _produtoController.CadastrarProduto(
                txtNome.Text.Trim(),
                txtDescricao.Text.Trim(),
                preco,
                categoria,
                ingredientes,
                tamanho,
                tempoPreparo,
                gelada,
                _caminhoImagemSelecionada
            );

            if (!sucesso)
            {
                MessageBox.Show("Restaurante não encontrado no banco de dados.");
                return;
            }

            MessageBox.Show("Produto adicionado com sucesso!");

            Opcoes telaOpcoes = new Opcoes();
            telaOpcoes.Show();
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Opcoes telaOpcoes = new Opcoes();
            telaOpcoes.Show();
            this.Close();
        }
    }
}