using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickbaseTask
{
    public class Handler 
    {
        private IGithubClient _githubClient;
        private IFreshdeskClient _freshdeskClient;

        public Handler(IGithubClient githubClient, IFreshdeskClient freshdeskClient)
        {
            _githubClient = githubClient;
            _freshdeskClient = freshdeskClient;
        }

        public async Task Handle(string gitHubUsername, string freshdeskSubdomain)
        {
            // Get GitHub user by username
            var githubUser = await _githubClient.GetUser(gitHubUsername);
            //Getting Freshdesk user 
            var freshdeskUser = await _freshdeskClient.GetContact(freshdeskSubdomain, githubUser.Email);


            if (freshdeskUser != null)
            {
                //Update a new Freshdesk user
                var updatedUser = new UpdateContactInput
                {
                    Name = githubUser.Login,
                    Email = githubUser.Email,
                    UniqueExternalId = githubUser.Id
                };
                await _freshdeskClient.UpdateContact(freshdeskSubdomain, githubUser.Id, updatedUser);
            }
            else
            {
                // Create existing Freshdesk user
                var newUser = new CreateContactInput
                {
                    Name = githubUser.Login,
                    Email = githubUser.Email,
                    Phone = "123456789",
                    Mobile = "123456789",
                    TwitterId = "twitter",
                    UniqueExternalId = githubUser.Id.ToString()
                };
                await _freshdeskClient.CreateContact(freshdeskSubdomain, newUser);
            }
        }
    }
    
}
