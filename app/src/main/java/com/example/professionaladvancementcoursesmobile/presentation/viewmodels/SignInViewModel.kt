package com.example.professionaladvancementcoursesmobile.presentation.viewmodels

import android.util.Log
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.professionaladvancementcoursesmobile.data.User
import com.example.professionaladvancementcoursesmobile.domain.Constants
import io.github.jan.supabase.auth.auth
import io.github.jan.supabase.auth.providers.builtin.Email
import io.github.jan.supabase.postgrest.from
import kotlinx.coroutines.launch

class SignInViewModel() : ViewModel() {
    var userMail by mutableStateOf("")
    var userPassword by mutableStateOf("")
    val openDialog = mutableStateOf(false)
    var message by mutableStateOf("")
    private var user by mutableStateOf<User?>(null)

    fun signIn(successAction: () -> Unit) {
        viewModelScope.launch {
            try {
                Constants.supabase.auth.signInWith(Email) {
                    email = userMail
                    password = userPassword
                }
                val userInfo = Constants.supabase.auth.currentUserOrNull()
                if (userInfo == null) {
                    message = "Пользователь не найден"
                    openDialog.value = true
                }
                else {
                    user = Constants.supabase.from("users").select {
                        filter {
                            eq("id", userInfo.id)
                        }
                    }.decodeSingle<User>()
                    user.toString()
                    Log.d("Auth", "Success")
                    successAction()
                }
            } catch (e: Exception) {
                Log.e("Auth", e.message ?: "")
                message = e.message ?: "Неизвестная ошибка"
                openDialog.value = true
            }
        }
    }
}