using ClubeLeitura.Telas;
using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaEmprestimo : TelaBase
    {
        private ControladorEmprestimo controladorEmprestimo;
        private ControladorAmigo controladorAmigo;
        private ControladorRevista controladorRevista;
        private TelaAmigo telaAmigo;
        private TelaRevista telaRevista;

        public TelaEmprestimo(ControladorEmprestimo controlador, ControladorAmigo cAmigo,
            ControladorRevista cRevista, TelaAmigo tAmigo, TelaRevista tRevista)
            : base("Cadastro de Emprestimo")
        {
            controladorAmigo = cAmigo;
            controladorEmprestimo = controlador;
            controladorRevista = cRevista;
            telaAmigo = tAmigo;
            telaRevista = tRevista;

        }
        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo empréstimo");
            Console.WriteLine("Digite 2 para registrar devolução");
            Console.WriteLine("Digite 3 para visualizar empréstimos fechados");
            Console.WriteLine("Digite 4 para visualizar empréstimos abertos");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
        public void RegistrarEmprestimo()
        {
            ConfigurarTela("Inserindo um novo emprestimo...");

            bool conseguiuGravar = GravarEmprestimo(0);

            if (conseguiuGravar)
                ApresentarMensagem("Emprestimo inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o emprestimo", TipoMensagem.Erro);
                RegistrarEmprestimo();
            }
        }
        public void EditarRegistro()
        {
            ConfigurarTela("Editando um emprestimo...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do emprestimo que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarEmprestimo(id);

            if (conseguiuGravar)
                ApresentarMensagem("Emprestimo editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o emprestimo", TipoMensagem.Erro);
                EditarRegistro();
            }
        }
        //public void ExcluirRegistro()
        //{
        //    ConfigurarTela("Excluindo um emprestimo...");

        //    VisualizarRegistros();

        //    Console.WriteLine();

        //    Console.Write("Digite o número do emprestimo que deseja excluir: ");
        //    int idSelecionado = Convert.ToInt32(Console.ReadLine());

        //    bool conseguiuExcluir = controladorEmprestimo.ExcluirEmprestimo(idSelecionado);

        //    if (conseguiuExcluir)
        //        ApresentarMensagem("Emprestimo excluído com sucesso", TipoMensagem.Sucesso);
        //    else
        //    {
        //        ApresentarMensagem("Falha ao tentar excluir o emprestimo", TipoMensagem.Erro);
        //        ExcluirRegistro();
        //    }
        //}
        public void RegistrarDevolucao()
        {
            Emprestimo emprestimo;
            ConfigurarTela("Registrando devolução...");
            VisualizarRegistrosEmprestimosAbertos();
            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarEmprestimosAbertos();
            if (emprestimos.Length == 0)
            {
                
            }else
            {
                Console.WriteLine("");

                Console.Write("Digite o id do emprestimo para devolver: ");
                int idEmprestimo = Convert.ToInt32(Console.ReadLine());
                //falta fazer validações
                emprestimo = controladorEmprestimo.SelecionarEmprestimoPorId(idEmprestimo);
                emprestimo.dataEntrega = DateTime.Now;
                emprestimo.revista.emprestada = false;
            }
                
        }
        public void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando emprestimos fechados...");

            string configuracaColunasTabela = "{0,-6} | {1,-15} | {2,-15} | {3,-6}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarTodosEmprestimos();

            if (emprestimos.Length == 0)
            {
                ApresentarMensagem("Nenhum emprestimos cadastrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < emprestimos.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   emprestimos[i].id, emprestimos[i].revista.tipoColecao, emprestimos[i].amigo.nome,
                   emprestimos[i].dataEntrega.ToShortDateString());
            }
        }
        public void VisualizarRegistrosEmprestimosFechados()
        {
            ConfigurarTela("Visualizando emprestimos fechados...");

            string configuracaColunasTabela = "{0,-6} | {1,-15} | {2,-15} | {3,-6}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarEmprestimosFechados();

            if (emprestimos.Length == 0)
            {
                ApresentarMensagem("Nenhum registro de emprestimo fechado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < emprestimos.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                   emprestimos[i].id, emprestimos[i].revista.tipoColecao, emprestimos[i].amigo.nome,
                   emprestimos[i].dataEntrega.ToShortDateString());
            }
        }
        public void VisualizarRegistrosEmprestimosAbertos()
        {
            ConfigurarTela("Visualizando emprestimos abertos...");

            string configuracaColunasTabela = "{0,-6} | {1,-15} | {2,-15} | {3,-25} | {4,-6}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarEmprestimosAbertos();

            if (emprestimos.Length == 0)
            {
                ApresentarMensagem("Nenhum emprestimo em aberto!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < emprestimos.Length; i++)
            {
                if (emprestimos[i] != null)
                {
                    Console.WriteLine(configuracaColunasTabela,
                   emprestimos[i].id, emprestimos[i].revista.tipoColecao, emprestimos[i].amigo.nome,
                   emprestimos[i].dataPrevistaEntrega.ToShortDateString(), emprestimos[i].dataEntrega.ToShortDateString());
                }

            }
        }
        #region métodos privados
        private static void MontarCabecalhoTabela(string configuracaoColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaoColunasTabela, "Id", "Revista", "Amigo", "Data prevista para Entrega", "Data Entrega");

            Console.WriteLine("----------------------------------------------------------------------------------");

            Console.ResetColor();
        }
        private bool GravarEmprestimo(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;
            Amigo amiguinho;
            Revista revistinha;

            telaAmigo.VisualizarRegistros();
            Console.WriteLine("");
            Console.Write("Digite o id do amigo que vai emprestar: ");
            int idAmigoEmprestimo = Convert.ToInt32(Console.ReadLine());

            amiguinho = controladorAmigo.SelecionarAmigoPorId(idAmigoEmprestimo);

            telaRevista.VisualizarRegistros();
            Console.WriteLine("");
            Console.Write("Digite o id da revistaque será emprestada: ");
            int idRevistaEmprestimo = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.Write("Digite o id do amigo que vai emprestar: "+ idAmigoEmprestimo);
            Console.Write("Digite o id da revistaque será emprestada: " + idRevistaEmprestimo);
            revistinha = controladorRevista.SelecionarRevistaPorId(idRevistaEmprestimo);

            Console.Write("Digite a data da entrega: ");
            DateTime dataPrevistaEntrega = Convert.ToDateTime(Console.ReadLine());


            resultadoValidacao = controladorEmprestimo.RegistrarEmprestimo(
                id, amiguinho, revistinha, dataPrevistaEntrega);

            if (resultadoValidacao != "EMPRESTIMO_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }
        #endregion
    }
}
