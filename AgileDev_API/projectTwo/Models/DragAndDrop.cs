using projectTwo.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
    public class DragAndDrop : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Topic { get; set; }
        public string ToDo { get; set; }
        public int Placement { get; set; }
        public int Number { get; set; }

        public DragAndDropItemDTO DragAndDropItemDto =>
            new DragAndDropItemDTO
            {
                Topic = this.Topic,
                ToDo = this.ToDo,
                Placement = this.Placement,
                Number = this.Number
            };
    }
}
