using PontoDepartamento.Entidades;
using NUnit.Framework.Internal;

namespace PontoDepartamentoTeste
{
    [TestFixture]
    public class ProcessaArquivoTests
    {
        private string _testDirectoryPath;
        ProcessaArquivo _processaArquivo = new ProcessaArquivo();

        [SetUp]
        public void Setup()
        {
            _testDirectoryPath = $"C:\\Arquivo Import";
            Directory.CreateDirectory(_testDirectoryPath);
        }

        /*[TearDown]
        public void TearDown()
        {
            Directory.Delete(_testDirectoryPath, true);
        }*/

        [Test]
        public void TestDiretorioCriarCsv()
        {
            // Arrange
            string directoryPath = _testDirectoryPath;
            string csvFilePath = Path.Combine(directoryPath, "Arquivo.csv");
            string csvContent = "1;Diego;10.0;01/01/2021;08:00:00;18:00:00;12:00:00";
            File.WriteAllText(csvFilePath, csvContent);

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Assert
                Assert.True(File.Exists(directoryPath + "/Arquivo.csv"));
            }

            // Cleanup
            File.Delete(csvFilePath);
        }

        [Test]
        public void TestProcessArquivoJSON()
        {
            // Arrange
            string directoryPath = $"C:\\Arquivo Import";

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _processaArquivo.ProcessCsvFiles(directoryPath);

                // Assert
                Assert.True(File.Exists(directoryPath + "/TodosDepartamentos.json"));
            }
        }

        public static implicit operator ProcessaArquivoTests(ProcessaArquivo v)
        {
            throw new NotImplementedException();
        }
    }

}