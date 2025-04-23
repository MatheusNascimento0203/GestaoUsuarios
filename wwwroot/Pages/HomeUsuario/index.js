document.addEventListener("DOMContentLoaded", function () {
  previewImageUpload("file-upload", "preview-image", "preview-container");
});
function abrirModal(tipoModal) {
  if (tipoModal === "cadastrar") {
    document.getElementById("modal-usuario").style.display = "block";
    return;
  }
}

function fecharModal(tipoModal) {
  if (tipoModal === "cadastrar") {
    document.getElementById("modal-usuario").style.display = "none";
    document.getElementById("form-modal").reset();

    document.getElementById("preview-container").classList.add("hidden");
    document.getElementById("preview-image").src = "#";
    document.getElementById("upload-content").classList.remove("hidden");

    document.getElementById("file-upload").value = "";
    return;
  }
}

window.onclick = function (event) {
  const modal = document.getElementById("modal-usuario");
  if (event.target == modal) {
    modal.style.display = "none";
  }
};

function cadastrarUsuario(url) {
  const nome = $("#nome").val().trim();
  const email = $("#email").val().trim();
  const papelUsuario = $("#papelUsuario").val();
  const statusUsuario = $("#statusUsuario").val();
  const fileInput = document.getElementById("file-upload");
  const file = fileInput.files[0];

  if (nome == "") {
    showAlert("Preencha o nome do usuário!", "error");
    return;
  }

  if (email == "") {
    showAlert("Preencha o email do usuário!", "error");
    return;
  }

  if (file == "") {
    showAlert("Selecione uma imagem para o usuário!", "error");
    return;
  }

  const reader = new FileReader();

  reader.onload = function (e) {
    const base64String = e.target.result;

    const objUsuario = {
      Nome: nome,
      Email: email,
      Foto: base64String,
      IdPapelUsuario: parseInt(papelUsuario),
      IdStatusUsuario: parseInt(statusUsuario),
    };

    $.ajax({
      url: url,
      type: "POST",
      data: JSON.stringify(objUsuario),
      contentType: "application/json",
      success: function () {
        showAlert("Usuário cadastrado com sucesso!", "success");
        setTimeout(() => location.reload(), 1000);
      },
      error: function (xhr) {
        const errorMsg =
          xhr.responseJSON?.message || "Erro ao adicionar usuário!";
        showAlert(errorMsg, "error");
        console.error("Detalhes do erro:", xhr.responseJSON);
      },
    });
  };
  reader.readAsDataURL(file);
}

function previewImageUpload(inputId, previewImageId, containerId) {
  const inputFile = document.getElementById(inputId);
  const previewImage = document.getElementById(previewImageId);
  const container = document.getElementById(containerId);
  const uploadContent = document.getElementById("upload-content");

  if (!inputFile || !previewImage || !container) return;

  inputFile.addEventListener("change", function () {
    const file = this.files[0];
    if (file && file.type.startsWith("image/")) {
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

let modalEditar = false;
let usuarioUrlAtual = "";
let usuarioIdAtual = null;

function editarUsuario(url, usuarioId) {}
