using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Microsoft.Extensions.Logging;

namespace TheWorld.Controllers.Web
    {
    public class AppController : Controller
        {
        private IMailService m_mailService;
        private IConfigurationRoot m_config;
        private IWorldRepository m_repository;
        private ILogger<AppController> m_logger;

        public AppController (IMailService mailService, 
            IConfigurationRoot config, 
            IWorldRepository repository,
            ILogger<AppController> logger)
            {
            m_mailService = mailService;
            m_config = config;
            m_repository = repository;
            m_logger = logger;
            }

        public IActionResult Index ()
            {
            try
                {
                var data = m_repository.GetAllTrips ();
                return View (data);
                }
            catch (Exception ex)
                {
                m_logger.LogError ($"Failed to get trips in Index page: { ex.Message }");
                return Redirect ("/error");
                }           
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
