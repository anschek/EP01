package com.example.professionaladvancementcoursesmobile.data

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
@SerialName("teacherloads")
data class TeacherLoad (
    val id: Int,
    @SerialName("teacher_lesson_id")
    val teacherLessonId: Int,
    val hours: Int
)