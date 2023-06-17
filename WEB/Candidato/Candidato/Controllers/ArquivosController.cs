using Microsoft.AspNetCore.Mvc;
using Candidato.Models;
using Candidato.Servico;
using System.Diagnostics;

namespace Candidato.Controllers
{
    public class ArquivosController : Controller
    {

        public IActionResult Index()
        {
            ViewData["Title"] = "Processa Arquivo";
            return View();
        }

        [HttpGet]
        public IActionResult Departamento(string? endereco)
        {
            if (Directory.Exists(endereco))
            {
                ProcessaArquivo csvProcessor = new ProcessaArquivo();
                csvProcessor.ProcessCsvFilesAsync(endereco);
            }
            else
            {
                return RedirectToAction(nameof(Error), new { message = ("Diretório inválido.") });
            }

            string arquivo = Path.Combine(endereco, "TodosDepartamentos.json");

            try
            {

                using (StreamReader sr = new StreamReader(arquivo))
                {
                    string json = sr.ReadToEnd();
                    List<Departamento> departamento = System.Text.Json.JsonSerializer.Deserialize<List<Departamento>>(json);
                    return View(departamento);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Error), new { message = ("Ocorreu um erro ao ler o arquivo JSON:" + ex.Message) });
            }

        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
