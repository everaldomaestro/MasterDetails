$('.btn-adicionar-produto').on('click', adicionarProduto);

function limparInputs() {
    $('#ProdutoId').val('');
    $('#Qtd').val('');
    $('#ProdutoId').focus();
}

function adicionarProduto() {
    var produtoId = $('#ProdutoId').val();
    var qtd = $('#Qtd').val();
    var table = $('#table-produto');

    if (produtoId != null && produtoId > 0 && qtd != null && qtd > 0) {
        $.ajax({
            url: "/Masters/AppendProdutos",
            data: { produtoId },
            success: function (produto) {
                var row =
                    "<tr class='tabela-produto-linhas'>" +
                        "<td class='nomeProduto'>" +
                            "<input type='hidden' id='Details[" + itens + "].DetailId' name='Details[" + itens + "].DetailId' value='0' class='form-control detailId' />"+
                            "<input type='hidden' id='Details[" + itens + "].MasterId' name='Details[" + itens + "].MasterId' value='" + masterId +"' class='form-control masterId' />"+
                            "<input type='hidden' class='form-control produtoId' id='Details[" + itens + "].ProdutoId' name='Details[" + itens + "].ProdutoId' value='" + produto.produtoId + "' />" +
                            produto.nome +
                        "</td>" +
                        "<td class='qtdProduto'>" +
                            "<input type='hidden' class='form-control qtdProduto' id='Details[" + itens + "].Qtd' name='Details[" + itens + "].Qtd' value='" + qtd + "' />" +
                            qtd +
                        "</td>" +
                        "<td>" +
                            "<button type='button' class='btn btn-danger btn-rm-produto' onclick='removerProduto(" + itens + ")'>Remover Produto</button>" +
                        "</td>" +
                    "</tr>";

                table.append(row);
                itens++;
                limparInputs();
            }
        })
    }
    else {
        alert('Selecione o produto');
    }
}

function removerProduto(index) {
    var table = $('#table-produto');
    var details = [];

    $(".tabela-produto-linhas").each(function () {

        var _detailId = $(this).find('.detailId').val();
        var _masterId = $(this).find('.masterId').val();
        var _produtoId = $(this).find('.produtoId').val();
        var _nomeProduto = $(this).find('.nomeProduto').text();
        var _qtdProduto = $(this).find('.qtdProduto').text().trim();

        details.push({ detailId: _detailId, masterId: _masterId, produtoId: _produtoId, nomeProduto: _nomeProduto, qtd: _qtdProduto });
    });

    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: "/Masters/RemoveProdutos",
        data: { Index: index, Details: details },
        success: function (dados) {
            table.empty();
            itens = 0;

            $.each(dados.details, function (i, detail) {
                var row =
                    "<tr class='tabela-produto-linhas'>" +
                        "<td class='nomeProduto'>" +
                            "<input type='hidden' id='Details[" + itens + "].DetailId' name='Details[" + itens + "].DetailId' value='" + (masterId > 0 ? detail.detailId : 0) + "' class='form-control detailId' />" +
                            "<input type='hidden' id='Details[" + itens + "].MasterId' name='Details[" + itens + "].MasterId' value='" + masterId + "' class='form-control masterId' />" +
                            "<input type='hidden' class='form-control produtoId' id='Details[" + itens + "].ProdutoId' name='Details[" + itens + "].ProdutoId' value='" + detail.produtoId + "' />" +
                            detail.nomeProduto +
                        "</td>" +
                        "<td class='qtdProduto'>" +
                            "<input type='hidden' class='form-control qtdProduto' id='Details[" + itens + "].Qtd' name='Details[" + itens + "].Qtd' value='" + detail.qtd + "' />" +
                            detail.qtd +
                        "</td>" +
                        "<td>" +
                            "<button type='button' class='btn btn-danger btn-rm-produto' onclick='removerProduto(" + itens + ")'>Remover Produto</button>" +
                        "</td>" +
                    "</tr>";
                table.append(row);
                itens++;
            });

            limparInputs();
        },
        error: function (erro) {
            //error occurred
        }
    })
}