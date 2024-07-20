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