using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WLA.System
{
    static class InputManager
    {
        public static KeyboardState OldKeyboardState { get; private set; }
        public static KeyboardState NewKeyboardState { get; private set; }

        public static MouseState OldMouseState { get; private set; }
        public static MouseState NewMouseState { get; private set; }

        public static void InitializeFields()
        {
            OldKeyboardState = Keyboard.GetState();
            OldMouseState = Mouse.GetState();
        }

        public static void Begin()
        {
            NewKeyboardState = Keyboard.GetState();
            NewMouseState = Mouse.GetState();
        }

        public static void End()
        {
            OldKeyboardState = NewKeyboardState;
            OldMouseState = NewMouseState;
        }
    }
}
