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
        public void CreateTableTets()
        {
            Database b = Database.CreateTestDatabase();
            List<ColumnDefinition> l1 = new List<ColumnDefinition>();
            List<ColumnDefinition> l2 = new List<ColumnDefinition>();
            List<ColumnDefinition> l3 = new List<ColumnDefinition>();
            l1.Add(new ColumnDefinition(ColumnDefinition.DataType.String, "Name"));
            l1.Add(new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"));
            l1.Add(new ColumnDefinition(ColumnDefinition.DataType.Double, "salary"));

            l2.Add(new ColumnDefinition(ColumnDefinition.DataType.String, "Name"));
            l2.Add(new ColumnDefinition(ColumnDefinition.DataType.Int, "NSSE"));
            l2.Add(new ColumnDefinition(ColumnDefinition.DataType.Double, "Weight"));

            Assert.True(b.CreateTable("n1",l1));
            Assert.False(b.CreateTable("n1",l1));
            Assert.False(b.CreateTable("",l1));
            Assert.False(b.CreateTable("n3",l3));

        }
    }
}