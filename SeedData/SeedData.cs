namespace SampleTaskApp.SeedData
{
    using Microsoft.Extensions.DependencyInjection;
    using SampleTaskApp.Models;
    using System;
    using System.Linq;

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SampleTaskDbContext>();

                // Ensure database is created
                context.Database.EnsureCreated();
              
                // Check if the database is already seeded
                if (!context.SystemPageAndActions.Any()) // Replace `ExampleEntities` with your DbSet
                {
                    // Add initial data
                    context.SystemPageAndActions.AddRange(
                        new SystemPageAndAction { PageName = "Home", ControllerName = "EfHome", PageUrl = "/ef/common/home" },
                        new SystemPageAndAction { PageName = "Register", ControllerName = "EfUserInfo", PageUrl = "/ef/anonymous/sign-up" },
                        new SystemPageAndAction { PageName = "Login", ControllerName = "Auth", PageUrl = "/ef/anonymous/sign-in" },
                        new SystemPageAndAction { PageName = "Hospitals", ControllerName = "EfHospitals", PageUrl = "/ef/authorized/home" },
                        new SystemPageAndAction { PageName = "Doctors", ControllerName = "EfDoctors", PageUrl = "/ef/authorized/doctors" },
                        new SystemPageAndAction { PageName = "Patients", ControllerName = "EfPatients", PageUrl = "/ef/authorized/patients" },
                        new SystemPageAndAction { PageName = "Beds", ControllerName = "EfBeds", PageUrl = "/ef/authorized/beds" },
                        new SystemPageAndAction { PageName = "Bed-allotments", ControllerName = "EfBed-allotments", PageUrl = "/ef/authorized/bed-allotments" },
                        new SystemPageAndAction { PageName = "Notifications", ControllerName = "EfNotifications", PageUrl = "/ef/authorized/notifications" },
                        //Dapper
                         new SystemPageAndAction { PageName = "Dapper-Home", ControllerName = "Home", PageUrl = "/dapper/common/home" },
                        new SystemPageAndAction { PageName = "Dapper-Register", ControllerName = "DapperUserInfo", PageUrl = "/dapper/anonymous/sign-up" },
                        new SystemPageAndAction { PageName = "Dapper-Login", ControllerName = "Auth", PageUrl = "/dapper/anonymous/sign-in" },
                        new SystemPageAndAction { PageName = "Dapper-Hospitals", ControllerName = "DapperHospitals", PageUrl = "/dapper/authorized/home" },
                        new SystemPageAndAction { PageName = "Dapper-Doctors", ControllerName = "DapperDoctors", PageUrl = "/dapper/authorized/doctors" },
                        new SystemPageAndAction { PageName = "Dapper-Patients", ControllerName = "DapperPatients", PageUrl = "/dapper/authorized/patients" },
                        new SystemPageAndAction { PageName = "Dapper-Beds", ControllerName = "DapperBeds", PageUrl = "/dapper/authorized/beds" },
                        new SystemPageAndAction { PageName = "Dapper-Bed-allotments", ControllerName = "DapperBed-allotments", PageUrl = "/dapper/authorized/bed-allotments" },
                        new SystemPageAndAction { PageName = "Dapper-Notifications", ControllerName = "DapperNotifications", PageUrl = "/dapper/authorized/notifications" }
                    );

                    // Save changes to the database
                    context.SaveChanges();
                }
               
                if (!context.UserInfos.Any()) // Replace `ExampleEntities` with your DbSet
                {
                    // Add initial data
                    context.UserInfos.AddRange(
                        new UserInfo {UserName="Admin" ,Password= "Admin123#",Role= "Admin" }

                    );

                    // Save changes to the database
                    context.SaveChanges();
                }
            }
        }
    }

}
