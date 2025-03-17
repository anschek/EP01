using System.Threading.Tasks;

namespace ProfessionalAdvancementCourses.Services
{
    public class SupabaseClientService
    {
        private Supabase.Client _supabase;

        // Приватный конструктор для предотвращения прямого создания экземпляра
        private SupabaseClientService(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        // Асинхронный фабричный метод
        public static async Task<SupabaseClientService> CreateAsync()
        {
            var url = "https://ublncqbjtsgafunzqmfw.supabase.co";
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVibG5jcWJqdHNnYWZ1bnpxbWZ3Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDE3MTY2NjYsImV4cCI6MjA1NzI5MjY2Nn0.yb1APxBNN-c5wqu5dB5-4C-zQL1Ug4GFtOfqciS3Evc";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            return new SupabaseClientService(supabase);
        }

        public async Task<string?> SignUp(string email, string password)
        {
             await _supabase.Auth.SignUp(email, password);
            var session = await _supabase.Auth.SignIn(email, password);
            var user = _supabase.Auth.CurrentUser;
            return user?.Id;
        }
    }
}
