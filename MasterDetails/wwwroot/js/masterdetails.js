$(function () {
    addEventListenerAddDetail();
    addEventListenerRemoveDetail();
    addEventListenerEditDetail();
})

let table = $("#table-produto");

//Ações Primárias
adicionarProduto = () => {
    var index = $('#Index').val();
    var detailId = $('#DetailId').val();
    var produtoId = $('#ProdutoId').val();
    var qtd = $('#Qtd').val();

    if (produtoId != null && produtoId > 0 && qtd != null && qtd > 0) {
        $.ajax({
            url: "/Masters/AppendProdutos",
            data: { produtoId },
            success: function (produto) {
                console.log(produto);

                if (index != null && index != '' && index >= 0) {
                    //Editar
                    let detail = { detailId: detailId, masterId: _masterId, produtoId: produto.produtoId, nome: produto.nome, quantidade: qtd, preco: produto.precoFormatado };
                    details.splice(index, 1);
                    details.splice(index, 0, detail);

                    table.empty();
                    countItens = 0;

                    $.each(details, function (i, detail) {
                        appendTable(i, detail);
                        countItens++;
                    });

                    $('.btn-add-produto').text('Adicionar Produto');

                } else {
                    //Adicionar
                    let detail = { detailId: 0, masterId: _masterId, produtoId: produto.produtoId, nome: produto.nome, quantidade: qtd, preco: produto.precoFormatado};
                    details.push(detail);
                    appendTable(countItens, detail);
                    countItens++;
                }
            }
        })
    }
    else {
        alert('Selecione o produto');
    }

    console.log(details);
    limparInputs();
}

removerProduto = (e) => {
    var index = e.value;

    details.splice(index, 1);
    table.empty();
    countItens = 0;

    $.each(details, function (i, detail) {
        appendTable(i, detail);
        countItens++;
    });

    limparInputs();
}

editarProduto = (e) => {
    var index = e.value;

    var detailEdit = details[index];

    $('#Index').val(index);
    $('#DetailId').val(detailEdit.detailId);
    $('#ProdutoId').val(detailEdit.produtoId);
    $('#Qtd').val(detailEdit.quantidade);
    $('#ProdutoId').focus();

    $('.btn-add-produto').text('Salvar Edição');
}

//Eventos
addEventListenerAddDetail = () => {
    var btnAddProduto = document.getElementsByClassName('btn-add-produto');
    btnAddProduto[0].addEventListener('click', function () {
        adicionarProduto();
    });
}

addEventListenerRemoveDetail = () => {
    var btnRemoverProduto = document.getElementsByClassName('btn-rm-produto');

    for (var i = 0; i < btnRemoverProduto.length; i++) {
        btnRemoverProduto[i].addEventListener('click', function () {
            removerProduto(this);
        });
    }
}

addEventListenerEditDetail = () => {
    var btnEditarProduto = document.getElementsByClassName('btn-edit-produto');

    for (var i = 0; i < btnEditarProduto.length; i++) {
        btnEditarProduto[i].addEventListener('click', function () {
            editarProduto(this);
        });
    }
}

//Auxiliar
limparInputs = () => {
    $('#Index').val('');
    $('#DetailId').val('');
    $('#ProdutoId').val('');
    $('#Qtd').val('');
    $('#ProdutoId').focus();
}

appendTable = (i, detail) => {
    var iptDetailId = document.createElement("input");
    iptDetailId.setAttribute("type", "hidden");
    iptDetailId.value = detail.detailId;
    iptDetailId.setAttribute("name", "Details[" + i + "].DetailId");

    var iptMasterId = document.createElement("input");
    iptMasterId.setAttribute("type", "hidden");
    iptMasterId.value = detail.masterId;
    iptMasterId.setAttribute("name", "Details[" + i + "].MasterId");

    var iptProdutoId = document.createElement("input");
    iptProdutoId.setAttribute("type", "hidden");
    iptProdutoId.value = detail.produtoId;
    iptProdutoId.setAttribute("name", "Details[" + i + "].ProdutoId");

    var iptQtd = document.createElement("input");
    iptQtd.setAttribute("type", "hidden");
    iptQtd.value = detail.quantidade;
    iptQtd.setAttribute("name", "Details[" + i + "].Qtd");

    var iptPreco = document.createElement("input");
    iptPreco.setAttribute("type", "hidden");
    iptPreco.value = detail.preco;
    iptPreco.setAttribute("name", "Details[" + i + "].Preco");

    var btnRemover = document.createElement("button");
    btnRemover.setAttribute("class", "btn btn-danger btn-rm-produto");
    btnRemover.setAttribute("type", "button");
    btnRemover.value = i;
    btnRemover.innerHTML = "Remover Produto";
    btnRemover.addEventListener('click', function () {
        removerProduto(this);
    });

    var btnEditar = document.createElement("button");
    btnEditar.setAttribute("class", "ml-1 btn btn-primary btn-edit-produto");
    btnEditar.setAttribute("type", "button");
    btnEditar.value = i;
    btnEditar.innerHTML = "Editar Produto";
    btnEditar.addEventListener('click', function () {
        editarProduto(this);
    });

    table.append($('<tr>')
        .append($('<td>').append(detail.nome))
        .append($('<td>').append(detail.quantidade))
        .append($('<td>').append("R$ " + detail.preco))
        .append($('<td>')
            .append(btnRemover).append(btnEditar)
            .append(iptDetailId).append(iptMasterId).append(iptProdutoId).append(iptQtd).append(iptPreco))
    );
}