﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THOK.Wms.DbModel
{
    public class InBillMaster
    {
        public InBillMaster()
        {
            this.InBillDetails = new List<InBillDetail>();
            this.InBillAllots = new List<InBillAllot>();
        }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string BillTypeCode { get; set; }
        public string WarehouseCode { get; set; }
        public Guid OperatePersonID { get; set; }
        public string Status { get; set; }
        public Guid ?VerifyPersonID { get; set; }
        public DateTime ?VerifyDate { get; set; }
        public string Description { get; set; }
        public string LockTag { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdateTime { get; set; }
        public byte[] RowVersion { get; set; }
        public string TargetCellCode { get; set; }

        public virtual BillType BillType { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Employee OperatePerson { get; set; }
        public virtual Employee VerifyPerson { get; set; }

        public virtual ICollection<InBillDetail> InBillDetails { get; set; }
        public virtual ICollection<InBillAllot> InBillAllots { get; set; }
    }
}
