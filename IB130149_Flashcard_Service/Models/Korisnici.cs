using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IB130149_Flashcard_Service.Models
{
    [Table("Korisnici")]
    public partial class Korisnici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Korisnici() { }

        [Key]
        public int KorisnikId { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Lozinka { get; set; }

        [Required]
        [StringLength(50)]
        public string KorisnickoIme { get; set; }

        [ForeignKey("Profili")]
        public int? ProfilId { get; set; }
        [JsonIgnore]
        public virtual Profili Profili { get; set; }
    }
}