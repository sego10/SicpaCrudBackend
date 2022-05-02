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
    public class DepartamentoEmpleadoController : ControllerBase
    {
        DateTime? hoy = DateTime.Now;
        private readonly IConfiguration _configuration;
        public DepartamentoEmpleadoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                string query = @"
                 SELECT ""DepEmpId"", ""DepEmpCreated_by"", ""DepEmpCreated_date"", ""DepEmpModified_by"", ""DepEmpStatus"", id_department, id_employee, ""DepEmpModified_date""

                FROM public.departments_employees;
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
        public JsonResult Post(DepartamentoEmpleado emp)
        {
            try
            {
                string query = @"
               INSERT INTO public.departments_employees(
                ""DepEmpCreated_by"", ""DepEmpCreated_date"", ""DepEmpModified_by"", ""DepEmpStatus"", id_department, id_employee, ""DepEmpModified_date"")

                VALUES(@DepEmpCreated_by, @DepEmpCreated_date, @DepEmpModified_by, @DepEmpStatus, @id_department, @id_employee, @DepEmpModified_date);
                ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@DepEmpCreated_by", emp.DepEmpCreated_by);
                        myCommand.Parameters.AddWithValue("@DepEmpCreated_date", hoy);
                        myCommand.Parameters.AddWithValue("@DepEmpModified_by", emp.DepEmpModified_by);
                        myCommand.Parameters.AddWithValue("@DepEmpStatus", emp.DepEmpStatus);
                        myCommand.Parameters.AddWithValue("@id_department", emp.id_department);
                        myCommand.Parameters.AddWithValue("@id_employee", emp.id_employee);
                        myCommand.Parameters.AddWithValue("@DepEmpModified_date", hoy);
                        
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
        public JsonResult Put(DepartamentoEmpleado emp)
        {
            try
            {
               
                string query = @"
               UPDATE public.departments_employees
	            SET ""DepEmpModified_by""=@DepEmpModified_by, ""DepEmpStatus""=@DepEmpStatus, id_department=@id_department, id_employee=@id_employee, ""DepEmpModified_date""=@DepEmpModified_date

                WHERE ""DepEmpId"" = @DepEmpId;
                ";          

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@DepEmpId", emp.DepEmpId);
                        myCommand.Parameters.AddWithValue("@DepEmpModified_by", emp.DepEmpModified_by);                    
                        myCommand.Parameters.AddWithValue("@DepEmpStatus", emp.DepEmpStatus);
                        myCommand.Parameters.AddWithValue("@id_department", emp.id_department);
                        myCommand.Parameters.AddWithValue("@id_department", emp.id_department);
                        myCommand.Parameters.AddWithValue("@DepEmpModified_date", hoy);
                       
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
                DELETE FROM public.departments_employees
	            WHERE ""DepEmpId"" = @DepEmpId;
            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@DepEmpId", id);
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
