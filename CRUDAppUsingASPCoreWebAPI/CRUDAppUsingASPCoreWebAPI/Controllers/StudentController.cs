using CRUDAppUsingASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CRUDAppUsingASPCoreWebAPI.Controllers
{
    public class StudentController : Controller
    {
        private readonly string url = "https://localhost:7090/api/StudentAPI";
        private readonly HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                students = JsonConvert.DeserializeObject<List<Student>>(result);
            }

            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
               return View(); 
        }
            

        [HttpPost]
        public IActionResult Create(Student s)
        {
            string data = JsonConvert.SerializeObject(s);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Student Added Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }


       [HttpGet]
public async Task<IActionResult> Edit(int id)
{
    try
    {
        HttpResponseMessage response = await client.GetAsync($"{url}/{id}");

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            var student = JsonConvert.DeserializeObject<Student>(result);
            return View(student);
        }

        TempData["Message"] = $"Failed to load student data. Status: {response.StatusCode}";
    }
    catch (Exception ex)
    {
        TempData["Message"] = $"Exception: {ex.Message}";
    }

    return RedirectToAction("Index");
}


        // POST: Student/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Student s)
        {
            string data = JsonConvert.SerializeObject(s);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"{url}/{s.id}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Student Updated Successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Update failed!";
            return View(s);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Student Deleted Successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}
