using System;
namespace DesignPatterns.Managers
{
	public class GlobalConfigManager
	{
		public static GlobalConfigManager? Instance = null;

        public string ApplicationName { get; set; }
        private GlobalConfigManager()
        {
            ApplicationName = "UserSystem";
        }

        public static GlobalConfigManager GetInstance()
		{
			if (Instance == null)
			{
                Instance = new GlobalConfigManager();
			}
			return Instance;
		}
	}
}

