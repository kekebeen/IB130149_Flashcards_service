using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IB130149_Flashcard_Service.Models
{
    [Table("Kategorije")]
    public partial class Kategorije
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategorije() { }

        [Key]
        public int KategorijaId { get; set; }
        public string Naziv { get; set; }

        [JsonIgnore]
        public virtual ICollection<Dekovi> Dekovi { get; set; }
        
    }
}