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
    
    public partial class DE_ORDER
    {
        public DE_ORDER()
        {
            this.DE_BILL = new HashSet<DE_BILL>();
        }
    
        public int IDORDER { get; set; }
        public int IDPRODUCT { get; set; }
        public int QUANTITY { get; set; }
        public int PRICE { get; set; }
    
        public virtual ICollection<DE_BILL> DE_BILL { get; set; }
        public virtual ORDER ORDER { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}