using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace CBZReader
{
    internal class FileHandler
    {
        public string path { get; set; }
        public int pagesNumber { get; set; }

        /// <summary>
        /// Constructor of the FileHandler class
        /// </summary>
        /// <param name="path">Path of the file</param>
        public FileHandler(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// List all files of the temp directory
        /// </summary>
        /// <returns>String array of files path</returns>
        private string[] LsFiles()
        {
            string tempPath = Path.GetTempPath() + "cbzreader";

            string[] allfiles = Directory.GetFiles(tempPath, "*.*", SearchOption.AllDirectories); // List every files in an array

            return allfiles;
        }

        /// <summary>
        /// Extracts the cbz file as zip to a temp folder and creates an array with every file path
        /// </summary>
        /// <returns>Files path array</returns>
        private string[] ExtractZip()
        {
            string tempPath = Path.GetTempPath() + "cbzreader";

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            ZipFile.ExtractToDirectory(this.path, tempPath);

            string[] allfiles = LsFiles();

            foreach (string file in allfiles)
                Console.WriteLine(file);

            return allfiles;
        }

        /// <summary>
        /// Deletes every files in the temp folder
        /// </summary>
        static public void NukeFiles()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Path.GetTempPath() + "cbzreader");
            try
            {

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Restart the program...");
                Application.Exit();
            }
        }

        /// <summary>
        /// Creates a List with every images
        /// </summary>
        /// <returns>List of images</returns>
        public List<Image> GetImages()
        {
            NukeFiles();

            ExtractZip();
            List<Image> images = new List<Image>();

            foreach (string file in LsFiles())
            {
                images.Add(Image.FromFile(file));
            }

            return images;
        }
    }
}
