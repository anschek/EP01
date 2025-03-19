package com.example.professionaladvancementcoursesmobile.presentation.components

import android.app.DatePickerDialog
import android.widget.DatePicker
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.ui.Alignment
import androidx.compose.ui.platform.LocalContext
import java.time.LocalDate
import java.util.Calendar
import java.util.Date

@Composable
fun DatePicker(selectedDate: MutableState<LocalDate>, action: ()-> Unit) {
    val context = LocalContext.current
    val calendar = Calendar.getInstance()

    val year = calendar.get(Calendar.YEAR)
    val month = calendar.get(Calendar.MONTH)
    val day = calendar.get(Calendar.DAY_OF_MONTH)

    calendar.time = Date()

    val datePickerDialog = DatePickerDialog(
        context,
        { _: DatePicker, mYear: Int, mMonth: Int, mDayOfMonth: Int ->
            selectedDate.value = LocalDate.of(year, month+1, day)
            action()
        }, year, month, day
    )
    Column(
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Button(onClick = { datePickerDialog.show()}
        ) {
            Text(text = "Выбрать дату")
        }
        Text(text = "Выбрано: ${day}.${month+1}.${year}")
    }
}