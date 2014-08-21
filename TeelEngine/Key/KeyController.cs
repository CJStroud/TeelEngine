using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TeelEngine
{
    public class KeyController
    {
        public Dictionary<Keys, string> Items { get; set; }
        [ContentSerializerIgnore]
        public Dictionary<string, Action> Actions { get; set; }  

        public KeyController()
        {
            Items = new Dictionary<Keys, string>();
            Actions = new Dictionary<string, Action>();
        }

        public bool AddKeybinding(Keys key, string action)
        {
            if (Items.ContainsKey(key) || !Actions.ContainsKey(action)) return false;
            Items.Add(key, action);
            return true;
        }

        public bool AddAction(string name, Action action)
        {
            if (Actions.ContainsKey(name)) return false;
            Actions.Add(name, action);
            return true;
        }

        public bool Add(string name, Keys key, Action action)
        {
            bool addAction = AddAction(name, action);

            return addAction && AddKeybinding(key, name);
        }

        public void InvokeAction(string name)
        {
            Action action;
            Actions.TryGetValue(name, out action);
            if (action != null) action.Invoke();
        }

        public void InvokeAction(Keys key)
        {
            foreach (string actionName in Items.Keys.Where(k => k == key).Select(k => Items[k]).Where(actionName => Actions.ContainsKey(actionName)))
            {
                Actions[actionName]();
            }
        }

        public void CheckKeyPresses(KeyboardState state)
        {
            foreach (string actionName in Items.Keys.Where(state.IsKeyDown).Select(key => Items[key]).Where(actionName => Actions.ContainsKey(actionName)))
            {
                Actions[actionName]();
            }
        }
    }
}
