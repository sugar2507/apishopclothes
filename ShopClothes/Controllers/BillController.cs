using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopClothes.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShopClothes.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class BillController : ControllerBase
        {
            private readonly IConfiguration _configuration;

            public BillController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpGet]
            public JsonResult Get()
            {
                string query = @"
                    select * from dbo.BILL";
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
            public JsonResult Post( BILL cp)
            {
                string query = @"
                    insert into dbo.BILL (IDORDER,ORI_PRICE,TOTALMONEY,DATETIME,NOTE,CREATEBY,CREATEAT,UPDATEBY,UPDATEAT,METHODS) values 
                    (@IDORDER,@ORI_PRICE,@TOTALMONEY,@DATETIME,@NOTE,@CREATEBY,@CREATEAT,@UPDATEBY,@UPDATEAT,@METHODS)
                    ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ShopClothes");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@IDORDER", cp.IDORDER);
                        myCommand.Parameters.AddWithValue("@ORI_PRICE", cp.ORI_PRICE);
                    myCommand.Parameters.AddWithValue("@TOTALMONEY", cp.TOTALMONEY);
                    myCommand.Parameters.AddWithValue("@DATETIME", cp.DATETIME);
                    myCommand.Parameters.AddWithValue("@NOTE", cp.NOTE);
                    myCommand.Parameters.AddWithValue("@CREATEBY", cp.CREATEBY);
                    myCommand.Parameters.AddWithValue("@CREATEAT", cp.CREATEAT);
                    myCommand.Parameters.AddWithValue("@UPDATEAT", cp.UPDATEAT);
                    myCommand.Parameters.AddWithValue("@UPDATEBY", cp.UPDATEBY);
                    myCommand.Parameters.AddWithValue("@METHODS", cp.METHODS);
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

