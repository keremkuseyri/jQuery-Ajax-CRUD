using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jQuery_Ajax_CRUD.Models
{
    public class GradesModel
    {   
        [Key]
        public int Id { get; set; }
        public int Number   { get; set; }
        [DisplayName("Student")]
        public string Name { get; set; }
        [DisplayName("Subject")]
        public string A { get; set; }
        public int Class { get; set; }
        public string Teacher { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Visa1 { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Visa2 { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Final { get; set; }
        [Column(TypeName="decimal")]
        public decimal Note { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}")]
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public string Passed { get; set; }




    }
    public class SecondGradesModel
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        [DisplayName("Student")]
        public string Name { get; set; }
        [DisplayName("Subject")]
        public string A { get; set; }
        public int Class { get; set; }
        public string Teacher { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Visa1 { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Visa2 { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Final { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Note { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}")]
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public string Passed { get; set; }
        public int TestNumbera { get; set; }
        public string TestNamea { get; set; }
    }
}
