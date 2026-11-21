using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto_2biPOO.Persistencia
{
    using Projeto_2biPOO.Entities;

    internal class BDdados
    {
        public void InicializarDados()
        {
            using var context = new SistemaDeliveryDbContext();

            if (context.Restaurantes.Any())
            {
                return;
            }

            Restaurante res = new Restaurante("WoodPeeks");
            context.Restaurantes.Add(res);
            context.SaveChanges();

            context.Mesas.AddRange(
                new Mesa(101, false),
                new Mesa(102, false),
                new Mesa(103, false),
                new Mesa(104, false),
                new Mesa(201, false),
                new Mesa(202, false),
                new Mesa(203, false),
                new Mesa(204, false),
                new Mesa(301, false),
                new Mesa(302, false),
                new Mesa(303, false),
                new Mesa(304, false),
                new Mesa(401, false),
                new Mesa(402, false),
                new Mesa(403, false),
                new Mesa(404, false)
            );

            var listaPratos = new List<Entities.Prato>
            {
                new Entities.Prato(
                    "Prato Kids - Iscas de Carne com Arroz e Batatas Sorriso",
                    "Prato infantil balanceado com suculentas iscas de carne bovina grelhadas, acompanhadas de arroz branco soltinho e divertidas batatas sorrisse crocantes.",
                    34.90,
                    "Iscas de carne bovina, arroz branco, batatas sorriso, temperos naturais",
                    25,
                    "Individual"),
                new Entities.Prato(
                    "Espaguete ao Molho de Tomate e Azeitonas",
                    "Massa espaguete servida com molho de tomate artesanal, azeitonas pretas e verdes, finalizada com azeite e cheiro-verde fresco.",
                    44.90,
                    "Espaguete, molho de tomate artesanal, azeitonas pretas e verdes, azeite, cheiro-verde",
                    15,
                    "Individual"),
                new Entities.Prato(
                    "Bife Grelhado Completo com Fritas e Vinagrete",
                    "Corte bovino nobre grelhado na brasa, acompanhado de arroz branco soltinho, batatas fritas onduladas crocantes e vinagrete artesanal fresco.",
                    69.90,
                    "Bife bovino nobre, arroz branco, batatas fritas onduladas, vinagrete artesanal, temperos naturais",
                    30,
                    "Individual"),
                new Entities.Prato(
                    "Prato Feito de Frango Assado Completo",
                    "Sobrecoxa de frango assada com tempero caseiro, acompanhada de arroz branco soltinho, feijão com caldo encorpado e salada fresca de alface, cenoura e tomate.",
                    49.90,
                    "Sobrecoxa de frango assada, arroz branco, feijão com caldo encorpado, salada de alface, cenoura e tomate",
                    20,
                    "Individual")
            };

            foreach (var prato in listaPratos)
            {
                prato.RestauranteId = res.Id;
            }

            context.Pratos.AddRange(listaPratos);

            var listaBebidas = new List<Bebida>
            {
                new Bebida("Coca-Cola", "Coca-Cola lata 350ml", 3.95, 350, true),
                new Bebida("Suco de Laranja", "Suco natural de laranja 400ml", 12.90, 400, true),
                new Bebida("Água Mineral", "Água mineral sem gás 500ml", 2.39, 500, true),
                new Bebida("Agua com gás", "Água com gás 500ml", 2.10, 500, true)
            };

            foreach (var bebida in listaBebidas)
            {
                bebida.RestauranteId = res.Id;
            }

            context.Bebidas.AddRange(listaBebidas);

            context.SaveChanges();
        }
    }
}