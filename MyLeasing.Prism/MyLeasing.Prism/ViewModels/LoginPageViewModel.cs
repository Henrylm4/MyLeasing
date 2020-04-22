using MyLeasing.Common.Models;
using MyLeasing.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLeasing.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;
        public LoginPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = "Login";
            _isEnabled = true;//por defecto los bool arrancan en face, hay que iniciarlo verdadero 
            _apiService = apiService;
        }
        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login)); //Cuando hagan clic en el boton Login, ejecuta en metodo Login



        public string Email { get; set; }
        public string Password //Password es un campo que vamos a manipular(cuando el usuario ingrese mal la contrase;a vamos a limpiar este campo, por ello creamos un campo privado en la clase con "_" y otro que comienza con Mayuscula representando el Bindiado
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You mus enter an email", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You mus enter an Password", "Accept");
                return;
            }
            IsRunning = true;//se activa el activity indicate
            IsEnabled = false;//se desactiva el boton

            var url = App.Current.Resources["UrlAPI"].ToString();//Accede a la biblioteca de recursos de App.xaml para sacar el url
            var connection = await _apiService.CheckConnection(url);//chequea si tiene internet antes de comprobar datos
            if (!connection)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }

            var request = new TokenRequest //se crea la clase para enviar el usuario y contrase;a en json
            {
                Password = Password,
                Username = Email
            };
            var response = await _apiService.GetTokenAsync(url, "Account", "/CreateToken", request);//usa el metodo del api para obtener respuesta del usuario y contrase;a

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "User or password incorrect.", "Accept");
                Password = string.Empty;//Limpia la caja password
                return;
            }

            var token = response.Result;//Response es un objeto donde el .result devuelve  el token

            IsRunning = false;
            IsEnabled = true;
        }
    }
}