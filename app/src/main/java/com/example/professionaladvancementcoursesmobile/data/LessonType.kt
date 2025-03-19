package com.example.professionaladvancementcoursesmobile.data

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
@SerialName("lessontypes")
data class LessonType(
    val id:Int,
    val name: String
)
