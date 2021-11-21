using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using projectTwo.DTOs;

namespace projectTwo.Models
{
    public class Item: BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public ItemDisplayDTO ItemDisplayDTO =>
            new ItemDisplayDTO
            {
                Id = this.Id,
                Description = this.Description,
                Price = this.Price,
            };
    }
}
