using FinekraTutorial.Business.Interfaces;
using FinekraTutorial.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace FinekraTutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfumesController : ControllerBase
    {
        private readonly IBaseService<Perfume> _perfumeService;

        public PerfumesController(IBaseService<Perfume> perfumeService)
        {
            _perfumeService = perfumeService;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult GetPerfumes()
        {
            var result = _perfumeService.GetAllFiltered();
            return Ok(result);
        }
    }
}
