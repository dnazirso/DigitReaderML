using Algebra;
using DataLoaders;
using Xunit;

namespace DataLoadersTests.ImageLoaderTests
{
    public class LoadTests
    {
        [Fact]
        public void LoadNominalBehavior()
        {
            // Arrange
            IDataLoader loader = new ImageLoader();

            // Act
            Matrix data = loader.Load("./ImageLoaderTests/1.png");

            // Assert
            Assert.NotEmpty(data.mat);
        }
    }

}
