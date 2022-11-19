using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductMiniFullstackApi.Models.Domain;
using ProductMiniFullstackApi.Models.DTOs;
using ProductMiniFullstackApi.Repositories.Abstract;
using System.Reflection.Metadata.Ecma335;

namespace ProductMiniFullstackApi.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepos;
        public ProductController(IProductRepository productRepo)
        {
            productRepos= productRepo;
        }

        [HttpGet]
        public IActionResult GetAll(string term="")
        {
            var data= productRepos.GetAll(term);
            return Ok(data);
        }

        [HttpGet("{id}")] // api/product/getbyid/1
        public IActionResult GetById(int id)
        {
            var data = productRepos.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddUpdate(Product model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Validatation failed";
            }
            var result = productRepos.AddUpdate(model);

            status.StatusCode = result ? 1 : 0;
            status.Message = result ? "Saved successfully" : "Error has occured";
             return Ok(status);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = productRepos.Delete(id);
            var status = new Status
            {
                StatusCode = result ? 1 : 0,
                Message = result ? "deleted successfully" : "Error has occured"
            };
            return Ok(status);
        }

    }
}
