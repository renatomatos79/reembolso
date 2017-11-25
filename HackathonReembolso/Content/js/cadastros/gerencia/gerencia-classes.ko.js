var GerenciaModel = function (id, nome, setores) {
    this.id = id;
    this.nome = nome;
    this.setores = setores;
};

var GerenciaSetorModel = function (id, nome) {
    this.id = id;
    this.nome = nome;
};

var GerenciaAprovadorModel = function (id, suplente, dataInicioVigencia, dataTerminoVigencia, ativo) {
    this.id = id;
    this.suplente = suplente;
    this.dataInicioVigencia = dataInicioVigencia;
    this.dataTerminoVigencia = dataTerminoVigencia;
    this.ativo = ativo;
};