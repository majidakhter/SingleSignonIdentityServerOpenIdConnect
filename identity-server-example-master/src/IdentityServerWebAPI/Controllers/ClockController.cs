using System;
using ClassLibrary1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerWebAPI.Controllers
{
    [Route("api/clock")] // http://localhost:5001/api/clock
    public class ClockController : Controller
    {
        [HttpGet("time")] // http://localhost:5001/api/clock/time
        [Authorize]
        public string Time()
        {
            return DateTime.UtcNow.ToString("hh:mm");
        }
        [HttpGet("Customer")] // http://localhost:5001/api/clock/Customer
        [Authorize]
        public CustomerViewModel Customer()
        {
            var customer = new CustomerViewModel { CustomerCode = "C001",CustomerName= "Rakesh Kumar" };
            return customer;
        }
    }
}
