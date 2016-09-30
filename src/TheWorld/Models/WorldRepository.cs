using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
    {
    public class WorldRepository : IWorldRepository
        {
        private WorldContext m_context;
        private ILogger<WorldRepository> m_logger;

        public WorldRepository (WorldContext context, ILogger<WorldRepository> logger)
            {
            m_context = context;
            m_logger = logger;
            }

        public void AddStop (string username, string tripName, Stop newStop)
            {
            var trip = GetUserTripByName (username, tripName);
            if (null != trip)
                {
                trip.Stops.Add (newStop);
                m_context.Stops.Add (newStop);
                }
            }

        public void AddTrip (Trip trip)
            {
            m_context.Add (trip);
            }

        public IEnumerable<Trip> GetAllTrips ()
            {
            m_logger.LogInformation ("Getting all trips from the database...");
            return m_context.Trips.ToList ();
            }

        public IEnumerable<Trip> GetTripsByUsername (string username)
            {
            m_logger.LogInformation ($"Getting all trips from the database for user {username}...");
            return m_context.Trips.Include (t => t.Stops).Where (t => t.UserName == username).ToList ();
            }

        public Trip GetTripByName (string tripName)
            {
            return m_context.Trips
                .Include (t => t.Stops)
                .Where (t => t.Name == tripName)
                .FirstOrDefault ();
            }

        public Trip GetUserTripByName (string username, string tripName)
            {
            return m_context.Trips
                .Include (t => t.Stops)
                .Where (t => (t.UserName == username && t.Name == tripName))
                .FirstOrDefault ();
            }

        public async Task<bool> SaveChangesAsync ()
            {
            return (await m_context.SaveChangesAsync ()) > 0;
            }        
        }
    }
