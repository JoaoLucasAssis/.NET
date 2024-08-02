- [.NET](#net)
  - [Dupla Compilação](#dupla-compilação)
  - [Garbage Collector](#garbage-collector)
  - [Entity Framework](#entity-framework)
- [ASP.NET](#aspnet)
  - [Hosting](#hosting)
  - [Pipeline](#pipeline)
  - [Middleware](#middleware)
  - [Injeção de Dependência](#injeção-de-dependência)
  - [Identity](#identity)
  - [NuGet](#nuget)
  - [Logging e LogLevel](#logging-e-loglevel)
- [MVC](#mvc)
  - [Getting Started](#getting-started)
  - [Conceitos](#conceitos)
    - [Bundling e Minification](#bundling-e-minification)
    - [Slugify](#slugify)
    - [Areas](#areas)
  - [Model](#model)
    - [DTO](#dto)
    - [Data Annotations](#data-annotations)
  - [View](#view)
    - [Razor](#razor)
    - [TagHelpers](#taghelpers)
    - [Partial Views](#partial-views)
    - [View Components](#view-components)
    - [Estados](#estados)
  - [Controller](#controller)
    - [Action Results](#action-results)
    - [Roteamento](#roteamento)
    - [Parâmetros](#parâmetros)
    - [ModelState](#modelstate)

# .NET

.NET é uma plataforma de desenvolvimento criada pela Microsoft que oferece um ambiente para construir e executar diferentes tipos de aplicativos, incluindo:

- Aplicativos Desktop (Windows Forms, WPF)
- Aplicativos Web (ASP.NET)
- Serviços Web
- Aplicativos Móveis
- Aplicativos em Nuvem

É um ecossistema que inclui várias ferramentas, bibliotecas e linguagens de programação (como C#, F#, e VB.NET).

Todo esse ecossistema trabalha junto para permitir o desenvolvimento eficiente e robusto de software.

## Dupla Compilação

Possui um processo de dupla compilação, que permite que os aplicativos desenvolvidos nessa plataforma rodem em diferentes sistemas operacionais.

O compilador converte o código de uma linguagem de programação suportada pelo .NET para uma forma intermediária (IL).

<h3>IL (Intermediate Language)</h3>
<h3></h3>
IL é uma linguagem de baixo nível que não é específica de nenhum sistema operacional ou hardware. 

Isso significa que o código IL pode ser executado em qualquer ambiente que tenha uma implementação da Common Language Runtime (CLR).

<h3>CLR (Common Language Runtime)</h3>

Quando um aplicativo .NET é executado, o código IL é compilado "Just-In-Time" (JIT) para o código de máquina nativo específico do sistema operacional e do hardware.

Esse processo é realizado pelo Just-In-Time Compiler (JIT Compiler), que faz parte da CLR.

## Garbage Collector

O Garbage Collector é um componente crucial do Common Language Runtime (CLR) que gerencia automaticamente a alocação e liberação de memória para aplicações .NET.

Ele ajuda a evitar problemas de memória, como vazamentos, ao identificar e liberar objetos que não são mais acessíveis no programa.

O .NET utiliza um heap gerenciado, dividido em três gerações para otimizar a coleta de lixo.

| Geração | Descrição                                                            |
| :-----: | -------------------------------------------------------------------- |
|    0    | Onde novos objetos são alocados. Coletas são frequentes e rápidas    |
|    1    | Área intermediária para objetos que sobrevivem à coleta na Geração 0 |
|    2    | Para objetos de longa duração, onde as coletas são menos frequentes  |

A coleta de Lixo envolve identificar objetos inacessíveis, compactar o heap para eliminar a fragmentação e liberar memória dos objetos mortos.

## Entity Framework

Entity Framework é um Object-Relational Mapping (ORM) framework para .NET.

Permite que desenvolvedores trabalhem com um banco de dados usando objetos .NET.

Elimina a necessidade de escrever grande parte do código SQL manualmente.

Permite escrever consultas utilizando LINQ (Language Integrated Query), proporcionando uma forma intuitiva e tipada de acessar dados.

> :bulb: Confira um template de configuração de modelo de dados no caminho /Templates/Entity Framework

<h3>Modelagem de Dados</h3>

Permite definir um modelo de dados usando classes .NET.

Cada classe corresponde a uma tabela no banco de dados e cada propriedade da classe a uma coluna na tabela.

<h3>Mecanismos de Modelagem</h3>

|   Mecanismos   | Descrição                                                        |
| :------------: | :--------------------------------------------------------------- |
|   Code-First   | O banco de dados é gerado através de classes C# ou VB.NET        |
| Database-First | As classes são definidas a partir de um banco de dados existente |
|  Model-First   | O modelo de dados é criado usando o Entity Framework Designer    |

<h3>DbContext</h3>

DbContext é uma classe central no Entity Framework essencial para a comunicação entre a aplicação e o banco de dados.

Ele gerencia a conexão com o banco de dados e fornece uma abstração para o acesso aos dados.

Essa classe é responsável por algumas outras funções, sendo elas:

- Configurar o modelo de dados
- Consultar e persistir dados em seu banco
- Fazer toda a rastreabilidade do objeto
- Materializar resultados das consultas
- Cache de primeiro nível

Existem duas maneiras de configurar o seu modelo de dados no DbContext:

<table style="width: 100%; border-collapse: collapse;">
    <thead>
        <tr>
            <th style="border: 1px solid #ddd; padding: 10px; vertical-align: top; width: 50%;">Using DbSet&lt;&gt;</th>
            <th style="border: 1px solid #ddd; padding: 10px; vertical-align: top; width: 50%;">Overriding OnModelCreating()</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style="border: 1px solid #ddd; padding: 10px; vertical-align: top; width: 50%;">
                <pre style="width: 100%; height: 300px; padding: 5px; border-radius: 5px; overflow-x: auto; margin: 0; box-sizing: border-box; white-space: pre-wrap;">
<code>
internal class AppDbContext : DbContext
{
    public DbSet&lt;Client&gt; Clients { get; set; }
    public DbSet&lt;Product&gt; Products { get; set; }
    public DbSet&lt;Order&gt; Orders { get; set; }
    public DbSet&lt;Item&gt; Items { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TemplateEF;Trusted_Connection=True;");
    }
}
</code>
                </pre>
            </td>
            <td style="border: 1px solid #ddd; padding: 10px; vertical-align: top; width: 50%;">
                <pre style="width: 100%; height: 300px; padding: 5px; border-radius: 5px; overflow-x: auto; margin: 0; box-sizing: border-box; white-space: pre-wrap;">
<code>
internal class AppDbContext : DbContext
{   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your-String-Connection-To-SqlServer-Here");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
</code>
                </pre>
            </td>
        </tr>
    </tbody>
</table>

<h3>Configuração do Modelo de Dados</h3>

A configuração dos modelos de dados pode ser feita usando duas abordagens principais: Fluent API e Data Annotations.

<h4>Fluent API</h4>

Fluent API é uma abordagem que permite configurar os modelos de dados usando uma API fluente no método `OnModelCreating` do seu `DbContext`. 

Essa abordagem é especialmente útil para configurações mais complexas e detalhadas.

```c#
internal class AppDbContext : DbContext
{   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
    }
}

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.StartDate).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(e => e.Status).HasConversion<string>();
        builder.Property(e => e.Observation).HasColumnType("VARCHAR(512)");

        builder.HasMany(e => e.Items).WithOne(e => e.Order).OnDelete(DeleteBehavior.Cascade);
    }
}
```

<h4>Data Annotations</h4>

Data Annotations são atributos que você pode adicionar diretamente às propriedades e classes do modelo para configurar o mapeamento de dados.

Ideal para configurações que podem ser facilmente expressas através de atributos.

```c#
internal class Order
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR(512)")]
    public string Observation { get; set; }

    [DefaultValueSql("GETDATE()")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime StartDate { get; set; }

    [NotMapped]
    public DateTime EndDate { get; set; }

    [Column(TypeName = "VARCHAR(20)")]
    public string Status { get; set; }

    public ICollection<Item> Items { get; set; }
}
```

<h3>Migrações</h3>

Migrações são uma ferramenta que ajudam a gerenciar mudanças no esquema do banco de dados ao longo do tempo.

Migrações são essencialmente um histórico de alterações do esquema do banco de dados.

|         Tipo         | Comandos                                                        |        Console         | Descrição                                                                                                           |
| :------------------: | :-------------------------------------------------------------- | :--------------------: | :------------------------------------------------------------------------------------------------------------------ |
|       Criação        | Add-Migration                                                   | Gerenciador de Pacotes | Esses comandos geram um arquivo de migração que contém o código necessário para aplicar a mudança no banco de dados |
|                      | dotnet ef migrations add                                        |      CLI do .NET       | Esses comandos geram um arquivo de migração que contém o código necessário para aplicar a mudança no banco de dados |
|      Aplicação       | Update-Database                                                 | Gerenciador de Pacotes | Esses comandos aplicam todas as migrações pendentes ao banco de dados                                               |
|                      | dotnet ef database update                                       |      CLI do .NET       | Esses comandos aplicam todas as migrações pendentes ao banco de dados                                               |
|       Rollback       | dotnet ef migrations remove                                     |      CLI do .NET       | Este comando reverte a última migração aplicada, retornando o banco de dados ao estado anterior                     |
|                      | dotnet ef database update <NomeDaMigracaoAnterior>              |      CLI do .NET       | Se você precisar reverter para uma migração específica                                                              |
|  Geração de Scripts  | dotnet ef migrations script -o MigrationScript.sql              |      CLI do .NET       | Esse comando gera um script SQL a partir de migrações para aplicar manualmente no banco de dados                    |
| Scripts Idempotentes | dotnet ef migrations script --idempotent -o MigrationScript.sql |      CLI do .NET       | Esse comando gera um script que garante que todas as alterações necessárias sejam executadas apenas uma vez         |

# ASP.NET

ASP.NET é uma extensão do .NET e é utilizado para construir aplicativos web dinâmicos e serviços web.

Ele fornece um modelo de programação que permite a criação de páginas web dinâmicas, APIs RESTful e outros tipos de serviços web.

<h2>Como o ASP.NET está organizado?</h2>

A arquitetura do ASP.NET está dividida em dois blocos principais: Razor e Services.

<h3>Razor (Sites)</h3>

O Razor é uma parte crucial do ASP.NET para a criação de páginas web dinâmicas.

Faz a transpilação do código p/ HTML, CSS e JS

|    Partes     | Descrição                                                           |
| :-----------: | :------------------------------------------------------------------ |
|      MVC      | Entrega de um site na arquitetura MVC                               |
|  Razor Pages  | Abordagem simplificada para criação de páginas web                  |
| Razor Library | Permite a criação de bibliotecas de componentes Razor reutilizáveis |
|    Blazor     | Entrega um SPA (Single Page Application)                            |

<h3>Services (API)</h3>

Os serviços no ASP.NET fornecem a funcionalidade de backend necessária para suportar aplicações web e móveis.

| Partes  | Descrição                                                                        |
| :-----: | :------------------------------------------------------------------------------- |
| Web API | Criação de APIs que seguem os princípios REST                                    |
| SignalR | Adiciona funcionalidades para comunicação em tempo real entre servidor e cliente |
|  gRPC   | Comunicação eficiente entre serviços usando o protocolo gRPC                     |

## Hosting

O hosting no ASP.NET refere-se ao ambiente e ao processo de execução das aplicações ASP.NET.

Esse ambiente pode variar dependendo de como e onde a aplicação é implantada.

<h3>In Process Hosting</h3>

<img src="https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/index/_static/ancm-inprocess.png?view=aspnetcore-8.0" alt="imagem" width="650px" height="100px"/>

<h3>Out Process Hosting</h3>

<img src="https://learn.microsoft.com/pt-br/aspnet/core/host-and-deploy/iis/index/_static/ancm-outofprocess.png?view=aspnetcore-8.0" alt="imagem" width="650px" height="100px"/>

|    Servidor    | Descrição                                                                                                                                                        |
| :------------: | :--------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|    Kestrel     | É um servidor web multiplataforma embutido no ASP.NET Core. Projetado para ser leve e eficiente, o Kestrel é adequado para ambientes de desenvolvimento e testes |
|      IIS       | É um servidor web da Microsoft que pode hospedar aplicações ASP.NET. O IIS fornece recursos avançados, como autenticação, caching e balanceamento de carga       |
| Nginx e Apache | Em ambientes Linux, servidores web como Nginx e Apache podem atuar como proxies reversos que direcionam o tráfego para o Kestrel                                 |

<details>
<summary>Clique aqui para saber mais sobre o Kestrel.</summary>
<p>

<h2>O que é Kestrel?</h2>

Kestrel é um servidor web multiplataforma e de alto desempenho embutido no .NET Core, usado principalmente com ASP.NET Core.

É ideal para desenvolvimento local e testes devido à sua configuração simples via código.

Ele processa solicitações através de um pipeline de middleware definido na aplicação.

O pipeline pode incluir middleware para autenticação, roteamento, manipulação de erros e muito mais.

<h3>Funcionamento em Ambientes de Produção</h3>

Para ambientes de produção, é recomendável usar Kestrel atrás de um proxy reverso, para adicionar camadas adicionais de segurança e funcionalidade.

Quando o servidor recebe uma solicitação do cliente, ela é enviada através de um pipeline de middleware para o Kestrel.

Após o processamento, a solicitação é roteada para o controlador ou página apropriada, que executa a lógica necessária e gera a resposta.

Esta resposta é então enviada de volta ao cliente através do Kestrel.

</p>
</details>

## Pipeline

O pipeline do ASP.NET refere-se ao conjunto de middlewares que processam as solicitações.

É uma cadeia de componentes que processam a solicitação antes de chegar ao controlador.

Cada middleware é responsável por uma parte específica do processamento da solicitação, desde o roteamento até a resposta.

<img src="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index/_static/middleware-pipeline.svg?view=aspnetcore-8.0" alt="imagem" width="650px" height="400px"/>

## Middleware

Middleware é um componente de software que processa solicitações em uma aplicação web ASP.NET Core.

Cada solicitação passa por uma série de middlewares antes de alcançar o endpoint final (como um controlador ou uma página Razor) que gera a resposta.

Após o endpoint processar a solicitação, a resposta passa de volta pelos middlewares na ordem inversa, permitindo que eles modifiquem a resposta antes que ela seja enviada ao cliente.

Muitos middlewares são fornecidos por bibliotecas e pacotes externos que você adiciona ao seu projeto.

> :bulb: Confira um template padrão de middleware no caminho /Templates/Middleware

<h3>Como funciona?</h3>

O pipeline de middlewares é configurado no servidor, no arquivo `Program.cs`, antes que qualquer solicitação seja processada.

A ordem dos middlewares no pipeline é definida pelo desenvolvedor durante essa configuração.

```c#
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware Pipeline
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.Run();
```

> Obs: Para entender na prática, abra e rode o projeto na pasta "\Templates\Middleware"

Cada middleware depende da sequência em que é executado, ou seja, a ordem dos middlewares no pipeline importa.

Cada middleware tem a capacidade de:

- Executar alguma lógica na solicitação (autenticação, roteamento ou logging).
- Invocar o próximo middleware no pipeline.
- Executar alguma lógica na resposta (adicionar cabeçalhos ou modificar o conteúdo da resposta).

<h3>Porque importa?</h3>

Permitem uma arquitetura modular onde diferentes responsabilidades são encapsuladas em componentes separados.

Podem ser reutilizados em diferentes aplicações.

Oferecem uma maneira flexível de adicionar ou remover funcionalidades da aplicação.

## Injeção de Dependência

A injeção de dependência é um padrão de projeto que ajuda a reduzir o acoplamento de código em sua aplicação.

No contexto do ASP.NET Core, a injeção de dependência é usada para adicionar serviços e gerenciar a vida útil dos objetos.

<h3>Como funciona?</h3>

O ASP.NET Core possui um contêiner de injeção de dependência integrado, o `IServiceProvide`, onde as dependências são registradas.

Este contêiner é responsável por construir os objetos e fornecê-los quando necessário.

Quando uma instância de um objeto é solicitada, o ASP.NET cuida da sua criação e fornecimento, ocultando a complexidade de sua instância.

<h3>Configuração</h3>

Para que um serviço seja injetado no código é necessário registrá-lo ao contêiner.

Usa uma interface ou classe base para abstrair a implementação da dependência.

```c#
// Usa a interface para definir o tipo do serviço a ser injetado, mas a instância real é criada a partir da classe concreta
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
```

Para uma controller fazer o uso do serviço registrado é de costume injetar este serviço via construtor.

A estrutura assume a responsabilidade de criar uma instância da dependência e de descartá-la quando não for mais necessária.

```c#
public class MyButtonTagHelper : TagHelper
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly LinkGenerator _linkGenerator;

    // IServiceProvider creates and provides the necessary instances for the constructor, which are assigned to variables
    public MyButtonTagHelper(IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator)
    {
        _contextAccessor = contextAccessor;
        _linkGenerator = linkGenerator;
    }
}
```

Garante que todas as dependências sejam fornecidas no momento da criação do objeto.

Quando você injeta dependências pelo construtor, o contêiner controla a criação e a vida útil desses objetos (transiente, scoped ou singleton).

Se não for possível usar o construtor para injeção de dependência, você pode injetar a dependência diretamente na ação.

> :warning: A instância da dependência só poderá ser usada dentro do escopo da ação.

```c#
public IActionResult About([FromServices] IDateTime dateTime)
{
    return Content( $"Current server time: {dateTime.Now}");
}
```

Quando você injeta dependências diretamente em uma ação, a instância da dependência pode não ser gerenciada da mesma forma que quando injetada pelo construtor.

<h3>Ciclos de vida</h3>

No ASP.NET Core, o ciclo de vida dos serviços define a duração e o escopo de uma instância do serviço durante a execução da aplicação. Existem três principais ciclos de vida para serviços:

|   Ciclo    | Descrição                                                                                          | Uso                                                                                                                                       | Registro                                    |
| :--------: | :------------------------------------------------------------------------------------------------- | :---------------------------------------------------------------------------------------------------------------------------------------- | :------------------------------------------ |
| Transiente | Os serviços são criados cada vez que são solicitados                    | Ideal para serviços leves e sem estado, que não precisam compartilhar dados entre as solicitações                                         | AddTransient<IService, Service>(); |
|   Scoped   | Os serviços são criados uma vez por solicitação                            | Ideal para serviços que precisam compartilhar dados durante uma solicitação, mas não devem manter estado entre diferentes solicitações | AddScoped<IService, Service>();    |
| Singleton  | Os serviços são criados uma única vez durante a vida útil da aplicação. | Ideal para serviços que mantêm estado global ou são pesados para criar e configurar                                                        | AddSingleton<IService, Service>(); |

<h3>Serviços com chave</h3>

No ASP.NET Core, você pode registrar serviços no contêiner de injeção de dependência com uma chave específica para diferenciar as implementações.

Um serviço é associado a uma chave, utilizando **AddKeyedSingleton**, **AddKeyedScoped** ou **AddKeyedTransient** para registrá-la.

Acesse um serviço registrado especificando a chave com o atributo **[FromKeyedServices]**.

```c#
builder.Services.AddKeyedSingleton<ICache, BigCache>("big");
builder.Services.AddKeyedSingleton<ICache, SmallCache>("small");

[Route("/cache")]
public class CustomServicesApiController : Controller
{
    [HttpGet("big-cache")]
    public ActionResult<object> GetOk([FromKeyedServices("big")] ICache cache)
    {
        return cache.Get("data-mvc");
    }
}

public class MyHub : Hub
{
    public void Method([FromKeyedServices("small")] ICache cache)
    {
        Console.WriteLine(cache.Get("signalr"));
    }
}
```

É uma técnica poderosa para resolver diferentes implementações de serviços com base em condições específicas.

## Identity

O ASP.NET Identity é uma biblioteca de autenticação e autorização usada para gerenciar usuários e suas permissões em aplicações web ASP.NET.

fornece uma infraestrutura robusta e extensível para lidar com autenticação (verificação de identidade) e autorização (controle de acesso) de usuários.

|  Componentes  | Descrição                                                                                                                                   |
| :-----------: | :------------------------------------------------------------------------------------------------------------------------------------------ |
|     User      | O ASP.NET Identity fornece uma classe IdentityUser que pode ser estendida para incluir propriedades adicionais específicas da sua aplicação |
|     Role      | Representa um papel ou grupo ao qual um usuário pode pertencer. Ajuda a gerenciar permissões de acesso com base no papel do usuário         |
| SignInManager | Gerencia o processo de login, logout e autenticação de usuários. É responsável por verificar credenciais e manter o estado de autenticação  |
|  UserManager  | Gerencia a criação, atualização e exclusão de usuários. Também fornece métodos para buscar e manipular informações de usuários              |
|  RoleManager  | Gerencia a criação, atualização e exclusão de roles (papéis). Também pode ser usado para atribuir e remover papéis dos usuários             |

Permite realizer o gerenciamento de usuários e papéis, além de gerenciar propriedades (nome, e-mail, senha) e associar usuários a esses papéis para controlar permissões.

O ASP.NET Identity oferece suporte para autenticação de dois fatores (2FA), autenticação externa (por exemplo, login com Google ou Facebook) e recuperação de senha.

Você pode estender IdentityUser e IdentityRole para adicionar propriedades personalizadas.

<h3>Getting Started</h3>

Defina uma classe que herda de **IdentityDbContext** e configure seu contexto de banco de dados.

```c#
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
```

Configure o ASP.NET Identity no Program.cs.

```c#
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("your-connection-string"));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
```

<h3>IdentityDbContext</h3>

É uma especialização de DbContext fornecida pelo ASP.NET Identity.

Ele fornece uma configuração pré-definida para as tabelas necessárias para a autenticação e autorização, como tabelas para usuários, papéis, e tokens de recuperação de senha.

|      Tabela      | Descrição                                                                                               |
| :--------------: | :------------------------------------------------------------------------------------------------------ |
|   AspNetUsers    | Armazena informações sobre os usuários. Cada entrada representa um usuário na aplicação                 |
|   AspNetRoles    | Armazena informações sobre os papéis (roles) definidos na aplicação. Cada entrada representa um papel   |
| AspNetUserLogins | Armazena informações de logins externos                                                                 |
| AspNetUserRoles  | Relaciona usuários aos papéis que eles possuem. Cada entrada mapeia um usuário para um papel específico |
| AspNetUserClaims | Armazena declarações (claims) associadas a usuários                                                     |
| AspNetRoleClaims | Armazena declarações associadas a papéis                                                                |

Simplifica a configuração e a integração do ASP.NET Identity com Entity Framework Core, ainda permitindo configurações do modelo de dados usando EF Core.

## NuGet

NuGet é o gerenciador de pacotes oficial para a plataforma .NET.

Ele simplifica o processo de adicionar, atualizar e remover bibliotecas e ferramentas.

Permite aos desenvolvedores compartilhar código de maneira eficiente e gerenciar as dependências do projeto.

<h3>Pacotes NuGet</h3>

Um pacote NuGet é uma coleção de arquivos que são compilados e empacotados em um único arquivo com extensão `.nupkg`.

Cada pacote contém metadados que descrevem seu conteúdo, dependências e outros detalhes importantes.

A instalação de pacotes pode ser feita através do Visual Studio, do .NET CLI ou diretamente do NuGet Package Manager.

## Logging e LogLevel

ASP.NET Core fornece uma infraestrutura de logging integrada que permite registrar informações em diferentes níveis de detalhe.

Pode ser configurado no arquivo `appsettings.json` ou via código no método ConfigureServices.

|  Log Level  | Descrição                                          |
| :---------: | :------------------------------------------------- |
|    Trace    | Informação mais detalhada e volumosa               |
|    Debug    | Informação de depuração, menos detalhada que Trace |
| Information | Informação geral sobre o fluxo da aplicação        |
|   Warning   | Potenciais problemas ou situações inusitadas       |
|    Error    | Erros que afetam o funcionamento da aplicação      |
|  Critical   | Falhas graves que necessitam de atenção imediata   |

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}

```
# MVC

O padrão MVC (Model-View-Controller) é um padrão de arquitetura de software que separa uma aplicação em três componentes principais: Model, View e Controller.

> :bulb: Confira o template ASP.NET MVC que implementa todos os conceitos abaixo no caminho /Templates/MVC

## Getting Started

Em seu terminal crie uma pasta para a criação do projetos e navegue até esta pasta. Execute o comando a seguir:

```txt
dotnet new sln -n <your-application-name>
```

Agora crie uma pasta chamada "src" e navegue até ela. Execute o comando a seguir:

```txt
dotnet new mvc -n <your-application-name> --auth Individual
```

Navegue até a pasta AppMvcFuncional e execute o comando para associação da Solution com o projeto:

```txt
dotnet sln ../../<your-application-name>.sln add <your-application-name>.csproj
```

## Conceitos

### Bundling e Minification

Bundling e minification são técnicas usadas para otimizar o desempenho das aplicações web.

Reduzem o tempo de carregamento das páginas e melhoram a eficiência geral.

`Bundling` é o processo de combinar vários arquivos (como arquivos CSS e JavaScript) em um único arquivo.

`Minification` é o processo de remover todos os caracteres desnecessários dos arquivos de código sem alterar a funcionalidade do código.

Para obter mais informações sobre este assunto, [clique aqui](https://github.com/JoaoLucasAssis/.NET/tree/main/Concepts/Development/Bundling%20%26%20Minification) para ler um resumo detalhado do conceito.

### Slugify

Slugify é um processo de conversão de uma string em um formato compatível com URL.

O objetivo de formatar uma string é torná-la mais legível para humanos e mecanismos de pesquisa, além de garantir que ela contenha apenas caracteres seguros para URL.

Melhora a indexação em motores de busca, criando URLs que sejam mais compreensíveis e relevantes, impactando positivamente a classificação SEO do site.

Para obter mais informações sobre este assunto, [clique aqui](https://github.com/JoaoLucasAssis/.NET/tree/main/Concepts/Utilities/Slugify) para ler um resumo detalhado do conceito.

### Areas

No ASP.NET, as áreas são um recurso usado para organizar funcionalidades relacionadas em grupos distintos.

As áreas ajudam a isolar diferentes partes da aplicação, como controladores e views, em módulos independentes.

Uma área divide a aplicação em módulos distintos. Cada módulo é representado por uma pasta e pode conter seus próprios controladores, modelos e views.

Para obter mais informações sobre este assunto, [clique aqui](https://github.com/JoaoLucasAssis/.NET/tree/main/Concepts/Development/Areas) para ler um resumo detalhado do conceito.

## Model

É a representação dos dados do mundo real que pode incluir validações de estado e regras de negócio.

### DTO

Um Data Transfer Object (DTO) é um padrão de design usado para transferir dados entre diferentes camadas de uma aplicação.

São usados para transportar dados entre a camada de apresentação (Views) e a camada de negócios (Controllers e Models) ou entre serviços web e clientes.

Ao limitar a quantidade de dados transferidos, eles ajudam a reduzir a sobrecarga de rede e melhorar o desempenho.

DTOs podem simplificar a estrutura de dados exposta às Views, oferecendo apenas as informações necessárias.

### Data Annotations

Data Annotations são atributos que você pode aplicar às classes e propriedades do modelo para definir regras de validação e comportamento de formatação.

Fornecem uma maneira simples de adicionar validação ao modelo, que é automaticamente respeitada pelas Views e pelo framework MVC.

```c#
internal class Order
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O {0} é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome {0} deve ter entre {2} e {1} caracteres.")]
    public string Observation { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data do Pedido")]
    public DateTime StartDate { get; set; }

    [Display(Name = "Data Final do Pedido")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "O {0} é obrigatório.")]
    public string Status { get; set; }

    public ICollection<Item> Items { get; set; }
}
```

## View

São as páginas do site, responsáveis pela navegação, design, UX.

### Razor 

`Razor Views` são arquivos HTML mesclados com recursos do Razor.

Transforma as views em arquivos HTML puros para a interpretação do browser.

`Razor Syntax` é a linguagem usada em Razor Views, que permite a combinação de C# e HTML.

Tem uma sintaxe minimalista que facilita a leitura e escrita de código.

Ele usa '@' para transitar entre HTML e C#.

```html
<!-- Specifies the model that will be used to the view. -->
@model MyApp.Models.Product

<!-- Access the properties and methods of the model specified by @model -->
<h2>@Model.Name</h2>
<p>Price: @Model.Price.ToString("C")</p>
```

### TagHelpers

TagHelpers são uma funcionalidade que permite a criação de tags HTML personalizadas com funcionalidades adicionais.

Maneira mais rica e intuitiva de integrar lógica de servidor diretamente no HTML.

```html
<form asp-controller="Product" asp-action="Edit">
    <div class="form-group mb-3">
        <label asp-for="Name"></label>
        <input asp-for="Name" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>
    
    <div class="form-group mb-3">
        <label asp-for="Description"></label>
        <input asp-for="Description" />
        <span asp-validation-for="Description" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="ProductType" class="control-label"></label>
        <select asp-for="ProductType" class="form-control" asp-items="ViewBag.Types"></select>
        <span asp-validation-for="ProductType" class="text-danger"></span>
    </div>

    <button type="submit">Login</button>
</form>
```

|     TagHelper      | Descrição                                                                                         |
| :----------------: | :------------------------------------------------------------------------------------------------ |
|   asp-controller   | É usado para gerar URLs ou definir ações em formulários que apontam para um controller específico |
|     asp-action     | É usado com asp-controller para especificar o método exato dentro do controller                   |
|      asp-for       | É usado em formulários para vincular campos de entrada às propriedades do modelo                  |
| asp-validation-for | É usado para exibir mensagens de validação para um campo específico em um formulário              |
|     asp-items      | É usado para criar uma lista suspensa (select) em um formulário, preenchendo-a com dados          |

<h4>TagHelpers Customizados</h4>

Permitem criar componentes HTML personalizados e reutilizáveis em diferentes partes da aplicação.

Encapsulam a lógica específica de renderização, mantendo as views Razor limpas e focadas na estrutura do layout.

Para criar um TagHelper customizado, crie uma pasta na raiz do seu projeto chamada **Extensions**.

```txt
/Extensions
    /CustomTagHelpers
        MyCustomTagHelper.cs
```

```c#
[HtmlTargetElement("my-button", Attributes = "btn-type, route-id")] // Defines the target elements and attributes that the TagHelper will handle.
public class MyButtonTagHelper : TagHelper
{
    private readonly IHttpContextAccessor _contextAccessor; // Dependency injection for IHttpContextAccessor

    [HtmlAttributeName("btn-type")] // Specifies the HTML attribute that is mapped to a property in the TagHelper class
    public ButtonType ButtonTypeSelected { get; set; }

    [HtmlAttributeName("route-id")]
    public int RouteId { get; set; }

    public MyButtonTagHelper(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor; // Initialize IHttpContextAccessor
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        switch (ButtonTypeSelected)
        {
            // logic
        }

        // Get the current controller name from HttpContext and route data
        var controller = _contextAccessor.HttpContext?.GetRouteData().Values["controller"]?.ToString();

        output.TagName = "a";
        output.Attributes.SetAttribute("href", $"{controller}/{ActionName}/{RouteId}");
    }
}
```

No arquivo **_ViewImports.cshtml**, registre o namespace onde seus tagHelpers customizados estão localizados.

```c#
@addTagHelper "*, your-namespace"
```

Para utilizar os tagHelpers customizados, crie uma tag com o nome definido no tagHelper e informe os valores dos atributos:

```html
<my-button btn-type="Details" route-id="@item.Id"></my-button>
```

### Partial Views

Partial Views são subcomponentes das views principais.

Dependem do modelo implementado na view principal.

São muito utilizadas para renderizar parte de uma view através de requisições AJAX.

De acordo com a convenção de nomenclatura em ASP.NET MVC, as partial views geralmente devem começar com um sublinhado '_'.

```html
<!-- Uses a taghelper to call a partial view within the _Layout.cshtml -->
<partial name="_NavBar" />
```

<h4>_ViewStart</h4>

`_ViewStart.cshtml` é um arquivo no ASP.NET MVC que define o layout padrão para todas as views dentro de uma pasta e suas subpastas.

o arquivo está localizado na raiz do diretório na pasta /Views.

```c#
@{
    // Defines that all views use the _Layout.cshtml layout
    Layout = "_Layout";
}
```

<h4>_ViewImports</h4>

`_ViewImports.cshtml` é um arquivo no ASP.NET MVC que é utilizado para importar namespaces e configurar diretivas comuns que todas as views devem usar.

```c#
@using MyMvcApp.Models
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

<h4>_Layout</h4>

`_Layout.cshtml` é o arquivo que define o layout principal da aplicação.

Define a estrutura geral da interface do usuário que será compartilhada por várias views.

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>@ViewData["Title"] - My ASP.NET MVC Application</title>
    <link rel="stylesheet" href="~/css/site.css" />
</head>
<body>
    <header>
        <partial name="_NavBar" />
    </header>
    <main role="main">
        <!-- The specific content of each view will be rendered here -->
        @RenderBody()
    </main>
    <footer>
        @await Component.InvokeAsync("Alert", new { message = "Policity Privacy © 2024" })
    </footer>
</body>
</html>
```

### View Components

View Components são componentes independentes das views.

Podem realizar ações como obter dados de uma tabela e exibir valores manipulados.

É uma excelente funcionalidade para componentizar recursos a página.

Todos os View Components devem ficar localizados na pasta **/ViewComponent**.

> :warning: Para usar View Component deve-se adicionar a linha:
>
> @addTagHelper "*, your-namespace"
> 
> :warning: A linha deve ser adicionada no arquivo _ViewImports.cshtml

```c#
public class PaginationViewComponent : ViewComponent
{   
    // All components must implement the ViewComponent class and have the InvokeAsync action
    public IViewComponentResult InvokeAsync(int totalItems, int currentPage, int itemsPerPage)
    {
        int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

        // Will return the view located in the path /Views/Shared/Components/Alert/Default.cshtml
        return View(new
        {
            CurrentPage = currentPage,
            TotalPages = totalPages
        });
    }
}
```

O arquivo `Default.cshtml`:

```html
<nav aria-label="Page navigation">
    <ul class="pagination">
        <!-- Previous Page Button -->
        <li class="page-item @(Model.CurrentPage == 1 ? "disabled" : "")">
            <a class="page-link" href="#" aria-label="Previous">
                <span aria-hidden="true">Previous</span>
            </a>
        </li>
        <!-- Page Number Buttons -->
        @for (int i = 1; i <= Model.TotalPages; i++)
        {
            <li class="page-item @(Model.CurrentPage == i ? "active" : "")">
                <a class="page-link" href="#">@i</a>
            </li>
        }
        <!-- Next Page Button -->
        <li class="page-item @(Model.CurrentPage == Model.TotalPages ? "disabled" : "")">
            <a class="page-link" href="#" aria-label="Next">
                <span aria-hidden="true">Next</span>
            </a>
        </li>
    </ul>
</nav>
```

A View Component em uma Razor View:

```html
<vc:pagination total-items="@Model.Count()" current-page="1" items-per-page="4"></vc:pagination>
```

### Estados

No contexto do ASP.NET MVC, "estados" referem-se às formas de armazenar e passar dados entre o controller e a view.

Permitem armazenar e transferir informações temporárias durante o ciclo de vida de uma solicitação HTTP.

| Estados  | Descrição                                                                                      |
| :------: | :--------------------------------------------------------------------------------------------- |
| ViewBag  | Usado para passar dados do controller para a view durante o processamento da solicitação atual |
| ViewData | Similar ao ViewBag, utilizado para passar dados do controller para a view                      |
| TempData | Usado para armazenar dados que precisam ser persistidos entre solicitações                     |

No Controller:

```c#
ViewData["Message"] = "Hello, World!";
```

Na View:

```html
<p>@ViewBag.Message</p>
```

## Controller

Intermediária entre a Model e a View. Invoca o método correto que irá processar e retornar os dados, para serem enviados para View.

No ASP.NET MVC, uma das principais convenções que facilita o desenvolvimento é a associação automática entre o nome dos métodos de ação nos controllers e as views correspondentes.

As views são organizadas em pastas que correspondem aos nomes dos controllers. Para o ProductsController, as views estariam na pasta Views/Products.

```c#
public class ProductsController : Controller
{
    // Returns a ViewResult action
    public IActionResult Index()
    {
        // Returns the page Views/Products/Index.cshtml
        return View();
    }
}
```

O framework procura uma view cujo nome corresponda ao nome do método de ação, permitindo que os desenvolvedores evitem a necessidade de especificar explicitamente o nome da view a ser retornada.

### Action Results

Os Action Results são componentes que determinam como uma resposta é retornada ao cliente após a execução de um método de ação em um controller.

Um Action Result é um objeto que implementa a interface IActionResult.

Permitem aos desenvolvedores controlar a saída de um método de ação de forma flexível e poderosa.

|     Action Result      | Descrição                                                    | Exemplo                                                  |
| :--------------------: | :----------------------------------------------------------- | :------------------------------------------------------- |
|       ViewResult       | Renderiza uma view HTML para o cliente                       | return View();                                           |
|       JsonResult       | Retorna dados JSON, ideal para APIs RESTful                  | return Json(data);                                       |
|     RedirectResult     | Redireciona o cliente para outra URL                         | return Redirect("http://example.com");                   |
| RedirectToActionResult | Redireciona para outra ação do controlador                   | return RedirectToAction("ActionName", "ControllerName"); |
|     ContentResult      | Retorna texto simples como resposta HTTP                     | return Content("Hello World!");                          |
|       FileResult       | Retorna um arquivo para download ou visualização             | return File("path/to/file", "mime/type");                |
|      EmptyResult       | Retorna uma resposta HTTP vazia, sem conteúdo                | return new EmptyResult();                                |
|    StatusCodeResult    | Retorna um código de status HTTP específico, como 404 ou 500 | return StatusCode(404);                                  |

### Roteamento

O roteamento no ASP.NET MVC funciona como um mapeamento entre a URL solicitada e o código que a processa, que geralmente é um controller.

As rotas são configuradas na aplicação normalmente no arquivo `Program.cs`.

```c#
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    // Neste exemplo, a rota padrão especifica que a aplicação deverá 
    // procurar um controlador chamado Home e uma ação chamada Index
};
```

<h4>Atributos</h4>

Os atributos de rota permitem definir regras de roteamento diretamente nos controladores e ações por meio de anotações.

Isso proporciona maior controle e flexibilidade ao roteamento, permitindo especificar rotas diretamente na definição de uma classe ou método.

```c#
[Route("/"), Order = 0] // Defines the default route for ProductsController, which automatically calls the Index() method
[Route("products"), Order = 1] // Defines that all actions in the ProductsController will be under the /products base URL
[Authorize]
public class ProductsController : Controller
{
    [HttpGet("list")]
    [AllowAnonymous]
    public IActionResult List()
    {
        return View();
    }

    [HttpPost("details/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Details(int id)
    {
	// logic
    }
}
```

<table style="width: 100%; background-color: transparent;">
    <thead>
        <tr style="background-color: transparent;">
            <th style="padding: 10px; width: 20px; text-align: center;">Atributo</th>
            <th style="padding: 10px; width: 570px;">Descrição</th>
        </tr>
    </thead>
    <tbody>
        <!-- Roteamento -->
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #00796b; text-align: center;">Route</td>
            <td style="padding: 10px;">Define uma rota para um método de ação.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #00796b; text-align: center;">RoutePrefix</td>
            <td style="padding: 10px;">Define um prefixo de rota comum para todas as ações em um controlador.</td>
        </tr>
        <!-- Métodos HTTP -->
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ab47bc; text-align: center;">HttpGet</td>
            <td style="padding: 10px;">Especifica que o método de ação responde a requisições HTTP GET.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ab47bc; text-align: center;">HttpPost</td>
            <td style="padding: 10px;">Especifica que o método de ação responde a requisições HTTP POST.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ab47bc; text-align: center;">HttpPut</td>
            <td style="padding: 10px;">Especifica que o método de ação responde a requisições HTTP PUT.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ab47bc; text-align: center;">HttpDelete</td>
            <td style="padding: 10px;">Especifica que o método de ação responde a requisições HTTP DELETE.</td>
        </tr>
        <!-- Autorização e Segurança -->
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #66bb6a; text-align: center;">Authorize</td>
            <td style="padding: 10px;">Restringe o acesso ao método de ação ou controlador a usuários autorizados.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #66bb6a; text-align: center;">AllowAnonymous</td>
            <td style="padding: 10px;">Permite acesso anônimo (não autenticado) ao método de ação ou controlador.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #66bb6a; text-align: center;">ValidateAntiForgeryToken</td>
            <td style="padding: 10px;">Valida o token anti-forgery para prevenir ataques CSRF.</td>
        </tr>
        <!-- Vinculação e Validação -->
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff7043; text-align: center;">FromBody</td>
            <td style="padding: 10px;">Faz a vinculação do parâmetro com o corpo da requisição.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff7043; text-align: center;">FromRoute</td>
            <td style="padding: 10px;">Faz a vinculação do parâmetro com os dados da rota.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff7043; text-align: center;">FromHeader</td>
            <td style="padding: 10px;">Faz a vinculação do parâmetro com o cabeçalho da requisição.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff7043; text-align: center;">FromForm</td>
            <td style="padding: 10px;">Faz a vinculação do parâmetro com os dados do formulário.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff7043; text-align: center;">NonAction</td>
            <td style="padding: 10px;">Marca um método que não deve ser tratado como um método de ação.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff7043; text-align: center;">Bind</td>
            <td style="padding: 10px;">Especifica quais propriedades devem ser vinculadas a partir da requisição.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff7043; text-align: center;">DefaultValue</td>
            <td style="padding: 10px;">Define um valor padrão para um parâmetro.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff7043; text-align: center;">AllowEmpty</td>
            <td style="padding: 10px;">Permite um valor vazio para o parâmetro.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff7043; text-align: center;">ValidateModel</td>
            <td style="padding: 10px;">Valida o estado do modelo antes de executar o método de ação.</td>
        </tr>
        <!-- Resposta e Consumo -->
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #64b5f6; text-align: center;">Produces</td>
            <td style="padding: 10px;">Especifica os tipos de resposta produzidos pelo método de ação.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #64b5f6; text-align: center;">Consumes</td>
            <td style="padding: 10px;">Especifica os tipos de requisições consumidos pelo método de ação.</td>
        </tr>
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #64b5f6; text-align: center;">ProducesResponseType</td>
            <td style="padding: 10px;">Especifica o tipo de resposta retornada pelo método de ação.</td>
        </tr>
        <!-- Filtros -->
        <tr style="background-color: transparent;">
            <td style="padding: 10px; color: #ff5555; text-align: center;">CustomFilter</td>
            <td style="padding: 10px;">Aplica um filtro personalizado que implementa a interface `IFilter`.</td>
        </tr>
    </tbody>
</table>

### Parâmetros

Passagem de parâmetros refere-se à capacidade de fornecer dados para um método de ação no controller a partir de uma solicitação HTTP.

Esses parâmetros podem ser extraídos de diferentes partes da solicitação, como a URL, parâmetros de consulta, corpo da solicitação, ou cabeçalhos.

<h4>Model Binding</h4>

O ASP.NET MVC usa um processo chamado model binding para mapear os valores das partes da solicitação para os parâmetros dos métodos de ação.

Extrai dados de várias fontes e os converte em tipos de dados apropriados para os parâmetros dos métodos do controlador.

Para parâmetros simples, o ASP.NET MVC automaticamente associa os valores passados na URL ou no corpo da solicitação aos parâmetros do método.

```c#
public ActionResult Details(int id)
{
    // 'id' is automatically filled with the URL value.
}
```

Quando você passa um objeto complexo como um parâmetro, o model binding associa os valores dos campos do formulário às propriedades do objeto.

```c#
public ActionResult Create(ICollection collection)
{
    // 'collection' is filled with the form data.
}
```

Atributos como `FromForm`, `FromQuery` e `FromBody` podem ser usados para especificar explicitamente a origem dos dados para parâmetros de métodos.

```c#
public ActionResult Create([FromForm] ICollection collection)
{
    // 'collection' is extracted from the form body.
}
```

O `Bind` é usado para especificar quais propriedades de um modelo devem ser incluídas ou excluídas.

```c#
public ActionResult Create([Bind("Name, Email")] ICollection collection)
{
    // 'collection' is extracted from the form body.
}
```

<h4>Múltiplos Parâmetros</h4>

Para passar múltiplos parâmetros, adicione-os à URL usando ? para o primeiro parâmetro e & para os subsequentes.

O nome dos parâmetros na URL deve corresponder exatamente aos nomes dos parâmetros do método de ação. 

Isso garante que o model binding funcione corretamente e os valores sejam atribuídos aos parâmetros certos.

```c#
public IActionResult Search(string query, int page, int index)
{
    // query, page and index are extracted from the URL
    // http://example.com/controller/search?query=ASP.NET&page=2&index=1
    // logic
}
```

### ModelState

ModelState é um dicionário que contém o estado de cada propriedade do modelo e quaisquer erros de validação associados.

Ele armazena o estado do modelo durante a validação e pode ser usado para verificar se os dados submetidos estão corretos e para exibir mensagens de erro.

```c#
[HttpPost("create-client")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Name,Phone,CEP,State,City")] Client client)
{
    if (ModelState.IsValid)
    {
        _context.Add(client);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    return View(client); // If ModelState is not valid, return the view with errors.
}
```
