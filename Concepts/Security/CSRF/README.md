# CSRF (Cross-Site Request Forgery)

Cross-Site Request Forgery (CSRF) é um ataque que engana um usuário autenticado para executar ações indesejadas em um site.

O atacante usa o navegador do usuário para enviar uma solicitação não autorizada, aproveitando a sessão do usuário com um site legítimo.

<img src="https://i0.wp.com/www.eduardopires.net.br/wp-content/uploads/2018/02/csrf.png?fit=754%2C160&ssl=1" alt="imagem" width="650px" height="200px"/>

Pode ser usado para realizar ações indesejadas em nome do usuário, como alterar configurações, fazer transações financeiras, ou mesmo comprometer a segurança do usuário e dos dados.

## Importância

É crucial proteger aplicações contra CSRF porque esses ataques podem comprometer a integridade dos dados do usuário e realizar ações indesejadas em nome do usuário.

Sem proteção adequada, os usuários podem ser enganados para realizar ações não intencionais que podem ter sérias consequências de segurança e privacidade

## Como prevenir?

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
