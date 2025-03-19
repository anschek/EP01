package com.example.professionaladvancementcoursesmobile.data

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
@SerialName("teacherslessons")
data class TeacherLesson (
    val id:Int,
    @SerialName("teacher_id")
    val teacherId: String,
    @SerialName("lesson_group_id")
    val lessonGroupId:Int
)