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

        public void SetGetTest()
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
        public void IsTrueTest()
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
        [Fact]
        public void asTextTest()
        {
            List<string> values = new List<string> { "Kevin", "7.9", "21" };
            Row f = new Row(new List<ColumnDefinition>(), values);
            string anw = "Kevin:7.9:21";
            string  real= f.AsText();
            Assert.Equal(anw, real);
        }
        [Fact]
        public void asTextTestDelimeter()
        {
            var r = new Row(new List<ColumnDefinition>(), new List<string> { "Val1", "Val2"});
            String anw = r.AsText();
            Assert.Equal("Val1:Val2", anw);
        }
        [Fact]
        public void asTextTestVal1Empty()
        {
            var r = new Row(new List<ColumnDefinition>(), new List<string> { "", "Val2" });
            String anw = r.AsText();
            Assert.Equal(":Val2", anw);
        }
        [Fact]
        public void asTextTestVal2Empty()
        {
            var r = new Row(new List<ColumnDefinition>(), new List<string> { "Val1", "" });
            String anw = r.AsText();
            Assert.Equal("Val1:", anw);
        }
        [Fact]
        public void asTextTestEmpty()
        {
            var r = new Row(new List<ColumnDefinition>(), new List<string> { "", "" });
            String anw = r.AsText();
            Assert.Equal(":", anw);
        }

        [Fact]
        public void asTextVacioTest() {
            var r = new Row(new List<ColumnDefinition>(), new List<string>());
            string anw= r.AsText();
            Assert.Equal("", anw);
        }
        [Fact]
        public void ParseTest()
        {
            List<ColumnDefinition> cols = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"),
                new ColumnDefinition(ColumnDefinition.DataType.Double, "Grade")
            };

            Row r = Row.Parse(cols, "Kevin:21:7.9");
            Assert.Equal("Kevin", r.Values[0]);
            Assert.Equal("21", r.Values[1]);
            Assert.Equal("7.9", r.Values[2]);
        }

        [Fact]
        public void ParseTestDelimiterInValue()
        {
            List<ColumnDefinition> cols = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.String, "Desc")
            };

            Row original = new Row(cols, new List<string> { "Igor", "val:con:delimitador" });
            string texto = original.AsText();
            Row r = Row.Parse(cols, texto);
            Assert.Equal("Igor", r.Values[0]);
            Assert.Equal("val:con:delimitador", r.Values[1]);
        }

        [Fact]
        public void ParseRoundTrip()
        {
            List<ColumnDefinition> cols = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
            };

            Row original = new Row(cols, new List<string> { "Pepe", "30" });
            Row parsed = Row.Parse(cols, original.AsText());
            Assert.Equal(original.Values[0], parsed.Values[0]);
            Assert.Equal(original.Values[1], parsed.Values[1]);
        }
    }
}