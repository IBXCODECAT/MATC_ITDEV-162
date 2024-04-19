using System;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

namespace Persistance
{
    public static class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.posts.Any())
            {
                var Posts = new List<Post>
                {
                    new Post {
                        Title = "First Post",
                        Body = "Lorem ipsum dolar sit amet",
                        Date = DateTime.Now.AddDays(-10)
                    },
                    new Post {
                        Title = "Second Post",
                        Body = "Lorem ipsum dolar sit amet",
                        Date = DateTime.Now.AddDays(-7)
                    },
                    new Post {
                        Title = "Third Post",
                        Body = "Lorem ipsum dolar sit amet",
                        Date = DateTime.Now.AddDays(-4)
                    },
                };

                context.posts.AddRange(Posts);
                context.SaveChanges();
            }
        }
    }
}
