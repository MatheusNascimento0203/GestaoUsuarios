let model;

function init(params) {
  model = params;
  renderizarUsuario();
}

function renderizarUsuario() {
  $.get(model.urls.buscarUsuario)
    .done((data) => {
      const divTabela = $("#tabela-usuarios");
      divTabela.empty();
      divTabela.append(data);
    })
    .fail(() => {
      showAlert("Erro ao buscar usuários.", "error");
    });
}

function abrirModalUsuario(urlEditar) {
  if (!urlEditar) {
    $.get(model.urls.buscarFormCadastrar)
      .done((data) => {
        const divConteudoModal = $("#modal-conteudo");
        divConteudoModal.empty();
        divConteudoModal.append(data);
        document.getElementById("modal-usuario").style.display = "block";
        cadastrarUsuario();
      })
      .fail((erro) => {
        showAlert(erro, "error");
      });
  } else {
    $.get(urlEditar)
      .done((data) => {
        const divConteudoModal = $("#modal-conteudo");
        divConteudoModal.empty();
        divConteudoModal.append(data);
        document.getElementById("modal-usuario").style.display = "block";
        editarUsuario(urlEditar);
        const fotoBase64 = $("#fotoBase64").val(); // pega o value que já veio preenchido no input hidden
        if (fotoBase64) {
          $("#preview-image").attr("src", fotoBase64);
          $("#preview-container").removeClass("hidden");
          $("#upload-content").addClass("hidden");
        }
      })
      .fail((erro) => {
        showAlert(erro, "error");
      });
  }
}

function fecharModal() {
  document.getElementById("modal-usuario").style.display = "none";
  document.getElementById("form-modal").reset();
  document.getElementById("preview-container").classList.add("hidden");
  document.getElementById("preview-image").src = "#";
  document.getElementById("upload-content").classList.remove("hidden");
  document.getElementById("file-upload").value = "";
  return;
}

window.onclick = function (event) {
  const modal = document.getElementById("modal-usuario");
  if (event.target == modal) {
    modal.style.display = "none";
  }
};

function cadastrarUsuario() {
  $("#confirmar-usuario")
    .off("click")
    .on("click", function () {
      const nome = $("#nome").val().trim();
      const email = $("#email").val().trim();
      const papelUsuario = $("#papelUsuario").val();
      const statusUsuario = $("#statusUsuario").val();
      const fileInput = document.getElementById("file-upload");
      const file = fileInput.files[0];

      if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
          const base64String = e.target.result;

          $.post(model.urls.cadastrar, {
            Nome: nome,
            Email: email,
            Foto: base64String,
            IdPapelUsuario: parseInt(papelUsuario),
            IdStatusUsuario: parseInt(statusUsuario),
          })
            .done(() => {
              showAlert("Usuário cadastrado com sucesso!", "success");
              setTimeout(() => location.reload(), 1000);
            })
            .fail((erro) => {
              const mensagemErro =
                erro.responseJSON?.message ||
                erro.responseText ||
                "Erro desconhecido.";
              showAlert(mensagemErro, "error");
            });
        };
        reader.readAsDataURL(file);
      } else {
        $.post(model.urls.cadastrar, {
          Nome: nome,
          Email: email,
          Foto: null,
          IdPapelUsuario: parseInt(papelUsuario),
          IdStatusUsuario: parseInt(statusUsuario),
        })
          .done(() => {
            showAlert("Usuário cadastrado com sucesso!", "success");
            setTimeout(() => location.reload(), 1000);
          })
          .fail((erro) => {
            const mensagemErro =
              erro.responseJSON?.message ||
              erro.responseText ||
              "Erro desconhecido.";
            showAlert(mensagemErro, "error");
          });
      }
    });
}

function editarUsuario(url) {
  $("#confirmar-usuario")
    .off("click")
    .on("click", function () {
      const nome = $("#nome").val().trim();
      const email = $("#email").val().trim();
      const papelUsuario = $("#papelUsuario").val();
      const statusUsuario = $("#statusUsuario").val();
      const fileInput = document.getElementById("file-upload");
      const file = fileInput.files[0];
      const fotoExistente = $("#fotoBase64").val(); // <<< aqui é o hidden agora

      if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
          const base64String = e.target.result;

          $.post(url, {
            Nome: nome,
            Email: email,
            Foto: base64String,
            IdPapelUsuario: parseInt(papelUsuario),
            IdStatusUsuario: parseInt(statusUsuario),
          })
            .done(() => {
              showAlert("Usuário alterado com sucesso!", "success");
              setTimeout(() => location.reload(), 1000);
            })
            .fail((erro) => {
              const mensagemErro =
                erro.responseJSON?.message ||
                erro.responseText ||
                "Erro desconhecido.";
              showAlert(mensagemErro, "error");
            });
        };
        reader.readAsDataURL(file);
      } else {
        $.post(url, {
          Nome: nome,
          Email: email,
          Foto: fotoExistente, // <<< vai enviar o que estava salvo
          IdPapelUsuario: parseInt(papelUsuario),
          IdStatusUsuario: parseInt(statusUsuario),
        })
          .done(() => {
            showAlert("Usuário alterado com sucesso!", "success");
            setTimeout(() => location.reload(), 1000);
          })
          .fail((erro) => {
            const mensagemErro =
              erro.responseJSON?.message ||
              erro.responseText ||
              "Erro desconhecido.";
            showAlert(mensagemErro, "error");
          });
      }
    });
}

function previewImageUpload() {
  $(document).on("change", "#file-upload", function () {
    const file = this.files[0];
    const previewImage = document.getElementById("preview-image");
    const container = document.getElementById("preview-container");
    const uploadContent = document.getElementById("upload-content");

    if (!file || !previewImage || !container) return;

    if (file.type.startsWith("image/")) {
      const reader = new FileReader();
      reader.onload = function (e) {
        previewImage.src = e.target.result;
        container.classList.remove("hidden");
        uploadContent.classList.add("hidden");
      };
      reader.readAsDataURL(file);
    } else {
      previewImage.src = "#";
      container.classList.add("hidden");
    }
  });
}

function pesquisarUsuario() {
  var usuario = $("#buscar-usuario").val().toLowerCase();
  console.log(usuario);
  $(".container-resultado").each(function () {
    var usuarioNome = $(this).find(".nome-usuario").text().toLowerCase();
    var usuarioEmail = $(this).find(".email-usuario").text().toLowerCase();
    var usuarioPapel = $(this).find(".papel-usuario").text().toLowerCase();
    var usuarioStatus = $(this).find(".status-usuario").text().toLowerCase();
    if (
      usuarioNome.includes(usuario) ||
      usuarioEmail.includes(usuario) ||
      usuarioPapel.includes(usuario) ||
      usuarioStatus.includes(usuario)
    ) {
      $(this).show();
    } else {
      $(this).hide();
    }
  });
}

function removerUsuario(url, usuarioId) {
  const modal = document.getElementById("modal-confirmarExclusao");
  modal.style.display = "block";
  // Remove listeners antigos para evitar múltiplos cliques
  $("#confirmar-exclusao").off("click");

  $("#confirmar-exclusao").on("click", function () {
    $.post(url)
      .done(function () {
        $(`#usuario-${usuarioId}`).remove();
        showAlert("Usuario removido com sucesso!", "error");
      })
      .fail(function () {
        showAlert("Erro ao remover usuario!", "error");
      })
      .always(() => (modal.style.display = "none"));
  });
}

module.exports = {
  init,
  abrirModalUsuario,
  fecharModal,
  cadastrarUsuario,
  pesquisarUsuario,
  removerUsuario,
  editarUsuario,
  previewImageUpload,
};
