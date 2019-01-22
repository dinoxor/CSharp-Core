using System.Collections.Generic;
using AddressBook.Model;

namespace AddressBook.Repository
{
    public interface IRepository
    {
        List<Contact> GetAll();
        Contact GetById(int id);
        Contact GetByFirstName(string name);
        int Insert(Contact contact);
    }
}