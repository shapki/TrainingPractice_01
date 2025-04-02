namespace Shapkin_Task_10.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Sales = new HashSet<Sale>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemCode { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }

        public int ItemTypeCode { get; set; }

        public int? ProductionYear { get; set; }

        [StringLength(255)]
        public string Owner { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceiptDate { get; set; }

        public decimal? EstimatedValue { get; set; }

        public string Description { get; set; }

        public virtual ItemType ItemType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
