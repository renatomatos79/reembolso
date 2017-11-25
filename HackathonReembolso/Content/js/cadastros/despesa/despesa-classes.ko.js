var DespesaModel = function (id, tipoDespesa, categoria, centroCusto, usuario, gerenciaId, projetoId, statusId, numeroDocumento, placa, descricao, dataDespesa, valor, observacao, dataCompetencia, dataRealizacao) {
    this.id = id;
    this.tipoDespesaId = tipoDespesa.Id;
    this.tipoDespesaNome = tipoDespesa.Nome;

    this.categoriaId = categoria.Id;
    this.categoriaNome = categoria.Descricao;

    this.centroCustoId = centroCusto.Id;
    this.centroCustoExterno = centroCusto.CodigoExterno;

    this.usuarioId = usuario.Id;
    this.usuarioMatricula = usuario.Matricula;
    this.usuarioNome = usuario.Nome;

    this.gerenciaId = gerenciaId;
    this.projetoId = projetoId;
    this.statusId = statusId;
    this.numeroDocumento = numeroDocumento;
    this.placa = placa;
    this.descricao = descricao;
    this.dataDespesa = dataDespesa;
    this.valor = valor;
    this.observacao = observacao;
    this.dataCompetencia = dataCompetencia;
    this.dataRealizacao = dataRealizacao;
};