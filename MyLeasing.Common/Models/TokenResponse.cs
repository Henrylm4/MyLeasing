using System;
using System.Collections.Generic;
using System.Text;

namespace MyLeasing.Common.Models
{
    public class TokenResponse //Respuesta al momento de enviar usuario y contraseña, nos devuelve el token
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public DateTime ExpirationLocal => Expiration.ToLocalTime();
    }

}
