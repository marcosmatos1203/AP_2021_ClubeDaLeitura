using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEquipamentos.ConsoleApp.Dominio
{
    public class Caixa
    {
        public int id;
        public string cor, etiqueta;
        //Revista[] revista;

        public Caixa()
        {
            id = GeradorId.GerarId();
        }

        public Caixa(int id)
        {
            this.id = id;
        }
        public string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(cor))
                resultadoValidacao += "O campo cor é obrigatório \n";

            if (string.IsNullOrEmpty(etiqueta))
                resultadoValidacao += "O campo responsável é obrigatório \n";
          
            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "CAIXA_VALIDA";

            return resultadoValidacao;
        }
        public override bool Equals(object obj)
        {
            Caixa caixa = (Caixa)obj;

            if (id == caixa.id)
                return true;
            else
                return false;
        }
       
    }
}
