﻿namespace DeliVeggie.Model
{
    public class AddProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Price { get; set; }
    }
}
