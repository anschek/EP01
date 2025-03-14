using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProfessionalAdvancementCourses.Models;
using ProfessionalAdvancementCourses.Models.DTOs;
using ReactiveUI;

namespace ProfessionalAdvancementCourses.ViewModels
{
	public class ScheduleViewModel : ReactiveObject
	{
		PostgresContext _db;
		public ScheduleViewModel()
		{
			_db = new PostgresContext();
			_schedules = new();
			_errorText = "";

        }
		public List<Group> Groups => _db.Groups.ToList();
        DateTime _selectedDate;
        public string SelectedDate { get => _selectedDate.ToString() ?? ""; set => this.RaiseAndSetIfChanged(ref _selectedDate, DateTime.Parse(value)); }
        Group? _selectedGroup;
        public Group? SelectedGroup { get => _selectedGroup; set => this.RaiseAndSetIfChanged(ref _selectedGroup, value); }

        public void GetSchedules()
		{
			Schedules = _db.Schedules.Include(s => s.TeacherLesson!.LessonGroup!.Lesson!.Subject)
				.Include(s => s.TeacherLesson!.LessonGroup!.Lesson!.LessonType)
				.Include(s => s.TeacherLesson!.Teacher!.IdNavigation)
				.Where(s => _selectedGroup != null && s.TeacherLesson!.LessonGroup!.GroupId == _selectedGroup.Id)
				.Where(s => s.DateTime.Date == _selectedDate.Date)
				.OrderBy(s => s.DateTime.TimeOfDay)
				.Select(MapToScheduleDto)
				.ToList();

			ErrorText = Schedules.Any() ? "" : "Занятий на этот день не найдено";
		}
		private ScheduleDto MapToScheduleDto(Schedule schedule) => new()
		{
			Time = schedule.DateTime.ToString("HH:mm"),
			Teacher = schedule?.TeacherLesson?.Teacher?.IdNavigation?.ToString() ?? "null",
			Lesson = schedule?.TeacherLesson?.LessonGroup?.Lesson?.ToString() ?? "null",
		};
		

		List<Schedule> _baseSchedules;
		List<ScheduleDto> _schedules;
        public List<ScheduleDto> Schedules { get => _schedules; private set => this.RaiseAndSetIfChanged(ref _schedules, value); }

        string _errorText;
        public string ErrorText { get => _errorText; set => this.RaiseAndSetIfChanged(ref _errorText, value); }

	}
}