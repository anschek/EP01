package com.example.professionaladvancementcoursesmobile.presentation.screens

import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
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
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavController
import com.example.professionaladvancementcoursesmobile.data.User
import com.example.professionaladvancementcoursesmobile.data.dto.ScheduleDto
import com.example.professionaladvancementcoursesmobile.presentation.components.DatePicker
import com.example.professionaladvancementcoursesmobile.presentation.components.HeaderText
import com.example.professionaladvancementcoursesmobile.presentation.navigation.NavigationRoutes
import com.example.professionaladvancementcoursesmobile.presentation.viewmodels.ScheduleViewModel

@Composable
fun ScheduleScreen(navController: NavController, user: User?) {
    val vm: ScheduleViewModel = viewModel()
    Column(
        modifier = Modifier.fillMaxSize()
            .padding(15.dp),
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
        HeaderText("Расписание")
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
    Column(
        modifier = Modifier
            .fillMaxWidth()
            .padding(10.dp)
            .border(1.dp, Color(0xFF324B9F))
    ) {
        Text("Начало занятия: ${schedule.time}")
        Text("Группа: ${schedule.group}")
        Text("Предмет: ${schedule.subject}")
        Text("Тип: ${schedule.lessonType}")
    }
}