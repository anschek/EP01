using Avalonia.Media.Imaging;
using ConferencesSystem.Models;
using ConferencesSystem.Models.DTOs;
using ConferencesSystem.Views;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConferencesSystem.ViewModels
{
    /* Главное окно системы
	В этом окне неавторизованный пользователь может просмотреть мероприятия, отфильтровав 
	их по направлению или по дате. Информация для просмотра: логотип, название мероприятия, 
	направление мероприятия, дата. Переход к авторизации.

	Критерии оценки:
	- Список мероприятий отображается верно		Минус 40% за каждую ошибку
	- Реализован фильтр по направлению		Минус 40% за каждую ошибку
	- Реализован поиск по дате		Минус 40% за каждую ошибку
	- Переход на авторизацию
	*/

    public class ActivitiesListViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainVm;
        public ActivitiesListViewModel(MainWindowViewModel mainVm)
        {
            _mainVm = mainVm;
            FilterActivities();
        }
        private IEnumerable<Activity> _baseActivities => _db.Activities.Include(a => a.ActivityTypeNavigation);
        private List<ActivityDto> _activities;
        public List<ActivityDto> Activities { get => _activities; set => this.RaiseAndSetIfChanged(ref _activities, value); }
        public void FilterActivities()
        {
            Activities = _baseActivities
                .Where(ActivityMatchesDate)
                .Where(ActivityMatchesType)
                .Select(MapToActivityDto).ToList();
        }
        private ActivityDto MapToActivityDto(Activity activity) =>
            new()
            {
                Date = activity.Date.ToString("dd MMMM yyyy"),
                Description = activity.Description,
                Type = activity.ActivityTypeNavigation.Name,
                Image = FoundImage(activity.Image)
                ?? new Bitmap(_baseImagePath + "empty.jpg")
            };
        private Bitmap? FoundImage(string? fileName)
        {
            var file = Directory.GetFiles(_baseImagePath, $"{fileName}.*").FirstOrDefault();
            return (fileName == null || file == null) ? null : new Bitmap(file);
        }
        private DateTime _searchDate = DateTime.MinValue;
        public string SearchDate
        {
            get => _searchDate.ToString();
            set
            {
                this.RaiseAndSetIfChanged(ref _searchDate, DateTime.Parse(value));
                FilterActivities();
            }
        }
        private bool ActivityMatchesDate(Activity activity)
            => _searchDate == DateTime.MinValue
            || _searchDate.Date == activity.Date.Date;
        private ActivitiyType? _selectedType;
        public ActivitiyType? SelectedType
        {
            get => _selectedType; 
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedType, value);
                FilterActivities();
            }
        }
        public List<ActivitiyType> Types => _db.ActivitiyTypes.ToList();
        private bool ActivityMatchesType(Activity activity)
            => _selectedType == null
            || activity.ActivityType == _selectedType.Id;
        public void ResetFilters()
        {
            SearchDate = DateTime.MinValue.ToString();
            SelectedType = null;
        }
        public void GoToAuthView()
        {
            _mainVm.CurrentView = new AuthView(_mainVm);
        }
    }
}