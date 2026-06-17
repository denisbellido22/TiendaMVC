using Microsoft.Data.SqlClient;
using TiendaMVC.Models;

namespace TiendaMVC.Models
{
    public class TiendaRepository
    {
        private readonly string _conn;
        public TiendaRepository(string conn) => _conn = conn;

        // ─── CATEGORIAS ────────────────────────────────────────────
        public List<Categoria> ObtenerCategorias()
        {
            var lista = new List<Categoria>();
            using var con = new SqlConnection(_conn);
            con.Open();
            using var reader = new SqlCommand("SELECT * FROM Categorias", con).ExecuteReader();
            while (reader.Read())
                lista.Add(new Categoria
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!
                });
            return lista;
        }

        public void InsertarCategoria(Categoria c)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand(
                "INSERT INTO Categorias (Nombre) VALUES (@n)", con);
            cmd.Parameters.AddWithValue("@n", c.Nombre);
            cmd.ExecuteNonQuery();
        }
        public void ActualizarProducto(Producto p)
        {
            using SqlConnection cn = new SqlConnection(_conn);
            cn.Open();

            string sql = @"UPDATE Productos
                   SET Nombre = @Nombre,
                       Precio = @Precio,
                       Stock = @Stock,
                       CategoriaId = @CategoriaId
                   WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@Precio", p.Precio);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);
            cmd.Parameters.AddWithValue("@CategoriaId", p.CategoriaId);

            cmd.ExecuteNonQuery();
        }

        public void EliminarCategoria(int id)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand("DELETE FROM Categorias WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // ─── CLIENTES ──────────────────────────────────────────────
        public List<Cliente> ObtenerClientes()
        {
            var lista = new List<Cliente>();
            using var con = new SqlConnection(_conn);
            con.Open();
            using var reader = new SqlCommand("SELECT * FROM Clientes", con).ExecuteReader();
            while (reader.Read())
                lista.Add(new Cliente
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!
                });
            return lista;
        }

        public void InsertarCliente(Cliente c)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand(
                "INSERT INTO Clientes (Nombre, Email, Telefono) VALUES (@n, @e, @t)", con);
            cmd.Parameters.AddWithValue("@n", c.Nombre);
            cmd.Parameters.AddWithValue("@e", c.Email);
            cmd.Parameters.AddWithValue("@t", c.Telefono);
            cmd.ExecuteNonQuery();
        }

        public void EliminarCliente(int id)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand("DELETE FROM Clientes WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // ─── PRODUCTOS ─────────────────────────────────────────────
        public List<Producto> ObtenerProductos()
        {
            var lista = new List<Producto>();
            using var con = new SqlConnection(_conn);
            con.Open();
            // JOIN para traer el nombre de la categoría
            var sql = @"SELECT p.*, c.Nombre AS CategoriaNombre
                        FROM Productos p
                        INNER JOIN Categorias c ON p.CategoriaId = c.Id";
            using var reader = new SqlCommand(sql, con).ExecuteReader();
            while (reader.Read())
                lista.Add(new Producto
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!,
                    Precio = (decimal)reader["Precio"],
                    Stock = (int)reader["Stock"],
                    CategoriaId = (int)reader["CategoriaId"],
                    CategoriaNombre = reader["CategoriaNombre"].ToString()!
                });
            return lista;
        }

        public void InsertarProducto(Producto p)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand(
                "INSERT INTO Productos (Nombre, Precio, Stock, CategoriaId) VALUES (@n,@p,@s,@c)", con);
            cmd.Parameters.AddWithValue("@n", p.Nombre);
            cmd.Parameters.AddWithValue("@p", p.Precio);
            cmd.Parameters.AddWithValue("@s", p.Stock);
            cmd.Parameters.AddWithValue("@c", p.CategoriaId);
            cmd.ExecuteNonQuery();
        }

        public void EliminarProducto(int id)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand("DELETE FROM Productos WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}