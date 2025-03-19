package com.example.professionaladvancementcoursesmobile.data

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
@SerialName("lessonsgroups")
data class LessonGroup (
    val id: Int,
    @SerialName("lesson_id")
    val lessonId: Int,
    @SerialName("group_id")
    val groupId: Int,
    @SerialName("total_planned_hours")
    val totalPlannedHours: Int
)