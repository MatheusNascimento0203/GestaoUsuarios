const puppeteer = require("puppeteer");

async function gerarPDFUsuarios() {
  const browser = await puppeteer.launch({ headless: "new" });
  const page = await browser.newPage();

  await page.goto("http://localhost:5107/exportar-usuarios-pdf", {
    waitUntil: "networkidle0",
  });

  await page.pdf({
    path: "usuarios-relatorio.pdf",
    format: "A4",
    printBackground: true,
  });

  await browser.close();
}

gerarPDFUsuarios().catch(console.error);
