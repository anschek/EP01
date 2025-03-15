
namespace ProfessionalAdvancementCourses.Models;

public partial class User
{
    public override string ToString() =>
        $"{SecondName} {FirstName} {MiddleName}";
}
