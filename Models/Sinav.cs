using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaWeb.Models
{
    public class Sinav
    {
        [Key]
        public int ID { get; set; }
        public string Baslik { get; set; }
        public string Yazi { get; set; }

        [ForeignKey("SoruID")]
        public int SoruID { get; set; }
    }
}
