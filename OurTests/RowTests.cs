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