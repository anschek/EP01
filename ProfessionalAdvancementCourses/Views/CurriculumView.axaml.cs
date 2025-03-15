using Avalonia.Controls;
using ProfessionalAdvancementCourses.ViewModels;

namespace ProfessionalAdvancementCourses.Views;

public partial class CurriculumView : UserControl
{
    public CurriculumView()
    {
        InitializeComponent();
        DataContext = new CurriculumViewModel();
    }
}