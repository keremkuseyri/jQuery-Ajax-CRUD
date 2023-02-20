using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using jQuery_Ajax_CRUD.Models;

namespace jQuery_Ajax_CRUD.Models
{
    public class BulkData
    {   [Key]
        public int BulkID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
