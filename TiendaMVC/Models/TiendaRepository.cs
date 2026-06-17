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

        public Categoria? ObtenerCategoriaPorId(int id)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand("SELECT * FROM Categorias WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Categoria
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!
                };
            }
            return null;
        }

        public void InsertarCategoria(Categoria c)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand("INSERT INTO Categorias (Nombre) VALUES (@n)", con);
            cmd.Parameters.AddWithValue("@n", c.Nombre);
            cmd.ExecuteNonQuery();
        }

        public void ActualizarCategoria(Categoria c)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand("UPDATE Categorias SET Nombre = @n WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@n", c.Nombre);
            cmd.Parameters.AddWithValue("@id", c.Id);
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

        public Cliente? ObtenerClientePorId(int id)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand("SELECT * FROM Clientes WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Cliente
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Telefono = reader["Telefono"].ToString()!
                };
            }
            return null;
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

        public void ActualizarCliente(Cliente c)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand(
                "UPDATE Clientes SET Nombre = @n, Email = @e, Telefono = @t WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@n", c.Nombre);
            cmd.Parameters.AddWithValue("@e", c.Email);
            cmd.Parameters.AddWithValue("@t", c.Telefono);
            cmd.Parameters.AddWithValue("@id", c.Id);
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

        public Producto? ObtenerProductoPorId(int id)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var sql = @"SELECT p.*, c.Nombre AS CategoriaNombre
                        FROM Productos p
                        INNER JOIN Categorias c ON p.CategoriaId = c.Id
                        WHERE p.Id = @id";
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Producto
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!,
                    Precio = (decimal)reader["Precio"],
                    Stock = (int)reader["Stock"],
                    CategoriaId = (int)reader["CategoriaId"],
                    CategoriaNombre = reader["CategoriaNombre"].ToString()!
                };
            }
            return null;
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

        public void ActualizarProducto(Producto p)
        {
            using var con = new SqlConnection(_conn);
            con.Open();
            var cmd = new SqlCommand(
                "UPDATE Productos SET Nombre = @n, Precio = @p, Stock = @s, CategoriaId = @c WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@n", p.Nombre);
            cmd.Parameters.AddWithValue("@p", p.Precio);
            cmd.Parameters.AddWithValue("@s", p.Stock);
            cmd.Parameters.AddWithValue("@c", p.CategoriaId);
            cmd.Parameters.AddWithValue("@id", p.Id);
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