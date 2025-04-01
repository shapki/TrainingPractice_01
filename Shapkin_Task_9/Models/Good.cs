namespace Shapkin_Task_9.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Good")]
    public partial class Good
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Good()
        {
            Sells = new HashSet<Sell>();
        }

        public int GoodId { get; set; }

        [StringLength(255)]
        public string GoodName { get; set; }

        public double? Price { get; set; }

        [StringLength(50)]
        public string Picture { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int? CountGood { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sell> Sells { get; set; }
    }
}
