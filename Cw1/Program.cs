using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Cw1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //example: string remoteUri = "https://blog.home.pl/2015/01/firmowy-adres-e-mail-dlaczego-jest-tak-wazny/";
            string remoteUri = "http://wp.pl";// args[0];

            if (remoteUri == null) {
                throw new ArgumentNullException("remoteUri", "No URL provided!");
            }

            if (isUrlInvalid(remoteUri)) {
                throw new ArgumentException("Invalid URL!", "remoteUri");
            }

            Console.WriteLine("Downloading " + remoteUri);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(remoteUri);
                Console.WriteLine("Download successful.");
                String[] Emails = GetEmailsFromWebContent(response);
                if (Emails.Length == 0)
                {
                    Console.WriteLine("No valid emails found!");
                } else {
                    var SetOfEmails = new HashSet<string>(Emails);
                    foreach (String Email in SetOfEmails)
                    {
                        Console.WriteLine(Email);
                    }
                }
                

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

        private static bool isUrlInvalid(string remoteUri) 
        {
            return !(Uri.IsWellFormedUriString(remoteUri, UriKind.Absolute));
        }

    }
}
