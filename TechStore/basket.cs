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
        public int idbasket { get; set; }
        public Nullable<int> idgood { get; set; }
        public Nullable<int> iduser { get; set; }
    
        public virtual goods goods { get; set; }
        public virtual orders orders { get; set; }
        public virtual users users { get; set; }
    }
}
