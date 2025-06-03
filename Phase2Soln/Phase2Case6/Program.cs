using System.Collections.Generic;

namespace Phase2Case6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyPlayList myPlayList = new MyPlayList();
            Console.WriteLine("Hello, World!");
            while (true)
            {
                Console.WriteLine("Enter 1. To Add Song 2. To Remove Song by Id 3. Get Song By Id 4. Get Song by Name 5. Get All Songs from a playlist:");
                int c=Convert.ToInt32(Console.ReadLine());
                switch (c)
                {
                    case 1:
                        Console.WriteLine("Enter songid:");
                        int id=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter song name:");
                        string sname=Console.ReadLine();
                        Console.WriteLine("Enter song gener");
                        string sgenre=Console.ReadLine();
                        myPlayList.Add(new Song(id, sname, sgenre));
                        break;
                    case 2:
                        Console.WriteLine("Enter song id to remove:");
                        int i=Convert.ToInt32(Console.ReadLine());
                        myPlayList.Remove(i);
                        break ;
                    case 3:
                        Console.WriteLine("Enter song id to view details:");
                        id=Convert.ToInt32(Console.ReadLine());
                        Song song=myPlayList.GetSongById(id);
                        Console.WriteLine($"SongId : {song.SongId},SongName : {song.SongName},SongGenre : {song.SongGenre}");
                        break;
                    case 4:
                        Console.WriteLine("Enter song name to view details:");
                        string sn=Console.ReadLine();
                        Song son=myPlayList.GetSongByName(sn);
                        Console.WriteLine($"SongId : {son.SongId},SongName : {son.SongName},SongGenre : {son.SongGenre}");
                        break;
                    case 5:                        
                        foreach (Song so in myPlayList.GetAllSongs())
                        {
                            Console.WriteLine($"SongId : {so.SongId},SongName : {so.SongName},SongGenre : {so.SongGenre}");
                        }
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
                
            }
        }
    }
}
