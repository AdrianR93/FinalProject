using SFML.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFinalProject
{
    class MusicManager
    {
        private readonly string defaultMusic = "Audio" + Path.DirectorySeparatorChar + "MainMusic.wav";

        private static MusicManager instance;

        public static MusicManager GetInstance()
        {
            if (instance == null)
            {
                instance = new MusicManager();
            }
            return instance;
        }
        private List<Music> music;
        private int currentSong;
        private MusicManager()
        {
            music = new List<Music>();
            currentSong = 0;
            Music m = new Music(defaultMusic);
            m.Loop = true;
            music.Add(m);
            SetVolume(10);
        }

        public void SetVolume(int newVolume)
        {
            for (int i = 0; i < music.Count; i++)
            {
                music[i].Volume = newVolume;
            }
        }

        public void Stop()
        {
            music[currentSong].Stop();
        }
        public void Pause()
        {
            music[currentSong].Pause();
        }
        public void Play()
        {
            music[currentSong].Play();
        }

    }



}


