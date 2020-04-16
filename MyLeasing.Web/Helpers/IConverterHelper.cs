using MyLeasing.Web.Data.Entity;
using MyLeasing.Web.Models;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Property> ToPropertyAsync(PropertyViewModel model, bool isNew);
        object TopropertyViewModel(Property property);
        Task<Contract> ToContractAsync(ContractViewModel view, bool isNew);
        ContractViewModel ToContractViewModel(Contract contract);
    }
}