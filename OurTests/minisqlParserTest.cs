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
        public void InsertTestCommasInEachColumn()
        {
            MiniSqlQuery insert = MiniSQLParser.Parse("INSERT INTO Test VALUES ('Kevin' 'A,inhoa' '20')");
            Assert.Null(insert);
            MiniSqlQuery insert2 = MiniSQLParser.Parse("INSERT INTO Test VALUES ('Kevin' 'Ainhoa' '20,')");
            Assert.Null(insert2);
            MiniSqlQuery insert3 = MiniSQLParser.Parse("INSERT INTO Test VALUES ('K,evin' 'Ainhoa' '20')");
            Assert.Null(insert3);
        }

        [Fact]
        public void DropTestValidValues()
        {
            DropTable drop = MiniSQLParser.Parse("DROP TABLE testTable") as DropTable;

            Assert.NotNull(drop);
        }

        [Fact]
        public void DropTestWithMultipleSpaces()
        {
            DropTable drop = MiniSQLParser.Parse("DROP    TABLE   testTable") as DropTable;
            Assert.NotNull(drop);
        }
        [Fact]
        public void DropTestNoTableKeyword()
        {
            DropTable drop = MiniSQLParser.Parse("DROP testTable") as DropTable;
            Assert.Null(drop);
        }

        [Fact]
        public void DropTestNoTableName()
        {
            DropTable drop = MiniSQLParser.Parse("DROP TABLE") as DropTable;
            Assert.Null(drop);
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
        public void CreateTableNothing()
        {
            CreateTable create = MiniSQLParser.Parse("CREATE TABLE") as CreateTable;
            Assert.Null(create);
        }
        [Fact]
        public void CreateTableNoTableName()
        {
            CreateTable create = MiniSQLParser.Parse("CREATE TABLE (Name TEXT") as CreateTable;
            Assert.Null(create);
        }
        [Fact]
        public void CreateTableMoreColumns()
        {
            CreateTable create = MiniSQLParser.Parse("CREATE TABLE user (Name TEXT,Age INT)") as CreateTable;
            Assert.NotNull(create);
        }
        [Fact]
        public void CreateTableNoParentesis()
        {
            CreateTable create = MiniSQLParser.Parse("CREATE TABLE user Name TEXT,Age INT") as CreateTable;
            Assert.Null(create);
        }
        [Fact]
        public void CreateTableEmptySpaces()
        {
            CreateTable create = MiniSQLParser.Parse("CREATE TABLE user (   )") as CreateTable;
            Assert.NotNull(create);
        }
        [Fact]
        public void CreateTableSpacesColumn()
        {
            CreateTable create = MiniSQLParser.Parse("CREATE TABLE user (name       TEXT)") as CreateTable;
            Assert.NotNull(create);
            Assert.Equal("name", create.ColumnsParameters[0].Name);

        }

        [Fact]
        public void DeleteTestValidWithWhereStringNoSpaces()
        {
            Delete delete = MiniSQLParser.Parse("DELETE FROM Test WHERE Name='Ainhoa'") as Delete;

            Assert.NotNull(delete);
            Assert.Equal("Test", delete.Table);
            Assert.Equal("Name", delete.Where.ColumnName);
        }
        [Fact]
        public void DeleteTest_Various_Spaces()
        {
            Delete delete = MiniSQLParser.Parse("DELETE FROM   Test WHERE     Name='Ainhoa'") as Delete;

            Assert.NotNull(delete);
            Assert.Equal("Test", delete.Table);
            Assert.Equal("Name", delete.Where.ColumnName);
        }
        [Fact]

        public void DeleteTest_Spaces()
        {
            Delete delete = MiniSQLParser.Parse("DELETE FROM Test WHERE Name= ' Ainhoa'") as Delete;

            Assert.Null(delete);
        }
        [Fact]
        public void DeleteTest_No_Where()
        {
            Delete delete = MiniSQLParser.Parse("DELETE FROM Test") as Delete;

            Assert.Null(delete);
        }

        public void DeleteTest_Comma()
        {
            Delete delete = MiniSQLParser.Parse("DELETE FROM Test WHERE Name='Ai,nhoa'") as Delete;

            Assert.Null(delete);
        }

        [Fact]
        public void GrantSelectTest()
        {
            Grant grant = MiniSQLParser.Parse("GRANT SELECT ON TestTable TO Admin") as Grant;
            Assert.NotNull(grant);
            Assert.Equal("SELECT", grant.PrivilegeName);
            Assert.Equal("TestTable", grant.TableName);
            Assert.Equal("Admin", grant.ProfileName);
        }

        [Fact]
        public void GrantInsertTest()
        {
            Grant grant = MiniSQLParser.Parse("GRANT INSERT ON TestTable TO Admin") as Grant;
            Assert.NotNull(grant);
            Assert.Equal("INSERT", grant.PrivilegeName);
            Assert.Equal("TestTable", grant.TableName);
            Assert.Equal("Admin", grant.ProfileName);
        }

        [Fact]
        public void GrantDeleteTest()
        {
            Grant grant = MiniSQLParser.Parse("GRANT DELETE ON TestTable TO UserOne") as Grant;
            Assert.NotNull(grant);
            Assert.Equal("DELETE", grant.PrivilegeName);
            Assert.Equal("TestTable", grant.TableName);
            Assert.Equal("UserOne", grant.ProfileName);
        }

        [Fact]
        public void GrantUpdateTest()
        {
            Grant grant = MiniSQLParser.Parse("GRANT UPDATE ON TestTable4 TO UserFourS") as Grant;
            Assert.NotNull(grant);
            Assert.Equal("UPDATE", grant.PrivilegeName);
            Assert.Equal("TestTable4", grant.TableName);
            Assert.Equal("UserFourS", grant.ProfileName);
        }

        [Fact]
        public void GrantInvalidPrivilegeTest()
        {
            var grant = MiniSQLParser.Parse("GRANT INVALID ON TestTable TO User") as Grant;
            Assert.Null(grant);
        }


        [Fact]
        public void GrantInvalidProfileNameTest()
        {
            var grant = MiniSQLParser.Parse("GRANT INSERT ON TestTable TO User_1") as Grant;
            Assert.Null(grant);
        }

        [Fact]
        public void RevokeSelectTest()
        {
            Revoke revoke = MiniSQLParser.Parse("REVOKE SELECT ON TestTable FROM Admin") as Revoke;
            Assert.NotNull(revoke);
            Assert.Equal("SELECT", revoke.PrivilegeName);
            Assert.Equal("TestTable", revoke.TableName);
            Assert.Equal("Admin", revoke.ProfileName);
        }

        [Fact]
        public void RevokeInsertTest()
        {
            Revoke revoke = MiniSQLParser.Parse("REVOKE INSERT ON TestTable FROM Admin") as Revoke;
            Assert.NotNull(revoke);
            Assert.Equal("INSERT", revoke.PrivilegeName);
            Assert.Equal("TestTable", revoke.TableName);
            Assert.Equal("Admin", revoke.ProfileName);
        }

        [Fact]
        public void RevokeDeleteTest()
        {
            Revoke revoke = MiniSQLParser.Parse("REVOKE DELETE ON TestTable FROM Admin") as Revoke;
            Assert.NotNull(revoke);
            Assert.Equal("DELETE", revoke.PrivilegeName);
            Assert.Equal("TestTable", revoke.TableName);
            Assert.Equal("Admin", revoke.ProfileName);
        }

        [Fact]
        public void RevokeUpdateTest()
        {
            Revoke revoke = MiniSQLParser.Parse("REVOKE UPDATE ON TestTable4 FROM UserFourS") as Revoke;
            Assert.NotNull(revoke);
            Assert.Equal("UPDATE", revoke.PrivilegeName);
            Assert.Equal("TestTable4", revoke.TableName);
            Assert.Equal("UserFourS", revoke.ProfileName);
        }

        [Fact]
        public void RevokeInvalidPrivilegeTest()
        {
            var revoke = MiniSQLParser.Parse("REVOKE INVALID ON TestTable FROM User") as Revoke;
            Assert.Null(revoke);
        }

        [Fact]
        public void RevokeInvalidTableNameTest()
        {
            var revoke = MiniSQLParser.Parse("REVOKE INSERT ON TestTable_2n FROM User") as Revoke;
            Assert.Null(revoke);
        }

        /*
                [Fact]
                public void GrantSelectTest()
                {
                    Grant grant = MiniSQLParser.Parse("GRANT SELECT ON TestTable TO Admin") as Grant;
                    Assert.NotNull(grant);
                    Assert.Equal("SELECT", grant.PrivilegeName);
                    Assert.Equal("TestTable", grant.TableName);
                    Assert.Equal("Admin", grant.ProfileName);
                }

                [Fact]
                public void GrantInsertTest()
                {
                    Grant grant = MiniSQLParser.Parse("GRANT INSERT ON TestTable TO Admin") as Grant;
                    Assert.NotNull(grant);
                    Assert.Equal("INSERT", grant.PrivilegeName);
                    Assert.Equal("TestTable", grant.TableName);
                    Assert.Equal("Admin", grant.ProfileName);
                }

                [Fact]
                public void GrantDeleteTest()
                {
                    Grant grant = MiniSQLParser.Parse("GRANTDELETE ON TestTable TO UserOne") as Grant;
                    Assert.NotNull(grant);
                    Assert.Equal("DELETE", grant.PrivilegeName);
                    Assert.Equal("TestTable", grant.TableName);
                    Assert.Equal("UserOne", grant.ProfileName);
                }

                [Fact]
                public void GrantUpdateTest()
                {
                    Grant grant = MiniSQLParser.Parse("GRANT UPDATE ON TestTable4 TO UserFourS") as Grant;
                    Assert.NotNull(grant);
                    Assert.Equal("UPDATE", grant.PrivilegeName);
                    Assert.Equal("TestTable4", grant.TableName);
                    Assert.Equal("UserFourS", grant.ProfileName);
                }

                [Fact]
                public void GrantInvalidPrivilegeTest()
                {
                    var grant = MiniSQLParser.Parse("GRANT INVALID ON TestTable TO User") as Grant;
                    Assert.Null(grant);
                }

                [Fact]
                public void GrantInvalidTableNameTest()
                {
                    var grant = MiniSQLParser.Parse("GRANT INSERT ON TestTable_2n TO User") as Grant;
                    Assert.Null(grant);
                }

                [Fact]
                public void GrantInvalidProfileNameTest()
                {
                    var grant = MiniSQLParser.Parse("GRANT INSERT ON TestTable TO User_1") as Grant;
                    Assert.Null(grant);
                }
                */
    }
}