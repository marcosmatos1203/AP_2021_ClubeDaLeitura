using System;

namespace GestaoEquipamentos.ConsoleApp.Dominio
{
    public class Revista
    {
        public int id;
        public string tipoColecao, numEdicao, anoRevista;
        public Caixa caixa;
        public bool emprestada=false;

        public Revista()
        {
            id = GeradorId.GerarId();
        }

        public Revista(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Validar()
        {
            string resultadoValidacao = "";

            //if (string.IsNullOrEmpty(titulo))
            //    resultadoValidacao += "O campo Nome é obrigatório \n";

            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "REVISTA_VALIDA";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Revista chamado = (Revista)obj;

            if (id == chamado.id)
                return true;
            else
                return false;
        }
    }
}
