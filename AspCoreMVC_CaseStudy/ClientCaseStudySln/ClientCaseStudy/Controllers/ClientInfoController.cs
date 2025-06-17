using ClientCaseStudy.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientCaseStudy.Controllers
{
    [Route("Client")]
    public class ClientInfoController : Controller
    {
        static List<ClientInfo> clients= new List<ClientInfo>
        {
            new ClientInfo{ClientId=1,CompanyName="Hexaware",WebUrl="hew.org",Email="hexa@gmail",Category="Low_Level_Managed_IT_Services"},
            new ClientInfo{ClientId=2,CompanyName="Google",WebUrl="hew.org",Email="hexa@gmail",Category="Low_Level_Managed_IT_Services"},
            new ClientInfo{ClientId=3,CompanyName="MicroSoft",WebUrl="hew.org",Email="hexa@gmail",Category="Low_Level_Managed_IT_Services"}
        };

        [Route("/")]
        [Route("List",Name ="List")]
        public IActionResult ShowAllClientDetails()
        {
            return View(clients);
        }
        [Route("GetDetailsById",Name = "GetDetailsById")]
        public IActionResult GetDetailsByClientId(int id)
        {
            var client=clients.FirstOrDefault(c => c.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }
        [Route("GetDetailsByCompName", Name = "GetDetailsByCompName")]
        public IActionResult GetDetailsByCompanyName(string name)
        {
            var client=clients.FirstOrDefault(c=>c.CompanyName.ToLower() == name.ToLower());
            if (client == null)
            {
                return NotFound();
            }
            return View("GetDetailsByClientId",client);
        }
        [Route("GetDetailsByEmail", Name = "GetDetailsByEmail")]
        public IActionResult GetDetailsByEmail(string email)
        {
            var client = clients.FirstOrDefault(c => c.Email.ToLower() == email.ToLower());
            if (client == null) return NotFound();
            return View("GetDetailsByClientId",client);
        }
        [Route("GetDetailsByCategory", Name = "GetDetailsByCategory")]
        public IActionResult GetDetailsByCategory(string category)
        {
            var result = clients.Where(c => c.Category.ToLower() == category.ToLower()).ToList();
            return View("ShowAllClientDetails", result);
        }
        [Route("GetDetailsByStandard", Name = "GetDetailsByStandard")]
        public IActionResult GetDetailsByStandard(string standard)
        {
            var result = clients.Where(c => c.Standard.ToLower() == standard.ToLower()).ToList();
            return View("ShowAllClientDetails", result);
        }
        [Route("AddClient", Name = "AddClient")]
        public IActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
        [Route("AddClient", Name = "AddClient")]
        public IActionResult AddClient(ClientInfo clientInfo)
        {
            clientInfo.ClientId = clients.Count > 0 ? clients.Max(c => c.ClientId) + 1 : 1;
            clients.Add(clientInfo);
            return RedirectToAction("ShowAllClientDetails");
        }
    }
}
