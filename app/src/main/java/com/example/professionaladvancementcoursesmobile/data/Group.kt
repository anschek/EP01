package com.example.professionaladvancementcoursesmobile.data

import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Serializable
@SerialName("groups")
data class Group(
    val id:Int,
    val name: String
)
