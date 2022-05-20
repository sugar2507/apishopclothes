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
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select ID, SDT ,NAME, ADDRESS, ROLES ,CREATEAT from dbo.USERS";
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
        public JsonResult Post(USER us)
        {
            string query = @"
                    insert into dbo.USERS (SDT,NAME,ADDRESS,ROLES,CREATEAT) values 
                    (@SDT,@NAME,@ADDRESS,@ROLES,@CREATEAT)
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopClothes");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@SDT", us.SDT);
                    myCommand.Parameters.AddWithValue("@NAME", us.NAME);
                    myCommand.Parameters.AddWithValue("@CREATEAT", us.CREATEAT);
                    myCommand.Parameters.AddWithValue("@ADDRESS", us.ADDRESS);
                    myCommand.Parameters.AddWithValue("@ROLES", us.ROLES);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(USER us)
        {
            string query = @"
                           update dbo.USERS
                           set SDT = @SDT,
                            NAME = @NAME,
                            ADDRESS = @ADDRESS,
                            ROLES = @ROLES,
                            CREATEAT = @CREATEAT
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
                    myCommand.Parameters.AddWithValue("@ID", us.ID);
                    myCommand.Parameters.AddWithValue("@SDT", us.SDT);
                    myCommand.Parameters.AddWithValue("@NAME", us.NAME);
                    myCommand.Parameters.AddWithValue("@ADDRESS", us.ADDRESS);
                    myCommand.Parameters.AddWithValue("@ROLES", us.ROLES);
                    myCommand.Parameters.AddWithValue("@CREATEAT", us.CREATEAT);

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
                           delete from dbo.USERS
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
