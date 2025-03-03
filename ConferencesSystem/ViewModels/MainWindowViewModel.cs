using Avalonia.Controls;
using ConferencesSystem.Views;
using ReactiveUI;

namespace ConferencesSystem.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _currentView;
        public UserControl CurrentView { get => _currentView; set => this.RaiseAndSetIfChanged(ref _currentView,value); }
        public MainWindowViewModel()
        {
            _currentView = new ActivitiesListView(this);
        }
    }
}
