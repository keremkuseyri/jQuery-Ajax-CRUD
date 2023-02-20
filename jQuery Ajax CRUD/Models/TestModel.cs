using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace jQuery_Ajax_CRUD.Models
{
    public class TestModel
    {
        [Key]
        public int TestID { get; set; }
        public int QNumber { get; set; }
        public string QText { get; set; }
        public string QAnswer { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public string StdAnswer { get; set; }
        
    }
}
