namespace digispace.recruiting.interview
{
    /***
    * 
    * 
    *               THIS CLASS IS OUT OF SCOPE FOR THIS EXCERSIZE!
    * 
    * 
    */
    public class OutOfScopeSqlMock {

        static Application.User Add(Application.User user)
        {
            return user;
        }

        public static Application.Users Execute(string type, string sql) 
        {
            Application app = new Application();

            if (type.Equals(Application.TYPE_ONE)) 
            { 
                // Mgrs

                return (new Application.Users() 
                        {
                            Add(new Application.User("Adam", "01.05.1980", 85000, "Marketing")),
                            Add(new Application.User("Bob", "21.06.1977", 105000, "R&D")),
                            Add(new Application.User("Aubrey", "09.01.1978", 127000, "Sales")),
                            Add(new Application.User("Liam", "21.06.1987", 163000, "Engineering")),
                        });
            } 
            else if (type.Equals(Application.TYPE_TWO)) 
            { 
                // Tests

                return (new Application.Users() 
                        {
                            Add(new Application.User("Thomas", "01.05.1980", 40000, "Engineering")),
                            Add(new Application.User("Erik", "17.11.1983", 40000, "Engineering")),
                            Add(new Application.User("Sinclair", "21.06.1977", 50000, "R&D")),
                            Add(new Application.User("Ulf", null, 150000, "R&D")),
                        });
            } 
            else if (type.Equals(Application.TYPE_TRE)) 
            {
                // Devs

                return (new Application.Users()
                        {
                            Add(new Application.User("Emil", "07.08.1993", 140000, "Engineering")),
                            Add(new Application.User("Eduard", "22.10.1984", 90000, "Engineering")),
                            Add(new Application.User("Rolf", "01.05.1980", 100000, "R&D")),
                            Add(new Application.User("Ulf", "21.06.1977", 150000, "R&D")),
                            Add(new Application.User("Dimi", "09.01.1978", 127000, "Engineering")),
                            Add(new Application.User("Omri", "21.06.1987", 63000, "Engineering")),
                            Add(new Application.User("Stanton", "09.01.1990", 80000, "Sales")),
                            Add(new Application.User("Clint", "31.01.1999", 74000, "Engineering")),
                            Add(new Application.User("Sam", "27.05.1981", 63000, "Sales")),
                            Add(new Application.User("Sam", "27.05.1981", 63000, "Marketing"))
                        });
            } 
            else if (type.Equals(Application.TYPE_FUR)) 
            { 
                // Studs

                return (new Application.Users()
                        {
                            Add(new Application.User("Sandra", "29.03.1998", 0, "Marketing")),
                            Add(new Application.User("Venice", "11.07.1993", 0, "R&D")),
                            Add(new Application.User("Stanton", "09.01.1990", 0, "Sales")),
                            Add(new Application.User("Clint", "31.01.1999", 0, "Engineering")),
                            Add(new Application.User("Vatican", "13.06.2000", 0, "Engineering"))
                        });
            }

            return null;
        }
    }
}