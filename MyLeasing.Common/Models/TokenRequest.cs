using System;
using System.Collections.Generic;
using System.Text;

namespace MyLeasing.Common.Models
{
    public class TokenRequest //clase que obtiene los atributos de las propiedades de usuario y contraseña para enviarlos al api
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

}
