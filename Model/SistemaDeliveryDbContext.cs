using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto_2biPOO.Entities
{
    internal class SistemaDeliveryDbContext : DbContext
    {
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Prato> Pratos { get; set; }
        public DbSet<Bebida> Bebidas { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ProdutoPedido> ProdutoPedidos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestauranteDb;Integrated Security=True;Encrypt=False;");
        }
    }
}
