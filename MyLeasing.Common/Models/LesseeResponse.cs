using System;
using System.Collections.Generic;
using System.Text;

namespace MyLeasing.Common.Models
{
    public class LesseeResponse //Este tipo de Response es para obtener un JSON 
        //Si no se los crea, no se va a poder entregar una respuesta clara
        //En la Base de datos Lesse solo tiene 3 propiedades, porque los demas estan incluidos en user
        //Aqui se expanden mas esas propiedades para que muestre de manera clara en el JSON
        //Estas propiedades son las que va a mostrar
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }

}
