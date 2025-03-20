package com.example.professionaladvancementcoursesmobile.presentation.viewmodels

import android.util.Log
import androidx.compose.runtime.mutableStateListOf
import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.professionaladvancementcoursesmobile.data.Group
import com.example.professionaladvancementcoursesmobile.data.Lesson
import com.example.professionaladvancementcoursesmobile.data.LessonGroup
import com.example.professionaladvancementcoursesmobile.data.LessonType
import com.example.professionaladvancementcoursesmobile.data.Schedule
import com.example.professionaladvancementcoursesmobile.data.Subject
import com.example.professionaladvancementcoursesmobile.data.TeacherLesson
import com.example.professionaladvancementcoursesmobile.data.dto.ScheduleDto
import com.example.professionaladvancementcoursesmobile.domain.Constants.supabase
import io.github.jan.supabase.postgrest.from
import kotlinx.coroutines.launch
import java.time.LocalDate
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

class ScheduleViewModel(): ViewModel() {
    private val schedules = mutableStateOf<List<Schedule>>(emptyList())
    val schedulesDto = mutableStateListOf<ScheduleDto>()
    val selectedDate = mutableStateOf<LocalDate>(LocalDate.now())

    fun getSchedules(teacherId: String){
        viewModelScope.launch {
            try{
                schedules.value = supabase.from("schedules").select{
                    filter {
                        Schedule::teacherLessonId isIn supabase.from("teacherslessons").select{
                            filter{
                                TeacherLesson::teacherId eq teacherId
                            }
                        }.decodeList<TeacherLesson>().map { it.id }.toList<Int>()
                    }
                }.decodeList<Schedule>().filter { schedule ->
                    Log.d("Schedules", "${ LocalDateTime.parse(schedule.dateTime).toLocalDate().atStartOfDay()}  ${selectedDate.value.atStartOfDay()}")
                    LocalDateTime.parse(schedule.dateTime).toLocalDate().atStartOfDay() == selectedDate.value.atStartOfDay()
                }.toList()

                Log.d("Schedules", schedules.value.toString())

                getLessonsName()
            }catch(ex: Exception){
                Log.e("Schedules", ex.message ?: "Неизвестная ошибка")
            }
        }
    }

    fun getLessonsName(){
        val formatter = DateTimeFormatter.ofPattern("HH:mm")
        schedulesDto.clear()
        viewModelScope.launch {
            try{
                val lessonsGroups = supabase.from("lessonsgroups").select{
                    filter{
                        LessonGroup::id isIn supabase.from("teacherslessons").select{
                            filter{
                                TeacherLesson::id isIn schedules.value.map { it.teacherLessonId }
                            }
                        }.decodeList<TeacherLesson>().map { it.lessonGroupId }
                    }
                }.decodeList<LessonGroup>()

                val groups = supabase.from("groups").select{
                    filter{
                        Group::id isIn lessonsGroups.map { it.groupId }
                    }
                }.decodeList<Group>().map { it.name }

                val lessons = supabase.from("lessons").select{
                    filter{
                        Lesson::id isIn lessonsGroups.map { it.lessonId }
                    }
                }.decodeList<Lesson>()

                val lessonTypes = supabase.from("lessontypes").select{
                    filter {
                        LessonType::id isIn lessons.map { it.lessonTypeId }
                    }
                }.decodeList<LessonType>().map { it.name }

                val subjects = supabase.from("subjects").select{
                    filter {
                        Subject::id isIn lessons.map {it.subjectId}
                    }
                }.decodeList<Subject>().map { it.name }

                for (i in 0..<schedules.value.count()){
                    schedulesDto.add(
                        ScheduleDto(
                            time = LocalDateTime.parse(schedules.value[i].dateTime).format(formatter),
                            group = groups[i],
                            lessonType = lessonTypes[i],
                            subject = subjects[i]
                        )
                    )
                }
            }catch(ex: Exception){
                Log.e("Schedules", ex.message ?: "Неизвестная ошибка")
            }
        }
    }
}