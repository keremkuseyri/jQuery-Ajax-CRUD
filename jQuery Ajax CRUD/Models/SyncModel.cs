using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jQuery_Ajax_CRUD.Models
{
    public class SyncModel
    {
        [Key]
        public int ID { get; set; }
        public int Numara { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int DogumTarihi { get; set; }
        public string Cinsiyeti { get; set; }

    }
}
