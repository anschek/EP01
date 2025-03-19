package com.example.professionaladvancementcoursesmobile.data

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
@SerialName("teachers")
data class Teacher (
    val id: String,
    val experience: Int
)