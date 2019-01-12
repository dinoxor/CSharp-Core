using System;
using System.Collections.Generic;
using AddressBook.Model;

namespace AddressBook.Repository
{
    public class ContactRepository
    {
        private string _url;

        public ContactRepository(string url)
        {
            _url = url;
        }

        public List<Contact> Get()
        {
            //reads from url
            //deserialize
            //returns
        }

        public int Insert(Contact contact)
        {
            //reads from URL
            //inserts
            //returns id if successful
        }
    }
}
