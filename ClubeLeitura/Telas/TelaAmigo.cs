using ClubeLeitura.Telas;
using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaAmigo : TelaBase,ICadastravel
    {
        private ControladorAmigo controladorAmigo;
        public TelaAmigo(ControladorAmigo controlador)
            : base("Cadastro de Amigo")
        {
            controladorAmigo = controlador;
        }
        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo amigo");
            Console.WriteLine("Digite 2 para visualizar amigos");
            Console.WriteLine("Digite 3 para editar um amigo");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void EditarRegistro()
        {
            ConfigurarTela("Editando um amigo...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o id do amigo que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarAmigo(id);

            if (conseguiuGravar)
                ApresentarMensagem("Amigo editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o amigo", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        //public void ExcluirRegistro()
        //{
        //    throw new NotImplementedException();
        //}

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo amigo...");

            bool conseguiuGravar = GravarAmigo(0);

            if (conseguiuGravar)
                ApresentarMensagem("Amigo inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o Amigo", TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando amigos...");

            string configuracaColunasTabela = "{0,-6} | {1,-15} | {2,-15} | {3,-15} | {4,-15}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Amigo[] amigos = controladorAmigo.SelecionarTodosAmigos();

            if (amigos.Length == 0)
            {
                ApresentarMensagem("Nenhum amigo cadastrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < amigos.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   amigos[i].id, amigos[i].nome, amigos[i].responsavel, amigos[i].telefone, amigos[i].endereco);
            }
        }



        
        #region métodos privados
        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Nome", "Responsável", "Telefone", "Endereço");

            Console.WriteLine("----------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarAmigo(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string responsavel = Console.ReadLine();

            Console.Write("Digite o telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o endereço: ");
            string endereco = Console.ReadLine();

            resultadoValidacao = controladorAmigo.RegistrarAmigo(
                id, nome, responsavel, telefone, endereco);

            if (resultadoValidacao != "AMIGO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }



        #endregion
    }

}
