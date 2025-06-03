using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase2Case6
{
    public interface IPlaylist
    {
        public void Add(Song song);
        public void Remove(int SongId);
        public Song GetSongById(int SongId);
        public Song GetSongByName(string SongName);
        public List<Song> GetAllSongs();
    }
    public class MyPlayList : IPlaylist
    {
        public static List<Song> myPlayList = new List<Song>();
        public MyPlayList() { }

        public List<Song> GetAllSongs()
        {
            List<Song> list = new List<Song>(); 
            foreach (Song song in myPlayList) { 
                list.Add(song);
                //Console.WriteLine($"SongId : {song.SongId},SongName : {song.SongName},SongGenre : {song.SongGenre}");
            }
            return list;
        }

        public Song GetSongById(int SongId)
        {
            foreach (Song song in myPlayList) { 
                if (song.SongId == SongId) return song;
            }
            return null;
        }

        public Song GetSongByName(string SongName)
        {
            foreach (Song song in myPlayList)
            {
                if(song.SongName == SongName) return song;
            }
            return null;
        }

        public void Remove(int SongId)
        {
            for(int i=0;i<myPlayList.Count();i++)
            {
                if (myPlayList[i].SongId.Equals(SongId))
                {
                    myPlayList.RemoveAt(i);
                }
            }
        }

        public void Add(Song song)
        {
            myPlayList.Add(song);

        }
    }
}
