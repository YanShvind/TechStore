//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechStore
{
    using System;
    using System.Collections.Generic;
    
    public partial class basket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public basket()
        {
            this.orders = new HashSet<orders>();
        }
    
        public int id { get; set; }
        public Nullable<int> idgood { get; set; }
        public Nullable<int> idbasket { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual goods goods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orders> orders { get; set; }
    }
}
