
using Avalonia.Media.Imaging;

namespace ConferencesSystem.Models.DTOs
{
    public class ActivityDto
    {
        public Bitmap Image { get; set; }
        public string Date {  get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
