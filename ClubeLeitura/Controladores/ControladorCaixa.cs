using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Controladores
{
    public class ControladorCaixa : ControladorBase
    {
        public ControladorCaixa(int numRegistro):base (numRegistro)
        {

        }
        
        public string RegistrarCaixa(int id, string cor, string etiqueta)
        {
            Caixa caixa = null;

            int posicao;

            if (id == 0)
            {
                caixa = new Caixa();
                posicao = ObterPosicaoVazia();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Caixa(id));
                caixa = (Caixa)registros[posicao];
            }

            caixa.cor = cor;
            caixa.etiqueta = etiqueta;

            string resultadoValidacao = caixa.Validar();

            if (resultadoValidacao == "CAIXA_VALIDA")
                registros[posicao] = caixa;

            return resultadoValidacao;
        }

        public Caixa SelecionarCaixaPorId(int id)
        {
            return (Caixa)SelecionarRegistroPorId(new Caixa(id));
        }

        public bool ExcluirCaixa(int idSelecionado)
        {
            return true;// ExcluirRegistro(new Amigo(idSelecionado));
        }

        public Caixa[] SelecionarTodasCaixas()
        {
            Caixa[] caixasAux = new Caixa[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), caixasAux, caixasAux.Length);

            return caixasAux;
        }

    }
}