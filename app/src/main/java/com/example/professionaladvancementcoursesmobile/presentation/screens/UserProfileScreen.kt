package com.example.professionaladvancementcoursesmobile.presentation.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.navigation.NavController
import com.example.professionaladvancementcoursesmobile.data.User
import com.example.professionaladvancementcoursesmobile.presentation.components.HeaderText
import com.example.professionaladvancementcoursesmobile.presentation.navigation.NavigationRoutes

@Composable
fun UserProfileScreen(navController: NavController, user: User?) {
    Column(
        modifier = Modifier.fillMaxSize(),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ){
        HeaderText("Личный кабинет преподавателя")

        Text("Фамилия: ${user?.secondName}")
        Text("Имя: ${user?.firstName}")
        Text("Отчество: ${user?.middleName}")
        Text("Телефон: ${user?.phone}")

        Button({navController.navigate(NavigationRoutes.TEACHER_LOAD)}) { Text("Нагрузка") }
        Button({navController.navigate(NavigationRoutes.SCHEDULE)}) { Text("Расписание") }
    }
}