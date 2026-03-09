using DbManager;
using DbManager.Parser;
using System.Threading.Channels;

namespace OurTests
{
    public class DatabaseTest
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

            table.Insert(new List<string> { "Pepe", "69" });
            table.Insert(new List<string> { "Igor", "20" });
            table.Insert(new List<string> { "Kevin", "20" });
            table.Insert(new List<string> { "Oier", "50" });
            table.Insert(new List<string> { "Ainhoa", "5" });

            Condition condition = new Condition("Num", "=", "20");
            bool result = database.DeleteWhere("Test", condition);

            Assert.Equal(3, table.NumRows());
        }

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

            Assert.True(b.CreateTable("n1", l1));
            Assert.False(b.CreateTable("n1", l1));
            Assert.False(b.CreateTable("", l1));
            Assert.False(b.CreateTable("n3", l3));

        }
        [Fact]
        public void UpdateTest()
        {
            Database b = Database.CreateTestDatabase();
            Table table = b.TableByName("TestTable");
            List<SetValue> set = new List<SetValue> {new SetValue("Age", "21")
    };
            string nf = table.GetRow(0).GetValue("Name");
            Condition condi = new Condition("Name", "=", nf);
            string orig= table.GetRow(1).GetValue("Age");
            bool result = b.Update(table.Name, set, condi);
            Assert.True(result);
            Assert.Equal("21", table.GetRow(0).GetValue("Age"));
            Assert.Equal(orig, table.GetRow(1).GetValue("Age"));

        }

            [Fact]
        public void UpdateTestNotTable()
        {
            Database b = Database.CreateTestDatabase();
            var set = new List<SetValue> { new SetValue("Age", "21") };
            var condi = new Condition("Age", "=", "30");
            bool result = b.Update("Doesn't exist", set, condi);
            Assert.False(result);
        }
        [Fact]
        public void UpdateTestNotColumn()
        {
            Table table= Table.CreateTestTable();
            Database b = Database.CreateTestDatabase();
            var set = new List<SetValue> { new SetValue("Age", "21") };
            var condi = new Condition("Ages", "=", "25");
            bool result = b.Update(table.Name, set, condi);
            Assert.False(result);
        }

        [Fact]
        public void TestSaveAndLoad()
        {
            Database database = Database.CreateTestDatabase();
            string databaseName = "test1";

            bool saved = database.Save(databaseName);
            Assert.True(saved);

            Database loadedData = Database.Load(databaseName, Database.AdminUsername, Database.AdminPassword);
            Assert.NotNull(loadedData);

            Table origTable = database.TableByName("test1");
            Table loadTable = loadedData.TableByName("test1");
            Assert.NotNull(origTable);
            Assert.NotNull(loadTable);

            Assert.Equal(origTable.NumColumns(), loadTable.NumColumns());
            Assert.Equal(origTable.NumRows(), loadTable.NumRows());

            for(int i = 0; i < origTable.NumColumns(); i++)
            {
                Assert.Equal(origTable.GetColumn(i).Name, loadTable.GetColumn(i).Name);
                Assert.Equal(origTable.GetColumn(i).Type, loadTable.GetColumn(i).Type);
            }
        }
        
        [Fact]
        public void TestSelect()
        {
            Database database = Database.CreateTestDatabase();

            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Nombre"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

            Table table = new Table("Test", col);
            database.AddTable(table);

            table.Insert(new List<string> { "Pepe", "69" });
            table.Insert(new List<string> { "Igor", "20" });
            table.Insert(new List<string> { "Kevin", "20" });
            table.Insert(new List<string> { "Oier", "50" });
            table.Insert(new List<string> { "Ainhoa", "5" });

            List<string> columns = new List<string> { "Nombre", "Num" };
            Condition condition = new Condition("Num", "=", "20");
            Table result = database.Select("Test", columns, condition);

            Assert.Equal(2, result.NumRows());
            Assert.Equal(2, result.NumColumns());
            Assert.Equal("Igor", result.GetRow(0).Values[0]);
            Assert.Equal("20", result.GetRow(0).Values[1]);
            Assert.Equal("Kevin", result.GetRow(1).Values[0]);
            Assert.Equal("20", result.GetRow(1).Values[1]);

            Table nullResult = database.Select("NoExiste", columns, condition);
            Assert.Null(nullResult);
        }

        [Fact]
        public void TestInsert()
        {
            Database database = Database.CreateTestDatabase();

            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Nombre"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

            Table table = new Table("Test", col);
            database.AddTable(table);

            database.Insert("Test", new List<string> { "Igor", "20" });

            Assert.Equal(1, table.NumRows());
            Assert.Equal("Igor", table.GetRow(0).Values[0]);
            Assert.Equal("20", table.GetRow(0).Values[1]);
        }
    }
}