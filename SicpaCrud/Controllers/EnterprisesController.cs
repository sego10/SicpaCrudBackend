using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SicpaCrud.Models;


namespace SicpaCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterprisesController : ControllerBase
    {
        DateTime? hoy = DateTime.Now;
        private readonly IConfiguration _configuration;
        public EnterprisesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                string query = @"
              
                SELECT ""enterprisesId"", ""enterprisesCreated_by"", ""enterpricesCreated_date"", ""enterpricesModified_by"", ""enterpricesModified_date"", ""enterprisesStatus"", ""enterprisesAddress"", ""enterprisesName"", ""enterprisesPhone""

                FROM public.""Enterprises"";

            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();

                    }
                }

                return new JsonResult(table);
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }

            
        }


        [HttpPost]
        public JsonResult Post(Enterprises ent)
        {
            try
            {
                string query = @"
                INSERT INTO public.""Enterprises""(""enterprisesCreated_by"",""enterpricesCreated_date"",""enterpricesModified_by"", ""enterpricesModified_date"", ""enterprisesStatus"", ""enterprisesAddress"", ""enterprisesName"", ""enterprisesPhone"")
	            VALUES(@enterprisesCreated_by, @enterpricesCreated_date, @enterpricesModified_by, @enterpricesModified_date, @enterprisesStatus, @enterprisesAddress, @enterprisesName, @enterprisesPhone);
            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@enterprisesCreated_by", ent.enterprisesCreated_by);
                        myCommand.Parameters.AddWithValue("@enterpricesCreated_date", hoy);
                        myCommand.Parameters.AddWithValue("@enterpricesModified_by", ent.enterpricesModified_by);
                        myCommand.Parameters.AddWithValue("@enterpricesModified_date", hoy);
                        myCommand.Parameters.AddWithValue("@enterprisesStatus", ent.enterprisesStatus.GetValueOrDefault());
                        myCommand.Parameters.AddWithValue("@enterprisesAddress", ent.enterprisesAddress);
                        myCommand.Parameters.AddWithValue("@enterprisesName", ent.enterprisesName);
                        myCommand.Parameters.AddWithValue("@enterprisesPhone", ent.enterprisesPhone);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();

                    }
                }

                return new JsonResult("Added Successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
            
        }

        [HttpPut("{id}")]
        public JsonResult Put(Enterprises ent)
        {
            try
            {
               
                string query = @"
                UPDATE public.""Enterprises""
                SET ""enterprisesName"" =@enterprisesName, ""enterprisesPhone"" =@enterprisesPhone, ""enterpricesModified_date"" =@enterpricesModified_date, 
                ""enterpricesModified_by"" =@enterpricesModified_by, 
                ""enterprisesStatus"" =@enterprisesStatus, ""enterprisesAddress"" =@enterprisesAddress
                WHERE ""enterprisesId"" = @enterprisesId;
                ";

               

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@enterprisesId", ent.enterprisesId);
                        myCommand.Parameters.AddWithValue("@enterprisesCreated_by", ent.enterprisesCreated_by);
                        //myCommand.Parameters.AddWithValue("@enterpricesCreated_date", hoy);
                        myCommand.Parameters.AddWithValue("@enterpricesModified_by", ent.enterpricesModified_by);
                        myCommand.Parameters.AddWithValue("@enterpricesModified_date", hoy);
                        myCommand.Parameters.AddWithValue("@enterprisesStatus", ent.enterprisesStatus.GetValueOrDefault());
                        myCommand.Parameters.AddWithValue("@enterprisesAddress", ent.enterprisesAddress);
                        myCommand.Parameters.AddWithValue("@enterprisesName", ent.enterprisesName);
                        myCommand.Parameters.AddWithValue("@enterprisesPhone", ent.enterprisesPhone);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();

                    }
                }

                return new JsonResult("Updated Successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }
            
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                string query = @"
                DELETE FROM public.""Enterprises""   
                WHERE ""enterprisesId"" = @enterprisesId;
            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@enterprisesId", id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();

                    }
                }

                return new JsonResult("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest(ex.Message));
            }

           
        }

    }
}
