using System.Collections.Generic;

namespace TeelEngine
{
    public interface IController<T>
    {
        List<T> Items { get; }
        void Add(T item);
    }
}