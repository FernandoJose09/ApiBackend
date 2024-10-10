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
                DataSet ds = dac.Fill("GetRecetas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new GetRecetasModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Usuarioregistra = int.Parse(dataRow["UsuarioRegistra"].ToString()),
                        Nombre = dataRow["Nombre"].ToString(),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
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

            parametros.Add(value: new SqlParameter { ParameterName = "@UsuarioRegistra", SqlDbType = System.Data.SqlDbType.Int, Value = Receta.UsuarioRegistra });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = Receta.Nombre});
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = System.Data.SqlDbType.Int, Value = 0});
           

            try
            {
                DataSet ds = dac.Fill("InsertReceta", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mensaje;
        }

        public string UpdateReceta(UpdateRecetaModel Receta)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;


            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Value = 0});
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar, Value = Receta.Nombre});
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = System.Data.SqlDbType.Int, Value = 0});


            try
            {
                DataSet ds = dac.Fill("UpdateReceta", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

        public void DeleteReceta(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("DeleteReceta", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}