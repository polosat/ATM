//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATM.Logic
{
    using System;
    using System.Collections.Generic;
    
    public partial class Operation
    {
        public long ID { get; set; }
        public long CardID { get; set; }
        public OperationCode Code { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        public virtual Card Card { get; set; }
    }
}
