using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TeelEngine
{
    public class KeyController : IController<Keybind>
    {
        public Dictionary<string, Keybind> Items { get; set; }

        public KeyController()
        {
            Items = new Dictionary<string, Keybind>();
        }

        public void Add(string name, Keybind item)
        {
            if (!Items.ContainsKey(name))
            {
                Items.Add(name, item);
            }
        }

        public void Add(string name, Keys key, Action action)
        {
            Add(name, new Keybind(key, action));
        }

        public void InvokeAction(string name)
        {
            Keybind keybind;
            Items.TryGetValue(name, out keybind);
            if (keybind != null) keybind.Action.Invoke();
        }

        public void InvokeAction(Keys key)
        {
            foreach (var keybind in Items.Values.Where(keybind => keybind.Key == key))
            {
                keybind.Action.Invoke();
            }
        }

        public void CheckKeyPresses(KeyboardState state)
        {
            foreach (var keybind in Items.Values.Where(keybind => state.IsKeyDown(keybind.Key)))
            {
                keybind.Action.Invoke();
            }
        }
    }
}
