using MauiBlazorHybrid.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MauiBlazorHybrid.Extensions;


namespace MauiBlazorHybrid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            /* Cria o banco de dados SQLite na raiz do sistema
            Em windows, normalmente fica em C:\Users\SeuUsuario\AppData\Local\Packages\NomeDoApp\LocalState */
            var dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                    "TesteDB.db"
            );

            /* Os comandos AddDbContext e AddDbContextFactory ensinam o Entity Framework Core a utilizar o provedor do SQLite, 
            passando o caminho exato do banco de dados. */

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            /* Chama um método de extensão as lógicas exclusivas do seu sistema, como os Services. */

            builder.Services.Dependencias();

            var app = builder.Build();

            // Aplica Migrations antes da inicialização da aplicação. Evita quebras pela falta do banco de dadados SQLite
            using (var scope = app.Services.CreateScope())
            {
                var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
                using var context = dbContextFactory.CreateDbContext();
                
                context.Database.Migrate();
            }

            return app;
        }
    }
}
