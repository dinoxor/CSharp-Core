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
    public class IdController : ControllerBase
    {
        //private IRepository _contactRepository;

        private const string url = @"https://www.dropbox.com/s/phffcbuedbir0a3/AddressBookData.txt?dl=1";
        //public ValuesController(IRepository contactRepository)
        //{
        //    _contactRepository = contactRepository;
        //}

        // GET api/id
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> Get()
        {
            var contactRepository = new ContactRepository(url);

            var results = contactRepository.GetAll();

            return results;
        }

        // GET api/id/5
        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {
            var contactRepository = new ContactRepository(url);
            Contact result;

            try
            {
                result = contactRepository.GetById(id);
            }
            catch (Exception ex)
            {
                return NotFound($"Id not found: \n{ex}");
            }

            return Ok(result);
        }

        // POST api/id
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/id/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/id/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
