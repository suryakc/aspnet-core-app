﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
    {
    public class TripViewModel
        {
        [Required]
        public string Name
            {
            get; set;
            }

        [Required]
        public DateTime DateCreated
            {
            get; set;
            } = DateTime.UtcNow;
        }
    }
