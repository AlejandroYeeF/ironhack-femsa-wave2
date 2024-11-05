using System;
using Lab3.Interfaces;
using Lab3.Models;

namespace Lab3.Managers
{
	public class DataManager : IDataManager
	{
        public bool FetchData(DataModel data)
        {
            if (data == null || !data.IsValid)
            {
                return false;
            }

            data.ProcessedSuccessFully = true;
            return data.ProcessedSuccessFully;
        }
    }
}

