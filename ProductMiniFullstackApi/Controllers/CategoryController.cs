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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepos;
        public CategoryController(ICategoryRepository catRepo)
        {
            _categoryRepos= catRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data= _categoryRepos.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")] // api/category/getbyid/1
        public IActionResult GetById(int id)
        {
            var data = _categoryRepos.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddUpdate(Category model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Validatation failed";
            }
            var result = _categoryRepos.AddUpdate(model);

            status.StatusCode = result ? 1 : 0;
            status.Message = result ? "Saved successfully" : "Error has occured";
            return Ok(status);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _categoryRepos.Delete(id);
            var status = new Status
            {
                StatusCode = result ? 1 : 0,
                Message = result ? "deleted successfully" : "Error has occured"
            };
            return Ok(status);
        }

    }
}
