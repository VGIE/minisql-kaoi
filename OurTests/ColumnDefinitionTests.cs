using DbManager;

namespace OurTests
{
    public class ColumnDefinitionsTests
    {
        //TODO DEADLINE 1A : Create your own tests for Table
        ColumnDefinition c1 = new ColumnDefinition(ColumnDefinition.DataType.String, "Name");
        ColumnDefinition c2 = new ColumnDefinition(ColumnDefinition.DataType.Int, "Salary");
        ColumnDefinition c3 = new ColumnDefinition(ColumnDefinition.DataType.Double, "Weight");
        /*
        [Fact]
        
        public void Test1()
        {
            
        }
        */
         [Fact]
         public void testAsText()
        {
            string expct= "Name->String";
            string result = c1.AsText();

            string expct2= "Salary->Int";
            string result2 = c2.AsText();

            string expct3 = "Weight->Double";
            string result3 = c3.AsText();

            Assert.Equal(expct,result);
            Assert.Equal(expct2,result2);
            Assert.Equal(expct3,result3);
        }
         [Fact]
         public void testParse()
        {
            ColumnDefinition exp = new ColumnDefinition(ColumnDefinition.DataType.String, "Name");
            ColumnDefinition result = ColumnDefinition.Parse(c1.AsText());

            ColumnDefinition exp2 = new ColumnDefinition(ColumnDefinition.DataType.Int, "Salary");
            ColumnDefinition result2 = ColumnDefinition.Parse(c2.AsText());

            ColumnDefinition exp3 = new ColumnDefinition(ColumnDefinition.DataType.Double, "Weight");
            ColumnDefinition result3 = ColumnDefinition.Parse(c3.AsText());

            Assert.Equal(exp.Type, result.Type);
            Assert.Equal(exp.Name,result.Name);

            Assert.Equal(exp2.Type, result2.Type);
            Assert.Equal(exp2.Name,result2.Name);

            Assert.Equal(exp3.Type, result3.Type);
            Assert.Equal(exp3.Name,result3.Name);
        }
    }
}