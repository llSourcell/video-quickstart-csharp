﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Auth;
using JWT;
using Faker;

namespace VideoQuickstart.Controllers
{
    public class TokenController : Controller
    {
        // GET: /token
        public ActionResult Index(string Device)
        {
            // Load Twilio configuration from Web.config
            var accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
            var apiKey = ConfigurationManager.AppSettings["TwilioApiKey"];
            var apiSecret = ConfigurationManager.AppSettings["TwilioApiSecret"];
            var videoConfigSid = ConfigurationManager.AppSettings["TwilioConfigurationSid"];

            // Create a random identity for the client
            var Identity = Internet.UserName();

            // Create an Access Token generator
            var Token = new AccessToken(accountSid, apiKey, apiSecret);
            Token.Identity = Identity;

            // Create an IP messaging grant for this token
            var grant = new ConversationsGrant();
            Token.AddGrant(grant);

            return Json(new
            {
                identity = Identity,
                token = Token.ToJWT()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}