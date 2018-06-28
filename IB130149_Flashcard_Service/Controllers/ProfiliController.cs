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
    public class ProfiliController : ApiController
    {
        private DBContext db = new DBContext();

        public class ProfilIndexVM
        {

        }

        public class ProfilCreateVM
        {
            public int KorisnikId { get; set; }
            public int? ProfilId { get; set; }
            public string Ulica { get; set; }
            public string Grad { get; set; }
            public string Opstina { get; set; }
            public string BrojTelefona { get; set; }

        }

        // GET: api/Profili/5
        [ResponseType(typeof(ProfilCreateVM))]
        public IHttpActionResult GetProfili(int KorisnikId)
        {
            //Profili profili = db.Profili.Find(id);
            ProfilCreateVM model = db.Korisnici.Where(x => x.KorisnikId == KorisnikId).Select(x => new ProfilCreateVM()
            {
                KorisnikId = KorisnikId,
                ProfilId = x.ProfilId,
                Ulica = x.Profili.Ulica,
                Grad = x.Profili.Grad,
                Opstina = x.Profili.Opstina,
                BrojTelefona = x.Profili.BrojTelefona

            }).SingleOrDefault();



            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // PUT: api/Profili/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProfili(ProfilCreateVM model)
        {
           if(model == null)
            {
                return BadRequest();
            }

            // get Korisnik by model Id
            Korisnici korisnik = db.Korisnici.Find(model.KorisnikId);
            Profili profil;
            if(model.ProfilId == null)
            {
                profil = new Profili();
                db.Profili.Add(profil);
            }
            else
            {
                profil = db.Profili.Find(model.ProfilId);
            }

            profil.BrojTelefona = model.BrojTelefona;
            profil.Grad = model.Grad;
            profil.Opstina = model.Opstina;
            profil.Ulica = model.Ulica;

            korisnik.ProfilId = profil.ProfilId;


            db.SaveChanges();

            return Ok(model);
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfiliExists(int id)
        {
            return db.Profili.Count(e => e.ProfilId == id) > 0;
        }
    }
}