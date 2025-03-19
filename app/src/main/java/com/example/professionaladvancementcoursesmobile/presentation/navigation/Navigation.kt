package com.example.professionaladvancementcoursesmobile.presentation.navigation

import androidx.compose.runtime.Composable
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.example.professionaladvancementcoursesmobile.presentation.screens.SignInScreen
import com.example.professionaladvancementcoursesmobile.presentation.screens.UserProfileScreen

@Composable
fun Navigation() {
    val navController = rememberNavController()
    NavHost(navController, NavigationRoutes.SIGN_IN){
        composable(NavigationRoutes.SIGN_IN){
            SignInScreen(navController)
        }
        composable(NavigationRoutes.USER_PROFILE){
            UserProfileScreen(navController)
        }
    }
}