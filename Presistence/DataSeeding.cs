using Domain.Contracts;
using Domain.Models.IdentityModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
{
    public class DataSeeding(CoreveraDbContext _corveraDbContext,
        RoleManager<IdentityRole> _roleManager,
        UserManager<ApplicationUser> _userManager) : IDataSeeding
    {
        public void SeedData()
        {


            try
            {
                if (_corveraDbContext.Database.GetPendingMigrations().Any())
                {
                    _corveraDbContext.Database.Migrate();

                }
                if (!_corveraDbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Presistence\Data\DataSeed\brands.json");
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (productBrands != null && productBrands.Any())
                    {
                        _corveraDbContext.ProductBrands.AddRange(productBrands);

                    }

                }


                if (!_corveraDbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Presistence\Data\DataSeed\types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                    {
                        _corveraDbContext.ProductTypes.AddRange(ProductTypes);

                    }
                }

                if (!_corveraDbContext.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"..\Presistence\Data\DataSeed\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products != null && Products.Any())
                    {
                        _corveraDbContext.Products.AddRange(Products);

                    }

                }


                if (!_corveraDbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodData = File.ReadAllText(@"..\Presistence\Data\DataSeed\delivery.json");
                    var DeliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
                    if (DeliveryMethod != null && DeliveryMethod.Any())
                    {
                        _corveraDbContext.Set<DeliveryMethod>().AddRange(DeliveryMethod);
                    }
                }

                _corveraDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }


        }

        public async Task SeedIdentityDataAsync()
        {

            //Seed Roles [Admin ,SuperAdmin]
            if (!_roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole{Name="Admin"},
                    new IdentityRole{Name="SuperAdmin"}
                };
             

                //Seed Users [Admin ,SuperAdmin]
                if (!_userManager.Users.Any())
                {
                    var AdminUser = new ApplicationUser()
                    {
                        DisplayName = "Admin",
                        UserName = "admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "01000000000"
                    };
                    
                    var SuperAdminUser = new ApplicationUser()
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "superadmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01000000990"
                    };

                   await _userManager.CreateAsync(AdminUser, "P@ss0rd");
                  await  _userManager.CreateAsync(SuperAdminUser, "Pa$$0rd");

                    //Seed UserRoles [Admin ,SuperAdmin]
                   await _userManager.AddToRoleAsync(AdminUser, "Admin");
                   await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");




                }
            }
        }


    }
}
