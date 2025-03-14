using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProfessionalAdvancementCourses.ViewModels;

namespace ProfessionalAdvancementCourses.Views;

public partial class ScheduleView : UserControl
{
    public ScheduleView()
    {
        InitializeComponent();
        DataContext = new ScheduleViewModel();
    }
}