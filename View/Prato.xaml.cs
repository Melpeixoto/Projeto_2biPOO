using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Projeto_2biPOO.Control;

namespace Projeto_2biPOO
{
    public partial class Prato : Window
    {
        private readonly int _produtoId;
        private readonly int _mesaId;
        private readonly ProdutoController _produtoController = new ProdutoController();
        private readonly PedidoController _pedidoController = new PedidoController();

        public Prato() : this(0, 0) { }

        public Prato(int produtoId, int mesaId)
        {
            InitializeComponent();
            _produtoId = produtoId;
            _mesaId = mesaId;
            CarregarProduto();
        }

        private void CarregarProduto()
        {
            var produto = _produtoController.ObterProdutoPorId(_produtoId);
            if (produto != null)
            {
                lblNome.Content = produto.Nome;
                txtDescricao.Text = produto.ExibirDetalhes();
                lblPreco.Content = $"Total: R$ {produto.PrecoBase:F2}";

                CarregarImagemProduto(produto.Nome);
            }
        }

        private void CarregarImagemProduto(string nomeProduto)
        {
            foreach (string caminho in _produtoController.ObterCaminhosCandidatosImagem(nomeProduto))
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(caminho, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    imgProduto.Source = bitmap;
                    return;
                }
                catch
                {
                }
            }
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            bool adicionado = _pedidoController.AdicionarProdutoAoPedido(_mesaId, _produtoId, 1, "");
            if (!adicionado)
            {
                MessageBox.Show("Selecione uma mesa e um produto válidos.");
                return;
            }

            NovaConta telaNovaConta = new NovaConta(_mesaId);
            telaNovaConta.Show();
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            NovaConta telaNovaConta = new NovaConta(_mesaId);
            telaNovaConta.Show();
            this.Close();
        }
    }
}