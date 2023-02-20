using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jQuery_Ajax_CRUD.Models
{
    public class SyncModel1
    {
        [Key]
        public int ID { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Hanesi { get; set; }
        public string Maasi { get; set; }
        public string AySayisi { get; set; }
    }
}
