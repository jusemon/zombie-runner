using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This class provides an extension method to an `IEnumerable` object that returns a new `IEnumerable` containing each element of the original collection along with its index position.
/// </summary>
/// <remarks>
/// An extension method is a static method that is called as if it were an instance method of the object being extended. In this case, the `WithIndex` method extends the `IEnumerable` interface to provide a convenient way to get both the element and its index in a collection.
/// </remarks>
public static class Extensions {
    /// <summary>
    /// Returns a new `IEnumerable` containing each element of the original collection along with its index position.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to enumerate.</param>
    /// <returns>An `IEnumerable` of tuples, where each tuple contains an element of the collection and its index position.</returns>
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable source)
    {
        return source.Cast<T>().Select((item, index) => (item, index));
    }
}
