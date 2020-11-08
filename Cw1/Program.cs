using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Cw1
{
    class Program
    {
        static void  Main(string[] args)
        {
            //example: string remoteUri = "https://blog.home.pl/2015/01/firmowy-adres-e-mail-dlaczego-jest-tak-wazny/";
            string remoteUri = args[0];
            Console.WriteLine("Downloading " + remoteUri);
            byte[] myDataBuffer = new WebClient().DownloadData(remoteUri);
            string download = Encoding.ASCII.GetString(myDataBuffer);
            Console.WriteLine("Download successful.");
            String[] Emails = GetEmailsFromWebContent(download);
            foreach (String Email in Emails)
            {
                Console.WriteLine(Email);
            }
        }

        private static string[] GetEmailsFromWebContent(string webcontent)
        {
            MatchCollection coll = default(MatchCollection);
            coll = Regex.Matches(webcontent, "([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})");
            string[] results = new string[coll.Count];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = coll[i].Value;
            }

            return results;
        }

    }
}
