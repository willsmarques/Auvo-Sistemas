

namespace Candidato.Models
{
    class Departamento
    {
        public string NomeDepartamento { get; set; }
        public string MesVigencia { get; set; }
        public string AnoVigencia { get; set; }
        public double TotalPagamento { get; set; }
        public double TotalDescontos { get; set; }
        public double TotalExtras { get; set; }
        public List<Funcionarios> Funcionarios { get; set; } = new List<Funcionarios>();

        public Departamento(string nomeDepartamento, string mesVigencia, string anoVigencia)
        {
            NomeDepartamento = nomeDepartamento;
            MesVigencia = mesVigencia;
            AnoVigencia = anoVigencia;
        }

        public void AddFuncionarios(Funcionarios funcionarios)
        {
            Funcionarios.Add(funcionarios);
        }

        public void RemoverFuncionarios(Funcionarios funcionarios)
        {
            Funcionarios.Remove(funcionarios);
        }

        public void CalculaDepartamento()
        {
            foreach (Funcionarios func in Funcionarios)
            {
                TotalPagamento += Math.Round(func.TotalReceber, 2);
                TotalDescontos += Math.Round(func.GetTotalDesconto(), 2);
                TotalExtras += Math.Round(func.GetTotalExtra(), 2);
            }
        }
    }
}
