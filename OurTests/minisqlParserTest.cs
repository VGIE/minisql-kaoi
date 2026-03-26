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

        [Fact]
        public void TestSelectAllColumns()
        {
            Select select = MiniSQLParser.Parse("SELECT Name,Height,Age FROM Test") as Select;
            Assert.Equal("Test", select.Table);
            Assert.Contains("Name", select.Columns);
            Assert.Contains("Height", select.Columns);
            Assert.Contains("Age", select.Columns);
            Assert.Null(select.Where);


        }

        [Fact]
        public void TestSelelectColumnsDisOrderedWithCondition()
        {
            Select select = MiniSQLParser.Parse("SELECT Age,Name FROM Test WHERE Name='Rodolfo'") as Select;
            Assert.Equal("Test", select.Table);
            Assert.Contains("Age", select.Columns);
            Assert.Contains("Name", select.Columns);
            Assert.Equal("Name", select.Where.ColumnName);

        }

        [Fact]
        public void TestSelectWithCondition()
        {
            Select select = MiniSQLParser.Parse("SELECT Name,Age FROM Test WHERE Age>'50'") as Select;
            Assert.Equal("Test", select.Table);
            Assert.Contains("Name", select.Columns);
            Assert.Contains("Age", select.Columns);
            Assert.Equal("Age", select.Where.ColumnName);

        }

        [Fact]
        public void TestSelectWithoutColumns()
        {
            var select = MiniSQLParser.Parse("SELECT FROM Test");           
            Assert.Null(select);
            
        }

        [Fact]
        public void TestSelectWithoutColumnsWithCondition()
        {
            var select = MiniSQLParser.Parse("SELECT FROM Test WHERE Age>'50'");
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


    [Fact]
        public void InsertTestValidValues()
        {
            
            Insert insert = MiniSQLParser.Parse("INSERT INTO Test VALUES ('Ainhoa','20','1.67')") as Insert;

            Assert.NotNull(insert);
            Assert.Equal("Test", insert.Table);
            Assert.Equal(3, insert.Values.Count); 
            Assert.Equal("Ainhoa", insert.Values[0]);
            Assert.Equal("20", insert.Values[1]);
            Assert.Equal("1.67", insert.Values[2]);
        }
        [Fact]
        public void InsertTestSpacesAndNoComilla()
        {

            Insert insert = MiniSQLParser.Parse("INSERT INTO Test VALUES ('Ainhoa ,'20 ' , ' 1.67')") as Insert;

            Assert.Null(insert);
        }

        [Fact]
        public void DropTestValidValues()
        {
            DropTable drop = MiniSQLParser.Parse("DROP TABLE testTable") as DropTable;

            Assert.NotNull(drop);
        }
        [Fact]
        public void CreateTableTestValid()
        {

            CreateTable create = MiniSQLParser.Parse("CREATE TABLE users (Name TEXT)") as CreateTable;

            Assert.NotNull(create);
            Assert.Equal("users", create.Table);

        }
        [Fact]
        public void CreateTableSpace()
        {
            
            CreateTable create = MiniSQLParser.Parse("CREATE TABLE users (Name TEXT , Age INT) ") as CreateTable;
            Assert.Null(create);
        
        }
        [Fact]
        public void CreateTableEmpty()
        {
            CreateTable create = MiniSQLParser.Parse("CREATE TABLE users ()") as CreateTable;
            Assert.NotNull(create);
        }
        [Fact]
        public void DeleteTest_Valid_WithWhere_String_NoSpaces()
        {
            Delete delete = MiniSQLParser.Parse("DELETE FROM Test WHERE Name='Ainhoa'") as Delete;

            Assert.NotNull(delete);
            Assert.Equal("Test", delete.Table);

        }

    }
}