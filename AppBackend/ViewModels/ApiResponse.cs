using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AppBackend.ViewModels
{
    public class ApiResponse<T> {
        public bool Success{ get; set; } = true;
        public T Data{ get; set; }

        public ApiResponse(bool success, T data){
            Success = success;
            Data = data;
        }
    }
}