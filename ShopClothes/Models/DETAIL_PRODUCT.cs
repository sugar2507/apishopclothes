//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopClothes.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DETAIL_PRODUCT
    {
        public int ID { get; set; }
        public int IDPRODUCT { get; set; }
        public int IDSIZE { get; set; }
        public int IDCOLOR { get; set; }
        public Nullable<int> QUANTITY { get; set; }
    
        public virtual COLOR COLOR { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual SIZE SIZE { get; set; }
    }
}
