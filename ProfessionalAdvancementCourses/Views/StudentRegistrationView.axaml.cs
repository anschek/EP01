using Avalonia.Controls;
using ProfessionalAdvancementCourses.ViewModels;
namespace ProfessionalAdvancementCourses.Views;

public partial class StudentRegistrationView : UserControl
{
    public StudentRegistrationView()
    {
        InitializeComponent();
        DataContext = new StudentRegistrationViewModel();
    }
}