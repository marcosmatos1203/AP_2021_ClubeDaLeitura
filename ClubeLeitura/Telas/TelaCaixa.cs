using ClubeLeitura.Telas;
using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaCaixa : TelaBase, ICadastravel
    {
        private ControladorCaixa controladorCaixa;

        public TelaCaixa(ControladorCaixa cc) : base("Cadastro de Caixa")
        {

            controladorCaixa = cc;
        }
        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir nova caixa");
            Console.WriteLine("Digite 2 para visualizar caixas");
            Console.WriteLine("Digite 3 para editar uma caixa");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma nova caixa...");

            bool conseguiuGravar = GravarCaixa(0);

            if (conseguiuGravar)
                ApresentarMensagem("Caixa inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o caixa", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        //public void ExcluirRegistro()
        //{

        //}

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma caixa...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número da caixa que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuEditar = GravarCaixa(idSelecionado);

            if (conseguiuEditar)
                ApresentarMensagem("Caixa editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar a caixa", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando caixas...");

            MontarCabecalhoTabela();

            Caixa[] caixas = controladorCaixa.SelecionarTodasCaixas();

            if (caixas.Length == 0)
            {
                ApresentarMensagem("Nenhuma caixa registrada!", TipoMensagem.Atencao);
                return;
            }

            foreach (Caixa caixa in caixas)
            {
                Console.WriteLine("{0,-10} | {1,-20} | {2,-20}",
                    caixa.id, caixa.etiqueta, caixa.cor);
            }
        }

        #region Métodos Privados
        private static void MontarCabecalhoTabela()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-20} | {2,-20} ", "Id", "Etiqueta", "cor");

            Console.WriteLine("-------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarCaixa(int idCaixaSelecionado)
        {

            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();


            string resultadoValidacao = controladorCaixa.
                RegistrarCaixa(idCaixaSelecionado, cor, etiqueta);

            bool conseguiuGravar = true;

            if (resultadoValidacao != "CAIXA_VALIDA")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }

        #endregion
    }
}
