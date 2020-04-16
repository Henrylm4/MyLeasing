using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entity;
using MyLeasing.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;//data context para consultar base de datos con await
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext dataContext, ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public async Task<Contract> ToContractAsync(ContractViewModel view, bool isNew)
        {
            return new Contract
            {
                EndDate = view.EndDate.ToUniversalTime(),
                IsActive = view.IsActive,
                Lessee = await _dataContext.Lessees.FindAsync(view.LesseeId),
                Owner = await _dataContext.Owners.FindAsync(view.OwnerId),
                Price = view.Price,
                Property = await _dataContext.Properties.FindAsync(view.PropertyId),
                Remarks = view.Remarks,
                StartDate = view.StartDate.ToUniversalTime(),//para guardar en hora de londres
                Id = isNew? 0:view.Id
            };

        }

        public ContractViewModel ToContractViewModel(Contract contract)
        {
            return new ContractViewModel
            {
                EndDate = contract.EndDateLocal,
                IsActive = contract.IsActive,
                Lessee = contract.Lessee,
                Owner = contract.Owner,
                Price = contract.Price,
                Property = contract.Property,
                Remarks = contract.Remarks,
                StartDate = contract.StartDateLocal,
                Id = contract.Id,
                LesseeId=contract.Owner.Id,
                Lessees=_combosHelper.GetComboLessees(),
                OwnerId=contract.Owner.Id,
                PropertyId=contract.Property.Id
            };
            }

        

        public async Task<Property> ToPropertyAsync(PropertyViewModel model, bool isNew)
        {
            return new Property
            {
                Address = model.Address,
                Contracts = isNew ? new List<Contract>() : model.Contracts,
                PropertyImages = isNew ? new List<PropertyImage>() : model.PropertyImages,
                HasParkingLot = model.HasParkingLot,
                Id = isNew ? 0 : model.Id,
                IsAvailable = model.IsAvailable,
                Neighborhood = model.Neighborhood,
                Owner = await _dataContext.Owners.FindAsync(model.OwnerId),
                Price = model.Price,
                Rooms = model.Rooms,
                SquareMeters = model.SquareMeters,
                Stratum = model.Stratum,
                PropertyType = await _dataContext.PropertyTypes.FindAsync(model.PropertyTypeId),
                Remarks = model.Remarks
            };
        }

        public object TopropertyViewModel(Property property)
        {
            return new PropertyViewModel
            {
                Address = property.Address,
                Contracts = property.Contracts,
                PropertyImages = property.PropertyImages,
                HasParkingLot = property.HasParkingLot,
                Id = property.Id,
                IsAvailable = property.IsAvailable,
                Neighborhood = property.Neighborhood,
                Owner = property.Owner,
                Price = property.Price,
                Rooms = property.Rooms,
                SquareMeters = property.SquareMeters,
                Stratum = property.Stratum,
                PropertyType = property.PropertyType,
                Remarks = property.Remarks,
                OwnerId = property.Owner.Id,
                PropertyTypes = _combosHelper.GetComboPropertyTypes() 
            };
        }
    }
}
