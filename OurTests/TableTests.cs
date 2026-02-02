using DbManager;
using Xunit;

namespace OurTests
{
    public class TableTests
    {
        //TODO DEADLINE 1A : Create your own tests for Table
        [Fact]
        public void Test1()
        {
            
        }


        [Fact]
        public void TestAddRowAndGetRow()
        {
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "NamePrueba1"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

            Table table = new Table("nameprueba", col);

            List<String> listPrueba = new List<string>()
            {
                "Name",
                "2"
            };

            Row rowPrueba = new Row(col, listPrueba);

            table.AddRow(rowPrueba);
            Row result = table.GetRow(0);

            Assert.Same(rowPrueba, result);


        }

        [Fact]
        public void TestNumRows()
        {
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "NamePrueba1"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

            Table table = new Table("nameprueba", col);

             List<String> listPrueba = new List<string>()
            {
                "Name",
                "2"
            };

            Row rowPrueba = new Row(col, listPrueba);
            table.AddRow(rowPrueba);
            int result = table.NumRows();

            Assert.Equal(1, result);
        }
        
    }
}