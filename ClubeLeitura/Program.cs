using ClubeLeitura.Telas;
using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
using GestaoEquipamentos.ConsoleApp.Telas;
using System;

namespace ClubeLeitura
{
    class Program
    {

        const int capRegistro = 2;
        
        static void Main(string[] args)
        {
            ControladorCaixa controladorCaixa = new ControladorCaixa(capRegistro);
            ControladorRevista controladorRevista = new ControladorRevista(capRegistro, controladorCaixa);
            ControladorEmprestimo controladorEmprestimo = new ControladorEmprestimo(capRegistro);
            ControladorAmigo controladorAmigo = new ControladorAmigo(capRegistro, controladorEmprestimo);
            TelaAmigo telaAmigo = new TelaAmigo(controladorAmigo);
            TelaBase telaBase = new TelaBase("");
            TelaCaixa telaCaixa = new TelaCaixa(controladorCaixa);
            TelaRevista telaRevista = new TelaRevista(controladorRevista, controladorCaixa, telaCaixa);
            
            TelaEmprestimo telaEmprestimo = new TelaEmprestimo(controladorEmprestimo, controladorAmigo,
                controladorRevista, telaAmigo, telaRevista);

            TelaPrincipal telaPrincipal = new TelaPrincipal(controladorRevista, controladorCaixa, controladorEmprestimo, controladorAmigo,
               telaCaixa, telaAmigo, telaRevista, telaEmprestimo);

            while (true)
            {
                TelaBase telaSelecionada = telaPrincipal.ObterTela();


                if (telaSelecionada == null)
                    break;

                Console.Clear();

                Console.WriteLine(telaSelecionada.Titulo); Console.WriteLine();

                string opcao = telaSelecionada.ObterOpcao();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (telaSelecionada is ICadastravel)
                {
                    ICadastravel telaCadastravel = (ICadastravel)telaSelecionada;

                    if (opcao == "1")
                        telaCadastravel.InserirNovoRegistro();

                    else if (opcao == "2")
                    {
                        telaCadastravel.VisualizarRegistros();
                        Console.ReadLine();
                    }

                    else if (opcao == "3")
                        telaCadastravel.EditarRegistro();

                    //else if (opcao == "4")
                    //    telaCadastravel.ExcluirRegistro();
                    Console.Clear();

                }
                else if (telaSelecionada is TelaEmprestimo)
                {
                    TelaEmprestimo telaEmprestimo1 = ((TelaEmprestimo)telaSelecionada);

                    if (opcao == "1")
                        telaEmprestimo1.RegistrarEmprestimo();

                    else if (opcao == "2")
                    {
                        telaEmprestimo1.RegistrarDevolucao();
                    }

                    else if (opcao == "3")
                    {
                        telaEmprestimo1.VisualizarRegistrosEmprestimosFechados();
                        Console.ReadLine();
                    }
                        

                    else if (opcao == "4")
                    {
                        telaEmprestimo1.VisualizarRegistrosEmprestimosAbertos();
                        Console.ReadLine();
                    }
                        

                    Console.Clear();

                }
            }
        }
       
    }

}
