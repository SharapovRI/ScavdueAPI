using System.Linq.Expressions;

namespace Scavdue.Core.Interfaces;

public interface ISpecification<T> where T : class, IEntity
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
}