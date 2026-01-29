using DbManager;

namespace OurTests
{
    public class RowTests
    {
        //TODO DEADLINE 1A : Create your own tests for Row
        /*
        [Fact]
        public void Test1()
        {

        }
        */
        [Fact]
        public void Test1() {
            List<ColumnDefinition> column = new List<ColumnDefinition>()
            {
                new columnDefinition(ColumnDefinitionsTests.DataType.String, "Name"),
                new columnDefinition(ColumnDefinitionsTests.DataType.Int, "Age")
                };
            List<string> rowValues = new List<string>()
            {
                "Borja", "27"
            };
            Row testRow = new Row(column, rowValues);
            Assert.Equal("Borja", testRow.GetValue("Name"));
            Assert.Equal("27",testRow.GetValue("Age"));
            Assert.Null(testRow.GetValue("Nombre"));
        }
    }
}