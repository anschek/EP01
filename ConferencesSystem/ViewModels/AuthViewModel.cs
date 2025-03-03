using ConferencesSystem.Models;
using ConferencesSystem.Views;
using ReactiveUI;
using System.Linq;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace ConferencesSystem.ViewModels
{
    /*Авторизация
	Создайте экран авторизации. В качестве учетных данных необходимо использовать
	IdNumber и Password. При вводе корректных данных пользователь должен перейти в «Окно
	организатора», «Окно участника», «Окно модератора», «Окно жюри».
	Для обеспечения безопасности реализуйте CAPTCHA (4 символа и графический
	шум) и блокировку системы на 10 секунд в случае неправильного ввода учетных данных
	после трех попыток входа. 
	Кроме этого, необходимо реализовать запоминание учетных данных пользователя.
	 
	Критерии оценки:
	- Окно авторизации реализовано		
	- Авторизация работает на основе БД		
	- Переход осуществляется в соответствии с ролью пользователя		Минус 40% за каждую ошибку или отсутствующее окно учетной записи (хотя бы заголовок окна)
	*/
    public class AuthViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainVm;
        public AuthViewModel(MainWindowViewModel mainVm)
        {
            _mainVm = mainVm;
        }
        private int _failedAuthAttempts = 0;
        private string _login = "";
        public string Login { get => _login; set => this.RaiseAndSetIfChanged(ref _login, value); }
        private string _password = "";
        public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
        private string _errorMessage = "";
        public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }
        private bool _systemIsNotFrozen = true;
        public bool SystemIsNotFrozen { get => _systemIsNotFrozen; private set => this.RaiseAndSetIfChanged(ref _systemIsNotFrozen, value); }
        public async void Auth()
        {
            User? user = _db.Users.FirstOrDefault(u => u.Id.ToString() == Login && u.Password == Password);
            if (user != null)
            {
                _mainVm.CurrentView = new UserProfileView(_mainVm, user.Id);
            }
            else
            {
                ++_failedAuthAttempts;
                ShowErrorMessage();

                if (_failedAuthAttempts == 3)
                {
                    SystemIsNotFrozen = false;
                    await Task.Delay(10000);
                    _failedAuthAttempts = 0;
                    SystemIsNotFrozen = true;
                }
            }
        }
        public async void ShowErrorMessage()
        {
            ErrorMessage = _failedAuthAttempts < 3 ? $"Попыток осталось: {3 - _failedAuthAttempts}"
            : $"Система заблокирована. Повторите попытку через 10 секунд";
            await Task.Delay(4000);
            ErrorMessage = "";
        }
    }
}