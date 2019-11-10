using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Banking.Infrastructure;
using Microsoft.OpenApi.Models;
using Banking.Infrastructure.Repositories.EFCore;
using FluentValidation;
using Banking.Application.DTOs;
using Banking.Application.Validations;
using Banking.Service.Services.Interfaces;
using Banking.Service.Services;
using Banking.Application.Models;
using Banking.Application.Middlewares;

namespace Banking.Web
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
            #region application
            services.AddTransient<IValidator<AccountRegisterDto>, AccountRegisterValidator>();
            services.AddScoped<AccountRegisterValidator>();
            services.AddTransient<IValidator<StatementDepositDto>, StatementDepositValidator>();
            services.AddScoped<StatementDepositValidator>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            #endregion

            #region infrastructure
            services.AddScoped<AccountRepository>();
            services.AddScoped<StatementRepository>();
            #endregion

            #region service
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStatementService, StatementService>();
            services.AddScoped<ICashTransferService, CashTransferService>();
            #endregion

            services.AddControllers();

            services.AddDbContext<BankingContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BankingContext")));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Banking API", Version = "1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
