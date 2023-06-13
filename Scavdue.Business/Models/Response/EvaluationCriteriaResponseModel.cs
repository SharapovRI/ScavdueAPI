using Scavdue.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Business.Models.Response;

public class EvaluationCriteriaResponseModel
{
    public float Value { get; set; }

    public string Description { get; set; } = string.Empty;

    public string EvaluationCriteriaTypeName { get; set; } = string.Empty;
}
