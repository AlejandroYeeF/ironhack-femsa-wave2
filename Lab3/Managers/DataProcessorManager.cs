using System;
using Lab3.Interfaces;
using Lab3.Models;

namespace Lab3.Managers
{
	public class DataProcessorManager
	{
		private readonly IDataManager _dataManager;

        public DataProcessorManager(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public bool ProcessData(DataModel? data)
        {
            if (data == null || !data.IsValid)
            {
                throw new Exception("Data inválida");
            }

            return _dataManager.FetchData(data);
        }
    }
}

