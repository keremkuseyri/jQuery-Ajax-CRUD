using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jQuery_Ajax_CRUD.Models
{
    public class ToSyncModel2
    {
        [Key]
        public int ID { get; set; }
        public int StockCode { get; set; }
        public string Urun { get;set; }
        public int Adet { get; set; }
        public DateTime Tarih { get; set; }
        public string Belge { get; set; }

    }
}
