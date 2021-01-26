using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DataBaseModels;

namespace BusinessLogicLayer.Initializer.Interfaces
{
    interface IIInitialize<T>
    {
        //void AddMaterial<T>(T item);
        List<T> GetDataFromFile();
    }
}
