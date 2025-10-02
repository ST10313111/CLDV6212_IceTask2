using Azure;
using Azure.Data.Tables;
using CLDV6212_ICETASK2.Models;

namespace CLDV6212_ICETASK2.services
{
    public class AzureTableStorage
    {
        private readonly TableClient _people;

        public AzureTableStorage(string connectionString)
        {
            _people = new TableClient(connectionString, "People");
        }

        public async Task<List<People>> GetPeoplesAsync()
        {
            var peoples = new List<People>();
            await foreach (var person in _people.QueryAsync<People>())
            {
                peoples.Add(person);
            }
            return peoples;
        }

        public async Task AddPeopleAsync(People people)
        {
            if (string.IsNullOrEmpty(people.PartitionKey) || string.IsNullOrEmpty(people.RowKey))
            {
                throw new ArgumentException("PartitionKey and RowKey must be set.");
            }

            try
            {
                await _people.AddEntityAsync(people);
            }
            catch (RequestFailedException ex)
            {
                throw new InvalidOperationException("Error adding entity to Table Storage", ex);
            }
        }

        public async Task DeletePeopleAsync(string partitionKey, string rowKey)
        {
            await _people.DeleteEntityAsync(partitionKey, rowKey);
        }
    }
}
