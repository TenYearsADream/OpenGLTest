using System;
using System.Collections.Generic;
using OpenTK.Input;

namespace OpenGLTest
{
    public static class Input
    {
        private static Dictionary<string, bool> _buttons = new Dictionary<string, bool>();
        private static Dictionary<string, int> _sticks = new Dictionary<string, int>();

        public static bool Button(string name)
        {
            throw new NotImplementedException();
        }

        public static float Stick(string name)
        {
            throw new NotImplementedException();
        }

        public static void AddButton(string name, Key key, int buttonNum)
        {
            if (name == null) { throw new ArgumentNullException(nameof(name)); }
            try
            {
                _buttons.Add(name, false);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        public static void AddStick(string name, int axis)
        {
            
        }

        internal static void UpdateState()
        {
            //var state = Joystick.GetState(0);
            //state.GetA
            //state.GetButton()
        }
    }
}
