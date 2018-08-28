using Curso.Udemy.Business;
using Curso.Udemy.Business.Implementations;
using Curso.Udemy.Hypermedia;
using Curso.Udemy.Model.Context;
using Curso.Udemy.Repository;
using Curso.Udemy.Repository.Generic;
using Curso.Udemy.Repository.Implementations;
using Curso.Udemy.Security.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using RestWithASPNETUdemy.Repository.Generic;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Tapioca.HATEOAS;

namespace Curso.Udemy
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // injetando a string de conexao do banco na classe de contexto do EntityFramework
            var connection = _configuration["ConnectionString:SqlServerStringConnection"];
            services.AddDbContext<SqlContext>(options => options.UseSqlServer(connection, opt => opt.EnableRetryOnFailure()));

            // execulta metodo para rodar migrations
            ExeculteMigrations(connection);

            // Autenticação
            Authentication(services);

            // Configuracoes do Data Content
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            // configuracoes do HATEOS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ObjectContentResponseEnricherList.Add(new BookEnricher());
            services.AddSingleton(filterOptions);

            // configuracoes do swagger
            services.AddApiVersioning(option => option.ReportApiVersions = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Restiful API with ASP.NET Core 2.0",
                        Version = "v1"
                    });
            });

            //Injeçoes de dependencia
            services.AddScoped<IPersonBusiness, PersonBusinessImp>();
            services.AddScoped<IBookBusiness, BookBusinessImp>();
            services.AddScoped<UserRepository, PersonRepositoryImp>();
            services.AddScoped<ILoginBusiness, LoginRepositoryImp>();
            services.AddScoped<IUsersRepository, UsersRepositoryImp>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        }

        private void Authentication(IServiceCollection services)
        {
            var signingConfigurations = new SigningConfiguration();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                _configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
        }

        private void ExeculteMigrations(string connection)
        {
            if (_environment.IsDevelopment())
            {
                try
                {
                    // configurações do Evolve para rodar migrations
                    SqlConnection cnx = new SqlConnection(connection);
                    var evolve = new Evolve.Evolve("Evolve.json", cnx, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();

                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed.", ex);
                    throw;
                }
            }
        }

        // Este método é chamado em tempo de execução. Use este método para configurar o pipeline de requisiçõees HTTP.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // configuracao do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => { 
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            // roteamento HATEOS
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id}");
            });
        }
    }
}
