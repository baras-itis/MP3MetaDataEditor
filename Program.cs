using static System.Console;

var trackList = ConsoleUI.PromtTrackList();
var mp3Editor = new Mp3Editor();
int changed = mp3Editor.EditMetaData(trackList);

/*
At first data that we mutate
Then service
*/

// its <<Data>>.
// we use sealed to avoid children that can destroy our class. Design-solution 
public sealed class TrackList
{
    // инкапсуляция - we prevent changing data in proccess. задаётся один раз в конструкторе, и после этого объект неизменяем. It changed поле to свойство. Avoid null
    public string Path {get;}
    public string Album {get;}
    // Abstraction. We suggest only reading, can't be changed after init
    public IReadOnlyList<string> Authors {get;}

    public TrackList(string path, string album, IEnumerable<string> author)
    {
        if(string.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));
        if(string.IsNullOrWhiteSpace(album)) throw new ArgumentException(nameof(album));
        if(!Directory.Exists(path)) throw new DirectoryNotFoundException(path);

        Path = path;
        Album = album;
        //  from strign create array
        Authors = author.Where(item => !string.IsNullOrWhiteSpace(item)).ToArray();
    }
    
    public override string ToString() => $"author: {string.Join(' ',Authors)} \n album: {Album} \n path: {Path}";
}

// Mp3Editor - service. We need to put it in other directory 
public sealed class Mp3Editor
{
    public int EditMetaData(TrackList tracklist)
    {
        // show how many tracks we changed
        int count = 0;
        foreach(string file in Directory.EnumerateFiles(tracklist.Path, "*.mp3"))
            {
                var tfile = TagLib.File.Create(file);
                tfile.Tag.Album = tracklist.Album;
                tfile.Tag.Performers =  tracklist.Authors.ToArray();
                tfile.Save();
                count++;
            }
            return count;
    }
}


public static class ConsoleUI
{
    public static TrackList PromtTrackList()
    {   
        Write("Write absolute path to album: ");
        string path = ReadLine() ?? throw new InvalidOperationException("No path");

        Write("Write authors/author divide with ','");

        var authors = (ReadLine() ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        Write("Write name of album: ");
        string album = (ReadLine() ?? "");

        TrackList trackList = new(path, album, authors);
        return trackList;
    }
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

    Mp3Editor at the moment is doing:
    - create TrackList
    - Editin metaData
*/