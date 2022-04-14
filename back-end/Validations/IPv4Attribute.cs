using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayManagingAPI.Validations{
    public class IPv4Attribute: ValidationAttribute{
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if ( value == null || string.IsNullOrEmpty(value.ToString()) ){
                return ValidationResult.Success;
            }
            string IPv4 = value.ToString()!;
            int cntP = 0;
            int []token = {0,0,0,0};
            for ( int i = 0, num = 0, p = 0; i < IPv4.Length; ++i ){
                if ( IPv4[i] == '.' ){
                    ++cntP;
                    if ( cntP > 3 ){
                        return new ValidationResult("IP address not valid. Too many dots");
                    }
                    token[p++] = num;
                    num = 0;
                }
                else {
                    num = num*10+(IPv4[i]-'0');
                    if ( num > 255 ){
                        return new ValidationResult("IP address not valid. Format is: [0-255].[0-255].[0-255].[0-255]");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}