using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Controladores
{
    public class ControladorAmigo : ControladorBase
    {
        public ControladorEmprestimo controladorEmprestimo;

        public ControladorAmigo(int capRegistro, ControladorEmprestimo ce) : base(capRegistro)
        {
            controladorEmprestimo = ce;
        }

        public string RegistrarAmigo(int id, string nome, string responsavel, string telefone, string endereco)
        {
            Amigo amigo = null;

            int posicao;

            if (id == 0)
            {
                amigo = new Amigo();
                posicao = ObterPosicaoVazia();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Amigo(id));
                amigo = (Amigo)registros[posicao];
            }
            
            amigo.nome = nome;
            amigo.responsavel = responsavel;
            amigo.telefone = telefone;
            amigo.endereco = endereco;

            string resultadoValidacao = amigo.Validar();

            if (resultadoValidacao == "AMIGO_VALIDO")
                registros[posicao] = amigo;

            return resultadoValidacao;
        }

        //public bool ExcluirAmigo(int idSelecionado)
        //{
        //    return ExcluirRegistro(new Revista(idSelecionado));
        //}
        public Amigo SelecionarAmigoPorId(int id)
        {
            return (Amigo)SelecionarRegistroPorId(new Amigo(id));
        }
        public Amigo[] SelecionarTodosAmigos()
        {
            Amigo[] amigosAux = new Amigo[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), amigosAux, amigosAux.Length);

            return amigosAux;
        }


    }
}
