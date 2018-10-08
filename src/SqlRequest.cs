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
            Trace.WriteLine("LOG: Created a connection to DB "
                + this.connstrg + "!");
        }

        public Application.Users Execute(string sql)
        {

            Application app = new Application();

            if (type.Equals(Application.TYPE_ONE)) {
                sql = sql + "WHERE role = Manager";
                Trace.WriteLine("LOG: SELECTING " + sql);
            } 
            else if (type.Equals(Application.TYPE_TWO)) 
            {
                sql = sql + " AND role = Tester";
                Trace.WriteLine("LOG: SELECTING " + sql);
            } 
            else if (type.Equals(Application.TYPE_TRE)) 
            {
                sql = sql + " AND role = Dev";
                Trace.WriteLine("LOG: SELECTING " + sql);
            } 
            else if (type.Equals(Application.TYPE_FUR)) 
            {
                sql = sql + " AND role = Student";
                Trace.WriteLine("LOG: SELECTING " + sql);
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

            return OutOfScopeSqlMock.Execute(type, sql);
        }
    }
}