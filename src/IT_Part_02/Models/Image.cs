using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IT_Part_02.Models
{
    public class Image
    {
        //Properties for image entity
        [ScaffoldColumn(false)]
        public int ImageID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Data { get; set; }

        public int Likes { get; set; }

        //Navigation property
        public virtual ApplicationUser Author { get; set; }
    }
}
