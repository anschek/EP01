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
import com.example.professionaladvancementcoursesmobile.data.Subject
import com.example.professionaladvancementcoursesmobile.data.TeacherLesson
import com.example.professionaladvancementcoursesmobile.data.TeacherLoad
import com.example.professionaladvancementcoursesmobile.data.dto.TeacherLoadDto
import com.example.professionaladvancementcoursesmobile.domain.Constants.supabase
import io.github.jan.supabase.postgrest.from
import kotlinx.coroutines.launch

class TeacherLoadViewModel : ViewModel() {
    var groups = mutableStateOf<List<Group>>(emptyList())
    val selectedGroup = mutableStateOf<Group?>(null)
    private val teacherLoads = mutableStateOf<List<TeacherLoad>>(emptyList())
    val teacherLoadsDto = mutableStateListOf<TeacherLoadDto>()

    init {
        viewModelScope.launch {
            groups.value = supabase.from("groups").select().decodeList<Group>()
        }
    }

    fun getTeacherLoads(teacherId: String) {
        viewModelScope.launch {
            try{
                teacherLoads.value = supabase.from("teacherloads").select {
                    filter {
                        TeacherLoad::teacherLessonId isIn supabase.from("teacherslessons").select {
                            filter{
                                TeacherLesson::teacherId eq teacherId
                                TeacherLesson::lessonGroupId isIn supabase.from("lessonsgroups").select {
                                    filter{
                                        LessonGroup::groupId eq selectedGroup.value?.id
                                    }
                                }.decodeList<LessonGroup>().map{it.id}
                            }
                        }.decodeList<TeacherLesson>().map{it.id}
                    }
                }.decodeList<TeacherLoad>().distinctBy { it.teacherLessonId }

                Log.d("Loads", teacherLoads.value.toString())

                getLoadsName()
            }catch(ex: Exception){
                Log.e("Loads", ex.message?: "Неизвестная ошибка")
            }
        }
    }

    fun getLoadsName(){
        teacherLoadsDto.clear()
        viewModelScope.launch {
            val lessons = supabase.from("lessons").select{
                filter{
                    Lesson::id isIn supabase.from("lessonsgroups").select{
                        filter{
                            LessonGroup::id isIn supabase.from("teacherslessons").select{
                                filter{
                                    TeacherLesson::id isIn teacherLoads.value.map { it.teacherLessonId }
                                }
                            }.decodeList<TeacherLesson>().map { it.lessonGroupId }
                        }
                    }.decodeList<LessonGroup>().map { it.lessonId }
                }
            }.decodeList<Lesson>()

            var rates = lessons.map { it.hourlyRate }

            try{
                for(i in 0 ..<teacherLoads.value.count()){
                    teacherLoadsDto.add(
                        TeacherLoadDto(
                            hours = teacherLoads.value[i].hours,
                            hourlyRate = rates[i],
                            subject = supabase.from("subjects").select{
                                filter {
                                    Subject::id eq lessons[i].subjectId
                                }
                            }.decodeSingle<Subject>().name,
                            lessonType = supabase.from("lessontypes").select{
                                filter {
                                    LessonType::id eq lessons[i].lessonTypeId
                                }
                            }.decodeSingle<LessonType>().name
                        )
                    )
                    Log.d("Loads", teacherLoadsDto.last().toString())
                }

            }catch(ex: Exception){
                Log.e("Loads", ex.message?: "Неизвестная ошибка")
            }
        }
    }
}