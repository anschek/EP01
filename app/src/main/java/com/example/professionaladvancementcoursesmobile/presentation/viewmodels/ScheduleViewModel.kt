package com.example.professionaladvancementcoursesmobile.presentation.viewmodels

import android.util.Log
import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.professionaladvancementcoursesmobile.data.Schedule
import com.example.professionaladvancementcoursesmobile.data.TeacherLesson
import com.example.professionaladvancementcoursesmobile.domain.Constants.supabase
import io.github.jan.supabase.postgrest.from
import kotlinx.coroutines.launch
import java.time.LocalDate
import java.time.LocalDateTime

class ScheduleViewModel(): ViewModel() {
    val schedules = mutableStateOf<List<Schedule>>(emptyList())
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
            }catch(ex: Exception){
                Log.e("Schedules", ex.message ?: "Неизвестная ошибка")
            }
        }
    }
}