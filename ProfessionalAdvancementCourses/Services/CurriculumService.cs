using ProfessionalAdvancementCourses.Models.DTOs;
using ProfessionalAdvancementCourses.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ProfessionalAdvancementCourses.Services
{
    public static class CurriculumService
    {
        public static IIncludableQueryable<Lessonsgroup, Lessontype> GetCurriculum(PostgresContext db)
        {
            return db.Lessonsgroups
                .Include(lg => lg.Teacherslessons)
                .ThenInclude(tl => tl.Schedules)
                .Include(lg => lg.Group)
                .Include(lg => lg.Lesson.Subject)
                .Include(lg => lg.Lesson.LessonType);
        }
        public static CurriculumDto MapToCurriculDto(Lessonsgroup lg)
        {
            int usedHours = lg.Lesson.Lessonsgroups
                .SelectMany(lg => lg.Teacherslessons)
                .SelectMany(tl => tl.Schedules)
                .Where(static sch => sch.DateTime < DateTime.Now).Count() * lg.Lesson.Hours;

            int remainingHours = lg.TotalPlannedHours > usedHours ? lg.TotalPlannedHours - usedHours : 0;

            return new()
            {
                Lesson = lg.Lesson.ToString(),
                TotalPlannedHours = lg.TotalPlannedHours,
                RemainingHours = remainingHours
            };
        }
    }
}
