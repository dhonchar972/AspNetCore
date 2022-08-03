using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;
using System;
using System.Linq;

namespace PartyInvites.Controllers;

// standart entry point, must attend by agreement, otherwise, manual configuration will be required.
// match to: "/", "/Ноmе", "/Home/Index"
public class HomeController : Controller
{
    //public string Index()
    //{
    //    return "Hello World";
    //}

    //http://localhost:5000/
    //[Route("/")]

    public ViewResult Index()
    {
        int hour = DateTime.Now.Hour;
        ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
        return View("MyView");
    }

    [HttpGet]
    public ViewResult RsvpForm()
    {
        return View();
    }

    //http://localhost:5000/Home/RsvpForm
    [HttpPost]
    public ViewResult RsvpForm(GuestResponse guestResponse)
    {
        if (ModelState.IsValid)
        {
            Repository.AddResponse(guestResponse);
            return View("Thanks", guestResponse);
        }
        else
        {
            // there is a validation error
            return View();
        }
    }

    public ViewResult ListResponses()
    {
        return View(Repository.Responses.Where(r => r.WillAttend == true));
    }
}
