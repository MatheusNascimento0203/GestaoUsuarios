document.addEventListener("DOMContentLoaded", function () {
  previewImageUpload("file-upload", "preview-image", "preview-container");
});
function abrirModal() {
  document.getElementById("modal-usuario").style.display = "block";
}

function fecharModal() {
  document.getElementById("modal-usuario").style.display = "none";
  document.getElementById("form-modal").reset();

  document.getElementById("preview-container").classList.add("hidden");
  document.getElementById("preview-image").src = "#";
  document.getElementById("upload-content").classList.remove("hidden");

  document.getElementById("file-upload").value = "";
}

window.onclick = function (event) {
  const modal = document.getElementById("modal-usuario");
  if (event.target == modal) {
    modal.style.display = "none";
  }
};

// function cadastrarUsuario() {
//   const nome = $("#nome").val();
//   const email = $("#email").val();
// }

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
