namespace Scavdue.Core.Models
{
    public partial class Population : Entity
    {
        public int AdministrativeUnitId { get; set; }

        public virtual AdministrativeUnit AdministrativeUnit { get; set; } = null!;

        public int NumberOfPeople { get; set; }

        public DateOnly Date { get; set; }
    }
}
