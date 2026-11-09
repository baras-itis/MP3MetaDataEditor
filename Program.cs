using static System.Console;

Mp3Editor editor = new();

public class Mp3Editor
{
    public Mp3Editor()
    {
        var trackList = createTrackList();
        EditMetaData(trackList);
    }

    private TrackList createTrackList()
    {
        Clear();
        Write("Полный путь к треку: ");
        string path = ReadLine();
        Write("Название альбома: ");
        string album = ReadLine();
        Write("Имя автора/авторов: ");
        string authors = ReadLine();
        var trackList = new TrackList(path, album, authors);
        return trackList;
    }

    private void EditMetaData(TrackList tracklist)
    {
        foreach(string file in Directory.EnumerateFiles(tracklist.Path, "*.mp3"))
            {
                var tfile = TagLib.File.Create(file);
                tfile.Tag.Album = tracklist.Album;
                tfile.Tag.Performers =  tracklist.Authors;
                tfile.Save();
            }
    }
   
}

public class TrackList
{
    public string Path = string.Empty, Album = string.Empty;
    public string[] Authors;
    public TrackList(string path, string album, string author)
    {
        Path = path;
        Album = album;
        Authors = new string[] { author }; // преобразовываем строку в массив строк, тк Perfomers требует массив строк
    }
    public override string ToString() => $"author: {Authors} \n album: {Album} \n path: {Path}";
}



/*
    Сущность СпискаТреков:
    - авторы 
    - альбом
    - путь

    Сущность едитора:
    - Запрашивать данные треков
    - Редактировать треков
    - Вывод успеха и количества изменненых треков
*/