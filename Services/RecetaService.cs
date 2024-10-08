using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using reportesApi.Models.Compras;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
namespace reportesApi.Services
{
    public class RecetaService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public RecetaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<GetRecetasModel> GetRecetas()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            GetRecetasModel proveedor = new GetRecetasModel();

            List<GetRecetasModel> lista = new List<GetRecetasModel>();
            try
            {
                parametros = new ArrayList();
                DataSet ds = dac.Fill("sp_get_recetas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetRecetasModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Usuarioregistra = dataRow["UsuarioRegistra"].ToString(),
                        Nombre = dataRow["Nombre"].ToString(),
                        Estatus = dataRow["Estatus"].ToString(),
                        FechaCreacion= dataRow["FechaCreacion"].ToString()
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public string InsertReceta(InsertRecetaModel Receta)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Usuario", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.Usuario });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.Nombre});
            parametros.Add(new SqlParameter { ParameterName = "@Direccion", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.Direccion});
            parametros.Add(new SqlParameter { ParameterName = "@Email", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.Email});
            parametros.Add(new SqlParameter { ParameterName = "@RFC", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.RFC});
            parametros.Add(new SqlParameter { ParameterName = "@PlazoPago", SqlDbType = System.Data.SqlDbType.Int, Value = Proveedor.PlazoPago});
            parametros.Add(new SqlParameter { ParameterName = "@PorcentajeRetencion", SqlDbType = System.Data.SqlDbType.Decimal, Value = Proveedor.PorcentajeRetencion});


            try
            {
                DataSet ds = dac.Fill("sp_insert_proveedores", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mensaje;
        }

        public string UpdateProveedor(UpdateProveedorModel Proveedor)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;


            parametros.Add(new SqlParameter { ParameterName = "@IdProveedor", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.IdProveedor });
            parametros.Add(new SqlParameter { ParameterName = "@Usuario", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.Usuario });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.Nombre});
            parametros.Add(new SqlParameter { ParameterName = "@Direccion", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.Direccion});
            parametros.Add(new SqlParameter { ParameterName = "@Email", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.Email});
            parametros.Add(new SqlParameter { ParameterName = "@RFC", SqlDbType = System.Data.SqlDbType.VarChar, Value = Proveedor.RFC});
            parametros.Add(new SqlParameter { ParameterName = "@PlazoPago", SqlDbType = System.Data.SqlDbType.Int, Value = Proveedor.PlazoPago });
            parametros.Add(new SqlParameter { ParameterName = "@PorcentajeRetencion", SqlDbType = System.Data.SqlDbType.Decimal, Value = Proveedor.PorcentajeRetencion });


            try
            {
                DataSet ds = dac.Fill("sp_update_proveedores", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

        public void DeleteProveedor(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@IdProveedor", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("sp_delete_proveedores", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}