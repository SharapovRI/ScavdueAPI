using Scavdue.Core.Interfaces;

namespace Scavdue.Core.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
