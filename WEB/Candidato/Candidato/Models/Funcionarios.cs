using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidato.Models
{
    class Funcionarios
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double TotalReceber { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double HorasExtras { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double HorasDebito { get; set; }
        public int DiasFalta { get; set; }
        public int DiasExtras { get; set; }
        public double DiasTrabalhados { get; set; }
        private double TotalHorasTrabalhada;
        private double TotalDesconto;
        private double TotalExtra;
        private double Extras;
        private double Debito;
        const int _horasDiaTrabalho = 8;

        public Funcionarios()
        {
        }

        public Funcionarios(int codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
        }

        public void CalculaPonto(double valorHoras, TimeSpan entrada, TimeSpan saida, TimeSpan almoco)
        {
            double horasTrabalhadas = saida.Subtract(entrada.Add(almoco)).TotalHours;

            TotalReceber += Math.Round((horasTrabalhadas * valorHoras), 2);
            TotalHorasTrabalhada += horasTrabalhadas;

            // horas extras ou débito feita no dia
            double horasExtrasDebito = horasTrabalhadas - _horasDiaTrabalho;

            if (horasExtrasDebito >= 0)
            {
                Extras += horasExtrasDebito;
                TotalExtra += Math.Round((valorHoras * horasExtrasDebito), 2);
            }
            else
            {
                Debito += horasExtrasDebito;
                TotalDesconto += Math.Round(horasExtrasDebito * valorHoras, 2);
            }

            SetHorasExtras();
            SetHorasDebito();
            SetDiasFalta();
            SetDiasExtras();
            SetDiasTrabalhados();
        }

        private void SetHorasExtras()
        {
            HorasExtras = (int)Extras % _horasDiaTrabalho;
        }

        private void SetHorasDebito()
        {
            HorasDebito = (int)Debito % _horasDiaTrabalho;
        }

        private void SetDiasFalta()
        {
            DiasFalta = (int)Debito / _horasDiaTrabalho;
        }

        private void SetDiasExtras()
        {
            DiasExtras = (int)Extras / _horasDiaTrabalho;
        }

        private void SetDiasTrabalhados()
        {
            DiasTrabalhados = (int)(TotalHorasTrabalhada / _horasDiaTrabalho);
        }

        public double GetTotalDesconto()
        {
            return TotalDesconto;
        }

        public double GetTotalExtra()
        {
            return TotalExtra;
        }
    }


}
