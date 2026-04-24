using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_ProductManager_WithoutBinding.DTOs
{
    public class ServiceResultDto
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;

        public static ServiceResultDto Ok(string message) => new() { Success = true, Message = message };
        public static ServiceResultDto Fail(string message) => new() { Success = false, Message = message };
    }
}
