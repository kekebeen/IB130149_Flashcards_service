using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using IB130149_Flashcard_Service.Models;

namespace IB130149_Flashcard_Service.Controllers
{
    public class PitanjaController : ApiController
    {
        private DBContext db = new DBContext();

        public class PitanjaIndexVM
        {
            public int PitanjeId { get; set; }
            public string Pitanje { get; set; }
            public string Odgovor { get; set; }
            public int DeckId { get; set; }
            public Boolean isFlipped { get; set; }
        }

        public class PitanjaCreateVM
        {
            public int DeckId { get; set; }
            public string Pitanje { get; set; }
            public string Odgovor { get; set; }
            public Boolean isFlipped { get; set; }
        }

        // GET: api/Pitanja/5
        [ResponseType(typeof(PitanjaIndexVM))]
        public ICollection<PitanjaIndexVM> GetPitanjaByDeckId(int Deckid)
        {
            ICollection<PitanjaIndexVM> pitanja = db.Pitanja.Where(x => x.DeckId == Deckid).Select(x => new PitanjaIndexVM()
            {
                DeckId = x.DeckId,
                PitanjeId = x.PitanjeId,
                Pitanje = x.Pitanje,
                Odgovor = x.Odgovor,
                isFlipped = false
            }).ToList();


            return pitanja;
        }

        // POST: api/Pitanja
        [ResponseType(typeof(PitanjaCreateVM))]
        public IHttpActionResult PostPitanja(PitanjaCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Pitanja pitanje;

            if (model.DeckId == null || model.Pitanje.Length <= 0 || model.Odgovor.Length <= 0)
            {
                return BadRequest(ModelState);
            }

            pitanje = new Pitanja();
            pitanje.Pitanje = model.Pitanje;
            pitanje.Odgovor = model.Odgovor;
            pitanje.DeckId = model.DeckId;

            db.Pitanja.Add(pitanje);

            db.SaveChanges();

            return Ok(model);
        }

        // DELETE: api/Pitanja/5
        [ResponseType(typeof(PitanjaCreateVM))]
        public IHttpActionResult DeletePitanje(int DeckId, int PitanjeId)
        {
            Dekovi dekovi = db.Dekovi.Find(DeckId);
            // use existing category field to find cateogry
            Pitanja pitanje = db.Pitanja.Find(PitanjeId);

            if (dekovi == null || pitanje == null)
            {
                return NotFound();
            }

            db.Pitanja.Remove(pitanje);

            db.SaveChanges();

            return Ok(pitanje);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PitanjaExists(int id)
        {
            return db.Pitanja.Count(e => e.PitanjeId == id) > 0;
        }
    }
}