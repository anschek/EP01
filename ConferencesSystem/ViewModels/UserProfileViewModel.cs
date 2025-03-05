
using Avalonia.Media.Imaging;
using ConferencesSystem.Models;
using ConferencesSystem.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferencesSystem.ViewModels
{
    /* Профиль пользователя
	 Реализуйте интерфейс для работы организатора мероприятий. При входе система
	приветствует пользователя по имени и отчеству с указанием времени работы:
	9.00-11.00 – Утро, 11.01-18.00 – День, 18.01 – 24.00 – Вечер
	При входе в систему подгружается фото пользователя. 

	Макет: название окна, приветствие, имя, фото

	Критерии оценки:
	- Окно создано и соответствует макету
	- Система приветствует пользователя по имени и отчеству 
	- Система приветствует пользователя  с указанием времени работы
	- При входе в систему подгружается фото пользователя
	*/
    public class UserProfileViewModel : ViewModelBase
	{
		private MainWindowViewModel _mainVm;
		private User _user;
        public UserProfileViewModel(MainWindowViewModel mainVm, int userId)
		{
			_mainVm = mainVm;
			_user = _db.Users.Find(userId)!;
			SetGreeting();
            SetViewDescription();
		}
		private string _greeting;
		public string Greeting { get => _greeting; private set => this.RaiseAndSetIfChanged(ref _greeting, value); }
		private void SetGreeting()
		{
			Greeting = DateTime.Now.Hour switch
			{
				>= 9 and <=11 => "Доброе утро",
				>11 and <=18 => "Добрый день",
				_ => "Добрый вечер"
			} + $", {_user.FullName}!";
		}
		private string _viewDescription;
        public string ViewDescription { get => _viewDescription; private set => this.RaiseAndSetIfChanged(ref _viewDescription, value); }
		private void SetViewDescription()
		{
			ViewDescription = "Окно " + _user.Role switch
			{
				2 => "Организатора",
				3 => "Модератора",
				4 => "Жюри",
				_ => "Участника"
			};
        }
		public List<string> Activities => _db.Activities.Select(a => a.Description).ToList();
		public List<string> Participants => _db.Users.Where(u => u.Role == 1).Select(u => u.FullName).ToList();
		public List<string> Jurors => _db.Users.Where(u => u.Role == 4).Select(u => u.FullName).ToList();
		public bool IsOrganizer => _user.Role == 2;
		public Bitmap UserAvatar => _user.Image == null 
			? new Bitmap(_baseImagePath + "empty.jpg") 
			: new Bitmap(_baseImagePath + _user.Image);
        public void GoToRegistrationView()
        {
            _mainVm.CurrentView = new UserRegistrationView(_mainVm, _user.Id);
        }
    }
}