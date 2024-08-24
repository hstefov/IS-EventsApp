using EmsAdminApp.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EmsAdminApp.Controllers
{
    public class AttendeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportAttendees(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<Attendee> attendees = getAllAttendeesFromFile(file.FileName);
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5088/api/admin/ImportAllAttendees";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(attendees), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index", "Order");

        }

        private List<Attendee> getAllAttendeesFromFile(string fileName)
        {
            List<Attendee> attendees = new List<Attendee>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        attendees.Add(new Models.Attendee
                        {
                            Email = reader.GetValue(0).ToString(),
                            Password = reader.GetValue(1).ToString(),
                            ConfirmPassword = reader.GetValue(2).ToString()
                        });
                    }

                }
            }
            return attendees;
        }
    }
}
