using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jQuery_Ajax_CRUD.Models
{
    
  
    public class TestResults
    {   [Key]
        public int ResultID { get; set; }
        
        
        public string StdName { get; set; }
       
        
        public int StdNumber { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Note { get; set; }
        
        public int Score { get; set; }
        [Column(TypeName = "decimal")]
        public decimal TestScore { get; set; }
        

    }
}
