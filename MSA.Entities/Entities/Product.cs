using MSA.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSA.Entities.Entities
{
   public class Product: IEntity
    {
        [Key]
        public int InProductId { get; set; }
        public string StName { get; set; }
        public string StCode { get; set; }
        public int InCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
