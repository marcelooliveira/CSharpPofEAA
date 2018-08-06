using System;

namespace P01._01.Transaction_Script
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    //class Gateway
    //{
    //    public ResultSet buscaLangamentoPor(long IDdoContrato, DateTime aPartirDe)
    //    {
    //        PreparedStatement dec = db.prepareStatement(declaragéoBuscaLangamentos);
    //            dec.setLong(l, IDcontrato);
    //        dec.setDate(2, aPartirDe);
    //        ResultSet resultado = dec.executeQuery(l);
    //        return resultado;
    //    }
    //    //private static final String declaragéoBuscaLangamentos =
    //    //"SELECT quantia " +
    //    //" FROM langamentosDeReceitas " +
    //    //" WHERE contrato = ? AND lanqadoEm <= ?";
    //    //private Connection db;
    //}

    //internal class PreparedStatement
    //{
    //    internal ResultSet executeQuery(object l)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    internal void setDate(int v, DateTime aPartirDe)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class ResultSet
    //{
    //}
}
