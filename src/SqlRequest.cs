using System;
using System.Diagnostics;

namespace digispace.recruiting.interview
{

    public class SqlRequest 
    {
        private readonly string connstrg;
        private readonly string type;

        public SqlRequest(string connstrg, string type) 
        {
            this.connstrg = connstrg;
            this.type = type;
            Console.WriteLine("LOG: Created a connection to DB "
                + this.connstrg + "!");
        }

        public Application.Users Execute<T>(string sql)
        {

            Application app = new Application();

            if (type.Equals(app.TYPE_ONE)) {
                sql = sql + "WHERE role = Manager";
                Console.WriteLine("LOG: SELECTING " + sql);
            } 
            else if (type.Equals(app.TYPE_TWO)) 
            {
                sql = sql + " AND role = Tester";
                Console.WriteLine("LOG: SELECTING " + sql);
            } 
            else if (type.Equals(app.TYPE_TRE)) 
            {
                sql = sql + " AND role = Dev";
                Console.WriteLine("LOG: SELECTING " + sql);
            } 
            else if (type.Equals(app.TYPE_FUR)) 
            {
                sql = sql + " AND role = Student";
                Console.WriteLine("LOG: SELECTING " + sql);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////
            //
            // The scope of this exercise ends here!
            //
            //
            //
            // Assume that this actually does IO now by reaching out to an SQL DB over
            // the network and parses the response
            //
            ///////////////////////////////////////////////////////////////////////////////////////////

            return OutOfScopeSqlMock.Execute<T>(type, sql);
        }
    }
}