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
    public class TicketModel
    { [Key]
        public int Id { get; set; }
        public string TicketHeader { get; set; }
        public string TicketContent { get; set; }
        [Column(TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        public string TicketAnswer { get; set; }
        public string Condition { get; set; }
        public string UserName  { get; set; }
        
    }
}
