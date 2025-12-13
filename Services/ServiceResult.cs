using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Project_Kredit.Services
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public int? Id { get; set; }

        public static ServiceResult Ok(string message, int? id = null)
            => new ServiceResult { Success = true, Message = message, Id = id };

        public static ServiceResult Fail(string message)
            => new ServiceResult { Success = false, Message = message };
    }
}