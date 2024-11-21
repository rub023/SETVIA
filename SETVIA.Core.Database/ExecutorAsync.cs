using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using SETVIA.Util.Api.Model.Parameter;
using SETVIA.Util.Api.Model.Parameter.Administrador;
using SETVIA.Util.Api.Model.Parameter.Empresa;
using SETVIA.Util.Api.Model.Parameter.Personal;
using SETVIA.Util.Api.Model.Response;
using SETVIA.Util.Api.Model.Parameter.CostoInfraccion;
using SETVIA.Util.Api.Model.Parameter.Feriado;
using SETVIA.Util.Api.Model.Parameter.Parqueo;
using SETVIA.Util.Api.Model.Parameter.Horario;
using SETVIA.Util.Api.Model.Parameter.TipoPago;
using SETVIA.Util.Api.Model.Parameter.Tarifario;
using SETVIA.Util.Api.Model.Parameter.DetalleParqueo;
using SETVIA.Util.Api.Model.Parameter.ExtensionParqueo;
using SETVIA.Util.Api.Model.Parameter.Inmovilizado;
using SETVIA.Util.Api.Model.Parameter.Remolcado;
using SETVIA.Util.Api.Model.Parameter.DEudaPAgos;
using SETVIA.Util.Api.Model.Response.DetalleParqueo;
using SETVIA.Util.Api.Model.Response.DeudaInmovilizado;
using SETVIA.Util.Api.Model.Parameter.PagoExtension;
using SETVIA.Util.Api.Model.Parameter.PagoInmovil;
using SETVIA.Util.Api.Model.Parameter.PagoRemolcado;
using SETVIA.Util.Api.Model.Parameter.Extorno;
using SETVIA.Util.Api;

namespace SETVIA.Core.Database
{
    public class ExecutorAsync
    {
        public string Error { get; set; }
        public List<StoreProcedureAsync> Items { get; set; }
        public ExecutorAsync()
        {
            Items = new List<StoreProcedureAsync>();
        }
        public async Task<bool> Run(string connectionString, int timeout)
        {
            if (Items.Count > 0)
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                try
                {
                    foreach (var item in Items)
                    {
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(item.Name, sqlConnection);
                        sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDataAdapter.SelectCommand.CommandTimeout = timeout;
                        foreach (var item1 in item.Items)
                        {
                            if (item1.Value == null)
                                sqlDataAdapter.SelectCommand.Parameters.AddWithValue(item1.Name, DBNull.Value);
                            else
                                sqlDataAdapter.SelectCommand.Parameters.AddWithValue(item1.Name, item1.Value);
                        }
                        sqlDataAdapter.SelectCommand.Transaction = sqlTransaction;
                        await sqlDataAdapter.SelectCommand.ExecuteNonQueryAsync();
                    }
                    sqlTransaction.Commit();
                    Error = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                    sqlTransaction.Rollback();
                    Error = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    public class StoreProcedureAsync
    {
        public string Name { get; set; }
        public List<Parameters> Items { get; set; }
        public string Error { get; set; }

        public StoreProcedureAsync(string name)
        {
            Name = name;
            Items = new List<Parameters>();
        }

        public void AddParameterAsync(string name, object value)
        {
            Items.Add(new Parameters(name, value));
        }

        public async Task<DataTable> ReturnData(string connectionString, int timeOut)
        {
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;

            foreach (var item in Items)
            {
                if (item.Value == null)
                    cmd.Parameters.AddWithValue(item.Name, DBNull.Value);
                else
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
            }

            try
            {
                await sqlConnection.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }

                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return dataTable;
        }

        public async Task<Tuple<DataTable, int>> ReturnRegistrarAdmin(string connectionString, int timeOut, CreateAdministradorParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Perfil", (object)param.Id_Perfil);
            cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);            
            cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
            cmd.Parameters.AddWithValue("@Id_Admin", (object)param.Id_Admin);
            cmd.Parameters.AddWithValue("@Nombres", (object)param.Nombres);
            cmd.Parameters.AddWithValue("@Ap_Paterno", (object)param.Ap_Paterno);
            cmd.Parameters.AddWithValue("@Ap_Materno", (object)param.Ap_Materno);
            cmd.Parameters.AddWithValue("@CI", (object)param.CI);
            cmd.Parameters.AddWithValue("@Extension", (object)param.Extension);
            cmd.Parameters.AddWithValue("@Complemento", (object)param.Complemento);
            cmd.Parameters.AddWithValue("@Genero", (object)param.Genero);
            cmd.Parameters.AddWithValue("@Direccion", (object)param.Direccion);
            cmd.Parameters.AddWithValue("@Celular", (object)param.Celular);
            cmd.Parameters.AddWithValue("@Correo", (object)param.Correo);
            //cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }
        //public async Task<Tuple<DataTable, int>> ReturnRegistrarAdminLogin(string connectionString, int timeOut, CreateAdminLoginParameter param)
        //{
        //    int estadoIni = -1;
        //    DataTable dataTable = new DataTable();
        //    SqlConnection sqlConnection = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand(Name, sqlConnection);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandTimeout = timeOut;
        //    DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
        //    cmd.Parameters.AddWithValue("@Id_Login", (object)param.Id_Login);
        //    cmd.Parameters.AddWithValue("@Id_Administrador", (object)param.Id_Administrador);
        //    cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
        //    cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
        //    cmd.Parameters.AddWithValue("@Us_Creacion", (object)param.Us_Creacion);
        //    DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
        //    parameter2.DbType = DbType.Int32;
        //    parameter2.ParameterName = "@Existe";
        //    parameter2.Value = (object)estadoIni;
        //    parameter2.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add((object)parameter2);
        //    try
        //    {
        //        await sqlConnection.OpenAsync();
        //        new SqlDataAdapter(cmd).Fill(dataTable);
        //        estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
        //        Error = string.Empty;
        //    }
        //    catch (SqlException ex)
        //    {
        //        //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
        //        Error = ex.Message;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //    return new Tuple<DataTable, int>(dataTable,estadoIni);
        //}
        

        //public async Task<Tuple<DataTable, int>> ReturnRegistrarEmpresLogin(string connectionString, int timeOut, CreateEmpresaLoginParameter param)
        //{
        //    int estadoIni = -1;
        //    DataTable dataTable = new DataTable();
        //    SqlConnection sqlConnection = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand(Name, sqlConnection);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandTimeout = timeOut;
        //    DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
        //    cmd.Parameters.AddWithValue("@Id_Login", (object)param.Id_Login);
        //    cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);
        //    cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
        //    cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
        //    cmd.Parameters.AddWithValue("@Us_Creacion", (object)param.Us_Creacion);
        //    DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
        //    parameter2.DbType = DbType.Int32;
        //    parameter2.ParameterName = "@Existe";
        //    parameter2.Value = (object)estadoIni;
        //    parameter2.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add((object)parameter2);
        //    try
        //    {
        //        await sqlConnection.OpenAsync();
        //        new SqlDataAdapter(cmd).Fill(dataTable);
        //        estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
        //        Error = string.Empty;
        //    }
        //    catch (SqlException ex)
        //    {
        //        //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
        //        Error = ex.Message;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //    return new Tuple<DataTable, int>(dataTable, estadoIni);
        //}

        //public async Task<Tuple<DataTable, int>> ReturnRegistrarPersonalLogin(string connectionString, int timeOut, CreatePersonaLoginParameter param)
        //{
        //    int estadoIni = -1;
        //    DataTable dataTable = new DataTable();
        //    SqlConnection sqlConnection = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand(Name, sqlConnection);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandTimeout = timeOut;
        //    DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
        //    cmd.Parameters.AddWithValue("@Id_Login", (object)param.Id_Login);
        //    cmd.Parameters.AddWithValue("@Id_Personal", (object)param.Id_Personal);
        //    cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
        //    cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
        //    cmd.Parameters.AddWithValue("@Us_Creacion", (object)param.Us_Creacion);
        //    DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
        //    parameter2.DbType = DbType.Int32;
        //    parameter2.ParameterName = "@Existe";
        //    parameter2.Value = (object)estadoIni;
        //    parameter2.Direction = ParameterDirection.Output;
        //    cmd.Parameters.Add((object)parameter2);
        //    try
        //    {
        //        await sqlConnection.OpenAsync();
        //        new SqlDataAdapter(cmd).Fill(dataTable);
        //        estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
        //        Error = string.Empty;
        //    }
        //    catch (SqlException ex)
        //    {
        //        //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
        //        Error = ex.Message;
        //    }
        //    finally
        //    {
        //        sqlConnection.Close();
        //    }
        //    return new Tuple<DataTable, int>(dataTable, estadoIni);
        //}

        public async Task<Tuple<DataTable, int>> ReturnRegistrarEmpresa(string connectionString, int timeOut, CreateEmpresaParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Perfil", (object)param.Id_Perfil);
            cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);

            cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);
            cmd.Parameters.AddWithValue("@Id_Admin", (object)param.Id_Admin);
            //cmd.Parameters.AddWithValue("@Id_Usuario", (object)param.Id_Usuario);
            cmd.Parameters.AddWithValue("@Nombre_Empresa", (object)param.Nombre_Empresa);
            cmd.Parameters.AddWithValue("@Representante", (object)param.Representante);
            cmd.Parameters.AddWithValue("@Nit", (object)param.Nit);
            cmd.Parameters.AddWithValue("@Direccion", (object)param.Direccion);
            cmd.Parameters.AddWithValue("@Correo", (object)param.Correo);
            cmd.Parameters.AddWithValue("@Telefono", (object)param.Telefono);
            //cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Us_Creacion", (object)param.Us_Creacion);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }


        public async Task<Tuple<DataTable, int>> ReturnRegistrarPersonal(string connectionString, int timeOut, CreatePersonalParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Perfil", (object)param.Id_Perfil);
            cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);

            cmd.Parameters.AddWithValue("@Id_Personal", (object)param.Id_Personal);
            cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);
            //cmd.Parameters.AddWithValue("@Id_Usuario", (object)param.Id_Usuario);
           // cmd.Parameters.AddWithValue("@Id_Tipo_Personal", (object)param.Id_Tipo_Personal);
            cmd.Parameters.AddWithValue("@Nombres", (object)param.Nombres);
            cmd.Parameters.AddWithValue("@Ap_Paterno", (object)param.Ap_Paterno);
            cmd.Parameters.AddWithValue("@Ap_Materno", (object)param.Ap_Materno);
            cmd.Parameters.AddWithValue("@CI", (object)param.CI);
            cmd.Parameters.AddWithValue("@Extension", (object)param.Extension);
            cmd.Parameters.AddWithValue("@Complemento", (object)param.Complemento);
            cmd.Parameters.AddWithValue("@Genero", (object)param.Genero);
            cmd.Parameters.AddWithValue("@Direccion", (object)param.Direccion);
            cmd.Parameters.AddWithValue("@Correo", (object)param.Correo);
            cmd.Parameters.AddWithValue("@Celular", (object)param.Celular);

            //cmd.Parameters.AddWithValue("@Us_Creacion", (object)param.Us_Creacion );
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }




        public async Task<Tuple<DataTable, int>> ReturnRegistrarCostoInfraccion(string connectionString, int timeOut, CreateCostoInfraccionParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Costo_Infraccion", (object)param.Id_Costo_Infraccion);
            cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);
            cmd.Parameters.AddWithValue("@Id_Tipo_Infraccion", (object)param.Id_Tipo_Infraccion);
            cmd.Parameters.AddWithValue("@Costo_Multa", (object)param.Costo_Multa);
            cmd.Parameters.AddWithValue("@Costo_Servicio", (object)param.Costo_Servicio);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
            //cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }


        public async Task<Tuple<DataTable, int>> ReturnUpdateAdministrador(string connectionString, int timeOut, UpdateAdministradorParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Nombres", (object)param.Nombres);
            cmd.Parameters.AddWithValue("@Ap_PAterno", (object)param.Ap_Paterno);
            cmd.Parameters.AddWithValue("@Ap_Materno", (object)param.Ap_Materno);
            cmd.Parameters.AddWithValue("@CI", (object)param.CI);
            cmd.Parameters.AddWithValue("@Extension", (object)param.Extension);
            cmd.Parameters.AddWithValue("@Complemento", (object)param.Complemento);
            cmd.Parameters.AddWithValue("@Genero", (object)param.Genero);
            cmd.Parameters.AddWithValue("@Direccion", (object)param.Direccion);
            cmd.Parameters.AddWithValue("@Celular", (object)param.Celular);
            cmd.Parameters.AddWithValue("@Correo", (object)param.Correo);            
            cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacionadmin);
            cmd.Parameters.AddWithValue("@Id_Usuario", (object)param.Id_Usuario);
            cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@EstadoUsuario", (object)param.EstadoUsuario);
            //cmd.Parameters.AddWithValue("@Usuario_Modificacion_Usuario", (object)param.Usuario_ModificacionUsuario);
            cmd.Parameters.AddWithValue("@Id_Admin", (object)param.Id_Admin);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }




        public async Task<Tuple<DataTable, int>> ReturnUpdateEmpresa(string connectionString, int timeOut, UpdateEmpresaParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Admin", (object)param.Id_Admin);
            cmd.Parameters.AddWithValue("@Id_Usuario", (object)param.Id_Usuario);
            cmd.Parameters.AddWithValue("@Nombre_Empresa", (object)param.Nombre_Empresa);
            cmd.Parameters.AddWithValue("@Representante", (object)param.Representante);
            cmd.Parameters.AddWithValue("@Nit", (object)param.Nit);
            cmd.Parameters.AddWithValue("@Direccion", (object)param.Direccion);
            cmd.Parameters.AddWithValue("@Correo", (object)param.Correo);
            cmd.Parameters.AddWithValue("@Telefono", (object)param.Telefono);
            cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
           
            cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            cmd.Parameters.AddWithValue("@Id_Perfil", (object)param.Id_Perfil);
            //cmd.Parameters.AddWithValue("@EstadoUsuario", (object)param.EstadoUsuario);
            cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);   
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        public async Task<Tuple<DataTable, int>> ReturnUpdateEmpresaEstado(string connectionString, int timeOut, UpdateEmpresaEstadoParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            cmd.Parameters.AddWithValue("@Us_Modificacion", (object)param.Us_Modificacion);
            cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }


        public async Task<Tuple<DataTable, int>> ReturnUpdatePersonalActivo(string connectionString, int timeOut, UpdatePersonalParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);
            cmd.Parameters.AddWithValue("@Id_Usuario", (object)param.Id_Usuario);
            cmd.Parameters.AddWithValue("@Id_Tipo_Personal", (object)param.Id_Tipo_Personal);
            cmd.Parameters.AddWithValue("@Nombres", (object)param.Nombres);
            cmd.Parameters.AddWithValue("@Ap_Paterno", (object)param.Ap_Paterno);
            cmd.Parameters.AddWithValue("@Ap_Materno", (object)param.Ap_Materno);
            cmd.Parameters.AddWithValue("@CI", (object)param.CI);
            cmd.Parameters.AddWithValue("@Extension", (object)param.Extension);
            cmd.Parameters.AddWithValue("@Complemento", (object)param.Complemento);
            cmd.Parameters.AddWithValue("@Genero", (object)param.Genero);
            cmd.Parameters.AddWithValue("@Direccion", (object)param.Direccion);
            cmd.Parameters.AddWithValue("@Correo", (object)param.Correo);
            cmd.Parameters.AddWithValue("@Celular", (object)param.Celular);
            cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
           
           

            cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Id_Perfil", (object)param.Id_Perfil);
            //cmd.Parameters.AddWithValue("@EstadoUsuario", (object)param.EstadoUsuario);
            cmd.Parameters.AddWithValue("@Id_Personal", (object)param.Id_Personal);

            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        public async Task<Tuple<DataTable, int>> ReturnUpdateCostoInfraccion(string connectionString, int timeOut, UpdateCostoInfraccionParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);
            cmd.Parameters.AddWithValue("@Id_Tipo_Infraccion", (object)param.Id_Tipo_Infraccion);
            cmd.Parameters.AddWithValue("@Costo_Multa", (object)param.Costo_Multa);
            cmd.Parameters.AddWithValue("@Costo_Servicio", (object)param.Costo_Servicio);
            cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            cmd.Parameters.AddWithValue("@Id_Costo_Infraccion", (object)param.Id_Costo_Infraccion);

            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }



        //Feriado
        public async Task<Tuple<DataTable, int>> ReturnRegistrarDFeriado(string connectionString, int timeOut, CreateFeriadoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Feriado", (object)param.Id_Feriado);
            cmd.Parameters.AddWithValue("@Dias", (object)param.Dias);
            cmd.Parameters.AddWithValue("@Detalle", (object)param.Detalle);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
           
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }

        public async Task<Tuple<DataTable, int>> ReturnUpdateDiaFeriado(string connectionString, int timeOut, UpdateFeriadoParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Feriado", (object)param.Id_Feriado);
            cmd.Parameters.AddWithValue("@Dias", (object)param.Dias);
            cmd.Parameters.AddWithValue("@Detalle", (object)param.Detalle);
            cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);

            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        //PARQUEO
        public async Task<Tuple<DataTable, int>> ReturnRegistrarParqueo(string connectionString, int timeOut, CreateParqueoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Parqueo", (object)param.Id_Parqueo);
            cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);
            cmd.Parameters.AddWithValue("@Codigo_Parqueo", (object)param.Codigo_Parqueo);
            cmd.Parameters.AddWithValue("@Ciudad", (object)param.Ciudad);
            cmd.Parameters.AddWithValue("@NombreParqueo", (object)param.NombreParqueo);
            cmd.Parameters.AddWithValue("@Direccion", (object)param.Direccion);
            cmd.Parameters.AddWithValue("@Ubicacion", (object)param.Ubicacion);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
            //cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }
        public async Task<Tuple<DataTable, int>> ReturnUpdateParqueo(string connectionString, int timeOut, UpdateParqueoParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Empresa", (object)param.Id_Empresa);
            cmd.Parameters.AddWithValue("@Codigo_Parqueo", (object)param.Codigo_Parqueo);
            cmd.Parameters.AddWithValue("@Ciudad", (object)param.Ciudad);
            cmd.Parameters.AddWithValue("@NombreParqueo", (object)param.NombreParqueo);
            cmd.Parameters.AddWithValue("@Direccion", (object)param.Direccion);
            cmd.Parameters.AddWithValue("@Ubicacion", (object)param.Ubicacion);
            cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);

          
            cmd.Parameters.AddWithValue("@Id_Parqueo", (object)param.Id_Parqueo);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }



        ///Horario
        public async Task<Tuple<DataTable, int>> ReturnRegistrarHorario(string connectionString, int timeOut, CreateHorarioParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Horario", (object)param.Id_Horario);
            //cmd.Parameters.AddWithValue("@Id_Feriado", (object)param.Id_Feriado);
            cmd.Parameters.AddWithValue("@Id_Parqueo", (object)param.Id_Parqueo);
            cmd.Parameters.AddWithValue("@DiaSemana", (object)param.DiaSemana);
            cmd.Parameters.AddWithValue("@Horario_Inicio", (object)param.Horario_Inicio);
            cmd.Parameters.AddWithValue("@Horario_Fin", (object)param.Horario_Fin);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
            //cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }

        public async Task<Tuple<DataTable, int>> ReturnUpdateHorario(string connectionString, int timeOut, UpdateHorarioParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Parqueo", (object)param.Id_Parqueo);
            cmd.Parameters.AddWithValue("@DiaSemana", (object)param.DiaSemana);
            cmd.Parameters.AddWithValue("@Horario_Inicio", (object)param.Horario_Inicio);
            cmd.Parameters.AddWithValue("@Horario_Fin", (object)param.Horario_Fin);
            cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);

          
            cmd.Parameters.AddWithValue("@Id_Horario", (object)param.Id_Horario);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        //tipo pago
        public async Task<Tuple<DataTable, int>> ReturnRegistrarTipoPago(string connectionString, int timeOut, CreateTipoPagoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_TipoPago", (object)param.Id_TipoPago);
            cmd.Parameters.AddWithValue("@Detalle", (object)param.Detalle);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
            //cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }
        //tarifario


        public async Task<Tuple<DataTable, int>> ReturnRegistrarTarifario(string connectionString, int timeOut, CreateTarifarioParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Tarifario", (object)param.Id_Tarifario);
            cmd.Parameters.AddWithValue("@Id_TipoVehiculo", (object)param.Id_TipoVehiculo);
            cmd.Parameters.AddWithValue("@Id_TipoTarifario", (object)param.Id_TipoTarifario);
            cmd.Parameters.AddWithValue("@Tiempo", (object)param.Tiempo);
            cmd.Parameters.AddWithValue("@CostoTarifario", (object)param.CostoTarifario);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }

        //update tarifario
        public async Task<Tuple<DataTable, int>> ReturnUpdateTarifario(string connectionString, int timeOut, UpdateTarifarioParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_TipoVehiculo", (object)param.Id_TipoVehiculo);
            cmd.Parameters.AddWithValue("@Id_TipoTarifario", (object)param.Id_TipoTarifario);
            cmd.Parameters.AddWithValue("@Tiempo", param.Tiempo);
            cmd.Parameters.AddWithValue("@CostoTarifario", (object)param.CostoTarifario);
            cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            cmd.Parameters.AddWithValue("@Id_Tarifario", (object)param.Id_Tarifario);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        //para primer paso de gestion de uso de parqueo
        public async Task<Tuple<DataTable, int>> ReturnRegistrarDetalleUsoParqueo(string connectionString, int timeOut, CreateDParqueoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Codigo_Parqueo", (object)param.Codigo_Parqueo);
            cmd.Parameters.AddWithValue("@Tipo_Vehiculo", (object)param.Tipo_Vehiculo);
            cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            // cmd.Parameters.AddWithValue("@Id_TipoPago", (object)param.Id_TipoPago);
            //  cmd.Parameters.AddWithValue("@Id_Tarifario", (object)param.Id_Tarifario);
            //cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            //cmd.Parameters.AddWithValue("@Tiempo_Ocupar", (object)param.Tiempo_Ocupar);
            //cmd.Parameters.AddWithValue("@Costo_Parqueo", (object)param.Costo_Parqueo);
            //cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
            //cmd.Parameters.AddWithValue("@Codigo_Pago", (object)param.Codigo_Pago);
            //cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }


        /// PARA INSERTAR EN EXTENSION DE PARQUEO
        /// 
        



        /// <returns></returns>
        //prueba
        public async Task<Tuple<DataTable, int>> ReturnRegistrarusotiempo(string connectionString, int timeOut, CreatePruebaUsoDetalleParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Id_Parqueo", (object)param.Id_Parqueo);
            cmd.Parameters.AddWithValue("@Id_TipoVehiculo", (object)param.Id_TipoVehiculo);
            cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
             //cmd.Parameters.AddWithValue("@Id_TipoPago", (object)param.Id_TipoPago);
             cmd.Parameters.AddWithValue("@Id_Tarifario", (object)param.Id_Tarifario);
            //cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            //cmd.Parameters.AddWithValue("@Tiempo_Ocupar", (object)param.Tiempo_Ocupar);
           // cmd.Parameters.AddWithValue("@Costo_Parqueo", (object)param.Costo_Parqueo);
            cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
            //cmd.Parameters.AddWithValue("@Codigo_Pago", (object)param.Codigo_Pago);
            //cmd.Parameters.AddWithValue("@Usuario", (object)param.Usuario);
            //cmd.Parameters.AddWithValue("@Contrasena", (object)param.Contrasena);
            //cmd.Parameters.AddWithValue("@Estado", (object)param.Estado);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }

        //update tiempo
        public async Task<Tuple<DataTable, int>> ReturnUpdateTiempoPsrqueo(string connectionString, int timeOut, UpdateTiempoUsoParqueoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Id_Tarifario", param.Id_Tarifario);
            SqlParameter outputParam = new SqlParameter("@Existe", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = outputParam.Value != DBNull.Value ? Convert.ToInt32(outputParam.Value) : -1;
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }


        //+ tiempo por Extension

        public async Task<Tuple<DataTable, int>> ReturnUpdateTiempoExtensionPsrqueo(string connectionString, int timeOut, UpdateExtensiondeTiempoParqueoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Id_Tarifario", param.Id_Tarifario);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", param.Usuario_Modificacion);
            SqlParameter outputParam = new SqlParameter("@Existe", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = outputParam.Value != DBNull.Value ? Convert.ToInt32(outputParam.Value) : -1;
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }


        public async Task<Tuple<DataTable, int>> ReturnUpdatePagoUsoPago(string connectionString, int timeOut, UpdatePagoUsoParquepParameter param)
        {
           // int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            
           // cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Id_Detalle", param.Id_Detalle);
          //  SqlParameter outputParam = new SqlParameter("@Existe", SqlDbType.Int)
            //{
            //    Direction = ParameterDirection.Output
            //};
         //   cmd.Parameters.Add(outputParam);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
               // estadoIni = outputParam.Value != DBNull.Value ? Convert.ToInt32(outputParam.Value) : -1;
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        public async Task<Tuple<DataTable, int>> ReturnUpdateVigenteAConluido(string connectionString, int timeOut, UpdateModificarAConcluidoParquepParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
           // cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        public async Task<Tuple<DataTable, int>> ReturnRegistrarNuevoExtension(string connectionString, int timeOut, CreateExtensionParqueoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Id_Extension", (object)param.Id_Extension);
            cmd.Parameters.AddWithValue("@Codigo_Parqueo", (object)param.Codigo_Parqueo);
            cmd.Parameters.AddWithValue("@Tipo_Vehiculo", (object)param.Tipo_Vehiculo);
            cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            //cmd.Parameters.AddWithValue("@Usuario_Creacion", (object)param.Usuario_Creacion);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }


        public async Task<Tuple<DataTable, int>> ReturnUpdateExtensionPagoUsoPago(string connectionString, int timeOut, UpdatePagoExtensionParquepParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Extension", (object)param.Id_Extension);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }
        /// modificar estado vigente extension a concluido 
        public async Task<Tuple<DataTable, int>> ReturnUpdateVigenteExtAConluido(string connectionString, int timeOut, UpdateModificarExtenAConcluidoParquepParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
           // cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }



        //Registar inmovilizado
        public async Task<Tuple<DataTable, int>> ReturnRegistrarInmovilizado(string connectionString, int timeOut, CreateInmovilizadoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            //cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Id_Infraccion_Inmovilizado", (object)param.Id_Infraccion_Inmovilizado);
           // cmd.Parameters.AddWithValue("@Id_Costo_Infraccion", (object)param.Id_Costo_Infraccion);
            
            //cmd.Parameters.AddWithValue("@NroTransaccion", (object)param.NroTransaccion);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }
        //pagos de inmovilizado

        public async Task<Tuple<DataTable, int>> ReturnUpdatePagoInmovilizado(string connectionString, int timeOut, UpdatePagInmovilizadoParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Infraccion_Inmovilizado", (object)param.Id_Infraccion_Inmovilizado);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        // CAMBIO ESTADO DE LIBERADO A CONCLUIDO
        public async Task<Tuple<DataTable, int>> ReturnUpdateliberadoAConluido(string connectionString, int timeOut, UpdateModificarLieradoAConcluidoParquepParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            //cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }


        //registrar remolcado
        //Registar inmovilizado
        public async Task<Tuple<DataTable, int>> ReturnRegistrarRemolcado(string connectionString, int timeOut, CreateRemolcadoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
           // cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Id_Infraccion_Remolque", (object)param.Id_Infraccion_Remolque);
           // cmd.Parameters.AddWithValue("@Id_Costo_Infraccion", (object)param.Id_Costo_Infraccion);

           // cmd.Parameters.AddWithValue("@NroTransaccion", (object)param.NroTransaccion);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }

        ///pagos de remolcado
        /// ACTUALIZA ESTADO A LIBERADO-INM

        public async Task<Tuple<DataTable, int>> ReturnUpdateRemolcado(string connectionString, int timeOut, UpdatePagoRemolcadoParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Infraccion_Remolque", (object)param.Id_Infraccion_Remolque);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        //remolcado liberado inm
        public async Task<Tuple<DataTable, int>> ReturnUpdateRemolcadoLiberado(string connectionString, int timeOut, UpdateModificarRemolcadoALiberadoParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        //para concluir
        public async Task<Tuple<DataTable, int>> ReturnUpdateRemolcadoLiberadoAConcluido(string connectionString, int timeOut, UpdateModificarRLiberadoAConcluidoParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@Usuario_Modificacion", (object)param.Usuario_Modificacion);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }
        public async Task<Tuple<DataTable, int,string>>ReturnRegistrarPAGODetalleUsoParqueo(string connectionString, int timeOut, UpdatePagoDeudaUsoParqueoParameter param)
        {
            //var response = new PagoResponse<Tuple<DataTable, string>>();
            int estadoIni = -1;
            string nroTransaccion = string.Empty; 
            //string nroTransaccion1 = string.Empty;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
           // cmd.Parameters.AddWithValue("@Id_Detalle", SqlDbType.Int).Value = param.Id_Detalle;
            cmd.Parameters.AddWithValue("@Codigo_Parqueo", SqlDbType.VarChar).Value = param.Codigo_Parqueo;
            cmd.Parameters.AddWithValue("@Tipo_Vehiculo", SqlDbType.VarChar).Value = param.Tipo_Vehiculo;
            cmd.Parameters.AddWithValue("@Placa", SqlDbType.VarChar).Value = param.Placa;
            cmd.Parameters.AddWithValue("@Tiempo", SqlDbType.Time).Value = param.Tiempo;
            cmd.Parameters.AddWithValue("@CostoTarifario", SqlDbType.Decimal).Value = param.CostoTarifario;
          //  cmd.Parameters.AddWithValue("@NroTransaccionPago", SqlDbType.VarChar).Value = param.NroTransaccionPago;
            cmd.Parameters.AddWithValue("@NIT", SqlDbType.VarChar).Value = param.NIT;
            cmd.Parameters.AddWithValue("@Razon_Social", SqlDbType.VarChar).Value = param.Razon_Social;
            cmd.Parameters.AddWithValue("@Correo", SqlDbType.VarChar).Value = param.Correo;
            cmd.Parameters.AddWithValue("@CUF", SqlDbType.VarChar).Value = param.CUF;
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            //var nroTransaccionParam = new SqlParameter("@NroTransaccion", SqlDbType.NVarChar, 50)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //cmd.Parameters.Add(nroTransaccionParam);

            // Parámetro de salida para "NroTransaccion"
            SqlParameter outputNroTransaccion = new SqlParameter("@NroTransaccion", SqlDbType.NVarChar, 50)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputNroTransaccion);

            //SqlParameter nroTransaccionParam = new SqlParameter("@NroTransaccion1", SqlDbType.NVarChar, 50)
            //{
            //    Direction = ParameterDirection.Output,
            //    //Value = nroTransaccion1
            //};
            //    cmd.Parameters.Add(nroTransaccionParam);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
               estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                nroTransaccion = outputNroTransaccion.Value?.ToString();
                //response.Data = new Tuple<DataTable, string>(dataTable, nroTransaccion);
                //response.Error = estadoIni;
                //response.MensajeError = estadoIni == 0 ? "Transacción exitosa." : "Error en la transacción.";
                //nroTransaccion1 = cmd.Parameters["@NroTransaccion1"].Value.ToString();
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
                throw new Exception($"Error en la base de datos: {ex.Message}", ex);
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int,string>(dataTable, estadoIni,nroTransaccion);
        }


        public async Task<Tuple<DataTable, int>> ReturnRegistrarPagoExtensionUsoParqueo(string connectionString, int timeOut, UpdatePagoExtensionUsoParqueoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            //  cmd.Parameters.AddWithValue("@Id_Extension", (object)param.Id_Extension);

            cmd.Parameters.AddWithValue("@Codigo_Parqueo", SqlDbType.VarChar).Value = param.Codigo_Parqueo;
            cmd.Parameters.AddWithValue("@Tipo_Vehiculo", SqlDbType.VarChar).Value = param.Tipo_Vehiculo;
            cmd.Parameters.AddWithValue("@Placa", SqlDbType.VarChar).Value = param.Placa;

            //cmd.Parameters.AddWithValue("@Codigo_Parqueo", (object)param.Codigo_Parqueo);
            //cmd.Parameters.AddWithValue("@Tipo_Vehiculo", (object)param.Tipo_Vehiculo);
            //cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@Tiempo_Extension", SqlDbType.VarChar).Value = param.Tiempo_Extension;
            cmd.Parameters.AddWithValue("@Costo_Extension", SqlDbType.VarChar).Value = param.Costo_Extension;
            cmd.Parameters.AddWithValue("@NroTransaccionPago", SqlDbType.VarChar).Value = param.NroTransaccionPago;
            cmd.Parameters.AddWithValue("@NIT", SqlDbType.VarChar).Value = param.NIT;
            cmd.Parameters.AddWithValue("@Razon_Social", SqlDbType.VarChar).Value = param.Razon_Social;
            cmd.Parameters.AddWithValue("@Correo", SqlDbType.VarChar).Value = param.Correo;
            cmd.Parameters.AddWithValue("@CUF", SqlDbType.VarChar).Value = param.CUF;
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }





        ///REalizar PAgos de deudas por Uso de 
        ///
        public async Task<Tuple<DataTable,int>> ReturnUpdatePagodeudaUsoPago(string connectionString, int timeOut, UpdatePagoDeudaUsoParqueoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            // cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Placa", param.Placa);
           // cmd.Parameters.AddWithValue("@NroTransaccionPago", param.NroTransaccionPago);
            //cmd.Parameters.AddWithValue("@Codigo_Parqueo", param.Codigo_Parqueo);

            //foreach (var deuda in param.ListaDeudas)
            //{
            //    cmd.Parameters.AddWithValue("@Id_Tarifario", deuda.Id); 
            //    cmd.Parameters.AddWithValue("@Costo_Parqueo", deuda.Monto);
            //}
                SqlParameter outputParam = new SqlParameter("@Existe", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = outputParam.Value != DBNull.Value ? Convert.ToInt32(outputParam.Value) : -1;
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }
        /// <summary>
        /// 
        /// 

        public async Task<Tuple<DataTable, int>> ReturnUpdatePagodeudaUsoPagoIR(string connectionString, int timeOut, UpdatePagoInfracionUsoParqueoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            // cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Placa", param.Placa);
            cmd.Parameters.AddWithValue("@NroTransaccion", param.NroTransaccion);
            cmd.Parameters.AddWithValue("@Codigo_Parqueo", param.Codigo_Parqueo);
            SqlParameter outputParam = new SqlParameter("@Existe", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = outputParam.Value != DBNull.Value ? Convert.ToInt32(outputParam.Value) : -1;
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }

        public async Task<Tuple<DataTable, int>> ReturnUpdatePagodeudaextensionUsoPago(string connectionString, int timeOut, UpdatePagoDeudaUsoParqueoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();

            // cmd.Parameters.AddWithValue("@Id_Detalle", (object)param.Id_Detalle);
            cmd.Parameters.AddWithValue("@Placa", param.Placa);
            //cmd.Parameters.AddWithValue("@NroTransaccion", param.NroTransaccion);

            //foreach (var deuda in param.ListaDeudas)
            //{
            //    cmd.Parameters.AddWithValue("@Id_Tarifario", deuda.Id);
            //    cmd.Parameters.AddWithValue("@Costo_Parqueo", deuda.Monto);
            //}
            SqlParameter outputParam = new SqlParameter("@Existe", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParam);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = outputParam.Value != DBNull.Value ? Convert.ToInt32(outputParam.Value) : -1;
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }


        //nuevo inmovilizado
        public async Task<Tuple<DataTable, int>> ReturnRegistrarPagosInmovilizado1(string connectionString, int timeOut, PagoInmovilizadoParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@NroTransaccionPago", (object)param.NroTransaccionPago);
            cmd.Parameters.AddWithValue("@Monto_Infraccion", (object)param.Monto_Infraccion);
            cmd.Parameters.AddWithValue("@Servicio_Infraccion", (object)param.Servicio_Infraccion);
            cmd.Parameters.AddWithValue("@Total_InfraccionInmovilizado", (object)param.Total_InfraccionInmovilizado);
            cmd.Parameters.AddWithValue("@NIT", param.NIT);
            cmd.Parameters.AddWithValue("@Razon_Social", (object)param.Razon_Social);
            cmd.Parameters.AddWithValue("@Correo", (object)param.Correo);
            cmd.Parameters.AddWithValue("@CUF", (object)param.CUF);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }

        public async Task<Tuple<DataTable, int>> ReturnRegistrarPagosRemolcados1(string connectionString, int timeOut, PagoRemolcadoParameter param)
        {
            //int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
            cmd.Parameters.AddWithValue("@Placa", (object)param.Placa);
            cmd.Parameters.AddWithValue("@NroTransaccionPago", (object)param.NroTransaccionPago);
            cmd.Parameters.AddWithValue("@Monto_Infraccion", (object)param.Monto_Infraccion);
            cmd.Parameters.AddWithValue("@Servicio_Infraccion", (object)param.Servicio_Infraccion);
            cmd.Parameters.AddWithValue("@Total_InfraccionRemolcado", (object)param.Total_InfraccionRemolcado);
            cmd.Parameters.AddWithValue("@NIT", param.NIT);
            cmd.Parameters.AddWithValue("@Razon_Social", (object)param.Razon_Social);
            cmd.Parameters.AddWithValue("@Correo", (object)param.Correo);
            cmd.Parameters.AddWithValue("@CUF", (object)param.CUF);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                //estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, timeOut);
        }
        ///
        public async Task<Tuple<DataTable, int>> ReturnRegistrarExtorno(string connectionString, int timeOut, ExtornoParameter param)
        {
            int estadoIni = -1;
            DataTable dataTable = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(Name, sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            DbParameter parameter1 = (DbParameter)cmd.CreateParameter();
         
            cmd.Parameters.AddWithValue("@NroTransaccionPago", (object)param.NroTransaccionPago);
            cmd.Parameters.AddWithValue("@NroTransaccionExtorno", (object)param.NroTransaccionExtorno);
            DbParameter parameter2 = (DbParameter)cmd.CreateParameter();
            parameter2.DbType = DbType.Int32;
            parameter2.ParameterName = "@Existe";
            parameter2.Value = (object)estadoIni;
            parameter2.Direction = ParameterDirection.Output;
            cmd.Parameters.Add((object)parameter2);
            try
            {
                await sqlConnection.OpenAsync();
                new SqlDataAdapter(cmd).Fill(dataTable);
                estadoIni = Convert.ToInt32(cmd.Parameters["@Existe"].Value);
                Error = string.Empty;
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("Message: {0}; Exception: {1}", ex.Message, SerializeJson.ToObject(ex));
                Error = ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }
            return new Tuple<DataTable, int>(dataTable, estadoIni);
        }


    }
    public class Parameters
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Parameters(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
