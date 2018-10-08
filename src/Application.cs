using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace digispace.recruiting.interview
{
    public class Application
    {
        public class User 
        {
            public string name;
            public string birth;
            public int salary;
            public string department;
            public string role;

            public User(string name, string birth, int salary, 
                    string department) 
            {
                this.name = name;
                this.birth = birth;
                this.salary = salary;
                this.department = department;
            }

            public override string ToString() {
                return name + " " + birth + " " + department + " " + salary + " "
                    + role;
            }
        }

        public class Users : System.Collections.Generic.List<User> {};

        class Where 
        {
            public string where;
        }

        static void Main(string[] args)
        {
            new Application().runLogic(args[0]);
        }

        public const string TYPE_ONE = "1";
        public const string TYPE_TWO = "2";
        public const string TYPE_TRE = "3";
        public const string TYPE_FUR = "4";

        public void runLogic(string executive) 
        {
            string sql = "Select name, birth, dept FROM users WHERE  supervisor='"
                + executive + "';";
            Users mgrs = new SqlRequest("sql://users.db/users", TYPE_ONE)
                    .Execute(sql);

            foreach (User mgr in mgrs) 
            {
                Where where = new Where();
                addWhereFilter(where, mgr);
                Users testers = new SqlRequest("sql://users.db/users", TYPE_TWO)
                        .Execute("Select name, birth, dept FROM users" + where.where);
                Users devs = new SqlRequest("sql://users.db/users", TYPE_TRE)
                        .Execute("Select name, birth, dept FROM users" + where.where);
                Users students = new SqlRequest("sql://users.db/users", TYPE_FUR)
                        .Execute("Select name, birth, dept FROM users" + where.where);

                Trace.WriteLine("");
                Trace.WriteLine("");
                Trace.WriteLine("");
                Trace.WriteLine(
                        "Manager " + mgr.name + " " + mgr.salary + " ("
                            + mgr.department + ")  manages:");

                Dictionary<string, User> persons = new Dictionary<string, User>();
                foreach (User p in testers) 
                {
                    p.role = "Tester";
                    persons.Add(p.name, p);
                }
                foreach (User p in devs) 
                {
                    p.role = "Developer";
                    User existing = persons[p.name];
                    if (existing == null) 
                    {
                        persons.Add(p.name, p);
                    } 
                    else 
                    {
                        mergeUser(p, existing);
                    }
                }
                foreach(User p in students) 
                {
                    p.role = "Student";
                    User existing = persons[p.name];
                    if (existing == null) 
                    {
                        persons.Add(p.name, p);
                    } 
                    else 
                    {
                        mergeUser(p, existing);
                    }
                    existing = persons[p.name];
                    existing.salary = 0;
                }
                Trace.WriteLine("");
                Trace.WriteLine("");

                foreach (User p in persons.Values) 
                {              
                    Regex rx = new Regex(@"\w*" + mgr.department + @"\w*");

                    if (rx.IsMatch(p.department))
                        Trace.WriteLine(p);
                }

                Trace.WriteLine("");
                Trace.WriteLine("");
                Trace.WriteLine("");
                Trace.WriteLine("");
            }
        }

        private void addWhereFilter(Where where, User user) 
        {
            where.where = " WHERE supervisor='";
            where.where = where.where + user.name;
            where.where = where.where + "'";
        }

        // Merges information from User one into user two
        private void mergeUser(User one, User two) 
        {
            Trace.WriteLine("LOG: Merging: " + one + " into: " + two + " ");

            two.name = one.name == null ? two.name : one.name;
            two.birth = one.birth == null ? two.birth : one.birth;
            two.salary = two.salary + one.salary;
            two.role = two.role + " " + one.role;
            if (!two.department.Equals(one.department)) 
            {
                two.department = two.department + " " + one.department;
            }
        }
    }
}