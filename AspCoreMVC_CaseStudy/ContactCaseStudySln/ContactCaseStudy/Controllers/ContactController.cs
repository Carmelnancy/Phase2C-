using ContactCaseStudy.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactCaseStudy.Controllers
{
    public class ContactController : Controller
    {
        private static List<ContactInfo> contacts = new List<ContactInfo>();
        public IActionResult ShowContacts()
        {
            return View(contacts);
        }

        public IActionResult GetContactById(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.ContactId == id);
            if (contact == null)
                return NotFound();
            return View(contact);
        }

        public ActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(ContactInfo contactInfo)
        {
            contactInfo.ContactId = contacts.Count > 0 ? contacts.Max(c => c.ContactId) + 1 : 1;
            contacts.Add(contactInfo);
            return RedirectToAction("ShowContacts");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
