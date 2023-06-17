
using PontoDepartamento.Entidades;

namespace PontoDepartamento
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Informe o diretório dos arquivos CSV: ");
                string endereco = Console.ReadLine();

                if (Directory.Exists(endereco))
                {
                    ProcessaArquivo csvProcessor = new ProcessaArquivo();
                    csvProcessor.ProcessCsvFiles(endereco);
                }
                else
                {
                    Console.WriteLine("Diretório inválido.");
                }

                Console.WriteLine("Pressione qualquer tecla para sair.");
                Console.ReadKey();

            }
            catch (SystemException e)
            {

                Console.WriteLine("Erro ao Abrir Arquivo" + e.Message);
            }
            catch (ApplicationException e)
            {

                Console.WriteLine("Erro no Formatado dos dados" + e.Message);
            }

        }
    }
}