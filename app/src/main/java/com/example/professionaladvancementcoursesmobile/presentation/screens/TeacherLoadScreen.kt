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
import com.example.professionaladvancementcoursesmobile.data.Group
import com.example.professionaladvancementcoursesmobile.data.User
import com.example.professionaladvancementcoursesmobile.presentation.components.ComboBox
import com.example.professionaladvancementcoursesmobile.presentation.navigation.NavigationRoutes
import com.example.professionaladvancementcoursesmobile.presentation.viewmodels.TeacherLoadViewModel

@Composable
fun TeacherLoadScreen(navController: NavController, user: User?) {

    val vm: TeacherLoadViewModel = viewModel()
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
        Text("Нагрузка")
        Text("Выберите группу")
        ComboBox<Group>(
            vm.groups.value,
            vm.selectedGroup.value,
            {
                vm.selectedGroup.value = it
                vm.getTeacherLoads(user!!.id)
            },
            {it.name}
        )
        Text(if(vm.teacherLoads.value.count() > 0) vm.teacherLoads.value.toString() else "Нагрузка не определена")
    }
}