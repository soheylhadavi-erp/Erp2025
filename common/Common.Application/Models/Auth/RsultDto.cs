using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Models.Auth
{
    public class ResultDto
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
