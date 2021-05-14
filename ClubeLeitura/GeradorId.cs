namespace GestaoEquipamentos.ConsoleApp
{
    public class GeradorId
    {
        private static int id = 0;

       
        public static int GerarId()
        {
            return ++id;
        }
       
    }
}