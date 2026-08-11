# 📱 MAUI Blazor Hybrid - CRUD com SQLite

Este é um projeto simples e prático desenvolvido em **.NET MAUI Blazor Hybrid**, demonstrando como criar um aplicativo multiplataforma (desktop e mobile) com interface web embutida, conectada a um banco de dados relacional local.

O aplicativo implementa um CRUD (Criar, Ler, Atualizar, Excluir) completo para gerenciamento de Cadastros (Nome, E-mail e Telefone), utilizando boas práticas de injeção de dependência e gerenciamento de estado em Blazor.

## 🚀 Funcionalidades

*   **Interface Reativa:** Construída inteiramente com componentes Razor (`.razor`) e HTML/CSS, sem a necessidade de XAML para o front-end.
*   **Persistência Local (Offline):** Salva os dados localmente no dispositivo (Windows, Android) utilizando o banco de dados SQLite.
*   **ORM Robusto:** Utiliza o Entity Framework Core (EF Core) para mapeamento objeto-relacional.
*   **Migrations Automáticas:** O banco de dados e suas tabelas são gerados/atualizados automaticamente no dispositivo do usuário assim que o aplicativo é iniciado pela primeira vez, graças à execução do `context.Database.Migrate()` na inicialização (`MauiProgram.cs`).
*   **Fábrica de Contextos:** Uso de `IDbContextFactory` para garantir transações de banco de dados seguras e isoladas no ecossistema do Blazor.

## 🛠️ Tecnologias Utilizadas

*   [.NET MAUI](https://learn.microsoft.com/pt-br/dotnet/maui/) (Multi-platform App UI)
*   [Blazor Hybrid](https://learn.microsoft.com/pt-br/aspnet/core/blazor/hybrid/)
*   C# 12 / .NET 8 (ou superior)
*   SQLite
*   Entity Framework Core (`Microsoft.EntityFrameworkCore.Sqlite`)

## 🏗️ Arquitetura

O projeto adota o padrão **Component-Service-Model**, ideal para o ecossistema Blazor, descartando a necessidade do tradicional MVVM (Model-View-ViewModel) ou MVC.

1.  **Models (`MauiBlazorHybrid.Data.Models`):** Classes anêmicas e atributos de validação (ex: `[Required]`) para mapeamento de dados (ex: `Cadastro.cs`).
2.  **Services (`MauiBlazorHybrid.Services`):** Onde reside a inteligência e as regras de negócio. O `CadastroService.cs` gerencia todas as transações com o SQLite.
3.  **Components (`Pages/*.razor`):** Interfaces "limpas" que focam apenas em exibir os dados e capturar cliques, delegando as ações pesadas aos Serviços injetados via Dependência.

