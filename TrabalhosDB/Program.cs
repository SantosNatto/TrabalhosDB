using System;

namespace TrabalhosDB
{
    class Program
    {
        static void Main()
        {
            var repo = new ProdutoRepository(DbConfig.ConnectionString);

            var novoProduto = new Produto { Nome = "Shampoo", Preco = 15.50m, Estoque = 20 };
            repo.CriarProduto(novoProduto);
            Console.WriteLine("Produto criado!");

            var produtos = repo.ListarProdutos();
            Console.WriteLine("Produtos cadastrados:");
            foreach (var p in produtos)
            {
                Console.WriteLine($"{p.Id} - {p.Nome} - R${p.Preco} - Estoque: {p.Estoque}");
            }

            if (produtos.Count > 0)
            {
                var p = produtos[0];
                p.Preco = 17.99m;
                repo.AtualizarProduto(p);
                Console.WriteLine($"Produto {p.Id} atualizado!");
            }
        }
    }
}
