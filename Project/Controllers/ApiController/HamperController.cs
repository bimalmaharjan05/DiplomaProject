using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;

namespace Project.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class HamperController : ControllerBase
    {
        private IDataService<Hamper> _hamperDataService;
        public HamperController(IDataService<Hamper> hamperService)
        {
            _hamperDataService = hamperService;
        }

        [HttpGet]
        public IEnumerable<Hamper> Get()
        {
            IEnumerable<Hamper> hampers = _hamperDataService.GetAll();
            return hampers.ToList();
        }

        // api/Hamper/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IEnumerable<Hamper> hampers = _hamperDataService.Query(h => h.CategoryId == id);
            return Ok(hampers);
        }
    }
}