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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;

namespace ShopClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ProductsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            
            string query = @"
                    select ID, NAME,QUANTITY,PRICE,ORI_PRICE,USD_PRICE,HOTPRODUCT,CREATEBY,CREATEAT,UPDATEBY,UPDATEAT,DESCRIPTION,IMAGE,COMPANY,IDCATEGORY,SEX from dbo.PRODUCTS";
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
        public JsonResult Post(PRODUCT prod)
        {
           
            string query = @"
                    insert into dbo.PRODUCTS (NAME,QUANTITY,PRICE,ORI_PRICE,USD_PRICE,CREATEBY,CREATEAT,UPDATEBY,UPDATEAT,IMAGE,DESCRIPTION,COMPANY,IDCATEGORY,SEX) values 
                    (@NAME,@QUANTITY,@PRICE,@ORI_PRICE,@USD_PRICE,@CREATEBY,@CREATEAT,@UPDATEBY,@UPDATEAT,@IMAGE,@DESCRIPTION,@COMPANY,@IDCATEGORY,@SEX)
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopClothes");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NAME", prod.NAME);
                    myCommand.Parameters.AddWithValue("@QUANTITY", prod.QUANTITY);
                    myCommand.Parameters.AddWithValue("@CREATEAT", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@CREATEBY", prod.CREATEBY ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@UPDATEBY", prod.UPDATEBY ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@UPDATEAT", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@PRICE", prod.PRICE);
                    myCommand.Parameters.AddWithValue("@ORI_PRICE", prod.ORI_PRICE);
                    myCommand.Parameters.AddWithValue("@USD_PRICE", prod.USD_PRICE);
                    myCommand.Parameters.AddWithValue("@IMAGE", prod.IMAGE);
                    myCommand.Parameters.AddWithValue("@DESCRIPTION", prod.DESCRIPTION);
                    myCommand.Parameters.AddWithValue("@COMPANY", prod.COMPANY);
                    myCommand.Parameters.AddWithValue("@IDCATEGORY", prod.IDCATEGORY);
                    myCommand.Parameters.AddWithValue("@SEX", prod.SEX);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(PRODUCT prod)
        {
         //   NAME,PRICE,ORI_PRICE,CREATEBY,CREATEAT,UPDATEBY,UPDATEAT,DESCRIPTION,IMAGE,COMPANY,IDCATEGORY,SEX
            string query = @"
                           update dbo.CATEGORIES
                           set NAME = @NAME,
                                QUANTITY = @QUANTITY,
                                PRICE=@PRICE,
                                ORI_PRICE=@ORI_PRICE,
                                DESCRIPTION=@DESCRIPTION,
                                IMAGE=@IMAGE,
                                COMPANY=@COMPANY,
                                IDCATEGORY=@IDCATEGORY,
                                SEX=@SEX,
                            CREATEBY = @CREATEBY,
                            CREATEAT = @CREATEAT,
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
                    myCommand.Parameters.AddWithValue("@ID", prod.ID);
                    myCommand.Parameters.AddWithValue("@NAME", prod.NAME);
                    myCommand.Parameters.AddWithValue("@QUANTITY", prod.QUANTITY);
                    myCommand.Parameters.AddWithValue("@CREATEAT", prod.CREATEAT);
                    myCommand.Parameters.AddWithValue("@CREATEBY", prod.CREATEBY ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@UPDATEBY", prod.UPDATEBY ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@UPDATEAT", prod.UPDATEAT);
                    myCommand.Parameters.AddWithValue("@PRICE", prod.PRICE);
                    myCommand.Parameters.AddWithValue("@IMAGE", prod.IMAGE);
                    myCommand.Parameters.AddWithValue("@ORI_PRICE", prod.ORI_PRICE);
                    myCommand.Parameters.AddWithValue("@DESCRIPTION", prod.DESCRIPTION);
                    myCommand.Parameters.AddWithValue("@COMPANY", prod.COMPANY);
                    myCommand.Parameters.AddWithValue("@IDCATEGORY", prod.IDCATEGORY);
                    myCommand.Parameters.AddWithValue("@SEX", prod.SEX);

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
                           delete from dbo.PRODUCTS
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
        [HttpGet("{id}")]
        public JsonResult GetProductById(int id)
        {
            string query = @"
                           select * from dbo.PRODUCTS
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

            return new JsonResult(table);
        }
        [Route("SaveFile")]
        [HttpPost,DisableRequestSizeLimit]
        public IActionResult SaveFile()
        {
            //var flie = Request.Form.Files[0];
            //var folderName = Path.Combine("Photos");
            //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


            //return new JsonResult(pathToSave);

            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Photos");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }


            //try
            //{
            //    var httpRequest = Request.Form;
            //    var postedFile = httpRequest.Files[0];
            //    string filename = postedFile.FileName;
            //    var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

            //    using (var stream = new FileStream(physicalPath, FileMode.Create))
            //    {
            //        postedFile.CopyTo(stream);
            //    }

            //    return new JsonResult(filename);
            //}
            //catch (Exception)
            //{

            //    return new JsonResult("anonymous.png");
            //}
        }
    }
}
