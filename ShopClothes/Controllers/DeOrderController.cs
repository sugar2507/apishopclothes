using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopClothes.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShopClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeOrderController : ControllerBase
    {
       
            private readonly IConfiguration _configuration;

            public DeOrderController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpGet]
            public JsonResult Get()
            {
                string query = @"
                    select * from dbo.DE_ORDER";
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
            public JsonResult Post(DE_ORDER cp)
            {
                string query = @"
                    insert into dbo.DE_ORDER (IDPRODUCT,IDORDER,QUANTITY ,PRICE) values 
                    (@IDPRODUCT,@IDORDER,@QUANTITY,@PRICE)
                    ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ShopClothes");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@IDPRODUCT", cp.IDPRODUCT);
                        myCommand.Parameters.AddWithValue("@IDORDER", cp.IDORDER);
                        myCommand.Parameters.AddWithValue("@QUANTITY", cp.QUANTITY);
                        myCommand.Parameters.AddWithValue("@PRICE ", cp.PRICE);
                     
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
