using System;

namespace WebApplication8.Models
{
    public class roleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public roleViewModel()
        {
            Id=Guid.NewGuid().ToString();
        }
    }
}
