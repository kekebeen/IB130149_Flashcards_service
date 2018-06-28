using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IB130149_Flashcard_Service.Models
{
    [Table("Profili")]
    public partial class Profili
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profili() { }
        
        [Key]
        public int ProfilId { get; set; }
        
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string Opstina { get; set; }
        public string BrojTelefona { get; set; }
    }
}