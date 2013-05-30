﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.DbModel;

namespace THOK.Wms.Bll.Interfaces
{
    public interface IWarehouseService : IService<Warehouse>
    {
        object GetDetails(int page, int rows, string warehouseCode);

        object GetDetail(int page, int rows, string type,string id);

        bool Add(Warehouse warehouse);

        bool Delete(string warehouseCode);

        bool Save(Warehouse warehouse);

        object FindWarehouse(string parameter);

        object GetWareCode();
    }
}
