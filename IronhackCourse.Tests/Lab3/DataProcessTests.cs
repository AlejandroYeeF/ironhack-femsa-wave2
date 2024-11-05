using Lab3.Interfaces;
using Lab3.Managers;
using Lab3.Models;
using Moq;

namespace IronhackCourse.Tests.Lab3
{
	public class DataProcessTests
	{
		[Fact]
		public void ProcessData_ValidData_ReturnsTrue()
		{
			var mockData = new Mock<IDataManager>();
			var data = new DataModel() { IsValid = true };
			mockData
                .Setup(d => d.FetchData(data))
                .Returns(true);

			var processor = new DataProcessorManager(mockData.Object);

			var result = processor.ProcessData(data);

			Assert.True(result);
		}

        [Fact]
        public void ProcessData_InvalidData_ThrowException()
        {
            var mockData = new Mock<IDataManager>();
            var invalidData = new DataModel() { IsValid = false };

            var processor = new DataProcessorManager(mockData.Object);

            Assert.Throws<Exception>(() => processor.ProcessData(invalidData));
        }

        [Fact]
        public void ProcessData_NullData_ThrowException()
        {
            var mockData = new Mock<IDataManager>();

            var processor = new DataProcessorManager(mockData.Object);

            Assert.Throws<Exception>(() => processor.ProcessData(null));
        }
    }
}

