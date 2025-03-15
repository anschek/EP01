using ProfessionalAdvancementCourses.Models;
using ReactiveUI;

namespace ProfessionalAdvancementCourses.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        protected PostgresContext _db;
        protected ViewModelBase()
        {
            _db = new();
        }
    }
}
