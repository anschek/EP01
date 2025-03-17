
using Avalonia.Media;
using ProfessionalAdvancementCourses.Models;
using ProfessionalAdvancementCourses.Models.DTOs;
using ProfessionalAdvancementCourses.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfessionalAdvancementCourses.ViewModels
{
    public class StudentRegistrationViewModel : ViewModelBase
    {
        public StudentRegistrationViewModel()
        {
            _db = new();
            _courseIsSet = false;
            _curriculum = new();
            _userMail = "";
            _user = new() { FirstName = "", SecondName = "", MiddleName = "", Phone = "" };
            _messageText = "";
            _messageColor = new(Colors.Black);
        }
        public List<Specialty> Specialties => _db.Specialties.ToList();
        private Specialty? _selectedSpeciality;
        public Specialty? SelectedSpeciality
        {
            get => _selectedSpeciality;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedSpeciality, value);
                CourseIsSet = SetCourse();
            }
        }
        public List<Department> Departments => _db.Departments.ToList();
        private Department? _selectedDepartment;
        public Department? SelectedDepartment
        {
            get => _selectedDepartment; set
            {
                this.RaiseAndSetIfChanged(ref _selectedDepartment, value);
                CourseIsSet = SetCourse();
            }
        }
        private bool SetCourse()
        {
            if (_selectedSpeciality == null || _selectedDepartment == null) return false;

            SelectedGroup = _db.Groups.Where(g => _selectedDepartment.Id == g.DepartmentId && _selectedSpeciality.Id == g.SpecialityId).FirstOrDefault();
            if (SelectedGroup == null) return false;

            Curriculum = CurriculumService.GetCurriculum(_db)
                .Where(lg => _selectedGroup != null && lg.GroupId == _selectedGroup.Id)
                .Select(CurriculumService.MapToCurriculDto).ToList();
            if (Curriculum == null || Curriculum.Count() == 0) return false;

            TuitionFee = CurriculumService.GetCurriculum(_db)
                .Where(lg => _selectedGroup != null && lg.GroupId == _selectedGroup.Id)
                .Select(lg => lg.TotalPlannedHours * lg.Lesson.HourlyRate).Sum();
            if (TuitionFee == 0) return false;

            return true;
        }
        private Group? _selectedGroup;
        public Group? SelectedGroup { get => _selectedGroup; private set => this.RaiseAndSetIfChanged(ref _selectedGroup, value); }
        private decimal _tuitionFee;
        public decimal TuitionFee { get => _tuitionFee; set => this.RaiseAndSetIfChanged(ref _tuitionFee, value); }
        List<CurriculumDto> _curriculum;
        public List<CurriculumDto> Curriculum { get => _curriculum; set => this.RaiseAndSetIfChanged(ref _curriculum, value); }
        private bool _courseIsSet;
        public bool CourseIsSet { get => _courseIsSet; private set => this.RaiseAndSetIfChanged(ref _courseIsSet, value); }


        public async void CreateNewUser()
        {
            try
            {
                var supabaseClient = await SupabaseClientService.CreateAsync();
                string userId = (await supabaseClient.SignUp(UserMail, "123456"))!;

                User.Id = Guid.Parse(userId);
                User.Student = new() { GroupId = SelectedGroup!.Id };

                _db.Add(User);
                _db.SaveChanges();

                MessageColor = new(Colors.Green);
                MessageText = $"Успешная регистрация в группе {SelectedGroup!.Name}";
            }
            catch(Exception ex) 
            {
                MessageColor = new(Colors.Red);
                MessageText = ex.Message;
            }
        }
        private string _userMail;
        public string UserMail { get => _userMail; set => this.RaiseAndSetIfChanged(ref _userMail, value); }
        private User _user;
        public User User { get => _user; set => this.RaiseAndSetIfChanged(ref _user, value); }
        private string _messageText;
        public string MessageText { get => _messageText; set => this.RaiseAndSetIfChanged(ref _messageText, value); }
        private SolidColorBrush _messageColor;
        public SolidColorBrush MessageColor { get => _messageColor; set => this.RaiseAndSetIfChanged(ref _messageColor, value); }
    }
}