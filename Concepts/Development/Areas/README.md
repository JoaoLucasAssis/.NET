# Áreas

No ASP.NET, as áreas são um recurso usado para organizar funcionalidades relacionadas em grupos distintos.

As áreas proporcionam uma maneira de particionar um aplicativo Web do ASP.NET em grupos funcionais menores, com sua própria estrutura de pastas e namespaces.

As áreas ajudam a isolar diferentes partes da aplicação, como controladores e views, em módulos independentes.

## Como funciona?

Uma área divide a aplicação em módulos distintos. Cada módulo é representado por uma pasta e pode conter seus próprios controladores, modelos e views.

As áreas têm seus próprios roteadores que podem ser configurados separadamente do roteador principal da aplicação.

O runtime do ASP.NET Core usa as convenções de nomenclatura para criar a relação entre esses componentes.

## Estrutura de pastas

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

## Roteamento de áreas

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

## Layout compartilhado

Para compartilhar um layout comum para o aplicativo inteiro, copie o arquivo`_ViewStart.cshtml` para a pasta raiz da pasta.

Copie o arquivo `_ViewImports.cshtml` para a raiz da pasta para habilitar o uso dos tag helpers do ASP.NET Core.

## Configuração dos controllers

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