namespace Shapkin_Task_9.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sell")]
    public partial class Sell
    {
        public int SellId { get; set; }

        public int? GoodId { get; set; }

        public DateTime? DateSell { get; set; }

        public int? SellCount { get; set; }

        public virtual Good Good { get; set; }
    }
}
