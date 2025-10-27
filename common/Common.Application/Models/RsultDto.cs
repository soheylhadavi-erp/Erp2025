using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Models
{
    public class ResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string[] Errors { get; set; } = [];
        public int ErrorCode { get; set; } = 0;
    }
}
