namespace Shapkin_Task_10.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SaleCode { get; set; }

        public int ItemCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime AuctionDate { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal? FinalPrice { get; set; }

        public bool Sold { get; set; }

        [StringLength(255)]
        public string BuyerLastName { get; set; }

        public virtual Item Item { get; set; }
    }
}
