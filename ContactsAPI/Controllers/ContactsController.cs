using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact() {
                Id = Guid.NewGuid(),
                FullName = addContactRequest.FullName,
                LastName = addContactRequest.LastName,
                Address = addContactRequest.Address,
                Phone = addContactRequest.Phone
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, AddContactRequest addContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact != null) {
               contact.FullName = addContactRequest.FullName;
               contact.LastName = addContactRequest.LastName;
               contact.Address = addContactRequest.Address;
               contact.Phone = addContactRequest.Phone;
               await dbContext.SaveChangesAsync();
               return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
             if(contact != null) {
               dbContext.Remove(contact);
               await dbContext.SaveChangesAsync();
               return Ok(contact);
            }
            return NotFound();
        }
    }
}