using Avalonia.Controls;
using ConferencesSystem.ViewModels;

namespace ConferencesSystem.Views;

public partial class UserRegistrationView : UserControl
{
    public UserRegistrationView(MainWindowViewModel mainVM, int userId)
    {
        InitializeComponent();
        DataContext = new UserRegistrationViewModel(mainVM, userId);
    }
}