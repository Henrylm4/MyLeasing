using MyLeasing.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyLeasing.Prism.ViewModels
{
    public class PropertiesPageViewModel : ViewModelBase
    {
        private OwnerResponse _owner;
        private ObservableCollection<PropertyResponse> _properties; //Lista para el lsit view
        public PropertiesPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Properties";
        }
        public ObservableCollection<PropertyResponse> Properties {
            get => _properties;
            set => SetProperty(ref _properties, value);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);//metodo para obtener los parametros de la pagina anterior
            if (parameters.ContainsKey("owner"))
            {
                _owner = parameters.GetValue<OwnerResponse>("owner");
                Title = $"Properties of: {_owner.Fullname}";
                Properties = new ObservableCollection<PropertyResponse>(_owner.Properties);
            }
        }
    }
}
