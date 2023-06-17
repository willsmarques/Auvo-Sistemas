using Newtonsoft.Json;
using System.Globalization;
using PontoDepartamento.Exceptions;

namespace PontoDepartamento.Entidades
{
    public class ProcessaArquivo
    {

        public void ProcessCsvFiles(string directoryPath)
        {
            string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv");

            if (csvFiles.Length > 0)
            {
                List<Departamento> ListDepartemento = new List<Departamento>();

                Parallel.ForEach(csvFiles, csvFile =>
                {
                    List<ArquivoPonto> records = LerArquivo(csvFile);
                    Departamento departamento = ProcessarDepartamento(records, csvFile);

                    lock (ListDepartemento)

                    {
                        //ListDepartemento.AddRange((IEnumerable<Departamento>)departamento);
                        ListDepartemento.Add(departamento);
                    }
                });

                string jsonOutput = JsonConvert.SerializeObject(ListDepartemento);

                string outputFilePath = Path.Combine(directoryPath, "TodosDepartamentos.json");
                File.WriteAllText(outputFilePath, jsonOutput);

                Console.WriteLine("Processamento concluído. Arquivo JSON gerado com sucesso.");
            }
            else
            {
                Console.WriteLine("Não foram encontrados arquivos CSV no diretório especificado.");
            }
        }
        /// <summary>
        /// Transforma o arquivo em uma lista Ordemada
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="DomainExeception"></exception>
        /// 
        private List<ArquivoPonto> LerArquivo(string enderecoArquivo)
        {
            List<ArquivoPonto> listArquivoPontos = new List<ArquivoPonto>();

            // ler o arquivo
            using (StreamReader reader = new StreamReader(enderecoArquivo))
            {
                string line;
                bool cabecalho = true;

                while ((line = reader.ReadLine()) != null)
                {
                    // Não incluir  o cabeçalho
                    if (cabecalho)
                    {
                        cabecalho = false;
                        continue;
                    }

                    string[] values = line.Split(';');

                    // Validar a quantidade de linhas
                    if (values.Length != 7)
                    {
                        throw new DomainExeception("Quantidade de campos envalido");
                    }

                    string[] _almoco = values[6].Split('-');
                    int _codigo = int.Parse(values[0].Trim());
                    string _nome = values[1].Trim();
                    double _valorHoras = double.Parse(values[2].Replace("R$", "").Replace(" ", ""), CultureInfo.GetCultureInfo("pt-BR"));// double.Parse(values[2].Replace("R$","").Trim(), CultureInfo.InvariantCulture);
                    DateTime _data = DateTime.ParseExact(values[3].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    TimeSpan _entrada = TimeSpan.Parse(values[4].Trim());
                    TimeSpan _saida = TimeSpan.Parse(values[5].Trim());
                    TimeSpan _almoco1 = (TimeSpan.Parse(_almoco[1].Trim()) - TimeSpan.Parse(_almoco[0].Trim()));

                    ArquivoPonto arquivoPonto = new ArquivoPonto
                    {
                        Codigo = _codigo,
                        Nome = _nome,
                        ValorHoras = _valorHoras,
                        Data = _data,
                        Entrada = _entrada,
                        Saida = _saida,
                        Almoco = _almoco1
                    };

                    listArquivoPontos.Add(arquivoPonto);
                }
            }
            //Ordenar o arquivo por código e Data 
            listArquivoPontos.Sort();

            return listArquivoPontos;
        }

        private Departamento ProcessarDepartamento(List<ArquivoPonto> lista, string Arquivo)
        {
            var idFuncionario = 0;
            Funcionarios funcionario = new Funcionarios();

            string nomeArquivo = Path.GetFileNameWithoutExtension(Arquivo);
            string[] valor = nomeArquivo.Split('-');

            // Validar nome do arquivo
            if (valor.Length != 3)
            {
                throw new DomainExeception("Nome do arquivo envalido.");
            }
            var departamento = new Departamento(valor[0], valor[1], valor[2]);

            foreach (ArquivoPonto arq in lista)
            {

                if (idFuncionario != arq.Codigo)
                {
                    if (idFuncionario != 0)
                    {
                        departamento.AddFuncionarios(funcionario);
                    }
                    funcionario = new Funcionarios(arq.Codigo, arq.Nome);
                    funcionario.CalculaPonto(arq.ValorHoras, arq.Entrada, arq.Saida, arq.Almoco);

                    //

                    idFuncionario = arq.Codigo;
                }
                else
                {
                    funcionario.CalculaPonto(arq.ValorHoras, arq.Entrada, arq.Saida, arq.Almoco);
                }

            }

            departamento.AddFuncionarios(funcionario);
            departamento.CalculaDepartamento();

            return departamento;

        }


    }

}

