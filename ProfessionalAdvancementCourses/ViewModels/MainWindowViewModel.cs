
using Avalonia.Controls;
using ProfessionalAdvancementCourses.Views;
using ReactiveUI;

namespace ProfessionalAdvancementCourses.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        UserControl _currentView = new DescriptionView();
        public UserControl CurrentView { get => _currentView; private set => this.RaiseAndSetIfChanged(ref _currentView, value); }

        public void GoToSchedule()
        {
            CurrentView = new ScheduleView();
        }
        public void GoToDescription()
        {
            CurrentView = new DescriptionView();
        }
    }
}
