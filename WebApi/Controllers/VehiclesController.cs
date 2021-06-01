using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.IRepositories;
using WebApi.Models;

namespace WebApi.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesController(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Vehicle> Get(int id)
        {
            Vehicle vehicle = vehicleRepository.Get(id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }
    }
}
