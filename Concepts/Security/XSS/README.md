# XSS (Cross-Site Scripting)

Cross-Site Scripting (XSS) é um ataque onde o atacante insere código JavaScript malicioso em uma página web.

Esse código é executado no navegador da vítima, permitindo ao atacante roubar informações sensíveis, como cookies ou tokens de sessão, ou manipular o comportamento da página.

<img src="https://www.imperva.com/learn/wp-content/uploads/sites/13/2019/01/sorted-XSS.png" alt="imagem" width="650px" height="400px"/>

Pode ser usados para roubar credenciais de login, obter informações privadas, manipular o conteúdo da página ou realizar outras ações maliciosas em nome do usuário.

## Importância

Proteger contra XSS é essencial para garantir a segurança das aplicações web e a privacidade dos dados dos usuários.

Sem proteção adequada, um aplicativo pode ser vulnerável a ataques que comprometem a segurança dos dados e a confiança dos usuários.

## Como prevenir?

Você pode usar validações de solicitação, bibliotecas AntiXSS ou codificação de conteúdo, mas não se preocupe. ASP.NET fornece proteção integrada contra XSS.