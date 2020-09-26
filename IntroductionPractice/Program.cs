using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroductionPractice
{
    // Display the file info (name and size) of the 10 largest files in the C:\Windows\System32 file directory
    // First, do it without using LINQ
    // Then, do it with LINQ
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows\System32";
            int numberOfFilesToShow = 10;
            ShowLargestFilesWithoutLINQ(path, numberOfFilesToShow);
            Console.WriteLine("****");
            ShowLargestFilesWithLINQ(path, numberOfFilesToShow);
        }

        private static void ShowLargestFilesWithLINQ(string path, int numberOfFilesToShow = 10)
        {
            FileInfo[] fileInfos = new DirectoryInfo(path).GetFiles();

            //IOrderedEnumerable<FileInfo> querySyntaxQuery = from fileInfo in fileInfos
            //    orderby fileInfo.Length descending
            //    select fileInfo;

            IEnumerable<FileInfo> methodSyntaxQuery = new DirectoryInfo(path).GetFiles()
                .OrderByDescending(f => f.Length)
                .Take(numberOfFilesToShow);

            //foreach (FileInfo fileInfo in querySyntaxQuery.Take(numberOfFilesToShow))
            //{
            //    Console.WriteLine($"{fileInfo.Name, -20} : {fileInfo.Length, 10:N0}");
            //}

            foreach (FileInfo fileInfo in methodSyntaxQuery)
            {
                Console.WriteLine($"{fileInfo.Name,-20} : {fileInfo.Length,10:N0}");
            }
        }

        private static void ShowLargestFilesWithoutLINQ(string path, int numberOfFilesToShow = 10)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            Array.Sort(fileInfos, new FileInfoComparer());
            for (int i = 0; i < numberOfFilesToShow; i++)
            {
                FileInfo fileInfo = fileInfos[i];
                Console.WriteLine($"{fileInfo.Name, -20} : {fileInfo.Length, 10:N0}");
            }
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
