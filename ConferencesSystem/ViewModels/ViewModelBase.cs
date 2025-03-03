using ConferencesSystem.Models;
using ReactiveUI;

namespace ConferencesSystem.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        protected static ConferencesContext _db = new();
        protected static string _baseImagePath = "../../../images/";
    }
}
