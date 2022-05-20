using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using ShopClothes.Models;

namespace ShopClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CategoriesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select ID, NAME,CREATEBY,CREATEAT,UPDATEBY,UPDATEAT from dbo.CATEGORIES";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopClothes");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(CATEGORIES cg)
        {
            string query = @"
                    insert into dbo.CATEGORIES (NAME,CREATEAT,CREATEBY,UPDATEBY,UPDATEAT) values 
                    (@NAME,@CREATEAT,@CREATEBY,@UPDATEBY,@UPDATEAT)
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopClothes");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NAME", cg.NAME);
                    myCommand.Parameters.AddWithValue("@CREATEAT", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@CREATEBY", cg.CREATEBY ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@UPDATEBY", cg.UPDATEBY ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@UPDATEAT", DateTime.Now);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(CATEGORIES cg)
        {
            string query = @"
                           update dbo.CATEGORIES
                           set NAME = @NAME,
                            CREATEBY = @CREATEBY,
                            
                            UPDATEBY = @UPDATEBY,
                            UPDATEAT = @UPDATEAT
                            where ID = @ID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopClothes");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", cg.ID);
                    myCommand.Parameters.AddWithValue("@NAME", cg.NAME);
                    myCommand.Parameters.AddWithValue("@CREATEBY", cg.CREATEBY ?? (object)DBNull.Value);
                    //myCommand.Parameters.AddWithValue("@CREATEAT", cg.CREATEAT);
                    myCommand.Parameters.AddWithValue("@UPDATEBY", cg.UPDATEBY ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@UPDATEAT", DateTime.Now);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.CATEGORIES
                            where ID=@ID
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopClothes");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
