using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickbaseTask
{
    public interface IFreshdeskClient
    {
        Task<Contact> GetContact(string subdomain, string email);

        Task<Contact> CreateContact(string subdomain, CreateContactInput createContactInput);
        
        Task<Contact> UpdateContact(string subdomain, int id, UpdateContactInput updateContactInput);
    }
}
