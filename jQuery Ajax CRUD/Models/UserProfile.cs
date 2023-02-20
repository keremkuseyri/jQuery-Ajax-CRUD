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
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string UserName  { get; set; }
        public string Password { get; set; }
    }
}
