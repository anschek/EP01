using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProfessionalAdvancementCourses.Models;
using ProfessionalAdvancementCourses.Models.DTOs;
using ReactiveUI;

namespace ProfessionalAdvancementCourses.ViewModels
{
	public class TeachersLoadsViewModel : ViewModelBase
	{
		public TeachersLoadsViewModel()
		{
			_selectedTeacher = new();
			Loads = new();
		}
		public List<Teacher> Teachers => _db.Teachers.Include(t => t.IdNavigation).ToList();
        private Teacher _selectedTeacher;
        private List<TeacherLoadDto> _loads;
        public Teacher SelectedTeacher { get => _selectedTeacher; set => this.RaiseAndSetIfChanged(ref _selectedTeacher, value); }
		public List<TeacherLoadDto> Loads { get => _loads; set => this.RaiseAndSetIfChanged(ref _loads, value); }
		public void GetTeacherLoad()
		{
            Loads = _db.Teacherloads.Include(tl => tl.TeacherLesson.LessonGroup.Lesson.Subject)
				.Include(tl => tl.TeacherLesson.LessonGroup.Lesson.LessonType)
				.Include(tl => tl.TeacherLesson.LessonGroup.Group)
				.Where(tl => tl.TeacherLesson.TeacherId == _selectedTeacher.Id)
				.Select(MapToTeacherLoadDto).ToList();
		}
		private TeacherLoadDto MapToTeacherLoadDto(Teacherload tl) =>
			new()
			{
				Group = tl.TeacherLesson.LessonGroup.Group.Name,
				Lesson = tl.TeacherLesson.LessonGroup.Lesson.ToString(),
				Hours = tl.Hours,
				HourlyRate = tl.TeacherLesson.LessonGroup.Lesson.HourlyRate
			};
    }
}