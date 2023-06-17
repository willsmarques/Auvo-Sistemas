using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontoDepartamento.Entidades
{
    class ArquivoPonto : IComparable<ArquivoPonto>
    {
        public int Codigo { get; set; }
        public String Nome { get; set; }
        public double ValorHoras { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Entrada { get; set; }
        public TimeSpan Saida { get; set; }
        public TimeSpan Almoco { get; set; }

        public override int GetHashCode()
        {
            return Codigo.GetHashCode() + Data.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is ArquivoPonto))
            {
                return false;
            }
            ArquivoPonto other = obj as ArquivoPonto;

            return Codigo.Equals(other.Codigo) && Data.Equals(other.Data);
        }

        public int CompareTo(ArquivoPonto? other)
        {
            if (Codigo.CompareTo(other.Codigo) != 0)
                return Codigo.CompareTo(other.Codigo);

            return Data.CompareTo(other.Data);
        }
    }


}
