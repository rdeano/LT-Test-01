using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class CategoryController : ControllerBase
    {
        DatabaseContext _dbContext = new DatabaseContext();

        [HttpGet]
        [Authorize]
        public List<Category> Get()
        {
            return _dbContext.Categories.ToList();
        }


        [HttpGet("GetCategory")]
        [Authorize]
        public Category GetCategory(Category categoryitem)
        {
            return _dbContext.Categories.Where(c => c.category_id == categoryitem.category_id).FirstOrDefault();
        }

        [HttpPost("InsertCategory")]
        [Authorize]
        public bool InsertCategory(Category categoryitem)
        {
            bool status;
            try
            {
                _dbContext.Categories.Add(categoryitem);
                _dbContext.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        [HttpPost("UpdateCategory")]
        [Authorize]
        public bool UpdateCategory(Category categoryitem)
        {
            bool status;
            try
            {
                Category catitem = _dbContext.Categories.Where(c => c.category_id == categoryitem.category_id).FirstOrDefault();
                if (catitem != null)
                {
                    catitem.name = categoryitem.name;
                    _dbContext.SaveChanges();
                }
                status = true;

            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }

        [HttpPost("DeleteCategory")]
        [Authorize]
        public bool DeleteCategory(Category categoryitem)
        {
            bool status;
            try
            {
                Category catItem = _dbContext.Categories.Where(c => c.category_id == categoryitem.category_id).FirstOrDefault();
                if (catItem != null)
                {
                    _dbContext.Categories.Remove(catItem);
                    _dbContext.SaveChanges();
                }
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }


    }
}