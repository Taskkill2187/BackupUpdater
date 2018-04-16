using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupUpdater
{
    class Program
    {
        static DateTime lastSaveTime;
        static string InputPath;
        static string OutputPath;
        static int Errors = 0;
        static int Overrides = 0;

        [STAThread]
        static void Main(string[] args)
        {
            Console.BufferHeight = 3000;

            FolderBrowserDialog InFolderD = new FolderBrowserDialog();
            InFolderD.Description = "Select the input Folder.";
            DialogResult result = InFolderD.ShowDialog();
            if (result == DialogResult.OK)
            {
                FolderBrowserDialog OutFolderD = new FolderBrowserDialog();
                OutFolderD.Description = "Select the output/backup Folder.";
                result = OutFolderD.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DateChooser GetDate = new DateChooser();
                    result = GetDate.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        lastSaveTime = GetDate.ChoosenDate;
                        InputPath = InFolderD.SelectedPath;
                        OutputPath = OutFolderD.SelectedPath;

                        RecursiveFolderSearch(InFolderD.SelectedPath);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Finished with " + Errors + " Errors and " + Overrides + " Overrides!");
                        Console.ReadLine();
                    }
                }
            }
        }

        public static void RecursiveFolderSearch(string StartDir)
        {
            try
            {
                Console.Title = StartDir;

                foreach (string s in Directory.GetFiles(StartDir))
                    if (File.GetLastAccessTime(s).CompareTo(lastSaveTime) > 0 ||
                        File.GetCreationTime(s).CompareTo(lastSaveTime) > 0)
                    {
                        string output = OutputPath + s.Remove(0, InputPath.Length);
                        Console.WriteLine("From: " + s + "\nTo: " + output + "\n");

                        if (File.Exists(output))
                            Overrides++;

                        Directory.CreateDirectory(Path.GetDirectoryName(output));
                        File.Copy(s, output, true);
                    }

                foreach (string D in Directory.GetDirectories(StartDir))
                    RecursiveFolderSearch(D);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + e.ToString() + " ERROR-PATH: " + StartDir);
                Console.ForegroundColor = ConsoleColor.White;
                Errors++;
            }
        }
    }
}
