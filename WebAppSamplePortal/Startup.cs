using System;
using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using WebAppSamplePortal.Business.Services;
using WebAppSamplePortal.Data;
using WebAppSamplePortal.Data.Abstractions;
using WebAppSamplePortal.Data.Repositories;

namespace WebAppSamplePortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Database support
            services.AddScoped<DbConnection, MySqlConnection>(_ =>
            {
                var connectionString = Configuration.GetConnectionString("LegacyDbContext");

                if (string.IsNullOrEmpty(connectionString))
                    throw new Exception("Empty [LegacyDbContext] connection string!");

                return new MySqlConnection(connectionString);
            });

            services.AddDbContext<LegacyDbContext>((serviceProvider, optionsBuilder) =>
            {
                var connection = serviceProvider.GetRequiredService<DbConnection>();
                optionsBuilder.UseMySql(connection);
            });

            services.AddScoped<UnitOfWorkProperty, AppUnitOfWork>(serviceProvider =>
            {
                DbTransaction transaction = null;
                var connection = serviceProvider.GetRequiredService<DbConnection>();
                var uow = new AppUnitOfWork();

                uow.Property<DbTransaction>(() =>
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    if (transaction == null)
                        transaction = connection.BeginTransaction();

                    return transaction;
                });

                uow.Property<LegacyDbContext>(() =>
                {
                    var context = serviceProvider.GetRequiredService<LegacyDbContext>();

                    if (context.Database.CurrentTransaction == null)
                        context.Database.UseTransaction(uow.Property<DbTransaction>());

                    return context;
                });

                return uow;
            });

            services.AddScoped(typeof(UnitOfWorkProperty<>));

            services.AddScoped<FornecedorRepository>();
            services.AddScoped<FornecedorRecursoRepository>();
            services.AddScoped<FornecedorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
