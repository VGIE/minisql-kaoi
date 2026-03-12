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
    public void TestMethod1()
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

    }
}