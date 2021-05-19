using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Revue_Par_Les_Pairs_V2.DAL_SQLServeur;
using Revue_Par_Les_Pairs_V2.DAL_SQLServeur.DepotSQLServeur;
using Revue_Par_Les_Pairs_V2.Model.Depot;
using System.IO;

namespace Revue_Par_Les_Pairs_V2
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
            services.AddDirectoryBrowser();


            // Connexion a la BD SQLServeur local
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Rplp"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            // Centre d'injection de dependence
            services.AddScoped<IDepotCommentaires, DepotCommentairesSQLServeur>();
            services.AddScoped<IDepotTravail, DepotTravailSQLServeur>();
            services.AddScoped<IDepotCorrections, DepotCorrectionsSQLServeur>();
            services.AddScoped<IDepotCours, DepotCoursSQLServeur>();
            services.AddScoped<IDepotEtudiants, DepotEtudiantsSQLServeur>();
            services.AddScoped<IDepotInscriptions, DepotInscriptionsSQLServeur>();
            services.AddScoped<IDepotProfesseur, DepotProfesseursSQLServeur>();
            services.AddScoped<IDepotSolution, DepotSolutionSQLServeur>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            // Pour mettre nos fichier importer par les professeurs dans un fichier temp dans WWWROOT et les rendre statique
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.WebRootPath, "temp")),
                RequestPath = "/temp"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.WebRootPath, "commentaires")),
                RequestPath = "/commentaires"
            });

            // Cette partie convertie les fichiers en texte pour faire afficher dans la boite de texte de revue de code 
            var provider = new FileExtensionContentTypeProvider();

            provider.Mappings[".cs"] = "text/html";
            provider.Mappings[".cshtml"] = "text/html";
            provider.Mappings[".csproj"] = "text/html";

            app.UseStaticFiles(

              new StaticFileOptions
              {
                  ContentTypeProvider = provider
              }

            );

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.WebRootPath, "temp")),
                RequestPath = "/temp"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.WebRootPath, "commentaires")),
                RequestPath = "/commentaires"
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
