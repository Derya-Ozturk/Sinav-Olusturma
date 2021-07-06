using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaWeb.Models
{
    public class Sorular
    {
        [Key]
        public int SoruID { get; set; }
        public string Soru { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public char DogruCevap { get; set; }
    }
}
