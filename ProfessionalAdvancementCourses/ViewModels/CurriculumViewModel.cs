
using Microsoft.EntityFrameworkCore;
using ProfessionalAdvancementCourses.Models;
using ProfessionalAdvancementCourses.Models.DTOs;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfessionalAdvancementCourses.ViewModels
{
	public class CurriculumViewModel : ViewModelBase
	{
		public CurriculumViewModel() : base()
		{
			_curriculum = new();
		}
        public List<Group> Groups => _db.Groups.ToList();
        Group? _selectedGroup;
        public Group? SelectedGroup { get => _selectedGroup; set => this.RaiseAndSetIfChanged(ref _selectedGroup, value); }
        List<CurriculumDto> _curriculum;        
		public List<CurriculumDto> Curriculum { get => _curriculum; set =>  this.RaiseAndSetIfChanged(ref _curriculum, value); }
		public void GetCurriculum()
		{
			Curriculum = _db.Lessonsgroups
				.Include(lg => lg.Teacherslessons)
				.ThenInclude(tl => tl.Schedules)
				.Include(lg => lg.Group)
				.Include(lg => lg.Lesson.Subject)
				.Include(lg => lg.Lesson.LessonType)
				.Where(lg => _selectedGroup!=null && lg.GroupId==_selectedGroup.Id)
				.Select(MapToCurriculDto).ToList();
		}
		private CurriculumDto MapToCurriculDto(Lessonsgroup lg)
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