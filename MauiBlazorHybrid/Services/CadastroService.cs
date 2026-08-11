using System;
using System.Collections.Generic;
using System.Text;
using MauiBlazorHybrid.Data.Models;
using Microsoft.EntityFrameworkCore;
using MauiBlazorHybrid.Data;

namespace MauiBlazorHybrid.Services
{
    public class CadastroService
    {
        /*Em vez de usar o banco de dados diretamente, uso a Fábrica de Contextos (IDbContextFactory). 
        Essa é a melhor prática recomendada pela Microsoft para o Blazor. Como os componentes do Blazor podem
        disparar vários eventos simultâneos, usar a fábrica garante que cada método crie sua própria conexão independente
        e curta com o banco, evitando que varias ações tentem usar a mesma conexão ao mesmo tempo.*/
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public CadastroService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        /* O await using cria a conexão (CreateDbContextAsync) e, graças ao termo using, garante que a conexão será fechada e 
        limpa da memória automaticamente assim que o método terminar sua ação. Já a ação, trás os registros da tabela Cadastros
        do SQLite em foomato de lista */
        public async Task<List<Cadastro>> ObterAsync()
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();            
            return await context.Cadastros.ToListAsync();
        }

        /* Se o Id do objeto for 0, significa que é um cadastro novo que ainda não foi pro banco. 
        Portanto, ele usa .Add para Inserir.  Se o Id for diferente de 0, significa que é um registro que já existe. 
        Portanto, ele usa .Update para Atualizar.  Por fim, SaveChangesAsync() pega a decisão tomada e efetiva no SQLite.   */

        public async Task SalvarAsync(Cadastro cadastro)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            if (cadastro.Id == 0)
                context.Cadastros.Add(cadastro);
            else
                context.Update(cadastro);
            await context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(Cadastro cadastro)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Remove(cadastro);
            await context.SaveChangesAsync();
        }

        /* Apesar de não ter sido usado no CadastroPage.razor, este método é excelente para construir telas de pesquisa.
        o AsQueryable() Ele inicia a montagem de uma consulta SQL na memória, mas ainda não vai ao banco de dados.
        As Condicionais (if) verificam um por um (nome, email, telefone). Se você passou algum valor, ele anexa uma cláusula 
        .Where(c => c.Campo.Contains(valor)) à consulta. O Contains é o equivalente do C# ao LIKE '%valor%' no SQL.
        Na ação, apenas na última linha, quando ele encontra o .ToListAsync(), é que o Entity Framework pega todas as
        condições que você acumulou, gera o script SQL definitivo, envia ao SQLite e traz os resultados, já ordenados
        ( devido ao OrderBy) por Nome */

        public async Task<List<Cadastro>> ObterFiltradoAsync (string nome, string email, string telefone)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var query = context.Cadastros.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));
            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Email.Contains(email));
            if (!string.IsNullOrEmpty(telefone))
                query = query.Where(c => c.Telefone.Contains(telefone));
            return await query.OrderBy(c => c.Nome).ToListAsync();
        }
    }
}
