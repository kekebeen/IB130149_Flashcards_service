using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IB130149_Flashcard_Service.Models
{
    [Table("Dekovi")]
    public partial class Dekovi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dekovi() { }

        [Key]
        public int DeckId { get; set; }
        public string Ime { get; set; }
        
        [ForeignKey("Kategorije")]
        public int KategorijaId { get; set; }

        [ForeignKey("Korisnici")]
        public int KorisnikId { get; set; }
        [JsonIgnore]
        public virtual Korisnici Korisnici { get; set; }

        [JsonIgnore]
        public virtual Kategorije Kategorije { get; set; }
        [JsonIgnore]
        public virtual List<Pitanja> Pitanja { get; set; }
    }
}