using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenGLTest
{
    public class VBOSample : GameWindow
    {
        Vector3[] _pos = new Vector3[1000];
        uint _bufNum;

        public VBOSample(): base(1000, 800, GraphicsMode.Default, "hogehoge")
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);

            const float l = 30f;
            const float half = l / 2;
            var rand = new Random();
            for (int i = 0; i < _pos.Length; i++)
            {
                var p = _pos[i];
                p.X = (float)rand.NextDouble() * l - half;
                p.Y = (float)rand.NextDouble() * l - half;
                p.Z = (float)rand.NextDouble() * l - half;
            }


            GL.EnableClientState(ArrayCap.VertexArray);         // 頂点配列を有効に
            GL.GenBuffers(1, out _bufNum);
            //_bufNum = GL.GenBuffer();                           // GPU内にバッファ生成し、番号を取得
            GL.BindBuffer(BufferTarget.ArrayBuffer, _bufNum);   // バッファ番号を指定

            int size = _pos.Length * Marshal.SizeOf(default(Vector3));  // バッファに配列ポインタを渡す
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size), _pos, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);         // バッファ指定を解除
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            GL.DeleteBuffers(1, ref _bufNum);                           // GPU内のバッファ削除
            GL.DisableClientState(ArrayCap.VertexArray);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 modelView = Matrix4.LookAt(Vector3.UnitZ * 10, Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelView);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Width / Height, 1f, 1000.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Color4(Color4.Red);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _bufNum);       // バッファ番号指定
            GL.VertexPointer(3, VertexPointerType.Float, 0, 0);     // 配列バッファの中身と取り出し方を指定
            GL.DrawArrays(PrimitiveType.Lines, 0, _pos.Length);     // バッファの中身で直線を描画
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);             // バッファ指定を解除

            SwapBuffers();
        }
    }
}
