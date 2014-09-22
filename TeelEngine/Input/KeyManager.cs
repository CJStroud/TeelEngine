using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TeelEngine.Input
{
    public static class KeyManager
    {
        public static Dictionary<Keys, string> KeyboardActions { get; set; }
        public static Dictionary<Buttons, string> GamepadActions { get; set; }

        public static Dictionary<string, Action> Actions { get; set; }  

        static KeyManager()
        {
            KeyboardActions = new Dictionary<Keys, string>();
            GamepadActions = new Dictionary<Buttons, string>();
            Actions = new Dictionary<string, Action>();
        }

        public static bool AddKeyBinding(Keys key, string action)
        {
            if (KeyboardActions.ContainsKey(key) || !Actions.ContainsKey(action)) return false;
            KeyboardActions.Add(key, action);
            return true;
        }

        public static bool AddGamepadBinding(Buttons button, string action)
        {
            if (GamepadActions.ContainsKey(button) || !Actions.ContainsKey(action)) return false;
            GamepadActions.Add(button, action);
            return true;
        }

        public static bool AddAction(string name, Action action)
        {
            if (Actions.ContainsKey(name)) return false;
            Actions.Add(name, action);
            return true;
        }

        public static void InvokeAction(string name)
        {
            Action action;
            Actions.TryGetValue(name, out action);
            if (action != null) action.Invoke();
        }

        public static void IsPressed(string action)
        {
            
        }
    }
}
