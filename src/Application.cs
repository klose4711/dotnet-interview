using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

/**
 * Takes as a first argument a name of an executive and prints out which users 
 * work under him and what their departments, salaries and roles are.
 *
 * It collects data from several tables, correlates different user types by 
 * name and fills in the blanks if a user has missing information on a table.
 * Students don't get a salary even if they have other roles with values in 
 * their salary field. That is company policy and not negotiable.
 * 
 * Note:
 * This application is supposed to be a reporting type application. This means
 * that it doesn't concern itself with putting data into the database. It just 
 * reads it out and corrects inconsistencies (if needed) after the reads.
 *
 * Assignment:
 * 1. Read the codebase and understand.
 * 2. Things to think about:
 *    - Is this good code?
 *    - Is the output of the application suited for an executive tool?
 *    - How would you test this code?
 *    - Is the code intuitive?
 *    - Are there things that should be refactored to make the code more 
 *      understandable?
 *    - Are there things that should be refactored to make it easier to write
 *      unit tests focusing on only one part of the logic?
 *    - Are there error cases that must be dealt with?
 */
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

        public class Users : List<User> {};

        class Where 
        {
            public string where;
        }

        static void Main(string[] args)
        {
            new Application().runLogic(args[0]);
        }

        public readonly string TYPE_ONE = "1";
        public readonly string TYPE_TWO = "2";
        public readonly string TYPE_TRE = "3";
        public readonly string TYPE_FUR = "4";

        public void runLogic(string executive) 
        {
            string sql = "Select name, birth, dept FROM users WHERE  supervisor='"
                + executive + "';";
            Users mgrs = new SqlRequest("sql://users.db/users", TYPE_ONE)
                    .Execute<Users>(sql);

            foreach (User mgr in mgrs) 
            {
                Where where = new Where();
                addWhereFilter(where, mgr);
                Users testers = new SqlRequest("sql://users.db/users", TYPE_TWO)
                        .Execute<Users>("Select name, birth, dept FROM users" + where.where);
                Users devs = new SqlRequest("sql://users.db/users", TYPE_TRE)
                        .Execute<Users>("Select name, birth, dept FROM users" + where.where);
                Users students = new SqlRequest("sql://users.db/users", TYPE_FUR)
                        .Execute<Users>("Select name, birth, dept FROM users" + where.where);

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(
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
                    User existing = persons.GetValueOrDefault(p.name);
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
                    User existing = persons.GetValueOrDefault(p.name);
                    if (existing == null) 
                    {
                        persons.Add(p.name, p);
                    } 
                    else 
                    {
                        mergeUser(p, existing);
                    }
                    existing = persons.GetValueOrDefault(p.name);
                    existing.salary = 0;
                }
                Console.WriteLine("");
                Console.WriteLine("");

                foreach (User p in persons.Values) 
                {              
                    Regex rx = new Regex(@".*" + mgr.department + @".*");

                    if (rx.IsMatch(p.department))
                        Console.WriteLine(p);
                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
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
            Console.WriteLine("LOG: Merging: " + one + " into: " + two + " ");

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