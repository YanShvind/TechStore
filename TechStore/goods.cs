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
    
    public partial class goods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public goods()
        {
            this.basket = new HashSet<basket>();
        }
    
        public int idgood { get; set; }
        public Nullable<int> quantity { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<int> price { get; set; }
        public string image { get; set; }
        public Nullable<int> idcategory { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<basket> basket { get; set; }
        public virtual category category { get; set; }

        public string CorrectImage
        {
            get
            {
                if (String.IsNullOrEmpty(image) || String.IsNullOrWhiteSpace(image))
                {
                    return "/Images1/picture.png";
                }
                else
                {
                    return "/Images1/" + image;
                }
            }
        }
    }
}
