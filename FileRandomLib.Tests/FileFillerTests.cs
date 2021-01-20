using System;
using System.IO;
using Xunit;

namespace FileRandomLib.Tests
{
    public class FileFillerTests
    {
        #region snippet_Fill_Passes_InputIsCorrect

        [Fact]
        public void Fill_Passes_InputIsCorrect()
        {
            // Arrange
            var ff = new FileFiller("Test1");

            // Act
            ff.Fill(1,100);

            // Assert
            Assert.True(File.Exists("Test1"));
        }

        #endregion

        #region snippet_CreateMatrix_ThrowsArgumentOutOfRangeException_LowerLimitGreaterThanUpper

        [Fact]
        public void CreateMatrix_ThrowsArgumentNullException_InputStreamIsNull()
        {
            // Arrange
            var ff = new FileFiller("Test2");

            // Act
            void Result()
            {
                ff.Fill(100,1);
            }

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(Result);
        }

        #endregion

        #region snippet_Append_Passes_InputIsCorrect

        [Fact]
        public void Append_Passes_InputIsCorrect()
        {
            // Arrange
            var ff = new FileFiller("Test1");
            var startLen = ff.ReadAllFile().Length;
            // Act
            ff.Append();

            // Assert
            Assert.True(startLen< ff.ReadAllFile().Length);
        }

        #endregion

        #region snippet_Find_Passes_InputIsCorrect

        [Fact]
        public void Find_Passes_InputIsCorrect()
        {
            // Arrange
            var ff = new FileFiller("Test4");
            ff.Fill(0,50);
            // Act
            var found = ff.Find(10);

            // Assert
            Assert.True(found);
        }

        #endregion
    }
}