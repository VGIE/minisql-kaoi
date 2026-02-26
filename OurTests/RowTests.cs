using DbManager;
using System.Data.Common;
using Xunit;

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
        private Row Test1()
        {
            List<ColumnDefinition> column = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
                new ColumnDefinition(ColumnDefinition.DataType.Double, "Grade")
                };
            List<string> rowValues = new List<string>()
            {
                "Borja", "27", "7.8"
            };
            Row testRow = new Row(column, rowValues);
            return testRow;

        }
        private Row TestNull()
        {
            List<ColumnDefinition> column = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
                new ColumnDefinition(ColumnDefinition.DataType.Double, "Grade")
                };
            List<string> rowValues1 = new List<string>()
            {
                "Oier"
            };
            Row testRow1 = new Row(column, rowValues1);
            return testRow1;

        }
        [Fact]

        public void Test2()
        {
            Row testRow = Test1();
            Assert.Equal("Borja", testRow.GetValue("Name"));
            Assert.Equal("27", testRow.GetValue("Age"));
            Assert.Equal("7.8", testRow.GetValue("Grade"));
            testRow.SetValue("Name", "Felix");
            Assert.Equal("Felix", testRow.GetValue("Name"));
            Assert.Equal("27", testRow.GetValue("Age"));
            Assert.Equal("7.8", testRow.GetValue("Grade"));
            Row testRow1 = TestNull();
            Assert.Equal("Oier", testRow1.GetValue("Name"));
            Assert.Null(testRow1.GetValue("Age"));
            Assert.Null(testRow1.GetValue("Grade"));
        }
        [Fact]
        public void Test3()
        {
            Row testRow = Test1();
            Assert.True(testRow.IsTrue(new Condition("Name", "=", "Borja")));
            Assert.False(testRow.IsTrue(new Condition("Name", "=", "Alex")));

            Assert.True(testRow.IsTrue(new Condition("Name", "<", "Carlos")));
            Assert.False(testRow.IsTrue(new Condition("Name", "<", "Alex")));
            Assert.True(testRow.IsTrue(new Condition("Name", ">", "Alex")));
            Assert.False(testRow.IsTrue(new Condition("Name", ">", "Carlos")));

           
            Assert.True(testRow.IsTrue(new Condition("Age", ">", "10")));
            Assert.False(testRow.IsTrue(new Condition("Age", ">", "30")));
            Assert.True(testRow.IsTrue(new Condition("Age", "<", "30")));
            Assert.False(testRow.IsTrue(new Condition("Age", "<", "10")));
            Assert.True(testRow.IsTrue(new Condition("Age", "=", "27")));
            Assert.False(testRow.IsTrue(new Condition("Age", "=", "10")));

            Assert.True(testRow.IsTrue(new Condition("Grade", "=", "7.8")));
            Assert.False(testRow.IsTrue(new Condition("Grade", "=", "7.5")));
            Assert.True(testRow.IsTrue(new Condition("Grade", ">", "7.5")));
            Assert.False(testRow.IsTrue(new Condition("Grade", ">", "10")));
            Assert.True(testRow.IsTrue(new Condition("Grade", "<", "8.5")));
            Assert.False(testRow.IsTrue(new Condition("Grade", "<", "7.5")));




        }
    }
}