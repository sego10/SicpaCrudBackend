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
    public class DepartmentController : ControllerBase
    {
        DateTime? hoy = DateTime.Now;
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                string query = @"
                 SELECT ""departmentsId"", ""departmentsCreated_by"", ""departmentsCreated_date"", ""departmentsModified_by"", ""departmentsStatus"", ""departmentsDescription"", ""departmentsName"", ""departmentsPhone"", id_enterprises, ""departmentsModified_date""

                FROM public.departments;
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
        public JsonResult Post(Department dep)
        {
            try
            {
                string query = @"
                INSERT INTO public.departments(
	            ""departmentsCreated_by"", ""departmentsCreated_date"", ""departmentsModified_by"", ""departmentsStatus"", ""departmentsDescription"", ""departmentsName"", ""departmentsPhone"", id_enterprises, ""departmentsModified_date"")

                VALUES(@departmentsCreated_by, @departmentsCreated_date, @departmentsModified_by, @departmentsStatus, @departmentsDescription, @departmentsName, @departmentsPhone, @id_enterprises, @departmentsModified_date);
                ";


                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@departmentsCreated_by", dep.departmentsCreated_by);
                        myCommand.Parameters.AddWithValue("@departmentsCreated_date", hoy);
                        myCommand.Parameters.AddWithValue("@departmentsModified_by", dep.departmentsModified_by);
                        myCommand.Parameters.AddWithValue("@departmentsStatus", dep.departmentsStatus);
                        myCommand.Parameters.AddWithValue("@departmentsDescription", dep.departmentsDescription);
                        myCommand.Parameters.AddWithValue("@departmentsName", dep.departmentsName);
                        myCommand.Parameters.AddWithValue("@departmentsPhone", dep.departmentsPhone);
                        myCommand.Parameters.AddWithValue("@id_enterprises", dep.id_enterprises);
                        myCommand.Parameters.AddWithValue("@departmentsModified_date", hoy);
              
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
        public JsonResult Put(Department dep)
        {
            try
            {
               
                string query = @"
                UPDATE public.departments
	            SET ""departmentsModified_by""=@departmentsModified_by, ""departmentsStatus""=@departmentsStatus, ""departmentsDescription""= @departmentsDescription, ""departmentsName""= @departmentsName, ""departmentsPhone""= @departmentsPhone, id_enterprises=@id_enterprises, ""departmentsModified_date""=@departmentsModified_date

                WHERE ""departmentsId"" = departmentsId;
                ";          

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@departmentsId", dep.departmentsId);
                        myCommand.Parameters.AddWithValue("@departmentsModified_by", dep.departmentsModified_by);                    
                        myCommand.Parameters.AddWithValue("@departmentsStatus", dep.departmentsStatus);
                        myCommand.Parameters.AddWithValue("@departmentsDescription", dep.departmentsDescription);
                        myCommand.Parameters.AddWithValue("@departmentsName", dep.departmentsName);
                        myCommand.Parameters.AddWithValue("@departmentsPhone", dep.departmentsPhone);
                        myCommand.Parameters.AddWithValue("@id_enterprises", dep.id_enterprises);
                        myCommand.Parameters.AddWithValue("@departmentsModified_date", hoy);
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
                DELETE FROM public.departments
	            WHERE ""departmentsId"" = departmentsId;
            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@departmentsId", id);
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
