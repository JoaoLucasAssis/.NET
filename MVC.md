# MVC

- [MVC](#mvc)
  - [Getting Started](#getting-started)
  - [Conceitos](#conceitos)
    - [Bundling e Minification](#bundling-e-minification)
    - [Slugify](#slugify)
    - [Areas](#areas)
    - [CSRF](#csrf)
    - [XSS](#xss)
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
    - [Convenções](#convenções)
    - [Action Results](#action-results)
    - [Roteamento](#roteamento)
    - [Parâmetros](#parâmetros)
    - [ModelState](#modelstate)


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

<details>
<summary>clique aqui para ler mais sobre</summary>
<p>
Bundling e Minification são técnicas usadas para otimizar o desempenho das aplicações web.

Reduzem o tempo de carregamento das páginas e melhoram a eficiência geral.

<h3>Getting Started</h3>

- Para utilizar a extensão Bundler & Minifier da Microsoft, siga os passos abaixo:

* Acesse o [marketplace da Microsoft]("https://marketplace.visualstudio.com/items?itemName=Failwyn.BundlerMinifier64") e faça download do **Bundler & Minifier 2022+**.

* Execute o arquivo e termine a instalação da extensão.

Após a instalação, reabra o Visual Studio 2022 e a extensão estará instalada.

Para configuração, [clique aqui](#configuração).

<h3>Bundling</h3>

`Bundling` é o processo de combinar vários arquivos (como arquivos CSS e JavaScript) em um único arquivo.

Reduz o número de requisições HTTP feitas pelo navegador, diminuindo a sobrecarga de rede e o tempo de carregamento da página.

<img src="https://res.cloudinary.com/practicaldev/image/fetch/s--wl-fanDF--/c_limit%2Cf_auto%2Cfl_progressive%2Cq_auto%2Cw_880/https://dev-to-uploads.s3.amazonaws.com/uploads/articles/9yd7wf7k9qq5y1ffxyz3.png" alt="imagem" width="450px" height="300px"/>

<h3>Minification</h3>

`Minification` é o processo de remover todos os caracteres desnecessários dos arquivos de código sem alterar a funcionalidade do código.

Remove espaços em branco, quebras de linha e comentário, diminuindo o tamanho dos arquivos e o tempo de carregamento das páginas.

<img src="https://res.cloudinary.com/practicaldev/image/fetch/s--JyHqtaOz--/c_limit%2Cf_auto%2Cfl_progressive%2Cq_auto%2Cw_880/https://dev-to-uploads.s3.amazonaws.com/uploads/articles/4d28md0n6bo2e4i13x16.png" alt="imagem" width="450px" height="300px"/>

<h3>Configuração</h3>

Crie um arquivo **bundleconfig.json** no raiz do projeto e adicione suas instruções de bundling:

```json
[
  {
    "outputFileName": "wwwroot/css/site.min.css", // Bundles the three files into a single file delivered to the browser
    "inputFiles": [
      "wwwroot/css/header.css",
      "wwwroot/css/aside.css",
      "wwwroot/css/home.css"
    ], // When not specifying minify, its value is true
  },
  {
    "outputFileName": "wwwroot/bootstrap/bootstrap.min.js", // Bundles the two files into a single file
    "inputFiles": [
      "wwwroot/bootstrap/bootstrap-grid.js",
      "wwwroot/bootstrap/bootstrap-reboot.js"
    ],
    "minify": { // Enables minification to optimize the code of this new file
      "enabled": true,
      "renameLocals": true // Allows variable renaming
    },
    "sourceMap": false
  },
  {
    "outputFileName": "wwwroot/js/site.min.js", // Bundles the files into a single file
    "inputFiles": [
      "wwwroot/js/header.js",
      "wwwroot/js/aside.js"
    ],
    "minify": {
      "enabled": false // Disables minification for this bundle
    }
  }
]
```
</p>
</details>

### Slugify

<details>
<summary>clique aqui para ler mais sobre</summary>
<p>
Slugify é um processo de conversão de uma string em um formato compatível com URL.

Frequentemente usado no desenvolvimento web ao criar URLs para páginas web, postagens de blog ou outros recursos.

O objetivo de formatar uma string é torná-la mais legível para humanos e mecanismos de pesquisa, além de garantir que ela contenha apenas caracteres seguros para URL.

Remove caracteres não alfanuméricos que não são permitidos em URLs.

Troca espaços por hífens (-) ou outro separador permitido.

Converte todo o texto para minúsculas para uniformidade.

<h3>Porque usar</h3>

A importância de formatar strings está em melhorar a usabilidade e SEO (Search Engine Optimization) de um site. 

Melhora a indexação em motores de busca, criando URLs que sejam mais compreensíveis e relevantes, impactando positivamente a classificação SEO do site.

<h3>Configuração</h3>

Crie uma pasta **/Extensions** e dentro dela crie o arquivo `RouteSlugifyParameterTransformer.cs`.

Esse arquivo vai ser responsável por transformar o parâmetro de entrada para um formato amigável para SEO.

```c#
public class RouteSlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value is null) return null;

        return Regex.Replace(
            value.ToString()!, 
            "([a-z])([A-Z])", "$1-$2", 
            RegexOptions.CultureInvariant, 
            TimeSpan.FromMilliseconds(100)
        ).ToLowerInvariant();

        /*
        This Regex converts the value parameter to a string, 
        then uses a regular expression to replace occurrences of 
        a lowercase letter followed by an uppercase letter for: 

        The lowercase letter, a hyphen, and the uppercase letter.

        After the replacement, the ToLowerInvariant() method is called to convert 
        the entire result to lowercase before returning it.
        */
    }
}
```

Depois disso, adicione, no arquivo **Program.cs**, o nosso transformador de parâmetro personalizado no mapeamento de restrições da rota.

```c#
// Configures the app routing services to use the custom parameter transformer
builder.Services.AddRouting(options =>
    options.ConstraintMap["slugify"] = typeof(RouteSlugifyParameterTransformer)
    // Adds a new constraint to the ConstraintMap, associating the "slugify" key with the RouteSlugifyParameterTransformer
);
```

Por fim, altere a rota padrão, aplicando a chave **slugify** aos parâmetros da rota.

```c#
app.MapControllerRoute(
    name: "default",
    pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");
```
</p>
</details>

### Areas

<details>
<summary>clique aqui para ler mais sobre</summary>
<p>
No ASP.NET, as áreas são um recurso usado para organizar funcionalidades relacionadas em grupos distintos.

As áreas proporcionam uma maneira de particionar um aplicativo Web do ASP.NET em grupos funcionais menores, com sua própria estrutura de pastas e namespaces.

As áreas ajudam a isolar diferentes partes da aplicação, como controladores e views, em módulos independentes.

<h3>Como funciona?</h3>

Uma área divide a aplicação em módulos distintos. Cada módulo é representado por uma pasta e pode conter seus próprios controladores, modelos e views.

As áreas têm seus próprios roteadores que podem ser configurados separadamente do roteador principal da aplicação.

O runtime do ASP.NET Core usa as convenções de nomenclatura para criar a relação entre esses componentes.

<h3>Estrutura de pastas</h3>

Cada pasta dentro da **/Areas** em um projeto ASP.NET MVC representa um módulo separado da aplicação.

```txt
/Areas
    /Admin
    /Inventory
        /Controllers
	/Data
        /Models
        /Views
            /Shared
                _Layout.cshtml
            _ViewStart.cshtml
```

<h3>Roteamento de áreas</h3>

Rotas de área normalmente usam roteamento convencional, em vez de roteamento de atributo.

De modo geral, rotas com áreas devem ser colocadas mais no início na tabela de rotas, uma vez que são mais específicas que rotas sem uma área.

```c#
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

<h3>Layout compartilhado</h3>

Para compartilhar um layout comum para o aplicativo inteiro, copie o arquivo`_ViewStart.cshtml` para a pasta raiz da pasta.

Copie o arquivo `_ViewImports.cshtml` para a raiz da pasta para habilitar o uso dos tag helpers do ASP.NET Core.

<h3>Configuração dos controllers</h3>

Para informar a área a que uma controller pertence, informe o nome da área utilizando o atributo [Area("")].

O nome nome deve corresponder exatamente ao nome da pasta que você criou dentro da pasta **/Areas**.

```c#
[Area("Inventory")] // The area name is the name of folder: /Areas/ ---> Inventory <--- /Controller/StocksController.cs
[Authorize]
public class StocksController : Controller
{
    private readonly ApplicationDbContext _context;

    public StocksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ...logic
```
</p>
</details>

### CSRF

<details>
<summary>clique aqui para ler mais sobre</summary>
<p>
Cross-Site Request Forgery (CSRF) é um ataque que engana um usuário autenticado para executar ações indesejadas em um site.

O atacante usa o navegador do usuário para enviar uma solicitação não autorizada, aproveitando a sessão do usuário com um site legítimo.

<img src="https://i0.wp.com/www.eduardopires.net.br/wp-content/uploads/2018/02/csrf.png?fit=754%2C160&ssl=1" alt="imagem" width="650px" height="200px"/>

Pode ser usado para realizar ações indesejadas em nome do usuário, como alterar configurações, fazer transações financeiras, ou mesmo comprometer a segurança do usuário e dos dados.

<h3>Importância</h3>

É crucial proteger aplicações contra CSRF porque esses ataques podem comprometer a integridade dos dados do usuário e realizar ações indesejadas em nome do usuário.

Sem proteção adequada, os usuários podem ser enganados para realizar ações não intencionais que podem ter sérias consequências de segurança e privacidade

<h3>Como prevenir?</h3>

ASP.NET fornece uma proteção integrada contra CSRF com o uso de tokens anti-forgery.

Esses tokens garantem que uma solicitação POST é realmente originada do site legítimo.

O token é gerado pelo servidor e deve ser incluído em cada solicitação POST.

No controller, adicione o atributo `[ValidateAntiForgeryToken]` para validar o token em métodos POST.

```c#
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Name,Phone,CEP,State,City")] Client client)
{
    if (ModelState.IsValid)
    {
        // logic
    }
    return View(client);
}
```

O ASP.NET adiciona o token gerado pelo servidor em um input com nome **___RequestVerificationToken** para garantir que, em cada solicitação POST, a solicitação é originada do site.

```html
<input name="___RequestVerificationToken" type="hidden" value=
"CfDJ8I-Z5sEFEaVGkfkiunx1bgZFC0FdkvxmgbKZvddYoJf1Vkq3gBw_gUn04 EuwtZPq8CdgAxSzKUEOTXDGgRQvZm1xU4H100riajfAjVd2h1a8hWBqe_ssWic d-h22w-r1wvCz1X7znoZ202LRrJIj5xx0JFiJ7iwrNRNBQRPMvmSmoy62Jbr6g
5bG6wv1nfB1WA">
```

No ASP.NET Core, você pode configurar a proteção CSRF globalmente no `Program.cs`.

```c#
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
});
```
</p>
</details>

### XSS

<details>
<summary>clique aqui para ler mais sobre</summary>
<p>
Cross-Site Scripting (XSS) é um ataque onde o atacante insere código JavaScript malicioso em uma página web.

Esse código é executado no navegador da vítima, permitindo ao atacante roubar informações sensíveis, como cookies ou tokens de sessão, ou manipular o comportamento da página.

<img src="https://www.imperva.com/learn/wp-content/uploads/sites/13/2019/01/sorted-XSS.png" alt="imagem" width="650px" height="400px"/>

Pode ser usados para roubar credenciais de login, obter informações privadas, manipular o conteúdo da página ou realizar outras ações maliciosas em nome do usuário.

<h3>Importância</h3>

Proteger contra XSS é essencial para garantir a segurança das aplicações web e a privacidade dos dados dos usuários.

Sem proteção adequada, um aplicativo pode ser vulnerável a ataques que comprometem a segurança dos dados e a confiança dos usuários.

<h3>Como prevenir?</h3>

Você pode usar validações de solicitação, bibliotecas AntiXSS ou codificação de conteúdo, mas não se preocupe. ASP.NET fornece proteção integrada contra XSS.
</p>
</details>

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

### Convenções

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

<a href="#mvc" style="text-decoration: none; position: absolute; left: 50%; transform: translateX(-50%); margin-top: 50px">
        <div style="width: 50px; height: 50px; border: 5px solid; border-radius: 50%; display: flex;  justify-content: center; align-items: center; font-size: 36px;">↑</div>
</a>