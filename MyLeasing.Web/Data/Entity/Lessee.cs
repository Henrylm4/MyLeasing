﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data.Entity
{
    public class Lessee
    {
        public int Id { get; set; }

        public User User { get; set; }
        public ICollection<Contract> Contracts { get; set; }//Relacion un Propietario tiene muchas propiedades

    }

}
