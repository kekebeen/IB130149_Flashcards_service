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
    public class AuthController : ApiController
    {
        private DBContext db = new DBContext();

        public class KorisnikVM
        {
            public int KorisnikId { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Email { get; set; }
            public string Lozinka { get; set; }
            public string KorisnickoIme { get; set; }
        }

        // GET: api/Korisnici
        public IQueryable<Korisnici> GetKorisnici()
        {
            return db.Korisnici;
        }

        // GET: api/Korisnicis/5
        [ResponseType(typeof(KorisnikVM))]
        public IHttpActionResult GetKorisnici(int id)
        {
            KorisnikVM model = db.Korisnici.Where(x => x.KorisnikId == id).Select(x => new KorisnikVM(){
                KorisnikId = id,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Lozinka = x.Lozinka,
                KorisnickoIme = x.KorisnickoIme,
                Email = x.Email
            }).SingleOrDefault();

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet]
        [ResponseType(typeof(KorisnikVM))]
        [Route("api/Auth/Prijava/")]
        public IHttpActionResult Prijava(string username, string password)
        {
            KorisnikVM korisnik = (from ko in db.Korisnici where (ko.KorisnickoIme == username && ko.Lozinka == password) select new KorisnikVM() {
                Email = ko.Email,
                Ime = ko.Ime,
                Prezime = ko.Prezime,
                KorisnickoIme = ko.KorisnickoIme,
                KorisnikId = ko.KorisnikId,
                Lozinka = ko.Lozinka
            }).FirstOrDefault();

            if(korisnik == null)
            {
                return Content(HttpStatusCode.NotFound, "Not found");
            }

            return Ok(korisnik);
        }

        // POST: api/Auth
        [ResponseType(typeof(KorisnikVM))]
        public IHttpActionResult PostKorisnici(KorisnikVM model)
        {
            Korisnici korisnik = db.Korisnici.Where(x => x.Ime == model.Ime || x.Prezime == model.Prezime || x.Email == model.Email || x.KorisnickoIme == model.KorisnickoIme || x.Lozinka == model.Lozinka).FirstOrDefault();

            if (model == null || korisnik != null)
            {
                return Content(HttpStatusCode.NotFound, "Error");
            } else
            {
                korisnik = new Korisnici();
                korisnik.Email = model.Email;
                korisnik.Ime = model.Ime;
                korisnik.Prezime = model.Prezime;
                korisnik.Lozinka = model.Lozinka;
                korisnik.KorisnickoIme = model.KorisnickoIme;
                db.Korisnici.Add(korisnik);
                db.SaveChanges();
                return Ok(korisnik);
            }

        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KorisniciExists(int id)
        {
            return db.Korisnici.Count(e => e.KorisnikId == id) > 0;
        }
    }
}