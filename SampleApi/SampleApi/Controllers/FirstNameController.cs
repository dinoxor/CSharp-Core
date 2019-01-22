using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Model;
using AddressBook.Repository;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstNameController : ControllerBase
    {
        private const string url = @"https://www.dropbox.com/s/phffcbuedbir0a3/AddressBookData.txt?dl=1";

        // GET api/firstName
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> Get()
        {
            var contactRepository = new ContactRepository(url);

            var results = contactRepository.GetAll();

            return results;
        }

        // GET api/firstName/Sam
        [HttpGet("{firstName}")]
        public ActionResult<Contact> Get(string firstName)
        {
            var contactRepository = new ContactRepository(url);
            Contact result;

            try
            {
                result = contactRepository.GetByFirstName(firstName);
            }
            catch (Exception ex)
            {
                return NotFound($"First name not found: \n{ex}");
            }

            return Ok(result);
        }
    }
}