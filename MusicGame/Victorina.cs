using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace MusicGame
{
    static class Victorina
    {
        static public List<string> list = new List<string>(); // список песен
        static public int gameDuration = 60; // продолжительность игры
        static public int musicDuration = 10; // время до перехода к след. композ.
        static public bool randomStart = false; // рандомное начало проигрыша
        static public string lastFolder = ""; // папка
        static public bool allDirectories = false; // вложенные папки
        static public string answer = ""; 

        static public void ReadMusic()  // запуск последней открываемой папки
        {
            try
            {
                string[] music_files = Directory.GetFiles(lastFolder, "*.mp3", allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                list.Clear();
                list.AddRange(music_files);
            }
            catch
            {
            }
        }

        static string redKeyName = "Software\\MyCompanyName\\MusicVictorina"; //создание в реестре

        public static void WriteParam() // запись
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(redKeyName); // запись redKeyName
                if (rk == null) return;
                rk.SetValue("LastFolder", lastFolder);
                rk.SetValue("RandomStart", randomStart);
                rk.SetValue("GameDuration", gameDuration);
                rk.SetValue("MusicDuration", musicDuration);
                rk.SetValue("AllDirectories", allDirectories);
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }

        public static void ReadParam() // чтение
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(redKeyName);
                if (rk != null)
                {
                    lastFolder = (string)rk.GetValue("LastFolder");
                    gameDuration = (int)rk.GetValue("GameDuration");
                    randomStart = Convert.ToBoolean(rk.GetValue("RandomStart", false));
                    musicDuration = (int)rk.GetValue("MusicDuration");
                    allDirectories = Convert.ToBoolean(rk.GetValue("AllDirectories", false));
                }
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }
    }
}
