using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace CRUDProducto.Models
{
	public class admProducto
	{
		SqlConnection conexion;
		public void Conectar()
		{
			string stringConexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();
			conexion = new SqlConnection(stringConexion);
		}

		public List<Producto> TraerProductos()
		{
			Conectar();

			SqlCommand sentencia = new SqlCommand("select * from Productos", conexion);


			conexion.Open();

			List<Producto> productos = new List<Producto>();

			SqlDataReader registros = sentencia.ExecuteReader();

			while (registros.Read())
			{
				Producto producto = new Producto
				{
					Id = int.Parse(registros["Id"].ToString()),
					Nombre = registros["Nombre"].ToString(),
					Descripcion = registros["Descripcion"].ToString(),
					Precio = decimal.Parse(registros["Precio"].ToString()),
					Stock = int.Parse(registros["Stock"].ToString()),

				};

				productos.Add(producto);
			}

			return productos;

		}

		public int CrearProducto(Producto producto)
		{
			Conectar();

			SqlCommand sentencia = new SqlCommand("insert into Productos (Nombre,Descripcion,Precio,Stock) values (@nombre,@descripcion,@precio,@stock)", conexion);

			sentencia.Parameters.Add("@nombre", SqlDbType.Text);
			sentencia.Parameters.Add("@descripcion", SqlDbType.Text);
			sentencia.Parameters.Add("@precio", SqlDbType.Decimal);
			sentencia.Parameters.Add("@stock", SqlDbType.Int);

			sentencia.Parameters["@nombre"].Value = producto.Nombre;
			sentencia.Parameters["@descripcion"].Value = producto.Descripcion;
			sentencia.Parameters["@Precio"].Value = producto.Precio;
			sentencia.Parameters["@Stock"].Value = producto.Stock;

			conexion.Open();

			int i = sentencia.ExecuteNonQuery();

			conexion.Close();

			return i;
		}

		public Producto MostrarProducto(int Id)
		{
			Conectar();

			SqlCommand sentencia = new SqlCommand("select * from  Productos where Id=@id", conexion);

			sentencia.Parameters.Add("@id", SqlDbType.Int);
			sentencia.Parameters["@id"].Value = Id;
			conexion.Open();
			Producto producto = new Producto();
			SqlDataReader registro = sentencia.ExecuteReader();
			while (registro.Read())
			{
				producto.Id = Id;
				producto.Nombre = registro["Nombre"].ToString();
				producto.Descripcion = registro["Descripcion"].ToString();
				producto.Precio = decimal.Parse(registro["Precio"].ToString());
				producto.Stock = int.Parse(registro["Stock"].ToString());

			}
			conexion.Close();
			return producto;
		}

		public int EditarProducto(Producto producto)
		{
			Conectar();

			SqlCommand sentencia = new SqlCommand("update Productos set Nombre=@nombre,Descripcion=@descripcion,Precio=@precio,Stock=@stock where Id=@id", conexion);

            sentencia.Parameters.Add("@Id", SqlDbType.Int);
            sentencia.Parameters.Add("@nombre", SqlDbType.Text);
			sentencia.Parameters.Add("@descripcion", SqlDbType.Text);
			sentencia.Parameters.Add("@precio", SqlDbType.Decimal);
			sentencia.Parameters.Add("@stock", SqlDbType.Int);

			sentencia.Parameters["@nombre"].Value = producto.Nombre;
			sentencia.Parameters["@descripcion"].Value = producto.Descripcion;
			sentencia.Parameters["@Precio"].Value = producto.Precio;
			sentencia.Parameters["@Stock"].Value = producto.Stock;
            sentencia.Parameters["@Id"].Value = producto.Id;

            conexion.Open();
			int i = sentencia.ExecuteNonQuery();
			conexion.Close();

			return i;
		}

        public int EliminarProducto(int? Id)
        {
            Conectar();

            SqlCommand sentencia = new SqlCommand("delete  from  Productos where Id=@id", conexion);

            sentencia.Parameters.Add("@id", SqlDbType.Int);
            sentencia.Parameters["@id"].Value = Id;
            conexion.Open();
            int i = sentencia.ExecuteNonQuery();
            conexion.Close();

			return i;
        }
    }
}