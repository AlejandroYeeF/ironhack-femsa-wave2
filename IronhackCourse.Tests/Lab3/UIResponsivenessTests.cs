using System;
using Lab3.Interfaces;
using Lab3.Managers;

namespace IronhackCourse.Tests.Lab3
{
	public class UIResponsivenessTests
	{
		[Fact]
		public void AdjustsToScreenSize_ValidSize_AdjustsSuccessfully()
		{
            IUIComponent uiComponent = new UIComponentManager(480);

            var result = uiComponent.AdjustsToScreenSize(1024);

            Assert.True(result);
            Assert.Equal(1024, uiComponent.GetCurrentWidth());
        }

        [Fact]
        public void AdjustsToScreenSize_InvalidSize_ReturnsFalse()
        {
            IUIComponent uiComponent = new UIComponentManager(480);

            var result = uiComponent.AdjustsToScreenSize(-1);

            Assert.False(result);
        }

        [Fact]
        public void AdjustsToScreenSize_ManyAdjustments_Successfully()
        {
            IUIComponent uiComponent = new UIComponentManager(480);

            var result1 = uiComponent.AdjustsToScreenSize(1440);
            Assert.True(result1);
            Assert.Equal(1440, uiComponent.GetCurrentWidth());

            var result2 = uiComponent.AdjustsToScreenSize(720);
            Assert.True(result2);
            Assert.Equal(720, uiComponent.GetCurrentWidth());

            var result3 = uiComponent.AdjustsToScreenSize(1080);
            Assert.True(result3);
            Assert.Equal(1080, uiComponent.GetCurrentWidth());
        }
    }
}

