using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudAjax.Models
{
    public class Employee
    {
        public int EmpId { get; set;}
        public string EmpName { get; set;}
        public int EmpAge { get; set;}
        public string EmpState { get; set;}
        public string EmpCountry { get; set;}
    }
}