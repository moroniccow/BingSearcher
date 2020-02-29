using System;
using System.Diagnostics;
using System.Threading;

namespace BingSearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting....");
            string baseUrl = "https://www.bing.com/search?q={0}";
            string[] lines = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "words.txt");
            Random r = new Random();
            
            int i = 0;
            while(i < 30)
            {
                int index = r.Next(0, lines.Length - 1);
                
                try
                {
                    string searchText = lines[index];
                    Console.WriteLine("Searching: " + searchText);
                    using (Process edge = new Process())
                    {
                        edge.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                        edge.StartInfo.Arguments = string.Format(baseUrl, searchText);
                        edge.Start();
                        Thread.Sleep(1000);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                i++;
            }

            try
            {
                Process[] browsers = Process.GetProcessesByName("msedge");
                foreach(Process browser in browsers)
                {
                    try
                    {
                        browser.Kill();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unable to close browser. Exception: " + ex.Message);
                    }
                    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to close edge. Exception: " + ex.Message);
            }
            Console.WriteLine("Success");
        }
    }
}
