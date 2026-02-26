using DbManager;
namespace OurTests
{
    public class ConditionTests
    {
        //TODO DEADLINE 1A : Create your own tests for Condition
        
        [Fact]
        public void Mayor()
        {
            var condition = new Condition("a", ">", "18");
            Assert.True(condition.IsTrue("20", ColumnDefinition.DataType.Int));
            Assert.False(condition.IsTrue("16", ColumnDefinition.DataType.Int));
            var condition2 = new Condition("b", ">", "cd");
            Assert.True(condition2.IsTrue("dd", ColumnDefinition.DataType.String));
            Assert.False(condition2.IsTrue("ab", ColumnDefinition.DataType.String));
            var condition3 = new Condition("c", ">", "100.5");
            Assert.False(condition3.IsTrue("100.5", ColumnDefinition.DataType.Double));
            Assert.True(condition3.IsTrue("100.6", ColumnDefinition.DataType.Double));
        }
        [Fact]
        public void Menor()
        {
            var condition = new Condition("a", "<", "18");
            Assert.True(condition.IsTrue("16", ColumnDefinition.DataType.Int));
            Assert.False(condition.IsTrue("20", ColumnDefinition.DataType.Int));
            var condition2 = new Condition("b", "<", "cd");
            Assert.True(condition2.IsTrue("ab", ColumnDefinition.DataType.String));
            Assert.False(condition2.IsTrue("cd", ColumnDefinition.DataType.String));
            var condition3 = new Condition("c", "<", "100.5");
            Assert.True(condition3.IsTrue("100.4", ColumnDefinition.DataType.Double));
            Assert.False(condition3.IsTrue("100.6", ColumnDefinition.DataType.Double));
        }
       /* [Fact]
       public void MayorIgual()
        {
            var condition = new Condition("a", ">=", "18");
            Assert.True(condition.IsTrue("20", ColumnDefinition.DataType.Int));
            Assert.False(condition.IsTrue("16", ColumnDefinition.DataType.Int));
            var condition2 = new Condition("b", ">=", "cd");
            Assert.True(condition2.IsTrue("cd", ColumnDefinition.DataType.String));
            Assert.False(condition2.IsTrue("ab", ColumnDefinition.DataType.String));
            var condition3 = new Condition("c", ">=", "100.5");
            Assert.True(condition3.IsTrue("100.5", ColumnDefinition.DataType.Double));
            Assert.False(condition3.IsTrue("100.4", ColumnDefinition.DataType.Double));
            Assert.True(condition3.IsTrue("100.6", ColumnDefinition.DataType.Double));
        }*/
        [Fact]
        public void Igual()
        {
            var condition = new Condition("a", "=", "18");
            Assert.True(condition.IsTrue("18", ColumnDefinition.DataType.Int));
            Assert.False(condition.IsTrue("16", ColumnDefinition.DataType.Int));
            var condition2 = new Condition("b", "=", "cd");
            Assert.True(condition2.IsTrue("cd", ColumnDefinition.DataType.String));
            Assert.False(condition2.IsTrue("ab", ColumnDefinition.DataType.String));
            var condition3 = new Condition("c", "=", "100.5");
            Assert.True(condition3.IsTrue("100.5", ColumnDefinition.DataType.Double));
            Assert.False(condition3.IsTrue("100.4", ColumnDefinition.DataType.Double));
        }
      /*  [Fact]
        public void MenorIgual()
        {
            var condition = new Condition("a", "<=", "18");
            Assert.True(condition.IsTrue("16", ColumnDefinition.DataType.Int));
            Assert.False(condition.IsTrue("20", ColumnDefinition.DataType.Int));
            var condition2 = new Condition("b", "<=", "cd");
            Assert.True(condition2.IsTrue("cd", ColumnDefinition.DataType.String));
            Assert.True(condition2.IsTrue("ab", ColumnDefinition.DataType.String));
            var condition3 = new Condition("c", "<=", "100.5");
            Assert.True(condition3.IsTrue("100.5", ColumnDefinition.DataType.Double));
            Assert.False(condition3.IsTrue("100.6", ColumnDefinition.DataType.Double));
        }*/
    }
}