using DbManager;
using DbManager.Parser;
using Xunit;

namespace OurTests
{
    public class ParserTest
    {
        //TODO DEADLINE 1A : Create your own tests for Table
        /*[Fact]
        public void Test1()
        {
            
        }
        */

        
         private Database db;
    private Table table;
    private List<ColumnDefinition> cd;
    private List<string> values;
    private MiniSQLParser miniSQLParser;

    [Fact]
    public void UpdateTest()
    {
        Start();
    }

    public void Start()
    {
        db = new Database("User1", "12345");
        miniSQLParser = new MiniSQLParser();
        cd = new List<ColumnDefinition>()
        {
        new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
        new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
        new ColumnDefinition(ColumnDefinition.DataType.Double, "Height")
        };


        table = new Table("testTable", cd);

        values = new List<String>() { "ainhoa", "20", "1.67" };
        table.AddRow(new Row(cd, values));
        values = new List<String>() { "Igor", "19", "1.77" };
        table.AddRow(new Row(cd, values));
        values = new List<String>() { "Oier", "23", "1.70" };
        table.AddRow(new Row(cd, values));

        db.AddTable(table);
    }

    [Fact]
    public void UpdateTestsStringValue()
    {
        Update update = MiniSQLParser.Parse("UPDATE testTable SET Name='Ainhoa Martinez' WHERE Name='Ainhoa'") as Update;

        Assert.NotNull(update);
        Assert.Equal("testTable", update.Table);
        Assert.Equal("Name", update.Columns[0].ColumnName);
        Assert.Equal("Ainhoa Martinez", update.Columns[0].Value);
        Assert.Equal("Name", update.Where.ColumnName);
        Assert.Equal("Ainhoa", update.Where.LiteralValue);

    }

    [Fact]
    public void UpdateTestsIntValues()
    {
        Update update = MiniSQLParser.Parse("UPDATE testTable SET Age='23',Height='1.70' WHERE Name='Ainhoa'") as Update;

        Assert.NotNull(update);
        Assert.Equal("testTable", update.Table);
        Assert.Equal("Age", update.Columns[0].ColumnName);
        Assert.Equal("23", update.Columns[0].Value);
        Assert.Equal("Height", update.Columns[1].ColumnName);
        Assert.Equal("1.70", update.Columns[1].Value);
        Assert.Equal("Name", update.Where.ColumnName);
        Assert.Equal("Ainhoa", update.Where.LiteralValue);
    }

    [Fact]
    public void UpdateTestsIncorrectSpacesOrMissingApostrophes()
    {
        Update update = MiniSQLParser.Parse("UPDATE testTable SET Age='23', Height='1.70' WHERE Name='Ana'") as Update;

        Assert.Null(update);
    }
    
       public void SelectTest()
        {
            Start2();
        }

        public void Start2()
        {
            db = new Database("user", "1234");
            miniSQLParser = new MiniSQLParser();

            cd = new List<ColumnDefinition>
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int,"Age"),
                new ColumnDefinition(ColumnDefinition.DataType.Double,"Height")
            };

            table = new Table("Test", cd);

            values = new List<string> { "Ainhoa", "25", "1.62" };
            table.AddRow(new Row(cd, values));
            values = new List<string> { "Igor", "19", "1.87" };
            table.AddRow(new Row(cd, values));
            values = new List<string> { "Oier", "21", "1.75" };
            table.AddRow(new Row(cd, values));

            db.AddTable(table);
        }



        [Fact]
        public void TestSelectAllColumns()
        {
            Select select = MiniSQLParser.Parse("SELECT Name,Height,Age FROM TestTable") as Select;
            Assert.Equal("Test", select.Table);
            Assert.Contains("Name", select.Columns);
            Assert.Contains("Height", select.Columns);
            Assert.Contains("Age", select.Columns);
            Assert.Null(select.Where);


        }

        [Fact]
        public void TestSelelectColumnsDisOrderedWithCondition()
        {
            Select select = MiniSQLParser.Parse("SELECT Age,Name FROM TestTable WHERE Name='Rodolfo'") as Select;
            Assert.Equal("Test", select.Table);
            Assert.Contains("Age", select.Columns);
            Assert.Contains("Name", select.Columns);
            Assert.Equal("Name", select.Where.ColumnName);

        }

        [Fact]
        public void TestSelectWithCondition()
        {
            Select select = MiniSQLParser.Parse("SELECT Name,Age FROM TestTable WHERE Age>'50'") as Select;
            Assert.Equal("TestTable", select.Table);
            Assert.Contains("Name", select.Columns);
            Assert.Contains("Age", select.Columns);
            Assert.Equal("Age", select.Where.ColumnName);

        }

        [Fact]
        public void TestSelectWithoutColumns()
        {
            var select = MiniSQLParser.Parse("SELECT FROM TestTable");           
            Assert.Null(select);
            
        }

        [Fact]
        public void TestSelectWithoutColumnsWithCondition()
        {
            var select = MiniSQLParser.Parse("SELECT FROM TestTable WHERE Age>'50'");
            Assert.Null(select);

        }


        [Fact]
        public void TestSelectWithDifferentOperators()
        {
           
            Select select1 = MiniSQLParser.Parse("SELECT Name,Age FROM TestTable WHERE Age='25'") as Select;
            Assert.Equal("TestTable", select1.Table);
            Assert.Contains("Name", select1.Columns);
            Assert.Contains("Age", select1.Columns);
            Assert.Equal("Age", select1.Where.ColumnName);


            Select select2 = MiniSQLParser.Parse("SELECT Name,Height FROM TestTable WHERE Height<'1.60'") as Select;
            Assert.Equal("TestTable", select2.Table);
            Assert.Contains("Name", select2.Columns);
            Assert.Contains("Height", select2.Columns);
            Assert.Equal("Height", select2.Where.ColumnName);


            Select select3 = MiniSQLParser.Parse("SELECT Name,Age FROM TestTable WHERE Name='Maider'") as Select;
            Assert.Equal("TestTable", select3.Table);
            Assert.Contains("Name", select3.Columns);
            Assert.Contains("Age", select3.Columns);
            Assert.Equal("Name", select3.Where.ColumnName);

           
        }

    

    }
}