using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
    {
    [Route("api/trips")]
    public class TripsController : Controller
        {
        private ILogger<TripsController> m_logger;
        private IWorldRepository m_repository;

        public TripsController (IWorldRepository repository, ILogger<TripsController> logger)
            {
            m_repository = repository;
            m_logger = logger;
            }

        [HttpGet("")]
        public IActionResult Get ()
            {
            try
                {
                var results = m_repository.GetAllTrips ();
                return Ok (Mapper.Map<IEnumerable<TripViewModel>> (results));
                }
            catch (Exception ex)
                {
                m_logger.LogError ($"Failed to get all trips. { ex }");
                return BadRequest ("Error occurred.");
                }           
            }

        [HttpPost("")]
        public async Task<IActionResult> Post ([FromBody]TripViewModel trip)
            {
            if (ModelState.IsValid)
                {
                var newTrip = Mapper.Map<Trip> (trip);
                m_repository.AddTrip (newTrip);
                if (await m_repository.SaveChangesAsync ())
                    {
                    return Created ($"api/trips/{trip.Name}", Mapper.Map<TripViewModel> (newTrip));
                    }
                else
                    {
                    return BadRequest ("Failed to save the trip to database...");
                    }              
                }

            return BadRequest (ModelState);
            }
        }
    }
