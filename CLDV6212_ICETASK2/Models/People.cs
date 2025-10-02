using Azure.Data.Tables;
using Azure;
using System.ComponentModel.DataAnnotations;

namespace CLDV6212_ICETASK2.Models
{
    public class People : ITableEntity
    {

        public string PartitionKey { get; set; } 
        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }


    
       
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
