using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyLeasing.Common.Models;
using MyLeasing.Web.Data;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entity;

namespace MyLeasing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly DataContext _dataContext; //Data Context para poder obtener valores de la base de datos

        public OwnersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //[HttpPost]//Es un get pero por seguridad se pone Post
        //[Route("GetOwnerByEmail")]//para llamar el metodo con el api/GetOwnerByEmail
        //public async Task<IActionResult> GetOwnerByEmailAsync(EmailRequest request)
        //{
        //    if (ModelState.IsValid)//valida que sea un Email lo que entro
        //    {
        //        return BadRequest();//Para que nos de el 404 en el postman
        //    }

        //    var owner = await _dataContext.Owners.Include(o =>o.User).Include(o =>o.Properties).Include(o =>o.Contracts).FirstOrDefaultAsync(o => o.User.Email.ToLower() == request.Email.ToLower());
        //    //el var owner es el objeto owner que tiene el email enviado al metodo
        //    //Se usa First o default para encontrar el primer usuario donde el email sea igual al enviado por el metodo
        //    //Si se tuviera el ID no se usa el first or default

        //    if (owner == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(owner);//retorna el objeto owner en json con el OK
        //}
        [HttpPost]
        [Route("GetOwnerByEmail")]
        public async Task<IActionResult> GetOwnerByEmailAsync(EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var owner = await _dataContext.Owners
                .Include(o => o.User)
                .Include(o => o.Properties)
                .ThenInclude(p => p.PropertyType)//se Usa el then porque propertytype depende de property
                .Include(o => o.Properties)//Se repite propertie porque despues se piensa que Property Image depende de propertytype pero depende de properties
                .ThenInclude(p => p.PropertyImages)
                .Include(o => o.Contracts)
                .ThenInclude(c => c.Lessee)
                .ThenInclude(l => l.User)
                .FirstOrDefaultAsync(o => o.User.Email.ToLower() == request.Email.ToLower());

            if (owner == null)
            {
                return NotFound();
            }

            var response = new OwnerResponse
            {
                Id = owner.Id,
                FirstName = owner.User.FirstName,
                LastName = owner.User.LastName,
                Address = owner.User.Address,
                Document = owner.User.Document,
                Email = owner.User.Email,
                PhoneNumber = owner.User.PhoneNumber,
                Properties = owner.Properties?.Select(p => new PropertyResponse
                {
                    Address = p.Address,
                    Contracts = p.Contracts?.Select(c => new ContractResponse //el signo de pregunta es para validar que existan contratos
                    {
                        EndDate = c.EndDate,
                        Id = c.Id,
                        IsActive = c.IsActive,
                        Lessee = ToLessesResponse(c.Lessee),
                        Price = c.Price,
                        Remarks = c.Remarks,
                        StartDate = c.StartDate
                    }).ToList(),
                    HasParkingLot = p.HasParkingLot,
                    Id = p.Id,
                    IsAvailable = p.IsAvailable,
                    Neighborhood = p.Neighborhood,
                    Price = p.Price,
                    PropertyImages = p.PropertyImages?.Select(pi => new PropertyImageResponse
                    {
                        Id = pi.Id,
                        ImageUrl = pi.ImageFullPath
                    }).ToList(),
                    PropertyType = p.PropertyType.Name,
                    Remarks = p.Remarks,
                    Rooms = p.Rooms,
                    SquareMeters = p.SquareMeters,
                    Stratum = p.Stratum
                }).ToList()
            };

            return Ok(response);
        }

        private LesseeResponse ToLessesResponse(Lessee lessee)
        {
            return new LesseeResponse
            {
                Id = lessee.Id,
                Address = lessee.User.Address,
                Document = lessee.User.Document,
                Email = lessee.User.Email,
                FirstName = lessee.User.FirstName,
                LastName = lessee.User.LastName,
                PhoneNumber = lessee.User.PhoneNumber
            };
        }
    }


}
