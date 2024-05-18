using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OrderDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<OrderDbContext>>()))
            {
                if (context.Customers.Any() || context.Products.Any() || context.Orders.Any())
                {
                    
                    return;
                }

              
                var customers = new List<Customer>
                {
                    new Customer { Name = "Rafał Witek", Email = "rafal.witek@microsoft.wsei.edu.pl" },
                    new Customer { Name = "Jan Kowalski", Email = "jan.kowalski@email.com" },
                    new Customer { Name = "Anna Nowak", Email = "anna.nowak@email.com" }
                };

                context.Customers.AddRange(customers);
                context.SaveChanges();

              
                var products = new List<Product>
                {
                    new Product { Name = "Jabłko", Price = 10.99m },
                    new Product { Name = "Banan", Price = 15.49m },
                    new Product { Name = "Pomidor", Price = 7.99m }
                };

                context.Products.AddRange(products);
                context.SaveChanges();

             
                var orders = new List<Order>
                {
                    new Order { CustomerId = customers[0].Id, OrderDate = DateTime.Now.AddDays(-10) },
                    new Order { CustomerId = customers[1].Id, OrderDate = DateTime.Now.AddDays(-5) },
                    new Order { CustomerId = customers[2].Id, OrderDate = DateTime.Now.AddDays(-1) }
                };

                context.Orders.AddRange(orders);
                context.SaveChanges();

             
                

                context.Customers.UpdateRange(customers);
                context.SaveChanges();

             
                var orderProducts = new List<OrderProduct>
                {
                    new OrderProduct { OrderId = orders[0].Id, ProductId = products[0].Id },
                    new OrderProduct { OrderId = orders[0].Id, ProductId = products[1].Id },
                    new OrderProduct { OrderId = orders[1].Id, ProductId = products[1].Id },
                    new OrderProduct { OrderId = orders[1].Id, ProductId = products[2].Id },
                    new OrderProduct { OrderId = orders[2].Id, ProductId = products[0].Id },
                    new OrderProduct { OrderId = orders[2].Id, ProductId = products[2].Id }
                };

                context.OrderProducts.AddRange(orderProducts);
                context.SaveChanges();
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
              
                var roles = new[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

               
                var adminUser = new IdentityUser { UserName = "admin", Email = "admin@example.com" };
                var normalUser = new IdentityUser { UserName = "user", Email = "user@example.com" };
                var users = new[] { adminUser, normalUser };

                foreach (var user in users)
                {
                    if (await userManager.FindByNameAsync(user.UserName) == null)
                    {
                        await userManager.CreateAsync(user, "Password123!");
                        if (user.UserName == "admin")
                        {
                            await userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(user, "User");
                        }
                    }
                }
            }).GetAwaiter().GetResult();

        }

    }
}
