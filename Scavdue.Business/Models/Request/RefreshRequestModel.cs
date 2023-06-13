using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Business.Models.Request;

public class RefreshRequestModel
{
    [Required]
    public string RefreshToken { get; set; }
}
