//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cafeteria_DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class CANVA_ADMIN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CANVA_ADMIN()
        {
            this.CAFETERIA_TABLE = new HashSet<CAFETERIA_TABLE>();
        }
    
        public string ID_CANVA { get; set; }
        public string ID_ADMIN { get; set; }
        public string LOGIN_STATUS { get; set; }
        public Nullable<System.DateTime> CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATE_AT { get; set; }
    
        public virtual ADMIN ADMIN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAFETERIA_TABLE> CAFETERIA_TABLE { get; set; }
        public virtual CANVA CANVA { get; set; }
    }
}
