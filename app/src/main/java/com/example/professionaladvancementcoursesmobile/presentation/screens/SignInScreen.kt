package com.example.professionaladvancementcoursesmobile.presentation.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.Button
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavController
import com.example.professionaladvancementcoursesmobile.presentation.components.Alert
import com.example.professionaladvancementcoursesmobile.presentation.viewmodels.SignInViewModel
import com.example.professionaladvancementcoursesmobile.presentation.navigation.NavigationRoutes

@Composable
fun SignInScreen(navController: NavController) {
    val vm: SignInViewModel = viewModel()
    Column(
        modifier = Modifier.fillMaxSize(),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Text("Авторизация")
        TextField(vm.userMail, { vm.userMail = it }, label = {Text("почта")})
        TextField(vm.userPassword, { vm.userPassword = it }, label = {Text("пароль")},
            visualTransformation = PasswordVisualTransformation())
        Button(onClick = {
            vm.signIn { navController.navigate(NavigationRoutes.USER_PROFILE) }
        }) {
            Text("Войти")
        }
        if(vm.openDialog.value){
            Alert(vm.openDialog, vm.message )
        }
    }
}