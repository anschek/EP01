using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia;
using SkiaSharp;
using System;
using System.Linq;

namespace ConferencesSystem.Services
{
    public class CaptchaService
    {
        private string _captchaText;
        public CaptchaService()
        {
            _captchaText = GenerateCaptchaText(4);
        }
        private string GenerateCaptchaText(int length)
        {
            const string chars = "йцукенгшщзхъфывапролджэячсмитьбю0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public bool CaptchaEnteredCorrectly(string userCaptchaText)
        => userCaptchaText == _captchaText;
        public Bitmap GetCaptcha()
        {
            var skBitmap = CreateCaptchaImage(_captchaText);

            // Создаём WriteableBitmap с теми же размерами, что и SKBitmap
            var avaloniaBitmap = new WriteableBitmap(
                new PixelSize(skBitmap.Width, skBitmap.Height),
                new Vector(96, 96), // DPI
                PixelFormat.Bgra8888, // Формат пикселей
                AlphaFormat.Premul); // Предварительно умноженный альфа-канал

            // Копируем данные из SKBitmap в WriteableBitmap
            using (var lockedBitmap = avaloniaBitmap.Lock())
            {
                unsafe
                {
                    // Получаем указатель на пиксели SKBitmap
                    var skiaPixels = (byte*)skBitmap.GetPixels();

                    // Рассчитываем размер буфера в байтах
                    long bufferSize = lockedBitmap.RowBytes * lockedBitmap.Size.Height;

                    // Копируем данные в память WriteableBitmap
                    Buffer.MemoryCopy(
                        skiaPixels,
                        (void*)lockedBitmap.Address,
                        bufferSize,
                        bufferSize);
                }
            }

            return avaloniaBitmap;
        }
        private static SKBitmap CreateCaptchaImage(string captchaText)
        {
            int width = 200;
            int height = 50;

            // Создаём новое изображение
            var bitmap = new SKBitmap(width, height);
            using (var canvas = new SKCanvas(bitmap))
            {
                // Заливаем фон белым цветом
                canvas.Clear(SKColors.White);

                // Рисуем текст
                DrawText(canvas, captchaText, width, height);

                // Добавляем шум
                AddNoise(canvas, width, height);
            }

            return bitmap;
        }
        private static void DrawText(SKCanvas canvas, string text, int width, int height)
        {
            var random = new Random();

            // Настройки шрифта
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true
            };

            float xPosition = 10;
            foreach (char character in text)
            {
                // Случайный угол наклона
                float angle = random.Next(-30, 30);

                // Случайный цвет
                paint.Color = new SKColor(
                    (byte)random.Next(0, 256),
                    (byte)random.Next(0, 256),
                    (byte)random.Next(0, 256));

                // Сохраняем текущее состояние холста
                canvas.Save();

                // Применяем поворот
                canvas.RotateDegrees(angle, xPosition + 15, height / 2);

                // Рисуем символ
                canvas.DrawText(character.ToString(), xPosition, height / 2, SKTextAlign.Left, new SKFont(SKTypeface.Default, 36), paint);

                // Восстанавливаем состояние холста
                canvas.Restore();

                // Сдвигаем позицию для следующего символа
                xPosition += 40;
            }
        }
        private static void AddNoise(SKCanvas canvas, int width, int height)
        {
            var random = new Random();

            // Добавляем случайные точки
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(width);
                int y = random.Next(height);
                canvas.DrawPoint(x, y, new SKPaint { Color = SKColors.Gray });
            }

            // Добавляем случайные линии
            for (int i = 0; i < 5; i++)
            {
                int x1 = random.Next(width);
                int y1 = random.Next(height);
                int x2 = random.Next(width);
                int y2 = random.Next(height);

                canvas.DrawLine(x1, y1, x2, y2, new SKPaint { Color = SKColors.Gray, StrokeWidth = 1 });
            }
        }
    }
}
