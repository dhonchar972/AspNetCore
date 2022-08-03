using ApiControllers.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiControllers.Controllers
{
    // REST controller
    // HttpGet, HttpPost, HttpDelete, HttpPut, HttpPatch, HttpHead, AcceptVerbs
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        // DI!!!
        private IRepository repository;
        public ReservationController(IRepository repo)
        {
            repository = repo;
        }
        // DI!!!

        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return repository.Reservations;
        }

        [HttpGet("{id}")]
        public Reservation Get(int id)
        {
            return repository[id];
        }

        [HttpPost]
        public Reservation Post([FromBody] Reservation res)
        {
            return repository.AddReservation(new Reservation
            {
                ClientName = res.ClientName,
                Location = res.Location
            });
        }

        [HttpPut]
        public Reservation Put([FromBody] Reservation res)
        {
            return repository.UpdateReservation(res);
        }

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Reservation> patch)
        {
            Reservation res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repository.DeleteReservation(id);
        }
    }
}