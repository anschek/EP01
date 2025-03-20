package com.example.professionaladvancementcoursesmobile.presentation.components

import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.sp

@Composable
fun HeaderText(text: String) {
    Text(
        text = text,
        style = TextStyle(
            fontSize = 20.sp,
            color = Color(0xFF324B9F),
            fontWeight = FontWeight.Medium
        )
    )
}