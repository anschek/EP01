package com.example.professionaladvancementcoursesmobile.presentation.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.automirrored.filled.ArrowBack
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavController
import com.example.professionaladvancementcoursesmobile.data.User
import com.example.professionaladvancementcoursesmobile.presentation.components.DatePicker
import com.example.professionaladvancementcoursesmobile.presentation.navigation.NavigationRoutes
import com.example.professionaladvancementcoursesmobile.presentation.viewmodels.ScheduleViewModel

@Composable
fun ScheduleScreen(navController: NavController, user: User?) {
    val vm: ScheduleViewModel = viewModel()
    Column(
        modifier = Modifier.fillMaxSize(),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {

        Row(
            modifier = Modifier.fillMaxWidth()
        ) {
            IconButton({navController.navigate(NavigationRoutes.USER_PROFILE)}) {
                Icon(Icons.AutoMirrored.Filled.ArrowBack, contentDescription = "Назад")
            }
        }
        Text("Расписание")
        DatePicker(vm.selectedDate, {vm.getSchedules(user!!.id)})

        Text(if(vm.schedules.value.count() > 0)vm.schedules.value.toString() else "Занятий на этот день нет" )
    }
}