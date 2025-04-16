function cadastrarPapelUsuario(url) {
  const nomePapel = $("#papelUsuario").val().toUpperCase().trim();

  if (nomePapel == "") {
    showAlert("Preencha o nome do Papel do Usuário!", "error");
    return;
  }

  $.post(url, { NomePapelUsuario: nomePapel })
    .done(function () {
      showAlert("Papel do Usuário cadastrado com sucesso!", "success");
      setTimeout(() => {
        location.reload();
      }, 1000);
    })
    .fail(function (xhr) {
      const errorMsg = xhr.responseJSON?.message || "Erro ao adicionar papel!";
      showAlert(errorMsg, "error");
    });
}

function excluirPapel(url, idPapel) {
  $.post(url)
    .done(function () {
      $(`#papelUsuario-${idPapel}`).remove();
      showAlert("Papel removido com sucesso!", "danger");
      setTimeout(() => {
        location.reload();
      }, 1000);
    })
    .fail(function () {
      showAlert("Erro ao remover papel!", "danger");
    });
}
