package com.example.professionaladvancementcoursesmobile.presentation.viewmodels

import android.util.Log
import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.professionaladvancementcoursesmobile.data.Group
import com.example.professionaladvancementcoursesmobile.data.LessonGroup
import com.example.professionaladvancementcoursesmobile.data.TeacherLesson
import com.example.professionaladvancementcoursesmobile.data.TeacherLoad
import com.example.professionaladvancementcoursesmobile.domain.Constants.supabase
import io.github.jan.supabase.postgrest.from
import kotlinx.coroutines.launch

class TeacherLoadViewModel : ViewModel() {
    var groups = mutableStateOf<List<Group>>(emptyList())
    val selectedGroup = mutableStateOf<Group?>(null)
    val teacherLoads = mutableStateOf<List<TeacherLoad>>(emptyList())

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
                                TeacherLesson::lessonGroupId isIn supabase.from("lessonsgroups").select {
                                    filter{
                                        LessonGroup::groupId eq selectedGroup.value?.id
                                    }
                                }.decodeList<LessonGroup>().map{it.id}
                            }
                        }.decodeList<TeacherLesson>().map{it.id}
                    }
                }.decodeList<TeacherLoad>().distinctBy { it.teacherLessonId }
            }catch(ex: Exception){
                Log.e("Loads", ex.message?: "Неизвестная ошибка")
            }
        }
    }
}