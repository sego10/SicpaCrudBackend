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
    public class EmployeeController : ControllerBase
    {
        DateTime? hoy = DateTime.Now;
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                string query = @"
                 SELECT ""employeesId"", ""employeesCreated_by"", ""employeedModified_by"", ""employeeModified_date"", ""employeeStatus"", ""employeeAge"", ""employeeEmail"", ""employeeName"", ""employeePosition"", ""employeeSurname"", ""employeeCreated_date""

                FROM public.employees;
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
        public JsonResult Post(Employee emp)
        {
            try
            {
                string query = @"
               INSERT INTO public.employees(
	            ""employeesCreated_by"", ""employeedModified_by"", ""employeeModified_date"", ""employeeStatus"", ""employeeAge"", ""employeeEmail"", ""employeeName"", ""employeePosition"", ""employeeSurname"", ""employeeCreated_date"")

                VALUES(@employeesCreated_by, @employeedModified_by, @employeeModified_date, @employeeStatus, @employeeAge, @employeeEmail, @employeeName, @employeePosition,@employeeSurname,@employeeCreated_date);
            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@employeesCreated_by", emp.employeesCreated_by);
                        myCommand.Parameters.AddWithValue("@employeedModified_by", emp.employeedModified_by);
                        myCommand.Parameters.AddWithValue("@employeeModified_date", hoy);
                        myCommand.Parameters.AddWithValue("@employeeStatus", emp.employeeStatus);
                        myCommand.Parameters.AddWithValue("@employeeAge", emp.employeeAge.GetValueOrDefault());
                        myCommand.Parameters.AddWithValue("@employeeEmail", emp.employeeEmail);
                        myCommand.Parameters.AddWithValue("@employeeName", emp.employeeName);
                        myCommand.Parameters.AddWithValue("@employeePosition", emp.employeePosition);
                        myCommand.Parameters.AddWithValue("@employeeSurname", emp.employeeSurname);
                        myCommand.Parameters.AddWithValue("@employeeCreated_date", hoy);
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
        public JsonResult Put(Employee emp)
        {
            try
            {
               
                string query = @"
                UPDATE public.""employees""
	            SET ""employeedModified_by""=@employeedModified_by, ""employeeModified_date""=@employeeModified_date,""employeeAge""=@employeeAge, ""employeeEmail""=@employeeEmail, ""employeeName""=@employeeName, ""employeePosition""=@employeePosition, ""employeeSurname""=@employeeSurname
                WHERE ""employeesId"" = @employeesId;
                ";          

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@employeesId", emp.employeesId);
                        myCommand.Parameters.AddWithValue("@employeedModified_by", emp.employeedModified_by);                    
                        myCommand.Parameters.AddWithValue("@employeeModified_date", hoy);
                        myCommand.Parameters.AddWithValue("@employeeAge", emp.employeeAge);
                        myCommand.Parameters.AddWithValue("@employeeEmail", emp.employeeEmail);
                        myCommand.Parameters.AddWithValue("@employeeName", emp.employeeName);
                        myCommand.Parameters.AddWithValue("@employeePosition", emp.employeePosition);
                        myCommand.Parameters.AddWithValue("@employeeSurname", emp.employeeSurname);
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
                DELETE FROM public.""employees""   
                WHERE ""employeesId"" = @employeesId;
            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("SicpaBd");
                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@employeesId", id);
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
