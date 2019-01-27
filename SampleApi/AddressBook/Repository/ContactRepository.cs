using System.Collections.Generic;
using AddressBook.Model;
using ServiceStack;
using System.Net;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SampleApi.Model;

namespace AddressBook.Repository
{
    public class ContactRepository : IRepository
    {
        private string _url;

        //public ContactRepository(IOptions<AppSettings> options) => _url = options.Value.TextDatabaseUrl;        
        public ContactRepository(IOptions<AppSettings> options)
        {
            _url = options.Value.TextDatabaseUrl;
        }

        public List<Contact> GetAll()
        {
            var webClient = new WebClient();
            var resultString = webClient.DownloadString(_url);

            return resultString.FromCsv<List<Contact>>();
        }

        public Contact GetById(int id)
        {
            var results = GetAll();

            var contact = results.Single(x => x.Id == id);

            return contact;
        }

        public Contact GetByFirstName(string firstName)
        {
            var results = GetAll();

            var contact = results.Single(x => x.FirstName.EqualsIgnoreCase(firstName.Trim()));

            return contact;
        }

        public int Insert(Contact contact)
        {
            var webClient = new WebClient();
            var resultString = webClient.DownloadString(_url);

            var contacts = resultString.FromCsv<List<Contact>>();

            var largestInt = contacts.Max(x => x.Id);

            contact.Id = ++largestInt;

            contacts.Add(contact);

            var result = contacts.ToCsv();

            //reads from URL
            //inserts
            //returns id if successful

            return 1;
        }
    }
}
