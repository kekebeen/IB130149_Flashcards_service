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
    public class DekoviController : ApiController
    {
        private DBContext db = new DBContext();

        public class DekoviIndexVM
        {
            public int DeckId { get; set; }
            public string Ime { get; set; }
            public int KategorijaId { get; set; }
            public string Kategorija { get; set; }
            public int KorisnikId { get; set; }
        }

        public class DekoviCreateVM
        {
            public int DeckId { get; set; }
            public string Ime { get; set; }
            public int KategorijaId { get; set; }
            public string Kategorija { get; set; }
            public int KorisnikId { get; set; }
        }

        // GET: api/Dekovi
        public ICollection<DekoviIndexVM> GetDekovi(int KorisnikID)
        {
            ICollection<DekoviIndexVM> dekovi = db.Dekovi.Where(x => x.KorisnikId == KorisnikID).Select(x => new DekoviIndexVM(){
                DeckId = x.DeckId,
                Ime = x.Ime,
                KategorijaId = x.KategorijaId,
                Kategorija = x.Kategorije.Naziv,
                KorisnikId = x.KorisnikId
            }).ToList();

            return dekovi;
        }


        // POST: api/Dekovi
        [ResponseType(typeof(DekoviCreateVM))]
        public IHttpActionResult PostDekovi(DekoviCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dekovi dek;

            if(model == null)
            {
                return BadRequest(ModelState);
            }

            dek = new Dekovi();
            dek.Ime = model.Ime;
            dek.KorisnikId = model.KorisnikId;

            // create new category for deck
            Kategorije kategorija = new Kategorije();
            kategorija.Naziv = model.Kategorija;


            db.Dekovi.Add(dek);
            db.Kategorije.Add(kategorija);

            db.SaveChanges();

            return Ok(dek);
        }

        // DELETE: api/Dekovi/5
        [ResponseType(typeof(DekoviCreateVM))]
        public IHttpActionResult DeleteDekovi(int DeckId)
        {
            Dekovi dekovi = db.Dekovi.Find(DeckId);
            // use existing category field to find cateogry
            Kategorije kategorije = db.Kategorije.Find(dekovi.KategorijaId);

            if (dekovi == null)
            {
                return NotFound();
            }

            db.Dekovi.Remove(dekovi);

            if(kategorije != null)
            {
                db.Kategorije.Remove(kategorije);
            }

            db.SaveChanges();

            return Ok(dekovi);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DekoviExists(int id)
        {
            return db.Dekovi.Count(e => e.DeckId == id) > 0;
        }
    }
}