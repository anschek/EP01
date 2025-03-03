using Avalonia.Controls;
using ConferencesSystem.ViewModels;

namespace ConferencesSystem.Views;

public partial class ActivitiesListView : UserControl
{
    public ActivitiesListView(MainWindowViewModel mainVm)
    {
        InitializeComponent();
        DataContext = new ActivitiesListViewModel(mainVm);
    }
}