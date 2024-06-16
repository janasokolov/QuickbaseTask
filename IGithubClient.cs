using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickbaseTask
{
    public interface IGithubClient
    {
        Task<GithubUser> GetUser(string username);
    }
}
