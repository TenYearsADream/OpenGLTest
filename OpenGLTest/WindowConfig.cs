using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;

namespace OpenGLTest
{
    public class WindowConfig
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool FullScreen { get; set; }
    }
}
