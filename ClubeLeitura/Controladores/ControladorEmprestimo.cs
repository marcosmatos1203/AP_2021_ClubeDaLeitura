using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Controladores
{
    public class ControladorEmprestimo : ControladorBase
    {
        public ControladorEmprestimo(int capRegistro) :base(capRegistro)
        {
        }

        public string RegistrarEmprestimo(int id, Amigo amigo, Revista revista, DateTime dataPrevistaEntrega)
        {
            Emprestimo emprestimo = null;
            
            int posicao;

            if (id == 0)
            {
                emprestimo = new Emprestimo();
                posicao = ObterPosicaoVazia();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Emprestimo(id));
                emprestimo = (Emprestimo)registros[posicao];
            }

            emprestimo.amigo = amigo;
            emprestimo.revista = revista;
            emprestimo.dataEmprestimo = DateTime.Now;
            emprestimo.dataEntrega = dataPrevistaEntrega;
            emprestimo.revista.emprestada = true;

            string resultadoValidacao = emprestimo.Validar();

            if (resultadoValidacao == "EMPRESTIMO_VALIDO")
                registros[posicao] = emprestimo;

            for (int i = 0; i < amigo.emprestimo.Length; i++)
            {
                if (amigo.emprestimo[i] == null)
                {
                    amigo.emprestimo[i] = emprestimo;
                    break;
                }
                   
            }
            

            return resultadoValidacao;
        }

        //public bool ExcluirEmprestimo(int idSelecionado)
        //{
        //    return ExcluirRegistro(new Emprestimo(idSelecionado));
        //}
        public Emprestimo SelecionarEmprestimoPorId(int id)
        {
            return (Emprestimo)SelecionarRegistroPorId(new Emprestimo(id));
        }
        public Emprestimo[] SelecionarTodosEmprestimos()
        {
            Emprestimo[] emprestimosAux = new Emprestimo[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), emprestimosAux, emprestimosAux.Length);

            return emprestimosAux;
        }
        public Emprestimo[] SelecionarEmprestimosFechados() 
        {
            Emprestimo[] emprestimosFechados = SelecionarTodosEmprestimos();
            Emprestimo[] todosEmprestimos = emprestimosFechados;

            for (int i = 0; i < todosEmprestimos.Length; i++)
            {
                if (todosEmprestimos[i]!=null)
                {
                    if (todosEmprestimos[i].revista.emprestada==false)
                        emprestimosFechados[i] = todosEmprestimos[i];
                }
                
            }

            return emprestimosFechados;
        }
        public Emprestimo[] SelecionarEmprestimosAbertos()
        {
            Emprestimo[] emprestimosAbertos = SelecionarTodosEmprestimos();
            Emprestimo[] emprestimosAux = emprestimosAbertos;

            for (int i = 0; i < emprestimosAux.Length; i++)
            {
                if (emprestimosAux[i].revista.emprestada)
                    emprestimosAbertos[i] = emprestimosAux[i];
            }

            return emprestimosAbertos;
        }

    }
}
