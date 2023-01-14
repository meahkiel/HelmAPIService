using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.Contracts
{
    public class UserResponse
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Token { get; set; }
    }
}