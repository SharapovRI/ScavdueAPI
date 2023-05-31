using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Business.Models.Response
{
    public class UnitWithLifeIndexResponseModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = null!;

        public int AdministrativeLevel { get; set; }

        public string CountryName { get; set; } = string.Empty;

        public string Place { get; set; }

        public string ISO { get; set; } = string.Empty; 

        public LifeIndexResponseModel LifeIndex { get; set; } = new LifeIndexResponseModel();
    }
}
