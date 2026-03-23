using DbManager;
using System.Data.Common;
using Xunit;

namespace OurTests
{
    public class SelectTests
    {
        [Fact]
        public void TestSelectAllColumns()
        {
            Database database = Database.CreateTestDatabase();
            Table table = Table.CreateTestTable();

            database.AddTable(table);

            Select select = new Select("TestTable", new List<string> {Table.TestColumn1Name, Table.TestColumn2Name, Table.TestColumn3Name });
            string result = select.Execute(database);
            string expected = "['Name','Height','Age']{'Rodolfo','1.62','25'}{'Maider','1.67','67'}{'Pepe','1.55','51'}";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestSelectSpecificColumnsWithCondition()
        {
            Database database = Database.CreateTestDatabase();
            Table table = Table.CreateTestTable();

            database.AddTable(table);

            Condition condition = new Condition(Table.TestColumn3Name, "=","25");
            Select select = new Select("TestTable", new List<string> { Table.TestColumn1Name, Table.TestColumn3Name }, condition);
            string result = select.Execute(database);
            string expected = "['Name','Age']{'Rodolfo','25'}";
            Assert.Equal(expected, result);
        }
        
    }
}