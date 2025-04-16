function abrirModal() {
  document.getElementById("modal-usuario").style.display = "block";
}

function fecharModal() {
  document.getElementById("modal-usuario").style.display = "none";
  document.getElementById("form-modal").reset();
}

window.onclick = function (event) {
  const modal = document.getElementById("modal-usuario");
  if (event.target == modal) {
    modal.style.display = "none";
  }
};

function cadastrarUsuario() {}
