# Bundling & Minification

Bundling e Minification são técnicas usadas para otimizar o desempenho das aplicações web.

Reduzem o tempo de carregamento das páginas e melhoram a eficiência geral.

## Getting Started

- Para utilizar a extensão Bundler & Minifier da Microsoft, siga os passos abaixo:

* Acesse o [marketplace da Microsoft]("https://marketplace.visualstudio.com/items?itemName=Failwyn.BundlerMinifier64") e faça download do **Bundler & Minifier 2022+**.

* Execute o arquivo e termine a instalação da extensão.

Após a instalação, reabra o Visual Studio 2022 e a extensão estará instalada.

Para configuração, [clique aqui](#configuração).

## Bundling

`Bundling` é o processo de combinar vários arquivos (como arquivos CSS e JavaScript) em um único arquivo.

Reduz o número de requisições HTTP feitas pelo navegador, diminuindo a sobrecarga de rede e o tempo de carregamento da página.

<img src="https://res.cloudinary.com/practicaldev/image/fetch/s--wl-fanDF--/c_limit%2Cf_auto%2Cfl_progressive%2Cq_auto%2Cw_880/https://dev-to-uploads.s3.amazonaws.com/uploads/articles/9yd7wf7k9qq5y1ffxyz3.png" alt="imagem" width="450px" height="300px"/>

## Minification

`Minification` é o processo de remover todos os caracteres desnecessários dos arquivos de código sem alterar a funcionalidade do código.

Remove espaços em branco, quebras de linha e comentário, diminuindo o tamanho dos arquivos e o tempo de carregamento das páginas.

<img src="https://res.cloudinary.com/practicaldev/image/fetch/s--JyHqtaOz--/c_limit%2Cf_auto%2Cfl_progressive%2Cq_auto%2Cw_880/https://dev-to-uploads.s3.amazonaws.com/uploads/articles/4d28md0n6bo2e4i13x16.png" alt="imagem" width="450px" height="300px"/>

## Configuração

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
