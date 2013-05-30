﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.Bll.Interfaces;
using THOK.Wms.DbModel;
using Microsoft.Practices.Unity;
using THOK.Wms.Dal.Interfaces;

namespace THOK.Wms.Bll.Service
{
    public class ShelfService : ServiceBase<Shelf>, IShelfService
    {
        [Dependency]
        public IAreaRepository AreaRepository { get; set; }

        [Dependency]
        public IWarehouseRepository WarehouseRepository { get; set; }

        [Dependency]
        public IShelfRepository ShelfRepository { get; set; }

        [Dependency]
        public ICellRepository CellRepository { get; set; }

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        #region IShelfService 成员

        public object GetDetails(string warehouseCode,string areaCode, string shelfCode)
        {
            IQueryable<Shelf> shelfQuery = ShelfRepository.GetQueryable();
            var shelf = shelfQuery.OrderBy(b => b.ShelfCode).AsEnumerable().Select(b => new { b.ShelfCode, b.ShelfName, b.ShelfType, b.ShortName,b.CellCols,b.CellRows,b.ImgX,b.ImgY,b.Description, b.Area.AreaCode, b.Area.AreaName, b.Warehouse.WarehouseCode, b.Warehouse.WarehouseName, IsActive = b.IsActive == "1" ? "可用" : "不可用", UpdateTime = b.UpdateTime.ToString("yyyy-MM-dd hh:mm:ss") });
            if (warehouseCode != null && warehouseCode != string.Empty)
            {
                shelf = shelf.Where(s => s.WarehouseCode == warehouseCode).OrderBy(s => s.ShelfCode).Select(s => s);
            }
            if (areaCode != null && areaCode != string.Empty)
            {
                shelf = shelf.Where(s => s.AreaCode == areaCode).OrderBy(s => s.ShelfCode).Select(s => s);
            }
            if (shelfCode != null && shelfCode!=string.Empty)
            {
                shelf = shelf.Where(s => s.ShelfCode == shelfCode).OrderBy(s => s.ShelfCode).Select(s => s);
            }
            return shelf.ToArray();
        }

        public object GetDetail(string type, string id)
        {
            IQueryable<Shelf> shelfQuery = ShelfRepository.GetQueryable();
            IQueryable<Cell> cellQuery = CellRepository.GetQueryable();
            var shelf = shelfQuery.OrderBy(b => b.ShelfCode).AsEnumerable().Select(b => new { b.ShelfCode, b.ShelfName, b.ShelfType, b.ShortName, b.CellCols, b.CellRows, b.ImgX, b.ImgY, b.Description, b.Area.AreaCode, b.Area.AreaName, b.Warehouse.WarehouseCode, b.Warehouse.WarehouseName, IsActive = b.IsActive == "1" ? "可用" : "不可用", UpdateTime = b.UpdateTime.ToString("yyyy-MM-dd hh:mm:ss") });
            if (type=="shelf")
            {
                shelf = shelf.Where(s => s.ShelfCode == id);
            }
            else if (type == "cell")
            {
                var shelfCode = cellQuery.Where(c => c.CellCode == id).Select(c => new { c.ShelfCode }).ToArray();
                shelf = shelf.Where(s => s.ShelfCode == shelfCode[0].ShelfCode);
            }  
            return shelf.ToArray();
        }
        public new bool Add(Shelf shelf)
        {
            var shelfAdd = new Shelf();
            var warehouse = WarehouseRepository.GetQueryable().FirstOrDefault(w => w.WarehouseCode == shelf.WarehouseCode);
            var area = AreaRepository.GetQueryable().FirstOrDefault(a => a.AreaCode == shelf.AreaCode);
            shelfAdd.ShelfCode = shelf.ShelfCode;
            shelfAdd.ShelfName = shelf.ShelfName;
            shelfAdd.ShortName = shelf.ShortName;
            shelfAdd.ShelfType = shelf.ShelfType;
            shelfAdd.CellRows = shelf.CellRows;
            shelfAdd.CellCols = shelf.CellCols;
            shelfAdd.ImgX = shelf.ImgX;
            shelfAdd.ImgY = shelf.ImgY;
            shelfAdd.Warehouse = warehouse;
            shelfAdd.Area = area;
            shelfAdd.Description = shelf.Description;
            shelfAdd.IsActive = shelf.IsActive;
            shelfAdd.UpdateTime = DateTime.Now;

            ShelfRepository.Add(shelfAdd);
            ShelfRepository.SaveChanges();
            return true;
        }

        public bool Delete(string shelfCode)
        {
            var shelf = ShelfRepository.GetQueryable()
                .FirstOrDefault(s => s.ShelfCode == shelfCode);
            if (shelf != null)
            {
                //Del(CellRepository, shelf.Cells);
                ShelfRepository.Delete(shelf);
                ShelfRepository.SaveChanges();
            }
            else
                return false;
            return true;
        }

        public bool Save(Shelf shelf)
        {
            var shelfSave = ShelfRepository.GetQueryable().FirstOrDefault(s => s.ShelfCode == shelf.ShelfCode);            
            var warehouse = WarehouseRepository.GetQueryable().FirstOrDefault(w => w.WarehouseCode == shelf.WarehouseCode);
            var area = AreaRepository.GetQueryable().FirstOrDefault(a => a.AreaCode == shelf.AreaCode);
            shelfSave.ShelfCode = shelfSave.ShelfCode;
            shelfSave.ShelfName = shelf.ShelfName;
            shelfSave.ShortName = shelf.ShortName;
            shelfSave.ShelfType = shelf.ShelfType;
            shelfSave.CellCols = shelf.CellCols;
            shelfSave.CellRows = shelf.CellRows;
            shelfSave.ImgX = shelf.ImgX;
            shelfSave.ImgY = shelf.ImgY;
            shelfSave.Warehouse = warehouse;
            shelfSave.Area = area;
            shelfSave.Description = shelf.Description;
            shelfSave.IsActive = shelf.IsActive;
            shelfSave.UpdateTime = DateTime.Now;

            ShelfRepository.SaveChanges();
            return true;
        }
       
        /// <summary>
        /// 根据参数Code查询货架信息
        /// </summary>
        /// <param name="shelfCode">货架Code</param>
        /// <returns></returns>
        public object FindShelf(string shelfCode)
        {
            IQueryable<Shelf> shelfQuery = ShelfRepository.GetQueryable();
            var shelf = shelfQuery.Where(s=>s.ShelfCode==shelfCode).OrderBy(b => b.ShelfCode).AsEnumerable()
                                  .Select(b => new { b.ShelfCode, b.ShelfName, b.ShelfType,b.CellCols,b.CellRows,b.ImgX,b.ImgY,b.ShortName, b.Description, b.Area.AreaCode, b.Area.AreaName, b.Warehouse.WarehouseCode, b.Warehouse.WarehouseName, IsActive = b.IsActive == "1" ? "可用" : "不可用", UpdateTime = b.UpdateTime.ToString("yyyy-MM-dd hh:mm:ss") });
            return shelf.First(s => s.ShelfCode == shelfCode);
        }

        /// <summary>
        /// 根据库区编码获取生成的货架编码
        /// </summary>
        /// <param name="areaCode">库区编码</param>
        /// <returns></returns>
        public object GetShelfCode(string areaCode)
        {
            string shelfCodeStr = "";
            IQueryable<Shelf> shelfQuery = ShelfRepository.GetQueryable();
            var shelfCode = shelfQuery.Where(s=>s.AreaCode==areaCode).Max(s => s.ShelfCode);
            if (shelfCode == string.Empty || shelfCode == null)
            {
                shelfCodeStr = areaCode + "-001";
            }
            else
            {
                int i = Convert.ToInt32(shelfCode.ToString().Substring(areaCode.Length+1, 3));
                i++;
                string newcode = i.ToString();
                if (newcode.Length <= 3)
                {
                    for (int j = 0; j < 3 - i.ToString().Length; j++)
                    {
                        newcode = "0" + newcode;
                    }
                    shelfCodeStr = areaCode + "-" + newcode;
                }
            }
            return shelfCodeStr;
        }

        #endregion


       
    }
}
