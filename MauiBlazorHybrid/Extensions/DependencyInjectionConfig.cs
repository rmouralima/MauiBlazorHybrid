using System;
using System.Collections.Generic;
using System.Text;
using MauiBlazorHybrid.Services;

namespace MauiBlazorHybrid.Extensions
{
    public static class DependencyInjectionConfig
    {
        /*Interface de injeção de dependências, contendo os Services para injeção no MauiProgram.cs
        Uso como boa prática, evitando inchaço de código no MauiProgram.cs*/
        public static IServiceCollection Dependencias(this IServiceCollection services)
        {
            services.AddScoped<CadastroService>();

            return services;
        }
    }
}
