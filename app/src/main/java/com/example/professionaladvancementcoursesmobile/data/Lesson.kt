package com.example.professionaladvancementcoursesmobile.data

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
@SerialName("lessons")
data class Lesson(
    val id:Int,
    @SerialName("subject_id")
    val subjectId:Int,
    @SerialName("lesson_type_id")
    val lessonTypeId: Int,
    @SerialName("hourly_rate")
    val hourlyRate: Double,
    val hours: Int
)
