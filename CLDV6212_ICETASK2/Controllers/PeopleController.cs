using CLDV6212_ICETASK2.Models;
using CLDV6212_ICETASK2.services;
using Microsoft.AspNetCore.Mvc;

namespace CLDV6212_ICETASK2.Controllers
{
    public class PeopleController : Controller
    {
        private readonly AzureTableStorage _azureTableStorage;

        public PeopleController(AzureTableStorage azureTableStorage)
        {
            _azureTableStorage = azureTableStorage;
        }

        public async Task<IActionResult> Index()
        {
            var peoples = await _azureTableStorage.GetPeoplesAsync();
            return View(peoples);
        }

        public async Task<IActionResult> Delete(string partitionKey, string rowKey)
        {
            await _azureTableStorage.DeletePeopleAsync(partitionKey, rowKey);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(People people)
        {
            if (!ModelState.IsValid)
            {
                people.PartitionKey = "PeoplePartition";
                people.RowKey = Guid.NewGuid().ToString();

                await _azureTableStorage.AddPeopleAsync(people);
                return RedirectToAction(nameof(Index));
            }
            return View(people);
        }
    }
}
