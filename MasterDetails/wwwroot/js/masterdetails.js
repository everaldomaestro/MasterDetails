let table = $("#table-produto");

$('.btn-adicionar-produto').on('click', function () {
    adicionarProduto();
});

limparInputs = () => {
    $('#ProdutoId').val('');
    $('#Qtd').val('');
    $('#ProdutoId').focus();
}

adicionarProduto = () => {
    var produtoId = $('#ProdutoId').val();
    var qtd = $('#Qtd').val();

    if (produtoId != null && produtoId > 0 && qtd != null && qtd > 0) {
        $.ajax({
            url: "/Masters/AppendProdutos",
            data: { produtoId },
            success: function (produto) {
                let detail = { detailId: 0, masterId: _masterId, produtoId: produto.produtoId, nome: produto.nome, quantidade: qtd };
                details.push(detail);

                appendTable(countItens, detail);
                countItens++;
            }
        })
    }
    else {
        alert('Selecione o produto');
    }

    limparInputs();
}

removerProduto = (index) => {
    details.splice(index, 1);
    table.empty();
    countItens = 0;

    $.each(details, function (i, detail) {
        appendTable(i, detail);
        countItens++;
    });

    limparInputs();
}

appendTable = (i, detail) => {    
    var row = "<tr><td>" + detail.nome + "</td><td>" + detail.quantidade + "</td><td>"+
        "<button type='button' class='btn btn-danger btn-rm-produto' onclick='removerProduto(" + i + ")'>Remover Produto</button>" +
        "<input type='hidden' name='Details[" + i + "].DetailId' value='" + detail.detailId +"' />" +
        "<input type='hidden' name='Details[" + i + "].MasterId' value='" + detail.masterId +"' />" +
        "<input type='hidden' name='Details[" + i + "].ProdutoId' value='" + detail.produtoId + "' />" +
        "<input type='hidden' name='Details[" + i + "].Qtd' value='" + detail.quantidade + "' />" +
        "</td></tr>";

    table.append(row);
}