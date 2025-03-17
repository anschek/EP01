
using Microsoft.EntityFrameworkCore;
using ProfessionalAdvancementCourses.Models;
using ProfessionalAdvancementCourses.Models.DTOs;
using ProfessionalAdvancementCourses.Services;
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
			Curriculum = CurriculumService.GetCurriculum(_db)
				.Where(lg => _selectedGroup!=null && lg.GroupId==_selectedGroup.Id)
				.Select( CurriculumService.MapToCurriculDto).ToList();
		}
    }
}