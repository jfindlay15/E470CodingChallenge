using AutoMapper;
using CodingExercise.Dtos;
using CodingExercise.Interfaces;
using CodingExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodingExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _service;
        private readonly IMapper _mapper;

        public VehicleController(ILogger<VehicleController> logger, IVehicleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a vehicle
        /// </summary>
        /// <param name="vehicleDto"></param>
        /// <returns code="200">Vehicle was saved</returns>
        [HttpPost]
        [Route("create-vehicle")]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleDto vehicleDto)
        {
            IActionResult result;

            if (vehicleDto == null)
            {
                throw new ArgumentNullException(nameof(vehicleDto));
            }

            if (!ModelState.IsValid)
            {
                throw new InvalidOperationException();
            }

            var vehicle = _mapper.Map(vehicleDto, new Vehicle());
            var newVehicle = await _service.Create(vehicle);
            result = Ok(newVehicle);


            return result;
        }
    }
}