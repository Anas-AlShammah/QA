﻿using QA.Data;
using QA.Interfaces;
using QA.Models;

namespace QA.Services
{
    public class CategoryService : ICategory
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public List<Category> GetAll()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }
    }
}