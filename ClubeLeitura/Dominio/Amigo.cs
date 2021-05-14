using System;

namespace GestaoEquipamentos.ConsoleApp.Dominio
{
    public class Amigo
    {
        public int id;
        public string nome, responsavel, telefone, endereco;
        public Emprestimo[] emprestimo = new Emprestimo[10];

        public Amigo()
        {
            id = GeradorId.GerarId();
        }

        public Amigo(int idSelecionado)
        {
            id = idSelecionado;
        }

        public string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(nome))
                resultadoValidacao += "O campo Nome é obrigatório \n";
            if (string.IsNullOrEmpty(responsavel))
                resultadoValidacao += "O campo responsável é obrigatório \n";
            if (string.IsNullOrEmpty(telefone))
                resultadoValidacao += "O campo telefone é obrigatório \n";
            if (string.IsNullOrEmpty(endereco))
                resultadoValidacao += "O campo endereço é obrigatório \n";


            if (string.IsNullOrEmpty(resultadoValidacao))
                resultadoValidacao = "AMIGO_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            Amigo amigo = (Amigo)obj;

            if (id == amigo.id)
                return true;
            else
                return false;
        }
    }
}