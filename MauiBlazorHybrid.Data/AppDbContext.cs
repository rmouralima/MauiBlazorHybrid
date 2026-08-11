using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MauiBlazorHybrid.Data.Models;


namespace MauiBlazorHybrid.Data
{
    public class AppDbContext: DbContext
    {
        /* O Construtor (public AppDbContext(...)) recebe as configurações (DbContextOptions) definidas lá no
        MauiProgram.cs e as repassa para a classe base. É isso que permite que o SQlite receba aquele caminho dinâmico
        conforme sistema operacional. */

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /* O Método OnConfiguring: Ele possui uma trava de segurança (if(!optionsBuilder.IsConfigured)). 
        Se o contexto for instanciado sem receber as opções do MauiProgram, ele usa o SQLite apontando para 
        um arquivo local "Filename=TesteDB.db" como um "plano B" (fallback) */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=TesteDB.db");
            }
        }

        /* A Propriedade DbSet<Cadastro> Cadastros ordena para o Entity Framework para criar uma tabela chamada
        Cadastros no SQlite. Todo objeto Cadastro que você adicionar aqui será salvo como uma linha nessa tabela */

        public DbSet<Cadastro> Cadastros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* O Método OnModelCreating desenha a estrutura da tabela, definindo regras rígidas que o banco de dados deve respeitar:
            HasKey(e => e.Id): Define que a propriedade Id será a Chave Primária (identificador único) da tabela.
            IsRequired(): Garante que os campos Nome, Email e Telefone não aceitem valores nulos (NOT NULL no banco de dados).
            HasMaxLength(...): Define o tamanho máximo das colunas, otimizando o espaço (ex: o Nome terá no máximo 150 caracteres).   */

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cadastro>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Telefone).IsRequired().HasMaxLength(15);
            });
        }
    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Filename=TesteDB.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }

}
