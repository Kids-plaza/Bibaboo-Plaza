﻿using BPA.BusinessObject.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA.BusinessObject.Entities
{
    [Table("OrderDetail")]
    public class OrderDetail : BaseEntity
    {
        [Column("quantity")]
        [Required]
        public int Quantity { get; set; }

        [Column("price")]
        [Required]
        public double Price { get; set; }

        [Column("total")]
        [Required]
        public double Total {  get; set; }

        [Column("order_id")]
        [ForeignKey("order")]
        public virtual Order Order { get; set; }

        [Column("product_id")]
        [ForeignKey("product")]
        public Guid ProductId { get; set; }
        public virtual Product product { get; set; }
    }
}
