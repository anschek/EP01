using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ConferencesSystem.ViewModels;

namespace ConferencesSystem.Views;

public partial class AuthView : UserControl
{
    public AuthView(MainWindowViewModel mainVm)
    {
        InitializeComponent();
        DataContext = new AuthViewModel(mainVm);
    }
}