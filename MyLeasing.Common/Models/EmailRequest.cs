using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Common.Models
{
    public class EmailRequest //Clase para validar que lo que ingresen sean emails, se usa en el API OwnersController
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
