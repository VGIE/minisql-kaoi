using DbManager;
using DbManager.Parser;
using Xunit;

namespace OurTests
{
    public class TableTests
    {
        //TODO DEADLINE 1A : Create your own tests for Table
        [Fact]
        public void Test1()
        {
            
        }


        [Fact]
        public void TestAddRowAndGetRow()
        {
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "NamePrueba1"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

            Table table = new Table("nameprueba", col);

            List<String> listPrueba = new List<string>()
            {
                "Name",
                "2"
            };

            Row rowPrueba = new Row(col, listPrueba);

            table.AddRow(rowPrueba);
            Row result = table.GetRow(0);

            Assert.Same(rowPrueba, result);


        }

        [Fact]
        public void TestNumRows()
        {
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "NamePrueba1"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

            Table table = new Table("nameprueba", col);

             List<String> listPrueba = new List<string>()
            {
                "Name",
                "2"
            };

            Row rowPrueba = new Row(col, listPrueba);
            table.AddRow(rowPrueba);
            int result = table.NumRows();

            Assert.Equal(1, result);
        }

        [Fact]
        public void TestGetColumn()
        {
             List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "NamePrueba1"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

             Table table = new Table("nameprueba", col);

             ColumnDefinition result = table.GetColumn(0);

             Assert.Same(col[0],result);

        }

        [Fact]
        public void TestNumColumns()
        {
             List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "NamePrueba1"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

            Table table = new Table("nameprueba", col);

            int result = table.NumColumns();

            Assert.Equal(2, result);
        }

        [Fact]
         public void TestColumnByName() 
         { 
            List<ColumnDefinition> col = new List<ColumnDefinition>() 
            { 
                new ColumnDefinition(ColumnDefinition.DataType.String, "NamePrueba1"), 
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            }; 

            Table table = new Table("nameprueba", col);

            ColumnDefinition result = table.ColumnByName("Num");

            Assert.Equal("Num", result.Name); 
        }

        [Fact]
        public void TestColumnIndexByName()
        {
            List<ColumnDefinition> col = new List<ColumnDefinition>() 
            { 
                new ColumnDefinition(ColumnDefinition.DataType.String, "NamePrueba1"), 
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            }; 

            Table table = new Table("nameprueba", col);

            int result = table.ColumnIndexByName("NamePrueba1");

            Assert.Equal(0, result); 

            int result2 = table.ColumnIndexByName("Num");

            Assert.Equal(1, result2);

            int result3 = table.ColumnIndexByName("Patata");

            Assert.Equal(-1, result3);

            List<ColumnDefinition> col2 = new List<ColumnDefinition>();

            Table table2 = new Table("nameprueba2", col2);

            int result4 = table2.ColumnIndexByName("Prueba");

            Assert.Equal(-1, result4);

            int result5 = table.ColumnIndexByName(null);

            Assert.Equal(-1, result5);

        }
        
        [Fact]
        public void TestToString()
        {
            // Case 1: NO colomuns
            List<ColumnDefinition> empty = new List<ColumnDefinition>();
            Table emptyTable = new Table("empty", empty);

            Assert.Equal("", emptyTable.ToString());

            // Case 2: One column, no rows
            List<ColumnDefinition> one = new List<ColumnDefinition>()
            {
              new ColumnDefinition(ColumnDefinition.DataType.String, "Name")
            };

            Table oneTable = new Table("1", one);

            Assert.Equal("['Name']", oneTable.ToString());

            // Case 3: One column, two rows
            
            Table moreRows = new Table("2", one);

            moreRows.AddRow(new Row(one, new List<string> { "Adolfo" }));
            moreRows.AddRow(new Row(one, new List<string> { "Jacinto" }));

            Assert.Equal("['Name']{'Adolfo'}{'Jacinto'}", moreRows.ToString());

            // Case 4: Two columns, two rows
            List<ColumnDefinition> twoCol = new List<ColumnDefinition>()
            {
              new ColumnDefinition(ColumnDefinition.DataType.String, "Name"),
              new ColumnDefinition(ColumnDefinition.DataType.Int, "Age")
            };

            Table tableTwoCol = new Table("3", twoCol);

            tableTwoCol.AddRow(new Row(twoCol, new List<string> {"Adolfo", "23"}));
            tableTwoCol.AddRow(new Row(twoCol, new List<string> {"Jacinto", "24"}));
        
            Assert.Equal("['Name','Age']{'Adolfo','23'}{'Jacinto','24'}", tableTwoCol.ToString());
        }
        [Fact]
        public void TestInsert()
        {
            List<ColumnDefinition> col = new List<ColumnDefinition>()
            {
                new ColumnDefinition(ColumnDefinition.DataType.String, "NamePrueba1"),
                new ColumnDefinition(ColumnDefinition.DataType.Int, "Num")
            };

            Table table = new Table("nameprueba", col);

             List<String> listPrueba = new List<string>()
            {
                "Name",
                "2"
            };

            Row rowPrueba = new Row(col, listPrueba);
            table.AddRow(rowPrueba);

            Assert.Same(rowPrueba, table.GetRow(0));
        } 
        [Fact]
        public void TestDeleteIth()
        {
             List<ColumnDefinition> columns = new List<ColumnDefinition>();
            columns.Add(new ColumnDefinition(ColumnDefinition.DataType.String, "Name"));
            columns.Add(new ColumnDefinition(ColumnDefinition.DataType.Int, "Age"));
            columns.Add(new ColumnDefinition(ColumnDefinition.DataType.Int, "salary"));
            Table tabla = new Table("People", columns);

            tabla.Insert(new List<string> { "Ainhoa", "23", "2000" });
            tabla.Insert(new List<string> { "Igor", "26", "5200" });
            tabla.Insert(new List<string> { "Kevin", "22", "1900" });
            tabla.Insert(new List<string> { "Oier", "28", "2200" });

            tabla.DeleteWhere(new Condition("Name", "=", "Kevin"));

            Assert.Equal(3, tabla.NumRows());

            tabla.DeleteWhere(new Condition("salary", ">", "2000"));

            Assert.Equal(1, tabla.NumRows());
        }

          
    }
    
}