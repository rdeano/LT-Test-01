using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT_Test001.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LT_Test001.Models;
using Microsoft.AspNetCore.Authorization;

namespace LT_Test001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        DatabaseContext _dbContext = new DatabaseContext();

        [HttpGet]
        [Authorize]
        public List<Product> Get()
        {
            return _dbContext.Products.ToList();
        }

        [HttpGet("GetProduct")]
        [Authorize]
        public Product GetProduct(Product productitem)
        {
            return _dbContext.Products.Where(c => c.product_id == productitem.product_id).FirstOrDefault();
        }

        [HttpPost("InsertProduct")]
        [Authorize]
        public bool InsertProduct(Product productitem)
        {
            bool status;
            try
            {
                _dbContext.Products.Add(productitem);
                _dbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        [HttpPost("UpdateProduct")]
        [Authorize]
        public bool UpdateProduct(Product productitem)
        {
            bool status;
            try
            {
                Product prodItem = _dbContext.Products.Where(p => p.product_id == productitem.product_id).FirstOrDefault();
                if (prodItem != null)
                {
                    prodItem.category_id = productitem.category_id;
                    prodItem.name = productitem.name;
                    prodItem.description = productitem.description;
                    prodItem.image = productitem.image;

                    _dbContext.SaveChanges();
                }

                status = true;
            }
            catch(Exception ex)
            {
                status = false;
            }

            return status;

        }

        [HttpPost("DeleteProduct")]
        [Authorize]
        public bool DeleteProduct(Product productitem)
        {
            bool status;
            try
            {
                Product prodItem = _dbContext.Products.Where(p => p.product_id == productitem.product_id).FirstOrDefault();
                if (prodItem != null)
                {
                    _dbContext.Products.Remove(prodItem);
                    _dbContext.SaveChanges();
                }
                status = true;
            }
            catch(Exception ex)
            {
                status = false;
            }

            return status;
        }





    }
}