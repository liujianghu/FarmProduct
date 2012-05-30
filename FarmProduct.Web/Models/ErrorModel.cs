
using System;
namespace FarmProduct.Web.Models
{
    public class ErrorModel : BaseModel
    {
        public string ErrorMessage { get; set; }

        public DateTime ErrorTime { get; set; }

        public string StackTrace { get; set; }
    }
}