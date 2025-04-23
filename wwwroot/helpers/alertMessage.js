function showAlert(message, status) {
  const alert = document.createElement("div");

  const baseClasses =
    "fixed top-4 right-8 p-4 mb-4 shadow-md z-[9999] rounded-md";

  const successClass = "bg-green-50 text-green-800";
  const errorClass = "bg-red-50 text-red-800";

  alert.className = `${baseClasses} ${
    status === "success" ? successClass : errorClass
  }`;

  alert.innerHTML = `
      <div class="flex">
        <div class="shrink-0">
          <svg class="size-5 ${
            status === "success" ? "text-green-800" : "text-red-800"
          } viewBox="0 0 20 20" fill="currentColor">
            <path fill-rule="evenodd" d="M10 18a8 8 0 1 0 0-16 8 8 0 0 0 0 16Zm3.857-9.809a.75.75 0 0 0-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 1 0-1.06 1.061l2.5 2.5a.75.75 0 0 0 1.137-.089l4-5.5Z" clip-rule="evenodd" />
          </svg>
        </div>
        <div class="ml-3">
          <p class="text-sm font-medium ${
            status === "success" ? "text-green-800" : "text-red-800"
          }">${message}</p>
        </div>
        <div class="ml-auto pl-3">
          <div class="-mx-1.5 -my-1.5">
            <button type="button" class="inline-flex rounded-md ${
              status === "success" ? "bg-green-50" : "bg-red-50"
            } p-1.5 ${
    status === "success"
      ? "text-green-800  hover:bg-green-100 "
      : "text-red-800  hover:bg-red-100 "
  }focus:ring-2focus:ring-green-600 focus:ring-offset-2 focus:ring-offset-green-50 focus:outline-hidden cursor-pointer" onclick="this.closest('.bg-green-50').remove()">
              <span class="sr-only">Dismiss</span>
              <svg class="size-5" viewBox="0 0 20 20" fill="currentColor">
                <path d="M6.28 5.22a.75.75 0 0 0-1.06 1.06L8.94 10l-3.72 3.72a.75.75 0 1 0 1.06 1.06L10 11.06l3.72 3.72a.75.75 0 1 0 1.06-1.06L11.06 10l3.72-3.72a.75.75 0 0 0-1.06-1.06L10 8.94 6.28 5.22Z" />
              </svg>
            </button>
          </div>
        </div>
      </div>
    `;

  alert.setAttribute("role", "alert");

  document.body.appendChild(alert);

  setTimeout(() => {
    alert.remove();
  }, 2000);
}
