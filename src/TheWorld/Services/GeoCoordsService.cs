using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Services
    {
    public class GeoCoordsService
        {
        private ILogger<GeoCoordsService> m_logger;

        public GeoCoordsService (ILogger<GeoCoordsService> logger)
            {
            m_logger = logger;
            }

        public async Task<GeoCoordsResult> GetCoordsAsync (string name)
            {
            var result = new GeoCoordsResult ()
                {
                Success = false,
                Message = "Failed to get coordinates"
                };


            }
        }
    }
