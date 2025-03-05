using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using ConferencesSystem.Models;
using ConferencesSystem.Views;
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
        private int _organizerId;
        public UserRegistrationViewModel(MainWindowViewModel mainVm, int userId)
        {
            _mainVm = mainVm;
            _organizerId = userId;
            _id = (_db.Users.OrderBy(u => u.Id).LastOrDefault()?.Id ?? 0) + 1;
            _user = new User();
            _userAvatar = new Bitmap(_baseImagePath + "empty.jpg");
        }
        private User _user;
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
        public string ErrorMessage { get => _errorMessage; set => this.RaiseAndSetIfChanged(ref _errorMessage, value); }
        public async void CreateNewUser() // ок - уведомление и сохранить
        {
            // todo
            // добавить картинку
            // картинка загружается

            if (!await AllValidationChecksPassed()) return;

            User.Id = Id;
            User.GenderNavigation = SelectedGender;
            User.RoleNavigation = SelectedRole;
            if (User.RoleNavigation.Name == "модератор")
            {
                User.Moderator = new Moderator { ActivityType = SelectedDirection.Id, DirectionType = SelectedDirection.Id };
            }
            else if (User.RoleNavigation.Name == "жюри")
            {
                User.Juror = new Juror { Direction = SelectedDirection.Id };
            }
            if (AttachToActivity)
            {
                User.Activities.Add(SelectedActivity);
            }

            try
            {
                _db.Users.Add(User);
                _db.SaveChanges();
                await ShowError("Новый пользователь сохранен успешно", 5000);
                GoBack();
            }
            catch
            {
                await ShowError("Что-то пошло не так", 10000);
            }
        }
        private async Task<bool> AllValidationChecksPassed()
        {
            var message = RequiredFieldsAreFilledIn();
            if (message != null)
            {
                await ShowError(message, 5000);
                return false;
            }

            if (!PasswordIsValid())
            {
                await ShowError("Не соблюдены условия к паролю:\n•не менее 6 символов;\n• заглавные и строчные буквы;\n" +
                    "• не менее одного спецсимвола;\r\n• не менее одной цифры.", 5000);
                return false;
            }

            if (User.Password != RePassword)
            {
                await ShowError("Пароли не совпадают", 5000);
                return false;
            }

            if (!EmailIsValid())
            {
                await ShowError("Почта указана неверно", 5000);
                return false;
            }

            return true;
        }
        private async Task ShowError(string message, int ms)
        {
            ErrorMessage = message;
            await Task.Delay(ms);
            ErrorMessage = "";
        }
        private string? RequiredFieldsAreFilledIn()
        {
            string message = "";
            if (string.IsNullOrWhiteSpace(User.FullName)) message += "Имя\n";
            if (SelectedGender == null) message += "Пол\n";
            if (SelectedRole == null) message += "Роль\n";
            if (string.IsNullOrWhiteSpace(User.Mail)) message += "Почтовый адрес\n";
            if (string.IsNullOrWhiteSpace(User.PhoneNumber)) message += "Номер телефона\n";
            if (SelectedDirection == null) message += "Направление\n";
            if (string.IsNullOrWhiteSpace(User.Password)) message += "Пароль\n";
            if (string.IsNullOrWhiteSpace(RePassword)) message += "Повторение пароля\n";
            return message == "" ? null : "Обязательные поля не заполнены:\n" + message;
        }
        private bool PasswordIsValid()
        {
            // ^ и $ - начало и конец
            // ?=.* - есть хотя бы одно из
            // (?=.*) - перечисляются группы: заглавных и маленьких букв, цифр, спец. символов
            // .{6,} - не менее 6 символов
            Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{6,}$");
            var match = regex.Match(User.Password);
            return match.Success;
        }
        private bool EmailIsValid()
        {
            // ^ и $ - начало и конец
            // [] - можно использовать такие символы
            // []+ - один и более символоы
            // \. - экранирование точки
            Regex regex = new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            var match = regex.Match(User.Mail);
            return match.Success;
        }
        public async Task CancelUserCreation() // отмена - данные потеряются, вы уверены? да/нет
        {
            СonfirmСancellation = true;
            await ShowError("Вы уверены, что хотите отменить запись? Все данные будут утеряны. В течение 10 секунд вы можете подтвердить отмену", 10000);
            СonfirmСancellation = false;
        }
        private bool _confirmСancellation = false;
        public bool СonfirmСancellation { get => _confirmСancellation; set => this.RaiseAndSetIfChanged(ref _confirmСancellation, value); }
        public void GoBack()
        {
            _mainVm.CurrentView = new UserProfileView(_mainVm, _organizerId);
        }
        private Bitmap _userAvatar;
        public Bitmap UserAvatar { get => _userAvatar; set => this.RaiseAndSetIfChanged(ref _userAvatar, value); }
        public static FilePickerFileType ImageAll { get; } = new("All Images")
        {
            Patterns = [ "*.png", "*.jpg", "*.jpeg", "*.gif", "*.bmp", "*.webp" ]
        };
        public async void UpdateAvatar()
        {
            // получение провайдера файловой системы
            var desktop = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            var provider = desktop?.MainWindow?.StorageProvider;
            // открытие диалога выбора файла
            var selectImages = await provider.OpenFilePickerAsync(
                    new FilePickerOpenOptions()
                    {
                        Title = "Выберите изображение",
                        AllowMultiple = false,
                        FileTypeFilter =[ ImageAll ]
                    }
            );
            // чтение выбранного файла в поток
            await using var readStream = await selectImages[0].OpenReadAsync();
            using var memoryStream = new MemoryStream();
            await readStream.CopyToAsync(memoryStream);
            // генерация уникального имени файлае
            string uniqueFileName = Guid.NewGuid().ToString();
            string filePath = _baseImagePath + uniqueFileName;
            // запись файла в папку со всеми картинками
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fileStream);
            }
            // запись пользователю имени картинки
            User.Image = uniqueFileName;
            // обновление свойства с отображение картинки
            UserAvatar = User.Image == null ? new Bitmap(_baseImagePath + "empty.jpg") : new Bitmap(_baseImagePath + User.Image);
        }
    }
}