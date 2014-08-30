using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TeelEngine
{
    public class InputManager
    {
        public Dictionary<Keys, string> KeyboardActions { get; set; }
        public Dictionary<MouseButtons, string> MouseActions { get; set; }
        public Dictionary<Buttons, string> GamepadActions { get; set; }

        public Dictionary<string, Action> Actions { get; set; }  

        public InputManager()
        {
            KeyboardActions = new Dictionary<Keys, string>();
            MouseActions = new Dictionary<MouseButtons, string>();
            GamepadActions = new Dictionary<Buttons, string>();
            Actions = new Dictionary<string, Action>();
        }

        public bool AddKeyBinding(Keys key, string action)
        {
            if (KeyboardActions.ContainsKey(key) || !Actions.ContainsKey(action)) return false;
            KeyboardActions.Add(key, action);
            return true;
        }

        public bool AddGamepadBinding(Buttons button, string action)
        {
            if (GamepadActions.ContainsKey(button) || !Actions.ContainsKey(action)) return false;
            GamepadActions.Add(button, action);
            return true;
        }

        public bool AddMouseBinding(MouseButtons button, string action)
        {
            if (MouseActions.ContainsKey(button) || !Actions.ContainsKey(action)) return false;
            MouseActions.Add(button, action);
            return true;
        }

        public bool AddAction(string name, Action action)
        {
            if (Actions.ContainsKey(name)) return false;
            Actions.Add(name, action);
            return true;
        }

        public void InvokeAction(string name)
        {
            Action action;
            Actions.TryGetValue(name, out action);
            if (action != null) action.Invoke();
        }

        public void IsPressed(string action)
        {
            
        }

        public void IsPressed(string action, PlayerIndex playerIndex)
        {
            
        }
    }
}
