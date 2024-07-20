# Anotações sobre .NET

## Sumário

* [.NET](#net)
  * [Dupla Compilação](#dupla-compilacao)

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

|||
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

> Obs: Para entender na prática, abra e rode o projeto na pasta "\TemplateMiddleware"

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

Para mais detalhes sobre logging no .NET Core e ASP.NET Core, consulte a [documentação oficial da Microsoft]("https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-6.0").