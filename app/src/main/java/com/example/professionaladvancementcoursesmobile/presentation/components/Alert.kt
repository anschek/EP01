package com.example.professionaladvancementcoursesmobile.presentation.components

import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState

@Composable
fun Alert(openDialog: MutableState<Boolean>, message: String) {
    AlertDialog(
        onDismissRequest = {openDialog.value = false},
        confirmButton = {Text("Ок")},
        text = {Text(message)}
    )
}