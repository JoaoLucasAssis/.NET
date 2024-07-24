# Anotações sobre .NET

## Sumário

* [.NET](#net)
  * [Dupla Compilação](#dupla-compilacao)
  * [Garbage Collector](#garbage-collector)
  * [Entity Framework](#entity-framework)

* [ASP.NET](#aspnet)
  * [Hosting](#hosting)
  * [Pipeline](#pipeline)
  * [Middleware](#middleware)
  * [NuGet](#nuget)
  * [Logging](#logging-e-loglevel)

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

### IL (Intermediate Language)

IL é uma linguagem de baixo nível que não é específica de nenhum sistema operacional ou hardware. 

Isso significa que o código IL pode ser executado em qualquer ambiente que tenha uma implementação da Common Language Runtime (CLR).

### CLR (Common Language Runtime)

Quando um aplicativo .NET é executado, o código IL é compilado "Just-In-Time" (JIT) para o código de máquina nativo específico do sistema operacional e do hardware.

Esse processo é realizado pelo Just-In-Time Compiler (JIT Compiler), que faz parte da CLR.

## Garbage Collector

O Garbage Collector é um componente crucial do Common Language Runtime (CLR) que gerencia automaticamente a alocação e liberação de memória para aplicações .NET.

Ele ajuda a evitar problemas de memória, como vazamentos, ao identificar e liberar objetos que não são mais acessíveis no programa.

O .NET utiliza um heap gerenciado, dividido em três gerações para otimizar a coleta de lixo.

|Geração|Descrição|
|:---:|---|
|0|Onde novos objetos são alocados. Coletas são frequentes e rápidas|
|1|Área intermediária para objetos que sobrevivem à coleta na Geração 0|
|2|Para objetos de longa duração, onde as coletas são menos frequentes|

A coleta de Lixo envolve identificar objetos inacessíveis, compactar o heap para eliminar a fragmentação e liberar memória dos objetos mortos.

## Entity Framework

Entity Framework é um Object-Relational Mapping (ORM) framework para .NET.

Permite que desenvolvedores trabalhem com um banco de dados usando objetos .NET.

Elimina a necessidade de escrever grande parte do código SQL manualmente.

Permite escrever consultas utilizando LINQ (Language Integrated Query), proporcionando uma forma intuitiva e tipada de acessar dados.

### Modelagem de Dados

Permite definir um modelo de dados usando classes .NET.

Cada classe corresponde a uma tabela no banco de dados e cada propriedade da classe a uma coluna na tabela.

### Mecanismos de Modelagem

|Mecanismos|Descrição|
|:---:|:---|
|Code-First|O banco de dados é gerado através de classes C# ou VB.NET|
|Database-First|As classes são definidas a partir de um banco de dados existente|
|Model-First|O modelo de dados é criado usando o Entity Framework Designer|

### DbContext

DbContext é uma classe central no Entity Framework essencial para a comunicação entre a aplicação e o banco de dados.

Ele gerencia a conexão com o banco de dados e fornece uma abstração para o acesso aos dados.

Essa classe é responsável por algumas outras funções, sendo elas:

- Configurar o modelo de dados
- Consultar e persistir dados em seu banco
- Fazer toda a rastreabilidade do objeto
- Materializar resultados das consultas
- Cache de primeiro nível

|Métodos|Descrição|
|:---:|:---|
|OnConfiguring|Esse método é usado para configurar o banco de dados que o contexto usará|
|SaveChanges|Esse métodos é usado para persistir todas as alterações feitas no contexto para o banco de dados|

<details>
<summary>Clique aqui para entender na prática!</summary>

```c#
internal class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public OrderStatus Status { get; set; }
    public string Observation {  get; set; }
    public ICollection<Item> Items { get; set; }
}

internal class AppDbContext : DbContext
{   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your-String-Connection-To-SqlServer-Here");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // The line below automatically applies all configurations of all classes that 
        // implement IEntityTypeConfiguration<T> found in the specified assembly.

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
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
</details>

### Configuração do Modelo de Dados

A configuração dos modelos de dados pode ser feita usando duas abordagens principais: Fluent API e Data Annotations.

#### Fluent API

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

#### Data Annotations

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

### Migrações

Migrações são uma ferramenta que ajudam a gerenciar mudanças no esquema do banco de dados ao longo do tempo.

Migrações são essencialmente um histórico de alterações do esquema do banco de dados.

|Tipo|Comandos|Console|Descrição|
|:---:|:---|:---:|:---|
|Criação|Add-Migration|Gerenciador de Pacotes|Esses comandos geram um arquivo de migração que contém o código necessário para aplicar a mudança no banco de dados|
||dotnet ef migrations add|CLI do .NET|Esses comandos geram um arquivo de migração que contém o código necessário para aplicar a mudança no banco de dados|
|Aplicação|Update-Database|Gerenciador de Pacotes|Esses comandos aplicam todas as migrações pendentes ao banco de dados|
||dotnet ef database update|CLI do .NET|Esses comandos aplicam todas as migrações pendentes ao banco de dados|
|Rollback|dotnet ef migrations remove|CLI do .NET|Este comando reverte a última migração aplicada, retornando o banco de dados ao estado anterior|
||dotnet ef database update <NomeDaMigracaoAnterior>|CLI do .NET|Se você precisar reverter para uma migração específica|
|Geração de Scripts|dotnet ef migrations script -o MigrationScript.sql|CLI do .NET|Esse comando gera um script SQL a partir de migrações para aplicar manualmente no banco de dados|
|Scripts Idempotentes|dotnet ef migrations script --idempotent -o MigrationScript.sql|CLI do .NET|Esse comando gera um script que garante que todas as alterações necessárias sejam executadas apenas uma vez|

# ASP.NET

ASP.NET é uma extensão do .NET e é utilizado para construir aplicativos web dinâmicos e serviços web.

Ele fornece um modelo de programação que permite a criação de páginas web dinâmicas, APIs RESTful e outros tipos de serviços web.

## Como o ASP.NET está organizado?

A arquitetura do ASP.NET está dividida em dois blocos principais: Razor e Services.

### Razor (Sites)

O Razor é uma parte crucial do ASP.NET para a criação de páginas web dinâmicas.

Faz a transpilação do código p/ HTML, CSS e JS

|Partes|Descrição|
|:---:|:---|
|MVC|Entrega de um site na arquitetura MVC|
|Razor Pages|Abordagem simplificada para criação de páginas web|
|Razor Library|Permite a criação de bibliotecas de componentes Razor reutilizáveis|
|Blazor|Entrega um SPA (Single Page Application)|

### Services (API)

Os serviços no ASP.NET fornecem a funcionalidade de backend necessária para suportar aplicações web e móveis.

|Partes|Descrição|
|:---:|:---|
|Web API|Criação de APIs que seguem os princípios REST|
|SignalR|Adiciona funcionalidades para comunicação em tempo real entre servidor e cliente|
|gRPC|Comunicação eficiente entre serviços usando o protocolo gRPC|

### Identity

Identity é um serviço de identificação que atende qualquer aplicação ASP.NET.

Usado para autenticação de usuário, validação de JWT, entre outros.

## Hosting

O hosting no ASP.NET refere-se ao ambiente e ao processo de execução das aplicações ASP.NET.

Esse ambiente pode variar dependendo de como e onde a aplicação é implantada.

### In Process Hosting

<img src="https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/index/_static/ancm-inprocess.png?view=aspnetcore-8.0" alt="imagem" width="650px" height="100px"/>

### Out Process Hosting

<img src="https://learn.microsoft.com/pt-br/aspnet/core/host-and-deploy/iis/index/_static/ancm-outofprocess.png?view=aspnetcore-8.0" alt="imagem" width="650px" height="100px"/>

|Servidor|Descrição|
|:---:|:---|
|Kestrel|É um servidor web multiplataforma embutido no ASP.NET Core. Projetado para ser leve e eficiente, o Kestrel é adequado para ambientes de desenvolvimento e testes|
|IIS|É um servidor web da Microsoft que pode hospedar aplicações ASP.NET. O IIS fornece recursos avançados, como autenticação, caching e balanceamento de carga|
|Nginx e Apache|Em ambientes Linux, servidores web como Nginx e Apache podem atuar como proxies reversos que direcionam o tráfego para o Kestrel|

<details>
<summary>Clique aqui para saber mais sobre o Kestrel.</summary>
<p>

## O que é Kestrel?

Kestrel é um servidor web multiplataforma e de alto desempenho embutido no .NET Core, usado principalmente com ASP.NET Core.

É ideal para desenvolvimento local e testes devido à sua configuração simples via código.

Ele processa solicitações através de um pipeline de middleware definido na aplicação.

O pipeline pode incluir middleware para autenticação, roteamento, manipulação de erros e muito mais.

### Funcionamento em Ambientes de Produção

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

### Como funciona?

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

### Porque importa?

Permitem uma arquitetura modular onde diferentes responsabilidades são encapsuladas em componentes separados.

Podem ser reutilizados em diferentes aplicações.

Oferecem uma maneira flexível de adicionar ou remover funcionalidades da aplicação.

## NuGet

NuGet é o gerenciador de pacotes oficial para a plataforma .NET.

Ele simplifica o processo de adicionar, atualizar e remover bibliotecas e ferramentas.

Permite aos desenvolvedores compartilhar código de maneira eficiente e gerenciar as dependências do projeto.

### Pacotes NuGet

Um pacote NuGet é uma coleção de arquivos que são compilados e empacotados em um único arquivo com extensão `.nupkg`.

Cada pacote contém metadados que descrevem seu conteúdo, dependências e outros detalhes importantes.

A instalação de pacotes pode ser feita através do Visual Studio, do .NET CLI ou diretamente do NuGet Package Manager.

## Logging e LogLevel

ASP.NET Core fornece uma infraestrutura de logging integrada que permite registrar informações em diferentes níveis de detalhe.

Pode ser configurado no arquivo `appsettings.json` ou via código no método ConfigureServices.

|Log Level|Descrição|
|:---:|:---|
|Trace|Informação mais detalhada e volumosa|
|Debug|Informação de depuração, menos detalhada que Trace|
|Information|Informação geral sobre o fluxo da aplicação|
|Warning|Potenciais problemas ou situações inusitadas|
|Error|Erros que afetam o funcionamento da aplicação|
|Critical|Falhas graves que necessitam de atenção imediata|

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

|||
|:---:|:---|
|Model|É a representação dos dados do mundo real que pode incluir validações de estado e regras de negócio|
|View|São as páginas do site, responsáveis pela navegação, design, UX|
|Controller|Intermediária entre a Model e a View. Invoca o método correto que irá processar e retornar os dados, para serem enviados para View|

## DTO

Um Data Transfer Object (DTO) é um padrão de design usado para transferir dados entre diferentes camadas de uma aplicação.

São usados para transportar dados entre a camada de apresentação (Views) e a camada de negócios (Controllers e Models) ou entre serviços web e clientes.

Ao limitar a quantidade de dados transferidos, eles ajudam a reduzir a sobrecarga de rede e melhorar o desempenho.

DTOs podem simplificar a estrutura de dados exposta às Views, oferecendo apenas as informações necessárias.

## Data Annotations

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

## Convenções

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

## Action Results

Os Action Results são componentes que determinam como uma resposta é retornada ao cliente após a execução de um método de ação em um controller.

Um Action Result é um objeto que implementa a interface IActionResult.

Permitem aos desenvolvedores controlar a saída de um método de ação de forma flexível e poderosa.

|Action Result|Descrição|Exemplo|
|:---:|:---|:---|
|ViewResult|Renderiza uma view HTML para o cliente|return View();|
|JsonResult|Retorna dados JSON, ideal para APIs RESTful|return Json(data);|
|RedirectResult|Redireciona o cliente para outra URL|return Redirect("http://example.com");|
|RedirectToActionResult|Redireciona para outra ação do controlador|return RedirectToAction("ActionName", "ControllerName");|
|ContentResult|Retorna texto simples como resposta HTTP|return Content("Hello World!");|
|FileResult|Retorna um arquivo para download ou visualização|return File("path/to/file", "mime/type");|
|EmptyResult|Retorna uma resposta HTTP vazia, sem conteúdo|return new EmptyResult();|
|StatusCodeResult|Retorna um código de status HTTP específico, como 404 ou 500|return StatusCode(404);|

## Roteamento

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

### Atributos

Os atributos de rota permitem definir regras de roteamento diretamente nos controladores e ações por meio de anotações.

Isso proporciona maior controle e flexibilidade ao roteamento, permitindo especificar rotas diretamente na definição de uma classe ou método.

```c#
[Route("/"), Order = 0] // Defines the default route for ProductsController, which automatically calls the Index() method
[Route("products"), Order = 1] // Defines that all actions in the ProductsController will be under the /products base URL
public class ProductsController : Controller
{
    // Defines that the action method should be called when the server receives an HTTP GET request for the /list URL
    [HttpGet("list")]
    public IActionResult List()
    {
        return View();
    }

    // Defines that the action method should be called when the server receives an HTTP POST request for the /details/id URL
    [HttpPost("details/{id}")]
    public IActionResult Details(int id)
    {
	// logic
    }
}
```

## Parâmetros

Passagem de parâmetros refere-se à capacidade de fornecer dados para um método de ação no controller a partir de uma solicitação HTTP.

Esses parâmetros podem ser extraídos de diferentes partes da solicitação, como a URL, parâmetros de consulta, corpo da solicitação, ou cabeçalhos.

### Model Binding

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

#### Atributos

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

### Múltiplos Parâmetros

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