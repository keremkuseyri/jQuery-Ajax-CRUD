using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jQuery_Ajax_CRUD.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        [DisplayName("Name")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(12, ErrorMessage = "Maximum 12 characters only")]
        public string AccountNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Surname")]
        [Required(ErrorMessage = "This Field is required.")]
        public string BeneficiaryName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Gender")]
        [Required(ErrorMessage = "This Field is required.")]
        public string BankName { get; set; }
        [Column(TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Year of Birth")]

        public DateTime SWIFTCode { get; set; }
        
        

        [DisplayName("Student Number")]
        [Required(ErrorMessage = "This Field is required.")]
        public int Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm}")]
        public DateTime Date { get; set; }
        [Column(TypeName = "int")]
        [DisplayName("Class")]
        [Required(ErrorMessage = "This Field is required.")]
        public int Class { get; set; }

        [ColumnAttribute(TypeName = "int")]
        [DisplayName("Age")]
        public int Age {get; set;}
           
        
        
        


    }
}
