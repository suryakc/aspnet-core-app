﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;

namespace TheWorld.Controllers.Web
    {
    public class AppController : Controller
        {
        private IMailService m_mailService;
        private IConfigurationRoot m_config;

        public AppController (IMailService mailService, IConfigurationRoot config)
            {
            m_mailService = mailService;
            m_config = config;
            }

        public IActionResult Index ()
            {
            return View ();
            }

        public IActionResult Contact ()
            {
            return View ();
            }

        [HttpPost]
        public IActionResult Contact (ContactViewModel model)
            {
            if (model.Message.Contains ("err"))
                ModelState.AddModelError ("Message", "Message has an error!");

            if (ModelState.IsValid)
                {
                m_mailService.SendMail (m_config["MailSettings:ToAddress"], model.Email, "From TheWorld", model.Message);
                ModelState.Clear ();
                ViewBag.UserMessage = "Message Sent!";
                }
            
            return View ();
            }

        public IActionResult About ()
            {
            return View ();
            }
        }
    }
