using DbManager;

namespace OurTests
{
    public class UnitTest1
    {
        //TODO DEADLINE 1B : Create your own tests for Database
        /*
        [Fact]
        public void Test1()
        {

        }
        */

        [Fact]
        public void TestTableByName()
        {
             List<ColumnDefinition> col = new List<ColumnDefinition>();

            List<Table> table = new List<Table>()
            {
                new Table("table1", col),

            };

                       


        }
    }
}