var json = [
    {
        active: true, expand: true, NrOrdem: 3, CdIcon: "menu-icon fa fa-plus", Titulo: "Segurança", Url: "#", datasource:
        [
            { NrOrdem: 2, CdIcon: "menu-icon fa fa-sitemap", Titulo: "Áreas", Url: "https://localhost:44301/Cadastros/Area/Index" },
            { NrOrdem: 1, CdIcon: "menu-icon fa fa-th", Titulo: "Controles", Url: "https://localhost:44301/Cadastros/Controle/Index" },
            { NrOrdem: 3, CdIcon: "menu-icon fa fa-flash", Titulo: "Acoes", Url: "https://localhost:44301/Cadastros/Acao/Index" },
            {
                NrOrdem: 0, expand: true, CdIcon: "menu-icon fa fa-plus", Titulo: "Segurança 2.1", Url: "#", datasource:
                [
                    { NrOrdem: 3, CdIcon: "menu-icon fa fa-sitemap", Titulo: "Áreas", Url: "https://localhost:44301/Cadastros/Area/Index" },
                    { NrOrdem: 2, CdIcon: "menu-icon fa fa-th", Titulo: "Controles", Url: "https://localhost:44301/Cadastros/Controle/Index" },
                    { NrOrdem: 1, CdIcon: "menu-icon fa fa-flash", Titulo: "Acoes", Url: "https://localhost:44301/Cadastros/Acao/Index" }
                ]
            },
            {
                expand: true, CdIcon: "menu-icon fa fa-plus", Titulo: "Segurança 2.2", Url: "#", datasource:
                [
                    { CdIcon: "menu-icon fa fa-sitemap", Titulo: "Áreas", Url: "https://localhost:44301/Cadastros/Area/Index" },
                    { CdIcon: "menu-icon fa fa-th", Titulo: "Controles", Url: "https://localhost:44301/Cadastros/Controle/Index" },
                    { CdIcon: "menu-icon fa fa-flash", Titulo: "Acoes", Url: "https://localhost:44301/Cadastros/Acao/Index" },
                    {
                        expand: true, CdIcon: "menu-icon fa fa-plus", Titulo: "Segurança 2.2.1", Url: "#", datasource:
                        [
                            { CdIcon: "menu-icon fa fa-sitemap", Titulo: "Áreas", Url: "https://localhost:44301/Cadastros/Area/Index" },
                            { CdIcon: "menu-icon fa fa-th", Titulo: "Controles", Url: "https://localhost:44301/Cadastros/Controle/Index" },
                            { CdIcon: "menu-icon fa fa-flash", Titulo: "Acoes", Url: "https://localhost:44301/Cadastros/Acao/Index" }
                        ]
                    }
                ]
            }
        ]
    },
    { NrOrdem: 2, CdIcon: "menu-icon fa fa-puzzle-piece", Titulo: "Tipo de Unidade 2", Url: "https://localhost:44301/Cadastros/DepartamentoTipo/Index" },
    { NrOrdem: 1, CdIcon: "menu-icon fa fa-puzzle-piece", Titulo: "Tipo de Unidade 1", Url: "https://localhost:44301/Cadastros/DepartamentoTipo/Index" }
];

// cria o menu lateral
var dataSource = new MenuJsonHelper().toDataSource(json);
new MainMenu({ id: "#mnuApp", datasource: dataSource }).draw();


// adiciona a query string do perfil o token
//var href = $("#user-profile").attr("href");
//href += "?" + tokenName + "=" + jwt;
// foto do usuario
//$("#user-profile").attr("href", href);
