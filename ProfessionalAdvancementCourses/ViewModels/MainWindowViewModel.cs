
using Avalonia.Controls;
using ProfessionalAdvancementCourses.Views;
using ReactiveUI;

namespace ProfessionalAdvancementCourses.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        UserControl _currentView = new DescriptionView();
        public UserControl CurrentView { get => _currentView; private set => this.RaiseAndSetIfChanged(ref _currentView, value); }
        DescriptionView _descriptionView = new DescriptionView();
        ScheduleView _schedule = new ScheduleView();
        CurriculumView _curriculumView = new CurriculumView();
        StudentRegistrationView _registrationView = new StudentRegistrationView();
        TeachersLoadsView _loadsView = new TeachersLoadsView();

        public void GoToSchedule() => CurrentView = _schedule;
        public void GoToDescription() => CurrentView = _descriptionView;
        public void GoToCurriculum() => CurrentView = _curriculumView;
        public void GoToStudentRegistration() => CurrentView = _registrationView;        
        public void GoToTeachersLoads() => CurrentView = _loadsView;        
    }
}
