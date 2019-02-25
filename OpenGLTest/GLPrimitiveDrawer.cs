using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenGLTest
{
    public static class GLPrimitiveDrawer
    {
        public static IGLPrimitiveDrawContext Begin()
        {
            throw new NotImplementedException();
        }
    }

    public interface IGLPrimitiveDrawContext : IDisposable
    {
        
    }

    public class GLPrimitiveDrawContextBase : IGLPrimitiveDrawContext
    {
        public GLPrimitiveDrawContextBase(PrimitiveType type)
        {
            GL.Begin(type);
        }

        public void Dispose()
        {
            GL.End();
        }
    }
}
