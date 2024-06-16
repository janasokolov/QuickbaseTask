using QuickbaseTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Quickbasetask.Test
{
    [TestClass]
    public class HandlerTests
    {
        public async Task HandleUserAsync_ExistingUser_UpdateContact()
        {
            // Arrange
            string gitHubUsername = "testuser";
            string freshdeskSubdomain = "example";

            // Mock GitHub client
            var githubClientMock = new Mock<IGithubClient>();
            var githubUser = new GithubUser
            {
                Login = "testuser",
                Email = "testuser@example.com",
                Id = 123
            };
            githubClientMock.Setup(client => client.GetUser(gitHubUsername))
                            .ReturnsAsync(githubUser);

            // Mock Freshdesk client
            var freshdeskClientMock = new Mock<IFreshdeskClient>();
            var freshdeskUser = new Contact
            {
                Id = 123
            };
            freshdeskClientMock.Setup(client => client.GetContact(freshdeskSubdomain, githubUser.Email))
                               .ReturnsAsync(freshdeskUser);

            // Create handler instance with mocked clients
            var handler = new Handler(githubClientMock.Object, freshdeskClientMock.Object);

            await handler.Handle(gitHubUsername, freshdeskSubdomain);

            freshdeskClientMock.Verify(client => client.UpdateContact(freshdeskSubdomain, githubUser.Id, It.IsAny<UpdateContactInput>()), Times.Once);
            freshdeskClientMock.Verify(client => client.CreateContact(freshdeskSubdomain, It.IsAny<CreateContactInput>()), Times.Never);
        }

        [TestMethod]
        public async Task HandleUserAsync_NewUser_CreateContact()
        {
            // Arrange
            string gitHubUsername = "newuser";
            string freshdeskSubdomain = "example";

            // Mock GitHub client
            var githubClientMock = new Mock<IGithubClient>();
            var githubUser = new GithubUser
            {
                Login = "newuser",
                Email = "newuser@example.com",
                Id = 456
            };
            githubClientMock.Setup(client => client.GetUser(gitHubUsername))
                            .ReturnsAsync(githubUser);

            // Mock Freshdesk client
            var freshdeskClientMock = new Mock<IFreshdeskClient>();
            freshdeskClientMock.Setup(client => client.GetContact(freshdeskSubdomain, githubUser.Email))
                               .ReturnsAsync((Contact)null); 

            // Create handler instance with mocked clients
            var handler = new Handler(githubClientMock.Object, freshdeskClientMock.Object);

            await handler.Handle(gitHubUsername, freshdeskSubdomain);

            freshdeskClientMock.Verify(client => client.UpdateContact(freshdeskSubdomain, githubUser.Id, It.IsAny<UpdateContactInput>()), Times.Never);
            freshdeskClientMock.Verify(client => client.CreateContact(freshdeskSubdomain, It.IsAny<CreateContactInput>()), Times.Once);
        }
    }
}