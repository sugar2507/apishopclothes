using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopClothes.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShopClothes.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class OrderController : ControllerBase
        {
            private readonly IConfiguration _configuration;

            public OrderController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpGet]
            public JsonResult Get()
            {
                string query = @"
                    select * from dbo.ORDERS";
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
            public JsonResult Post(ORDER cp)
            {
                string query = @"
                    insert into dbo.ORDERS (ORI_PRICE,TOTALMONEY,DAY ,NOTE) values 
                    (@ORI_PRICE,@TOTALMONEY,@DAY,@NOTE)
                    ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ShopClothes");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                       
                        myCommand.Parameters.AddWithValue("@ORI_PRICE", cp.ORI_PRICE);
                        myCommand.Parameters.AddWithValue("@TOTALMONEY", cp.TOTALMONEY);
                        myCommand.Parameters.AddWithValue("@DAY ", DateTime.Now);
                        myCommand.Parameters.AddWithValue("@NOTE", cp.NOTE);
                      
                       
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Added Successfully");
            }



        }
    }

