using Avalonia.Controls;
using ConferencesSystem.ViewModels;

namespace ConferencesSystem.Views;

public partial class UserProfileView : UserControl
{
    public UserProfileView(MainWindowViewModel mainVm, int uerId)
    {
        InitializeComponent();
        DataContext = new UserProfileViewModel(mainVm, uerId);
    }
}