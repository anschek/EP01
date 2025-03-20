package com.example.professionaladvancementcoursesmobile.presentation.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.itemsIndexed
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
import com.example.professionaladvancementcoursesmobile.data.dto.ScheduleDto
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

        Text(if(vm.schedulesDto.count() == 0) "Занятий на этот день нет" else "")

        LazyColumn {
            itemsIndexed(vm.schedulesDto){_, sch ->
                ScheduleItem(sch)
            }
        }
    }
}

@Composable
fun  ScheduleItem(schedule: ScheduleDto){
    Row {
        Text("Начало занятия: ${schedule.time}")
        Text(" Группа: ${schedule.group}")
    }
    Column {
        Text("Предмет: ${schedule.subject}")
        Text("Тип: ${schedule.lessonType}")
    }
}