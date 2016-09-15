using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheWorld.Models
    {
    public class WorldContext : DbContext
        {
        private IConfigurationRoot m_config;

        public WorldContext (IConfigurationRoot config, DbContextOptions options)
            : base (options)
            {
            m_config = config;
            }

        public DbSet<Trip> Trips
            {
            get; set;
            }

        public DbSet<Stop> Stops
            {
            get; set;
            }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
            {
            base.OnConfiguring (optionsBuilder);

            optionsBuilder.UseSqlServer (m_config["ConnectionStrings:WorldContextConnection"]);
            }
        }
    }
