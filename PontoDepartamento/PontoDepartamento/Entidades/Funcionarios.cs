using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoDepartamento.Entidades
{
    class Funcionarios
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double TotalReceber { get; set; }
        public double HorasExtras { get; set; }
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

            double _horasTrabalhadas = 0;


            TotalReceber += Math.Round(((saida.TotalHours - (entrada.TotalHours + almoco.TotalHours)) * valorHoras), 2);
            TotalHorasTrabalhada += (saida.TotalHours - (entrada.TotalHours + almoco.TotalHours));


            /// horas extras ou debito feita no dia
            _horasTrabalhadas = ((saida.TotalHours - (entrada.TotalHours + almoco.TotalHours)) - _horasDiaTrabalho);
            if (_horasTrabalhadas >= 0)
            {
                Extras += _horasTrabalhadas;
                TotalExtra += Math.Round((valorHoras * _horasTrabalhadas), 2);

            }
            else
            {
                Debito += _horasTrabalhadas;
                TotalDesconto += Math.Round(_horasTrabalhadas * valorHoras, 2);
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

        public double getTotalDesconto()
        {
            return TotalDesconto;
        }

        public double getTotalExtra()
        {
            return TotalExtra;
        }

    }


}
