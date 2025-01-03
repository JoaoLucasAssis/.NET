# ASP.NET

- [ASP.NET](#aspnet)
  - [Hosting](#hosting)
  - [Pipeline](#pipeline)
  - [Middleware](#middleware)
  - [Injeção de Dependência](#injeção-de-dependência)
  - [Identity](#identity)
  - [NuGet](#nuget)
  - [Logging e LogLevel](#logging-e-loglevel)

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
// Uses the interface to define the type of service to be injected, but the actual instance is created from the concrete class
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

|   Ciclo    | Descrição                                                               | Uso                                                                                                                                    | Registro                           |
| :--------: | :---------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------- | :--------------------------------- |
| Transiente | Os serviços são criados cada vez que são solicitados                    | Ideal para serviços leves e sem estado, que não precisam compartilhar dados entre as solicitações                                      | AddTransient<IService, Service>(); |
|   Scoped   | Os serviços são criados uma vez por solicitação                         | Ideal para serviços que precisam compartilhar dados durante uma solicitação, mas não devem manter estado entre diferentes solicitações | AddScoped<IService, Service>();    |
| Singleton  | Os serviços são criados uma única vez durante a vida útil da aplicação. | Ideal para serviços que mantêm estado global ou são pesados para criar e configurar                                                    | AddSingleton<IService, Service>(); |

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

<h3>Autenticação</h3>

Autenticação é o processo de verificar a identidade de um usuário.

É a forma de garantir que uma pessoa é quem diz ser.

Serve para assegurar que apenas usuários legítimos tenham acesso ao sistema ou aos recursos protegidos.

É o primeiro passo na implementação de segurança em uma aplicação.

<h4>Como funciona no Identity?</h4>

No ASP.NET Identity, a autenticação é gerenciada através de cookies.

Quando um usuário faz login, o sistema cria um cookie de autenticação que contém um token que identifica o usuário.

Este cookie é enviado com cada solicitação subsequente para que o sistema possa reconhecer o usuário e manter a sessão ativa.

O ASP.NET Identity fornece um middleware para gerenciar a autenticação e assegurar que as requisições sejam processadas de acordo com o status de autenticação do usuário.

```c#
app.UseAuthentication(); // Enable authentication middleware
```

<h3>Autorização</h3>

Autorização é o processo de determinar se um usuário autenticado tem permissão para acessar um recurso específico ou realizar uma ação.

Serve para controlar o acesso a diferentes partes da aplicação ou a operações específicas, com base nas permissões do usuário.

É crucial para proteger recursos sensíveis e garantir que apenas usuários com as permissões adequadas possam acessá-los.

<h4>Como funciona no Identity?</h4>

No ASP.NET Identity, a autorização pode ser configurada usando **roles**, **claims** e **policies**. 

Essas abordagens ajudam a definir regras sobre quem pode acessar o quê e sob quais condições.

| Permissão | Descrição                                                                                                                                                                         |
| :-------: | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|   Roles   | São grupos de permissões que podem ser atribuídos a usuários. Permite que você defina permissões globais e simples                                                                |
|  Claims   | São informações específicas associadas a um usuário, como nome, email ou permissões detalhadas. Permite criar regras mais detalhadas baseadas em atributos específicos do usuário |
|  Policy   | Combinam roles e claims para definir regras complexas sobre quais usuários têm acesso a quais recursos, combinando múltiplos requisitos                                           |

<table style="width: 100%; border-collapse: collapse;">
    <thead>
        <tr style="background-color: #f2f2f2;">
            <th style="border: 1px solid #ddd; padding: 10px; vertical-align: top; text-align: center;">Role</th>
            <th style="border: 1px solid #ddd; padding: 10px; vertical-align: top; text-align: center;">Claim</th>
            <th style="border: 1px solid #ddd; padding: 10px; vertical-align: top; text-align: center;">Policy</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style="border: 1px solid #ddd; padding: 10px; vertical-align: top;">
                <pre style="height: 300px; padding: 5px; margin: 0; box-sizing: border-box; overflow-x: auto; white-space: pre-wrap;"><code>
// controller
[Authorize(Roles = "Admin")]
public IActionResult AdminOnly()
{
    return View();
}
                </code></pre>
            </td>
            <td style="border: 1px solid #ddd; padding: 10px; vertical-align: top;">
                <pre style="height: 300px; padding: 5px; margin: 0; box-sizing: border-box; overflow-x: auto; white-space: pre-wrap;"><code>
// controller
[Authorize(Policy = "CanEdit")]
public IActionResult Edit()
{
    return View();
}
                </code></pre>
            </td>
            <td style="border: 1px solid #ddd; padding: 10px; vertical-align: top;">
                <pre style="height: 150px; padding: 5px; margin: 0; box-sizing: border-box; overflow-x: auto; white-space: pre-wrap;"><code>
// Program.cs
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanEdit", policy =>
        policy.RequireClaim("Edit", "true"));
});
                </code></pre>
                <pre style="height: 150px; padding: 5px; margin: 0; box-sizing: border-box; overflow-x: auto; white-space: pre-wrap;"><code>
// controller
[Authorize(Policy = "CanEdit")]
public IActionResult Edit()
{
    return View();
}
                </code></pre>
            </td>
        </tr>
    </tbody>
</table>

<h4>Autorização personalizada</h4>

Autorização personalizada com claims permite definir regras de acesso específicas com base em informações (claims) associadas ao usuário.

Serve para criar regras de acesso mais granulares e específicas que não podem ser facilmente definidas apenas com roles.

Isso é útil quando você precisa de uma lógica de autorização que vai além dos papéis tradicionais (roles) e pode exigir verificação de informações adicionais associadas ao usuário.

Crie um arquivo `CustomAuthorization.cs` na pasta **/Extensions** e adicione as seguintes classes:

`CustomAuthorization` contém um método estático para validar se o usuário possui uma claim específica.

```c#
public class CustomAuthorization
{
    public static bool UserClaimValidation(HttpContext context, string claimName, string claimValue)
    {
        if (context.User.Identity == null) throw new InvalidOperationException();

        return context.User.Identity.IsAuthenticated &&
            context.User.Claims.Any(c => c.Type == claimName && c.Value.Split(',').Contains(claimValue));
    }
}
```

`ClaimRequestFilter` implementa o filtro de autorização que verifica se o usuário está autenticado e possui a claim necessária.

```c#
public class ClaimRequestFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public ClaimRequestFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity == null) throw new InvalidOperationException();

        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        area = "Identity",
                        page = "Account/Login",
                        ReturnUrl = context.HttpContext.Request.Path.ToString()
                    }
                )
            );
        }

        if (!CustomAuthorization.UserClaimValidation(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}
```

`ClaimAuthorizationAttribute` define um atributo personalizado que aplica o filtro de autorização com base em claims.

```c#
public class ClaimAuthorizationAttribute : TypeFilterAttribute
{
    public ClaimAuthorizationAttribute(string claimName, string claimValue) : base(typeof(ClaimRequestFilter))
    {
        Arguments = new object[] { new Claim(claimName, claimValue) };
    }
}
```

Para aplicar a autorização personalizada em uma action de um controller, use o atributo **ClaimAuthorization** na action desejada:

```c#
[ClaimAuthorization("product-manager", "create")]
public IActionResult Create()
{
    return View();
}
```

Na tabela **AspNetUserClaims** inclua um array com as claims que define as permissões concedidas a cada usuário.

|id|userId                              |type           |value             |
|:-|:-----------------------------------|:--------------|:-----------------|
|1 |e5441243-2f93-4a65-8449-863f2435d884|product-manager|create,edit,delete|

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

<div align="center">
    <a href="#aspnet">Voltar ao topo &#8593;</a>
</div>
