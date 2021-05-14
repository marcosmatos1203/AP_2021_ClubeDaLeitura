using GestaoEquipamentos.ConsoleApp.Controladores;
using System;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaPrincipal:TelaBase
    {
        ControladorRevista controladorRevista;
        ControladorCaixa controladorCaixa;
        ControladorEmprestimo controladorEmprestimo;
        ControladorAmigo controladorAmigo;
        TelaCaixa telaCaixa;
        TelaAmigo telaAmigo;
        TelaRevista telaRevista;
        TelaEmprestimo telaEmprestimo;

        public TelaPrincipal(ControladorRevista crrv, ControladorCaixa crcx, ControladorEmprestimo crep, ControladorAmigo
            crag, TelaCaixa tlcx, TelaAmigo tlag, TelaRevista tlrv, TelaEmprestimo tlep):base("Tela Principal")
        {
            controladorRevista = crrv;
            controladorCaixa = crcx;
            controladorEmprestimo = crep;
            controladorAmigo = crag;
            telaCaixa = tlcx;
            telaAmigo = tlag;
            telaRevista = tlrv;
            telaEmprestimo = tlep;
        }

        public TelaBase ObterTela()
        {
            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.Clear();

                Console.WriteLine("Digite 1 para o Cadastro de Caixa");
                Console.WriteLine("Digite 2 para o Cadastro de Amigo");
                Console.WriteLine("Digite 3 para o Cadastro de Revista");
                Console.WriteLine("Digite 4 para o Controle de emprestimos");

                Console.WriteLine("Digite S para Sair");

                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = new TelaCaixa(controladorCaixa);
                else if (opcao == "2")
                    telaSelecionada = new TelaAmigo(controladorAmigo);

                else if (opcao == "3")
                    telaSelecionada = new TelaRevista(controladorRevista, controladorCaixa, telaCaixa);

                else if (opcao == "4")
                    telaSelecionada = new TelaEmprestimo(controladorEmprestimo,controladorAmigo,controladorRevista,
                        telaAmigo, telaRevista);


                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "S" && opcao != "s")
            {
                Console.WriteLine("Opção inválida");
                Console.ReadLine();
                return true;
            }
            else
                return false;
        }
    }
}
