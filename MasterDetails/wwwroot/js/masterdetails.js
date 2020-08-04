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
                    let detail = { detailId: detailId, masterId: _masterId, produtoId: produto.produtoId, nome: produto.nome, quantidade: qtd };
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
                    let detail = { detailId: 0, masterId: _masterId, produtoId: produto.produtoId, nome: produto.nome, quantidade: qtd };
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
    var row = "<tr><td>" + detail.nome + "</td><td>" + detail.quantidade + "</td><td>" +
        "<button type='button' class='btn btn-danger btn-rm-produto' value='" + i + "'>Remover Produto</button>" +
        "<button type='button' class='ml-1 btn btn-primary btn-edit-produto' value='" + i +"'>Editar Produto</button>" +
        "<input type='hidden' name='Details[" + i + "].DetailId' value='" + detail.detailId + "' />" +
        "<input type='hidden' name='Details[" + i + "].MasterId' value='" + detail.masterId + "' />" +
        "<input type='hidden' name='Details[" + i + "].ProdutoId' value='" + detail.produtoId + "' />" +
        "<input type='hidden' name='Details[" + i + "].Qtd' value='" + detail.quantidade + "' />" +
        "</td></tr>";

    table.append(row);

    document.getElementsByClassName('btn-rm-produto')[i].addEventListener('click', function () {
        removerProduto(this);
    });

    document.getElementsByClassName('btn-edit-produto')[i].addEventListener('click', function () {
        editarProduto(this);
    });
}