using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Core.Models
{
    public class Team : BaseIdentity
    {
        public string FullName { get; set; }
        public Posittion Posittion { get; set; }
        public int PosittionId { get; set; }
        public string TwiterReDir { get; set; }
        public string FaceBookReDir { get; set; }
        public string InstagramReDir { get; set; }
        public string LinkedinReDir { get; set; }
        public string? Imageurl { get; set; }
        [NotMapped]
        public IFormFile? Img { get; set; }
    }
}
