package com.example.professionaladvancementcoursesmobile.presentation.navigation

import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.example.professionaladvancementcoursesmobile.data.User
import com.example.professionaladvancementcoursesmobile.presentation.screens.ScheduleScreen
import com.example.professionaladvancementcoursesmobile.presentation.screens.SignInScreen
import com.example.professionaladvancementcoursesmobile.presentation.screens.TeacherLoadScreen
import com.example.professionaladvancementcoursesmobile.presentation.screens.UserProfileScreen

@Composable
fun Navigation() {
    val navController = rememberNavController()
    val user = remember{ mutableStateOf<User?>(null) }
    NavHost(navController, NavigationRoutes.SIGN_IN){
        composable(NavigationRoutes.SIGN_IN){
            SignInScreen(navController, user)
        }
        composable(NavigationRoutes.USER_PROFILE){
            UserProfileScreen(navController, user.value)
        }
        composable(NavigationRoutes.TEACHER_LOAD){
            TeacherLoadScreen(navController,  user.value)
        }
        composable(NavigationRoutes.SCHEDULE){
            ScheduleScreen(navController, user.value)
        }
    }
}