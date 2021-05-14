using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Controladores
{
    public class ControladorRevista : ControladorBase
    {
        ControladorCaixa controladorCaixa;
        public ControladorRevista(int capRegistro, ControladorCaixa crcx) : base (capRegistro)
        {
            controladorCaixa = crcx;
        }
        public Revista SelecionarRevistaPorId(int id)
        {
            return (Revista)SelecionarRegistroPorId(new Revista(id));
        }
        public string RegistrarRevista(int id, string numEdicao, string tipoColecao,
            string anoRevista, Caixa caixa)
        {
            Revista revista = null;

            int posicao;

            if (id == 0)
            {
                revista = new Revista();
                posicao = ObterPosicaoVazia();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Revista(id));
                revista = (Revista)registros[posicao];
            }

            revista.numEdicao = numEdicao;
            revista.tipoColecao = tipoColecao;
            revista.anoRevista = anoRevista;
            revista.caixa = caixa;

            string resultadoValidacao = revista.Validar();

            if (resultadoValidacao == "REVISTA_VALIDA")
                registros[posicao] = revista;

            return resultadoValidacao;
        }

        public bool ExcluirRevista(int idSelecionado)
        {
            return ExcluirRegistro(new Revista(idSelecionado));
        }

        public Revista[] SelecionarTodosRevistas()
        {
            Revista[] revistasAux = new Revista[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), revistasAux, revistasAux.Length);

            return revistasAux;
        }


    }
}
