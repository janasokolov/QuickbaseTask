using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickbaseTask
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Github username: ");
            var githubUsername = Console.ReadLine();

            if (githubUsername == null)
            {
                throw new Exception("Plesase enter valid Github username!");
            }

            Console.WriteLine("Enter Freshdesk domain: ");
            var freshdeskDomain = Console.ReadLine();

            if (githubUsername == null || freshdeskDomain == null)
            {
                throw new Exception("Plesase enter valid Freshdesk domain!");
            }

            var githubApiToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            if (githubApiToken == null)
            {
                throw new Exception("Github API token is required!");
            }

            var freshdeskApiToken = Environment.GetEnvironmentVariable("FRESHDESK_TOKEN");
            if (githubApiToken == null)
            {
                throw new Exception("Freshdesk API token is required!");
            }

            var githubClient = new GithubClient(githubApiToken);
            var freshdeskClient = new FreshdeskClient(freshdeskDomain, freshdeskApiToken);

            var handler = new Handler(githubClient, freshdeskClient);
            handler.Handle(githubUsername, freshdeskDomain).RunSynchronously();

            Console.WriteLine("Excecuted!");
        }
    }
}
