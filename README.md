### Main Program (`Program.cs`)

1. **Main Method (`Program.Main`)**:
   - The `Main` method serves as the entry point of the application. It interacts with the user via console input to gather necessary information (GitHub username and Freshdesk domain).
   - It validates the input to ensure that both the GitHub username and Freshdesk domain are provided and not null.
   - Retrieves API tokens (`GITHUB_TOKEN` and `FRESHDESK_TOKEN`) from environment variables and validates their presence.
   - Creates instances of `GithubClient` and `FreshdeskClient`, passing in the respective API tokens.
   - Initializes a `Handler` instance with these clients.
   - Calls the `Handle` method on the `Handler` instance to manage GitHub and Freshdesk user interactions based on the input.

2. **GithubClient (`GithubClient.cs`)**:
   - **Constructor (`GithubClient`)**: Initializes an instance of `HttpClient` configured with GitHub API base URL. It sets up authorization using the provided GitHub API token (`Bearer` token in headers) and specifies the API version (`X-GitHub-Api-Version` header).
   - **GetUser Method (`GithubClient.GetUser`)**: Performs a GET request to fetch GitHub user details.
   
3. **FreshdeskClient (`FreshdeskClient.cs`)**:
   - **Constructor (`FreshdeskClient`)**: Initializes an instance of `HttpClient` configured with the Freshdesk API base URL. It sets up authorization using the provided Freshdesk API token (`Bearer` token in headers).
   - **GetContact Method (`FreshdeskClient.GetContact`)**: Performs a GET request to retrieve a contact by email (`contacts?email={email}`). If successful, deserializes the response JSON into a `Contact` object and returns it.
   - **CreateContact Method (`FreshdeskClient.CreateContact`)**: Converts a `CreateContactInput` object to JSON and sends a POST request to create a new contact. If successful, deserializes the response JSON into a `Contact` object and returns it.
   - **UpdateContact Method (`FreshdeskClient.UpdateContact`)**: Converts an `UpdateContactInput` object to JSON and sends a PUT request to update a contact. If successful, deserializes the response JSON into a `Contact` object and returns it.

4. **Handler (`Handler.cs`)**:
   - **Constructor (`Handler`)**: Initializes instances of `IGithubClient` and `IFreshdeskClient`.
   - **Handle Method (`Handler.Handle`)**:
     - Calls `GetUser` on `IGithubClient` to fetch GitHub user details.
     - Calls `GetContact` on `IFreshdeskClient` to retrieve Freshdesk contact based on GitHub user's email.
     - Depending on whether the Freshdesk contact exists (`null` or not), either updates the contact using `UpdateContact` or creates a new contact using `CreateContact`.

### Unit Testing (`QuickbaseTask.Tests`)

1. **Setup (`HandlerTests.cs`)**:
   - **Arrange**: Sets up necessary mock objects or instances of classes/interfaces (`IGithubClient` and `IFreshdeskClient`) needed for testing.
   - **Act**: Invokes methods on the `Handler` instance being tested.
   - **Assert**: Verifies that the expected outcomes match the actual results from method calls.

2. **Testing Scenarios**:
   - **Handle Method Test**:
     - Tests scenarios where GitHub user exists in Freshdesk (`UpdateContact` should be called).
     - Tests scenarios where GitHub user does not exist in Freshdesk (`CreateContact` should be called).
   - **Mocking Clients**: Uses mocking frameworks like Moq to create mock instances of `IGithubClient` and `IFreshdeskClient` for isolated testing.
