using TagLib;
using static System.Console;
using System.IO;
using System.Collections;
using System.Drawing;

// Очистка консоли
Clear();
ForegroundColor = ConsoleColor.Green;
Write("Введите абсолютный путь к директории с файлами: ");
string? path = ReadLine();
Write("Введите общий альбом: ");
string album = ReadLine();
Write("Введите автора: ");

string author = ReadLine();
string[] authors = new string[] {author}; // преобразовываем строку в массив строк, тк Perfomers требует массив строк

Write("Введите путь к изображению (опционально): ");
string imagePath = ReadLine();



foreach(string file in Directory.EnumerateFiles(path, "*.mp3"))
{
    
    var tfile = TagLib.File.Create(file);
    tfile.Tag.Album = album;
    tfile.Tag.Performers =  authors;
    if(imagePath != "")
    {
        var newPic = new Picture(imagePath)
        {
            Type = PictureType.FrontCover,
            Description = "Cover"
        };
    }
    tfile.Save();
}
Write("Все прошло успешно. Приятного прослушивания :)");

/*
    Запрпашивать у пользователя путь к файлу
    перебрать все файлы
    применить к ним изменения
*/

