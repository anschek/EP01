package com.example.professionaladvancementcoursesmobile.domain

import io.github.jan.supabase.createSupabaseClient
import io.github.jan.supabase.postgrest.Postgrest
import io.github.jan.supabase.auth.Auth
object Constants {
    val supabase = createSupabaseClient(
        supabaseUrl = "https://ublncqbjtsgafunzqmfw.supabase.co",
        supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVibG5jcWJqdHNnYWZ1bnpxbWZ3Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDE3MTY2NjYsImV4cCI6MjA1NzI5MjY2Nn0.yb1APxBNN-c5wqu5dB5-4C-zQL1Ug4GFtOfqciS3Evc"
    ) {
        install(Auth)
        install(Postgrest)
    }
}