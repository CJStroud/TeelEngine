using System.Collections.Generic;

namespace TeelEngine
{
    public interface IController<T>
    {
        Dictionary<string, T> Items { get; set; }

        void Add(string name, T item);
    }
}