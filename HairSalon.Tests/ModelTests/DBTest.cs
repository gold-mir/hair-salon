using HairSalon;

namespace HairSalon.Tests
{
    public abstract class DBTest
    {
        public DBTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=miranda_keith_test";
        }
    }
}
