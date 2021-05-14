using ClubeLeitura.Telas;
using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaRevista : TelaBase, ICadastravel
    {
        private ControladorRevista controladorRevista;
        private ControladorCaixa controladorCaixa;
        private TelaCaixa telaCaixa;
        public TelaRevista(ControladorRevista cRevista, ControladorCaixa cCaixa, TelaCaixa tCaixa)
            : base("Cadastro de Revista")
        {
            controladorRevista = cRevista;
            controladorCaixa = cCaixa;
            telaCaixa = tCaixa;
        }
        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir nova revista");
            Console.WriteLine("Digite 2 para visualizar revistas");
            Console.WriteLine("Digite 3 para editar uma revista");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma nova revista...");

            bool conseguiuGravar = GravarRevista(0);

            if (conseguiuGravar)
                ApresentarMensagem("Revista inserida com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o revista", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma revista...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o id da revista que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarRevista(id);

            if (conseguiuGravar)
                ApresentarMensagem("Revista editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar a revista", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        //public void ExcluirRegistro()
        //{
        //    ConfigurarTela("Excluindo uma revista...");

        //    VisualizarRegistros();

        //    Console.WriteLine();

        //    Console.Write("Digite o número do id da revista que deseja excluir: ");
        //    int idSelecionado = Convert.ToInt32(Console.ReadLine());

        //    bool conseguiuExcluir = controladorRevista.ExcluirRevista(idSelecionado);

        //    if (conseguiuExcluir)
        //        ApresentarMensagem("Revista excluída com sucesso", TipoMensagem.Sucesso);
        //    else
        //    {
        //        ApresentarMensagem("Falha ao tentar excluir a revista", TipoMensagem.Erro);
        //        ExcluirRegistro();
        //    }
        //}

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando revistas...");

            string configuracaColunasTabela = "{0,-6} | {1,-15} | {2,-18} | {3,-15} | {4,-25} | {5,-8}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Revista[] revistas = controladorRevista.SelecionarTodosRevistas();

            if (revistas.Length == 0)
            {
                ApresentarMensagem("Nenhuma revista cadastrada!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < revistas.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   revistas[i].id, revistas[i].tipoColecao, revistas[i].numEdicao, revistas[i].anoRevista, revistas[i].caixa.etiqueta, revistas[i].caixa.cor);
            }
        }

        #region métodos privados
        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(configuracaoColunasTabela, "Id", "Tipo de Coleção", "Número da Edição", "Ano da Revista", "Etiqueta da caixa", "cor da caixa");

            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarRevista(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;
            Caixa caixinha;

            telaCaixa.VisualizarRegistros();
            Console.WriteLine("");
            Console.Write("Digite o id da caixa qual a revista vai ficar: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());
            caixinha = controladorCaixa.SelecionarCaixaPorId(idCaixa);

            Console.Write("Digite o Tipo de coleção: ");
            string tipoColecao = Console.ReadLine();

            Console.Write("Digite o número da edição: ");
            string numEdicao = Console.ReadLine();


            Console.Write("Digite o ano da revista: ");
            string anoRevista = Console.ReadLine();

            resultadoValidacao = controladorRevista.RegistrarRevista(
                id, numEdicao,tipoColecao, anoRevista, caixinha);

            if (resultadoValidacao != "REVISTA_VALIDA")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }



        #endregion
    }

}
