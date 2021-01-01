using System;
using System.Reflection;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Abstractions;
using Repository.Context;
using Repository.Implementations;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Service.Configurations;
using Service.Helpers;


namespace Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var connectionString = configuration.GetValue<string>(VotingServiceConstants.ConnectionString);
            services.AddDbContext<ApplicationContext>(s => 
                s.UseNpgsql(connectionString));
            services.AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationContext>();
            
            services.AddTransient<ITopicsRepository, TopicsRepository>();
            services.AddTransient<IOptionsRepository, OptionsRepository>();
            services.AddTransient<IVotesRepository, VotesRepository>();
            services.AddTransient<ITransaction, DatabaseTransaction>();
            
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, env))
                .Enrich.WithProperty(VotingServiceConstants.Environment, env.EnvironmentName)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            services.AddLogging(loggerBuilder => loggerBuilder.AddSerilog());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            return services;
        }
        
        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, IHostEnvironment env)
        {
            var options = configuration.GetSection(VotingServiceConstants.ElasticSection).Get<LoggingOptions>();
            var uri = options?.Uri ?? throw new ArgumentNullException();
            var indexFormat = options?.IndexFormat ?? throw new ArgumentNullException();
            
            return new ElasticsearchSinkOptions(new Uri(uri))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{indexFormat}-{env.EnvironmentName}"
            };
        }
    }
}