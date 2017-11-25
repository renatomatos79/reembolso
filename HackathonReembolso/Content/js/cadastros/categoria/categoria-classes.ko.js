var CategoriaModel = function (id, descricao, tipoDespesa, valorFixo, valor) {
    this.id = id;
    this.descricao = descricao;
    this.tipoDespesaId = tipoDespesa.Id;
    this.tipoDespesaNome = tipoDespesa.Nome;
    this.valorFixo = valorFixo;
    this.valor = valor;
};