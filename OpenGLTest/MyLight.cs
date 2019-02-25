using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenGLTest
{
    public class MyLight : GameWindow
    {
        Vector4 _lightPosition = new Vector4(100, 150, 100, 0);
        Color4 _lightAmbient = UtilGL.Color4("#333333");
        Color4 _lightDiffuse = UtilGL.Color4("#777777");
        Color4 _lightSpecular = UtilGL.Color4("#FFFFFF");
        //Color4 _materialAmbient = new Color4(0.24725f, 0.1995f, 0.0225f, 1.0f);
        //Color4 _materialDiffuse = new Color4(0.75164f, 0.60648f, 0.22648f, 1.0f);
        //Color4 _materialSpecular = new Color4(0.628281f, 0.555802f, 0.366065f, 1.0f);
        Color4 _materialAmbient = UtilGL.Color4("#FFFFFF");
        Color4 _materialDiffuse = UtilGL.Color4("#FFFFFF");
        Color4 _materialSpecular = UtilGL.Color4("#FFFFFF");
        //float _materialShininess = 51.4f;
        float _materialShininess = 128f;

        public MyLight()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Normalize);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientSize);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            var modelView = Matrix4.LookAt(Vector3.UnitZ * 10, Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref modelView);

            GL.MatrixMode(MatrixMode.Projection);
            var projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1.0f, 64.0f);
            GL.LoadMatrix(ref projection);


            GL.Light(LightName.Light0, LightParameter.Position, _lightPosition);
            GL.Light(LightName.Light0, LightParameter.Ambient, _lightAmbient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, _lightDiffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, _lightSpecular);

            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, _materialAmbient);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, _materialDiffuse);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, _materialSpecular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, _materialShininess);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            UtilGL.DrawSphere();
            GL.PopMatrix();

            SwapBuffers();
        }
    }
}
