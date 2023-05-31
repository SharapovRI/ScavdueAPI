using Scavdue.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Business.Models.Response
{
    public class LifeIndexResponseModel
    {
        public virtual ICollection<EvaluationCriteriaResponseModel> EvaluationCriterias { get; set; } = new List<EvaluationCriteriaResponseModel>();
    }
}
