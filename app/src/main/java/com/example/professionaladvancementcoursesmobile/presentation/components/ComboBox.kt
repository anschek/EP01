package com.example.professionaladvancementcoursesmobile.presentation.components

import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.ExposedDropdownMenuBox
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun <T> ComboBox(
    items: List<T>, // menu's elements
    selectedItem: T?, // current element in text field
    onItemSelected: (T) -> Unit, // action performed when new element selected
    itemText: (T) -> String,  // text describing element of type T
) {
    var generalExpanded by remember { mutableStateOf(false) } // controls state of dropdown
    ExposedDropdownMenuBox(// container for text field and drop down menu
        expanded = generalExpanded,
        onExpandedChange = { generalExpanded = !generalExpanded } // change state on tap
    ) {
        TextField(
            value = selectedItem?.let { itemText(it) } ?: "  ---", // text for null object
            onValueChange = {},
            modifier = Modifier
                .fillMaxWidth()
                .menuAnchor(), // anchor for dropdown menu
            readOnly = true
        )
        // draws menu with elements
        DropdownMenu(
            expanded = generalExpanded,
            onDismissRequest = { generalExpanded = false }
        ) {
            items.forEach { item ->
                DropdownMenuItem(
                    text = { Text(itemText(item)) },
                    onClick = {
                        onItemSelected(item)
                        generalExpanded = false
                    }
                )
            }
        }
    }
}