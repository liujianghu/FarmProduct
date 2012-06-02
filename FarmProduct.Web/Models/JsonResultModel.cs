using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmProduct.Web.Models
{
    public class JsonResultModel
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public JsonResultModel()
        {
            this.IsSuccess = true;
        }

        public virtual void SetFailure(string errorMessage)
        {
            this.IsSuccess = false;
            this.ErrorMessage = errorMessage;
        }

    }
}