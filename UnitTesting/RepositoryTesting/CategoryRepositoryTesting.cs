using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.Admin;
using net_design_pattern.Persistence.Context;
using net_design_pattern.Persistence.Repositories.Admin;
using Xunit;

namespace net_design_pattern.UnitTesting.RepositoryTesting
{
    public class CategoryRepositoryTesting
    {
        ICategoryRepository categoryRepository;
        
        [Fact]
        public void CategoryRepository_GetAll_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "phonestore")
            .Options;
            using (var context = new AppDbContext(options))
            {
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "IPad"
                });
                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                categoryRepository = new CategoryRepository(context);
                var list = categoryRepository.GetCategories().ToList();
                Assert.NotNull(list);
                list.Should().HaveCount(1);
            }
        }
    }
}