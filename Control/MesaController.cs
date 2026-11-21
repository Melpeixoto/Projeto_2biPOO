using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Projeto_2biPOO.Persistencia;

namespace Projeto_2biPOO.Control
{
    using Projeto_2biPOO.Entities;

    internal class MesaController
    {
        public void InicializarBanco()
        {
            var bd = new BDdados();
            bd.InicializarDados();
        }

        public List<Mesa> ListarMesas()
        {
            using var context = new SistemaDeliveryDbContext();
            return context.Mesas
                .FromSqlRaw("SELECT Id, Numero, Status FROM Mesas ORDER BY Numero")
                .ToList();
        }

        public Mesa? ObterMesaPorId(int id)
        {
            using var context = new SistemaDeliveryDbContext();
            return context.Mesas.Find(id);
        }

        public bool CadastrarMesa(int numero, out string mensagem)
        {
            using var context = new SistemaDeliveryDbContext();
            if (context.Mesas.Any(m => m.Numero == numero))
            {
                mensagem = $"A mesa {numero} já está cadastrada.";
                return false;
            }

            context.Database.ExecuteSqlRaw(
                "INSERT INTO Mesas (Numero, Status) VALUES ({0}, {1})",
                numero,
                false
            );

            mensagem = $"Mesa {numero} adicionada com sucesso!";
            return true;
        }

        public bool AlocarMesa(int mesaId)
        {
            using var context = new SistemaDeliveryDbContext();
            var mesa = context.Mesas.Find(mesaId);
            if (mesa == null) return false;

            bool alocada = mesa.AlocarMesa();
            context.SaveChanges();
            return alocada;
        }

        public void LiberarMesa(int mesaId)
        {
            using var context = new SistemaDeliveryDbContext();
            var mesa = context.Mesas.Find(mesaId);
            if (mesa != null)
            {
                mesa.LiberarMesa();
                context.SaveChanges();
            }
        }
    }
}