using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage); //Describe any method that takes a string and returns a string

    public class TypeTests
    {
        int count = 0;
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            log += IncrementCount; //Multi cast delegate: I can assign more than one method

            var result = log("Hello!");
            Assert.Equal(3, count);
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }

        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            //Given
            var x = GetInt();
            SetInt(ref x);
            //When
            Assert.Equal(42, x);
            //Then
        }

        private void SetInt(ref int z)
        {
            z = 42;
        }

        private int GetInt()
        {
            return 3;
        }
        
          [Fact]
        public void PassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(out book1, "New Name");
 
            Assert.Equal("New Name", book1.Name);

        }

        private void GetBookSetName(out InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");
 
            Assert.Equal("New Name", book1.Name);

        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }


        // [Fact]
        // public void CanSetNameFromReference()
        // {
        //     var book1 = GetBook("Book 1");
        //     SetName(book1, "New Name");

        //     Assert.Equal("New Name", book1.Name);

        // }

        // private void SetName(Book book1, string name)
        // {
        //     book1.Name = name;
        // }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);


        }

        [Fact]        
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            //Check if two objects point to the same place in memory
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));

        }

        private InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }

        [Fact]
        public void StringsBehaveLikeValueType()
        {
            //Given
            string name = "Santiago";
            //When
            var upper = MakeUpperCase(name);
            //Then
            Assert.Equal("SANTIAGO", upper);
        }

        private string MakeUpperCase(string name)
        {
            return name.ToUpper();
        }
    }
}
