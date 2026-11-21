using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Projeto_2biPOO.Control
{
    using Projeto_2biPOO.Entities;

    internal class ProdutoController
    {
        private static readonly string[] ExtensoesSuportadas = { ".jpeg", ".jpg", ".png" };

        private static readonly Dictionary<string, string> ImagensPadraoIniciais = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Prato Kids - Iscas de Carne com Arroz e Batatas Sorriso", "prato_kids.jpeg" },
            { "Espaguete ao Molho de Tomate e Azeitonas", "espaguete.jpeg" },
            { "Bife Grelhado Completo com Fritas e Vinagrete", "bife_grelhado.jpeg" },
            { "Prato Feito de Frango Assado Completo", "frango_assado.jpeg" },
            { "Coca-Cola", "coca_cola.jpeg" },
            { "Suco de Laranja", "suco_laranja.jpeg" },
            { "Água Mineral", "agua_mineral.jpeg" },
            { "Agua com gás", "agua_gas.jpeg" }
        };

        public List<Entities.Prato> ListarPratos()
        {
            using var context = new SistemaDeliveryDbContext();
            return context.Pratos.ToList();
        }

        public List<Bebida> ListarBebidas()
        {
            using var context = new SistemaDeliveryDbContext();
            return context.Bebidas.ToList();
        }

        public Produto? ObterProdutoPorId(int produtoId)
        {
            using var context = new SistemaDeliveryDbContext();
            return context.Produtos.Find(produtoId);
        }

        public bool CadastrarProduto(
            string nome,
            string descricao,
            double preco,
            string categoria,
            string ingredientes = "",
            string tamanho = "Individual",
            int tempoPreparo = 20,
            bool gelada = false,
            string? caminhoImagemOrigem = null)
        {
            using var context = new SistemaDeliveryDbContext();
            var restaurante = context.Restaurantes.FirstOrDefault();
            if (restaurante == null) return false;

            if (categoria == "Prato")
            {
                string ingFinal = string.IsNullOrWhiteSpace(ingredientes) ? descricao : ingredientes;
                var novoPrato = new Entities.Prato(nome, descricao, preco, ingFinal, tempoPreparo, tamanho);
                restaurante.AdicionarProdutoCardapio(novoPrato);
                context.Pratos.Add(novoPrato);
            }
            else
            {
                int volumeMl = 350;
                string apenasDigitos = new string(tamanho.Where(char.IsDigit).ToArray());
                if (!string.IsNullOrEmpty(apenasDigitos) && int.TryParse(apenasDigitos, out int volParsed) && volParsed > 0)
                {
                    volumeMl = volParsed;
                }

                var novaBebida = new Bebida(nome, descricao, preco, volumeMl, gelada);
                restaurante.AdicionarProdutoCardapio(novaBebida);
                context.Bebidas.Add(novaBebida);
            }

            context.SaveChanges();
            SalvarImagemDoProduto(nome, caminhoImagemOrigem);
            return true;
        }

        public void RemoverProduto(int produtoId)
        {
            using var context = new SistemaDeliveryDbContext();
            var produto = context.Produtos.Find(produtoId);
            if (produto != null)
            {
                context.Produtos.Remove(produto);
                context.SaveChanges();
            }
        }

        public List<string> ObterCaminhosCandidatosImagem(string nomeProduto)
        {
            var caminhos = new List<string>();
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string slug = NormalizarSlug(nomeProduto);

            foreach (string ext in ExtensoesSuportadas)
            {
                string caminhoDisco = Path.Combine(baseDir, "Imagens", slug + ext);
                if (File.Exists(caminhoDisco))
                    caminhos.Add(caminhoDisco);
            }

            foreach (string ext in ExtensoesSuportadas)
            {
                caminhos.Add($"pack://application:,,,/Imagens/{slug}{ext}");
            }

            if (ImagensPadraoIniciais.TryGetValue(nomeProduto.Trim(), out string? arquivoInicial))
            {
                caminhos.Add($"pack://application:,,,/Imagens/{arquivoInicial}");
            }

            foreach (string ext in ExtensoesSuportadas)
            {
                string caminhoPadraoDisco = Path.Combine(baseDir, "Imagens", "padrao" + ext);
                if (File.Exists(caminhoPadraoDisco))
                    caminhos.Add(caminhoPadraoDisco);

                caminhos.Add($"pack://application:,,,/Imagens/padrao{ext}");
            }

            return caminhos;
        }

        private void SalvarImagemDoProduto(string nomeProduto, string? caminhoOrigem)
        {
            if (string.IsNullOrWhiteSpace(caminhoOrigem) || !File.Exists(caminhoOrigem))
                return;

            string ext = Path.GetExtension(caminhoOrigem).ToLowerInvariant();
            if (!ExtensoesSuportadas.Contains(ext))
                ext = ".jpeg";

            string pastaImagens = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Imagens");
            Directory.CreateDirectory(pastaImagens);

            string destino = Path.Combine(pastaImagens, NormalizarSlug(nomeProduto) + ext);
            File.Copy(caminhoOrigem, destino, overwrite: true);
        }

        private static string NormalizarSlug(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return "padrao";

            string normalizado = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char c in normalizado)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            string slug = Regex.Replace(sb.ToString().ToLowerInvariant(), @"[^a-z0-9]+", "_").Trim('_');
            return string.IsNullOrEmpty(slug) ? "padrao" : slug;
        }
    }
}