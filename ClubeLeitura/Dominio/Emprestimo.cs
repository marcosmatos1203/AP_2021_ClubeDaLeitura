using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEquipamentos.ConsoleApp.Dominio
{
    public class Emprestimo
    {
        public int id;
        public Amigo amigo;
        public Revista revista;
        public DateTime dataEmprestimo, dataEntrega, dataPrevistaEntrega;

        public Emprestimo()
        {
            id = GeradorId.GerarId();
        }

        public Emprestimo(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Validar()
        {
            string resultadoValidacao = "";

            //if (string.IsNullOrEmpty(nome))
            //    resultadoValidacao += "O campo Nome é obrigatório \n";

            //if (nome.Length < 6)
            //    resultadoValidacao += "O campo Nome não pode ter menos de 6 letras \n";
            
            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "EMPRESTIMO_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Emprestimo emprestimo = (Emprestimo)obj;

            if (id == emprestimo.id)
                return true;
            else
                return false;
        }
    }
}
