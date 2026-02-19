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
        public void TestAddTableAndTableByName()
        {
            Database database = Database.CreateTestDatabase();

            List<ColumnDefinition> col = new List<ColumnDefinition>();

            Table table1 = new Table("table1", col);
            Table table2 = new Table("table2", col);
            Table table3 = new Table("table3", col);

            database.AddTable(table1);
            database.AddTable(table2);
            database.AddTable(table3);            

            Table result = database.TableByName("table1");
            Assert.Equal("table1", result.Name);

            Table result2 = database.TableByName("table3");
            Assert.Equal("table3", result2.Name);

            Table result3 = database.TableByName("patata");
            Assert.Null(result3);
    
        }

        [Fact]
        public void TestDeleteWhere()
        {
            Database database = Database.CreateTestDatabase();

            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Nombre"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

            Table table = new Table("Test", col);
            database.AddTable(table);

            table.Insert(new List<string> {"Pepe", "69"});
            table.Insert(new List<string> {"Igor", "20"});
            table.Insert(new List<string> {"Kevin", "20"});
            table.Insert(new List<string> {"Oier", "50"});
            table.Insert(new List<string> {"Ainhoa", "5"});
            
            Condition condition = new Condition("Num", "=", "20");
            bool result = database.DeleteWhere("Test", condition);

            Assert.Equal(3, table.NumRows());
        }
    }
}