﻿namespace BookService.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
