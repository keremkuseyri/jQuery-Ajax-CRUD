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
    public class LessonsModel
    {
        
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "int")]
        [DisplayName("Class")]
        [Required(ErrorMessage = "This Field is required.")]
        public int Class { get; set; }
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Subject Name")]
        [Required(ErrorMessage = "This Field is required.")]

        public string A { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Teacher")]
        [Required(ErrorMessage = "This Field is required.")]
        public string Teacher { get; set; }
        
       
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}")]
        [Column(TypeName = "datetime")]
        [DisplayName("Start Time")]
        [Required(ErrorMessage = "This Field is required.")]
        public DateTime ClassStart { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}")]
        [Column(TypeName = "datetime")]
        [DisplayName("End Time")]
        [Required(ErrorMessage = "This Field is required.")]
        public DateTime ClassEnd { get; set; }


    }

        






}



