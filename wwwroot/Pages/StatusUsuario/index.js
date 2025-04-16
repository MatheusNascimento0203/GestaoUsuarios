function cadastrarStatusUsuario(url) {
  const nomeStatus = $("#statusUsuario").val().toUpperCase().trim();

  if (nomeStatus == "") {
    showAlert("Preencha o nome do status!", "error");
    return;
  }

  $.post(url, { NomeStatusUsuario: nomeStatus })
    .done(function () {
      showAlert("Status cadastrado com sucesso!", "success");
      setTimeout(() => {
        location.reload();
      }, 1000);
    })
    .fail(function (xhr) {
      const errorMsg = xhr.responseJSON?.message || "Erro ao adicionar status!";
      showAlert(errorMsg, "error");
    });
}

function excluirStatus(url, idStatus) {
  $.post(url)
    .done(function () {
      $(`#statusUsuario-${idStatus}`).remove();
      showAlert("Status removido com sucesso!", "danger");
      setTimeout(() => {
        location.reload();
      }, 1000);
    })
    .fail(function () {
      showAlert("Erro ao remover status!", "danger");
    });
}

function editarStatus(url, idStatus) {
  const nomeStatus = $("#statusUsuario").val().toUpperCase().trim();

  if (nomeStatus == "") {
    showAlert("Preencha o nome do status!", "error");
    return;
  }

  $.post(url, { idStatus, NomeStatusUsuario: nomeStatus })
    .done(function () {
      showAlert("Status atualizado com sucesso!", "success");
      setTimeout(() => {
        location.reload();
      }, 1000);
    })
    .fail(function (xhr) {
      const errorMsg = xhr.responseJSON?.message || "Erro ao atualizar status!";
      showAlert(errorMsg, "error");
    });
}
