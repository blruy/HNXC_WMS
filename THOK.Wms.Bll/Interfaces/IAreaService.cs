﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.DbModel;

namespace THOK.Wms.Bll.Interfaces
{
    public interface IAreaService : IService<Area>
    {
        object GetDetails(string warehouseCode, string areaCode);
        object GetDetail(string type, string id);

        bool Add(Area area);

        bool Delete(string areaCode);

        bool Save(Area area);

        object FindArea(string parameter);

        object GetWareArea();

        object GetAreaCode(string WareCode);
    }
}
