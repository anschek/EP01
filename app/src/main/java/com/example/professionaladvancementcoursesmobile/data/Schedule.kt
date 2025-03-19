package com.example.professionaladvancementcoursesmobile.data

import kotlinx.serialization.Contextual
import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable
import java.time.LocalDateTime

@Serializable
@SerialName("schedules")
data class Schedule(
    val id: Int,
    @SerialName("teacher_lesson_id")
    val teacherLessonId: Int,
    @SerialName("date_time")
    val dateTime:  String
)
