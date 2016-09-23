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
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
        {
        private ILogger<StopsController> m_logger;
        private IWorldRepository m_repository;

        public StopsController (IWorldRepository repository, ILogger<StopsController> logger)
            {
            m_repository = repository;
            m_logger = logger;
            }

        [HttpGet("")]
        public IActionResult Get (string tripName)
            {
            try
                {
                var trip = m_repository.GetTripByName (tripName);

                return Ok (Mapper.Map<IEnumerable<StopViewModel>> (trip.Stops.OrderBy (s => s.Order).ToList ()));
                }
            catch (Exception ex)
                {
                m_logger.LogError ($"Failed to get stops: { ex }");
                }

            return BadRequest ($"Failed to get stops for trip { tripName }");
            }

        [HttpPost("")]
        public async Task<IActionResult> Post (string tripName, [FromBody]StopViewModel stopVModel)
            {
            try
                {
                if (ModelState.IsValid)
                    {
                    var newStop = Mapper.Map<Stop> (stopVModel);

                    m_repository.AddStop (tripName, newStop);

                    if (await m_repository.SaveChangesAsync ())
                        {
                        return Created ($"/api/trips/{tripName}/stops/{newStop.Name}",
                            Mapper.Map<StopViewModel> (newStop));
                        }                    
                    }
                }
            catch (Exception ex)
                {
                m_logger.LogError ($"Failed to add stop: { ex }");
                }

            return BadRequest ($"Failed to add stop for trip { tripName }");
            }
        }
    }
