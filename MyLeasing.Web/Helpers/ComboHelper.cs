using Microsoft.AspNetCore.Mvc.Rendering;
using MyLeasing.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers
{
    public class ComboHelper : ICombosHelper
    //clase para obtener los datos de la base de datos para el combo box
    {
        private readonly DataContext _dataContext;
        public ComboHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboPropertyTypes()
        {
            var list = _dataContext.PropertyTypes.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a property type...)",
                Value = "0"
            });
            return list;
        }
    }
}
