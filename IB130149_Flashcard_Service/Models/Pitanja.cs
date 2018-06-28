using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IB130149_Flashcard_Service.Models
{
    [Table("Pitanja")]
    public partial class Pitanja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pitanja() { }

        [Key]
        public int PitanjeId { get; set; }

        public string Pitanje { get; set; }
        public string Odgovor { get; set; }

        [ForeignKey("Dekovi")]
        public int DeckId { get; set; }
        [JsonIgnore]
        public virtual Dekovi Dekovi { get; set; }
    }
}