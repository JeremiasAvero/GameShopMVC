using TiendaElectronica.Models;
using System.Data.SqlClient;
using System.Data;

namespace TiendaElectronica.Data
{
    public class UsuarioDatos
    {
        public List<UsuarioModel> ListarUsuarios()
        {
            var lista = new List<UsuarioModel>();
            var datos = new DatosConexion();

            try
            {
                datos.Procedimiento("ListarUsuarios");
                datos.Lectura();

                while(datos.Lector.Read())
                {
                    UsuarioModel usuario = new UsuarioModel();
                    usuario.Id = (int)datos.Lector["IdUsuario"];
                    usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    usuario.Correo = (string)datos.Lector["Correo"];
                    usuario.Password = (string)datos.Lector["Password"];
                    usuario.IdTipoUsuario = (int)datos.Lector["IdTipoUsuario"];
                    lista.Add(usuario);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
            //using (var conexion = new SqlConnection(con.getCadenaSql()))
            //{
            //    conexion.Open();
            //    SqlCommand cmd = new SqlCommand("ListarUsuarios", conexion);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    using (var dr = cmd.ExecuteReader())
            //    {
            //        while (dr.Read())
            //        {
            //            oLista.Add(new UsuarioModel
            //            {
            //                Id = Convert.ToInt32(dr["IdUsuario"]),
            //                NombreUsuario = dr["NombreUsuario"].ToString(),
            //                Correo = dr["Correo"].ToString(),
            //                Password = dr["Password"].ToString(),
            //                IdTipoUsuario = Convert.ToInt32(dr["IdTipoUsuario"]),
            //            });
            //        }
            //    }

            //}
        }
        public UsuarioModel ObtenerUsuario(int idUser)
        {
            var datos = new DatosConexion();
            UsuarioModel usuario = new UsuarioModel();
            try
            {
                datos.Procedimiento("ObtenerUsuario");
                datos.Parametro("IdUsuario", idUser);
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["IdUsuario"];
                    usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    usuario.Correo = (string)datos.Lector["Correo"];
                    usuario.Password = (string)datos.Lector["Password"];
                    usuario.IdTipoUsuario = (int)datos.Lector["IdTipoUsuario"];

                }
                return usuario;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
            
           

            //using (var conexion = new SqlConnection(datos.getCadenaSql()))
            //{
            //    conexion.Open();
            //    SqlCommand cmd = new SqlCommand("ObtenerUsuario", conexion);
            //    cmd.Parameters.AddWithValue("IdUsuario", idUser);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    using (var dr = cmd.ExecuteReader())
            //    {
            //        while (dr.Read())
            //        {
            //            Usuario.Id = Convert.ToInt32(dr["IdUsuario"]);
            //            Usuario.NombreUsuario = dr["NombreUsuario"].ToString();
            //            Usuario.Correo = dr["Correo"].ToString();
            //            Usuario.Password = dr["Password"].ToString();
            //            Usuario.IdTipoUsuario = Convert.ToInt32(dr["IdTipoUsuario"]);
            //        }
            //    }

            //}
            //return Usuario;
        }

        public bool RegistrarUsuario(UsuarioModel usuario)
        {
            bool rpta;
            var datos = new DatosConexion();
            try
            {
                
                datos.Procedimiento("RegistrarUsuario");
                datos.Parametro("NombreUsuario", usuario.NombreUsuario);
                datos.Parametro("Correo", usuario.Correo);
                datos.Parametro("Password", usuario.Password);
                datos.Parametro("IdTipoUsuario", usuario.IdTipoUsuario);

                datos.ExecuteNonQuery();
                
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message; 
                rpta = false;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return rpta;

            //var con = new DatosConexion();
            //using (var conexion = new SqlConnection(con.getCadenaSql()))
            //{
            //    conexion.Open();
            //    SqlCommand cmd = new SqlCommand("InsertarUsuario", conexion);
            //    cmd.Parameters.AddWithValue("NombreUsuario", rUsuario.NombreUsuario);
            //    cmd.Parameters.AddWithValue("Correo", rUsuario.Correo);
            //    cmd.Parameters.AddWithValue("Password", rUsuario.Password);
            //    cmd.Parameters.AddWithValue("IdTipoUsuario", rUsuario.IdTipoUsuario);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.ExecuteNonQuery();
            //}
            //rpta = true;
        }
        public bool EditarUsuario(UsuarioModel usuario)
        {
            bool rpta;
            var datos = new DatosConexion();
            try
            {
                datos.Procedimiento("EditarUsuario");
                datos.Parametro("IdUsuario", usuario.Id);
                datos.Parametro("NombreUsuario", usuario.NombreUsuario);
                datos.Parametro("Correo", usuario.Correo);
                datos.Parametro("Password", usuario.Password);
                datos.Parametro("IdTipoUsuario", usuario.IdTipoUsuario);

                datos.ExecuteNonQuery();

                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return rpta;
            //var con = new DatosConexion();
            //using (var conexion = new SqlConnection(con.getCadenaSql()))
            //{
            //    conexion.Open();
            //    SqlCommand cmd = new SqlCommand("EditarUsuario", conexion);
            //    cmd.Parameters.AddWithValue("IdUsuario", rUsuario.Id);
            //    cmd.Parameters.AddWithValue("NombreUsuario", rUsuario.NombreUsuario);
            //    cmd.Parameters.AddWithValue("Correo", rUsuario.Correo);
            //    cmd.Parameters.AddWithValue("Password", rUsuario.Password);
            //    cmd.Parameters.AddWithValue("IdTipoUsuario", rUsuario.IdTipoUsuario);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.ExecuteNonQuery();
            //}
            //rpta = true;
        }

        public bool EliminarUsuario(int id)
        {
            bool rpta;
            DatosConexion datos = new DatosConexion();
            try
            {
               
                datos.Procedimiento("EliminarUsuario");
                datos.Parametro("IdUsuario", id);
                datos.ExecuteNonQuery() ;
                rpta = true;
            }
            catch (Exception e)
            {

                string error = e.Message;
                rpta = false;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return rpta;
            //var con = new DatosConexion();
            //using (var conexion = new SqlConnection(con.getCadenaSql()))
            //{
            //    conexion.Open();
            //    SqlCommand cmd = new SqlCommand("EliminarUsuario", conexion);
            //    cmd.Parameters.AddWithValue("Id", id);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.ExecuteNonQuery();
            //}
            //rpta = true;

        }
        public UsuarioModel LoguearUsuario(string nombre, string contraseña)
        {
         
            DatosConexion datos = new DatosConexion();
            UsuarioModel usuario = new UsuarioModel();  
            try
            {

                datos.Procedimiento("LoguearUsuario");
                datos.Parametro("NombreUsuario", nombre);
                datos.Parametro("Password", contraseña);
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["IdUsuario"];
                    usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    usuario.Correo = (string)datos.Lector["Correo"];
                    usuario.Password = (string)datos.Lector["Password"];
                    usuario.IdTipoUsuario = (int)datos.Lector["IdTipoUsuario"];
                }
                
              
            }
            catch (Exception e)
            {

                string error = e.Message;
                
            }
            finally
            {
                datos.CerrarConexion();
            }
            return usuario;

        }

        public bool ValidarCorreoNombre(string? correo, string? nombreUsuario)
        {
            DatosConexion datos = new DatosConexion();
            UsuarioModel usuario = new UsuarioModel();
            bool rpta = false;
            try
            {

                datos.Procedimiento("ValidarCorreoNombre");
                datos.Parametro("NombreUsuario", nombreUsuario);
                datos.Parametro("Password", correo);
                datos.Lectura();

                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["id"];
                }

                if (usuario.Id > 0 && usuario != null)
                    rpta = true;
                else
                    rpta = false;   

            }
            catch (Exception e)
            {

                string error = e.Message;

            }
            finally
            {
                datos.CerrarConexion();
            }
            return rpta;
        }
    }
}
