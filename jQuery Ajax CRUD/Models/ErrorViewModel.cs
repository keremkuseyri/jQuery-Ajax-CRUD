using System;
using System.ComponentModel.DataAnnotations;

namespace jQuery_Ajax_CRUD.Models
{
    
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
