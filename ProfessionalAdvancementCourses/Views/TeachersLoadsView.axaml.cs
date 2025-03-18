using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProfessionalAdvancementCourses.ViewModels;

namespace ProfessionalAdvancementCourses.Views;

public partial class TeachersLoadsView : UserControl
{
    public TeachersLoadsView()
    {
        InitializeComponent();
        DataContext = new TeachersLoadsViewModel();
    }
}