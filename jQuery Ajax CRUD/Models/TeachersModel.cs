using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jQuery_Ajax_CRUD.Models
{
    public class TeachersModel
    {

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Name")]
        [Required(ErrorMessage = "This Field is required.")]
        
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        [DisplayName("Surname")]
        [Required(ErrorMessage = "This Field is required.")]
        public string Surname { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime")]
        [DisplayName("Date of Birth")]
        [Required(ErrorMessage = "This Field is required.")]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "varchar(50)")]
        [DisplayName("Gender")]
        [Required(ErrorMessage = "This Field is required.")]
        public string Gender { get; set; }

        [Column(TypeName = "int")]
        [DisplayName("Subject")]
        [Required(ErrorMessage = "This Field is required.")]
        public int Subject { get; set; }

        [Column(TypeName = "int")]
        [DisplayName("Class")]
        [Required(ErrorMessage = "This Field is required.")]
        public int Class { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}")]
        public DateTime Date { get; set; }
      
        [Column(TypeName = "varchar(50)")]
        [DisplayName("SubjectName")]
        public string A { get; set; }

        [Column(TypeName ="int")]
        [DisplayName("Age")]
        
        public int Age { get; set; }

    }





    }



