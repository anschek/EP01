using System.Collections.Generic;
using System.Linq;
using ConferencesSystem.Models;
using ReactiveUI;

namespace ConferencesSystem.ViewModels
{
    /*Окно регистрации жюри/модераторов
     * 
	 Реализуйте интерфейс для регистрации жюри/модераторов. Эта функция доступна
	только организаторам.
	При открытии окна регистрации система генерирует уникальный ID Number,
	изменить его нельзя.
	При регистрации жюри/модератора необходимо указать
		• ФИО;
		• пол (выбор из списка);
		• роль (выбор из списка)’
		• e-mail;
		• телефон в формате +7(999) - 099-90-90;
		• направление (выбор из выпадающего списка или ручной ввод; при ручном
		вводе данные сохраняются в базе данных);
		• мероприятие (в случае выбора функции «Прикрепить к мероприятию», выбор
		из списка);
		• фото (необязательное поле);
		• пароль (с повтором), соответствующий требованиям:
		• не менее 6 символов;
		• заглавные и строчные буквы;
		• не менее одного спецсимвола;
		• не менее одной цифры.

	Критерии оценки:
	- Окно создано и соответствует макету		Минус 40% за каждую ошибку
	- Окно доступно только организаторам		
	- Происходит генерирация уникального ID Number, изменить его нельзя		
	- Выбор пола из списка		
	- Маска на поле телефон		
	- Выбор роли из списка		
	- Ввод корректного e-mail		
	- Требования к паролю учтены		Минус 40% за каждую ошибку
	- Изображение загружается		
	- Повтор пароля реализован с указанием видимости		Минус 40% за каждую ошибку
	*/
    public class UserRegistrationViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainVm;
        public UserRegistrationViewModel(MainWindowViewModel mainVm)
        {
            _mainVm = mainVm;
            _id = (_db.Users.OrderBy(u => u.Id).LastOrDefault()?.Id ?? 0) + 1;
        }
        private User _user = new User { Activities = new List<Activity>() };
        public User User { get => _user; set => this.RaiseAndSetIfChanged(ref _user, value); }
        private int _id;
        public int Id => _id;
        private string _rePassword = "";
        public string RePassword { get => _rePassword; set => this.RaiseAndSetIfChanged(ref _rePassword, value); }
        public List<Gender> Genders => _db.Genders.ToList();
        private Gender? _selectedGender;
        public Gender? SelectedGender { get => _selectedGender; set => this.RaiseAndSetIfChanged(ref _selectedGender, value); }
        public List<Role> Roles => _db.Roles.Where(r => r.Id == 3 || r.Id == 4).ToList();
        private Role? _selectedRole;
        public Role? SelectedRole { get => _selectedRole; set => this.RaiseAndSetIfChanged(ref _selectedRole, value); }

        private bool _attachToActivity = false;
        public bool AttachToActivity { get => _attachToActivity; set => this.RaiseAndSetIfChanged(ref _attachToActivity, value); }
        private bool _displayPassword = false;
        public bool DisplayPassword
        {
            get => _displayPassword;
            set
            {
                this.RaiseAndSetIfChanged(ref _displayPassword, value);
                PasswordChar = DisplayPassword ? '\0' : '*';
            }
        }
        public char _passwordChar = '*';
        public char PasswordChar { get => _passwordChar; set => this.RaiseAndSetIfChanged(ref _passwordChar, value); }
        public List<DirectionType> Directions => _db.DirectionTypes.ToList();
        private DirectionType? _selectedDirection;
        public DirectionType? SelectedDirection
        {
            get => _selectedDirection;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedDirection, value);
                Activities = _db.Activities.Where(a => SelectedDirection == null || a.ActivityType == SelectedDirection.Id).ToList();
            }
        }
        public List<Activity> _activities = new();
        public List<Activity> Activities { get => _activities; set => this.RaiseAndSetIfChanged(ref _activities, value); }

        private Activity? _selectedActivity;
        public Activity? SelectedActivity { get => _selectedActivity; set => this.RaiseAndSetIfChanged(ref _selectedActivity, value); }
        private string _errorMessage = "";
        public  string ErrorMessage{ get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }
        public void CreateNewUser() // ок - уведомление и сохранить
        {
            // regex email
            // regex password
            // картинка загружается
        }        
        public void CancelUserCreation() // отмена - данные потеряются, вы уверены? да/нет
        {
            
        }



    }
}