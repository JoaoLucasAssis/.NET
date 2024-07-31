# Slugify

Slugify é um processo de conversão de uma string em um formato compatível com URL.

Frequentemente usado no desenvolvimento web ao criar URLs para páginas web, postagens de blog ou outros recursos.

O objetivo de formatar uma string é torná-la mais legível para humanos e mecanismos de pesquisa, além de garantir que ela contenha apenas caracteres seguros para URL.

Remove caracteres não alfanuméricos que não são permitidos em URLs.

Troca espaços por hífens (-) ou outro separador permitido.

Converte todo o texto para minúsculas para uniformidade.

## Porque usar?

A importância de formatar strings está em melhorar a usabilidade e SEO (Search Engine Optimization) de um site. 

Melhora a indexação em motores de busca, criando URLs que sejam mais compreensíveis e relevantes, impactando positivamente a classificação SEO do site.

## Configuração

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




