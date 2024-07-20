# Anotações sobre .NET

# O que é .NET?

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

IL é uma linguagem de baixo nível que não é específica de nenhum sistema operacional ou hardware. 

Isso significa que o código IL pode ser executado em qualquer ambiente que tenha uma implementação da Common Language Runtime (CLR).

Quando um aplicativo .NET é executado, o código IL é compilado "Just-In-Time" (JIT) para o código de máquina nativo específico do sistema operacional e do hardware.

Esse processo é realizado pelo Just-In-Time Compiler (JIT Compiler), que faz parte da CLR.

# O que é ASP.NET?

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

## O que é Kestrel?

Kestrel é um servidor web multiplataforma e de alto desempenho embutido no .NET Core, usado principalmente com ASP.NET Core.

É ideal para desenvolvimento local e testes devido à sua configuração simples via código.

Ele processa solicitações através de um pipeline de middleware definido na aplicação.

O pipeline pode incluir middleware para autenticação, roteamento, manipulação de erros e muito mais.

Para ambientes de produção, é recomendável usar Kestrel atrás de um proxy reverso, para adicionar camadas adicionais de segurança e funcionalidade.

### Funcionamento em Ambientes de Produção

Quando o servidor recebe uma solicitação do cliente, ela é enviada através de um pipeline de middleware para o Kestrel.

Após o processamento, a solicitação é roteada para o controlador ou página apropriada, que executa a lógica necessária e gera a resposta.

Esta resposta é então enviada de volta ao cliente através do Kestrel.