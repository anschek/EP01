
namespace ProfessionalAdvancementCourses.Models;

public partial class Lesson
{
    public override string ToString() =>
        $"{Subject?.Name} - {LessonType?.Name}";

}