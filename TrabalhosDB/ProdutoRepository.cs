using Npgsql;
using System.Collections.Generic;

namespace TrabalhosDB
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
    }

    public class ProdutoRepository
    {
        private readonly string _connectionString;

        public ProdutoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CriarProduto(Produto produto)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            string sql = "INSERT INTO produtos (nome, preco, estoque) VALUES (@nome, @preco, @estoque)";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("nome", produto.Nome);
            cmd.Parameters.AddWithValue("preco", produto.Preco);
            cmd.Parameters.AddWithValue("estoque", produto.Estoque);

            cmd.ExecuteNonQuery();
        }

        public List<Produto> ListarProdutos()
        {
            var produtos = new List<Produto>();

            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            string sql = "SELECT id, nome, preco, estoque FROM produtos";
            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                produtos.Add(new Produto
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco = reader.GetDecimal(2),
                    Estoque = reader.GetInt32(3)
                });
            }

            return produtos;
        }

        public void AtualizarProduto(Produto produto)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            string sql = "UPDATE produtos SET nome = @nome, preco = @preco, estoque = @estoque WHERE id = @id";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("nome", produto.Nome);
            cmd.Parameters.AddWithValue("preco", produto.Preco);
            cmd.Parameters.AddWithValue("estoque", produto.Estoque);
            cmd.Parameters.AddWithValue("id", produto.Id);

            cmd.ExecuteNonQuery();
        }
    }
}
