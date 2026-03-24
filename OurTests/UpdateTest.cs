using DbManager;
using DbManager.Parser;

namespace OurTests
{
    public class UpdateTest
    {
        private Database Start()
        {
            Database database = Database.CreateTestDatabase();

            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            columns.Add(new ColumnDefinition(ColumnDefinition.DataType.String, "Name"));
            columns.Add(new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"));
            columns.Add(new ColumnDefinition(ColumnDefinition.DataType.String, "City"));
            Table tabla = new Table("Test", columns);

            tabla.Insert(new List<string> { "Igor", "30", "Bilbao" });
            tabla.Insert(new List<string> { "Kevin", "25", "Vitoria" });
            tabla.Insert(new List<string> { "Oier", "24", "Bilbao" });
            tabla.Insert(new List<string> { "Ainhoa", "21", "Vitoria" });
            database.AddTable(tabla);
            return database;
        }
        [Fact]
        public void TestUpdateSuccess()
        {
            Database databaseTest = Start();
            List<SetValue> set= new List<SetValue>{
                new SetValue("Age","22"),
                new SetValue("City","Otxandio")
                };
            Condition con = new Condition("Name","=", "Kevin");
            Update updateTest = new Update("Test",set, con);
            String result = updateTest.Execute(databaseTest);

            Assert.Equal(Constants.UpdateSuccess,result);
        }
        
        [Fact]
        public void TestUpdateTableDoesNotExist()
        {
           Database databaseTest = Start();
            List<SetValue> set= new List<SetValue>{
                new SetValue("Age","22"),
                new SetValue("City","Otxandio")
                };
            Condition con = new Condition("Test","=", "Kevin");
            Update updateTest = new Update("Test2",set, con);
            String result = updateTest.Execute(databaseTest);
            Assert.Equal(Constants.TableDoesNotExistError,result);
        }
        [Fact]
        public void TestUpdateTableDoesColumnNotExistInTheCondition()
        {
           Database databaseTest = Start();
            List<SetValue> set= new List<SetValue>{
                new SetValue("Age","22"),
                new SetValue("City","Otxandio")
                };
            Condition con = new Condition("UnknowColunm","=", "Kevin");
            Update updateTest = new Update("Test",set, con);
            String result = updateTest.Execute(databaseTest);
            Assert.Equal(Constants.ColumnDoesNotExistError,result);
        }
        /*
        [Fact]
        public void TestDeleteError()
        {
            
        }
        */
    }
}