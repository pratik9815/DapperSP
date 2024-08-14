﻿namespace DapperWithSQL.Models
{
    public class Image
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public IFormFile ImageData { get; set; } 
    }
}
