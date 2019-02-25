using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace OpenGLTest
{
    public class ElffyWindow : GameWindow
    {
        FPSManager _fpsManager = new FPSManager();

        Random _rand = new Random();
        const int N = 100;
        Vector2[] _pos = new Vector2[N];
        Color4[] _color = new Color4[N];

        public ElffyWindow(int width, int height, string title, bool fullScreen)
            : base(width, height, GraphicsMode.Default, title, fullScreen ? GameWindowFlags.Fullscreen : GameWindowFlags.FixedWindow)
        {
            Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"FPS : {_fpsManager.GetFPS():N2}");
                }
            });
        }

        // 画面起動時に呼び出される
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine("OnLoaded");

            GL.ClearColor(Color4.Black);        // 背景色
            GL.Enable(EnableCap.DepthTest);

            GL.PointSize(10.0f);     //点の大きさを変更

            GL.LineWidth(5.5f);     //線の太さを変更
            SetRandom();
        }

        // 画面のサイズが変更されたときに呼ばれる
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Console.WriteLine("OnResize");

            GL.Viewport(ClientRectangle);
        }

        // 画面を更新するときに呼ばれる
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            // Console.WriteLine("OnUpdateFrame");
            _fpsManager.Aggregate(e.Time);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SetRandom();

            //点をすべて描画
            GL.Begin(PrimitiveType.Points);
            for (int i = 0; i < N; i++)
            {
                GL.Color4(_color[i]);
                GL.Vertex2(_pos[i]);
            }
            GL.End();

            //線をすべて描画
            GL.Begin(PrimitiveType.Lines);
            //GL.Begin(BeginMode.Lines);
            for (int i = 0; i < N; i++)
            {
                GL.Color4(_color[i]);
                GL.Vertex2(_pos[i]);
            }
            GL.End();

            SwapBuffers();
        }

        // 画面が描画されるときに呼ばれる
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            // Console.WriteLine("OnRenderFrame");
        }

        // 画面終了時に呼び出される
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            Console.WriteLine("OnUnload");
        }

        private void SetRandom()
        {
            for (int i = 0; i < N; i++)
            {
                _pos[i].X = (float)_rand.NextDouble() * 2 - 1;
                _pos[i].Y = (float)_rand.NextDouble() * 2 - 1;
                //_pos[i].X = (2f * i / N  - 1f) * 0.9f;
                //_pos[i].Y = (2f * i / N - 1f) * 0.9f;
                _color[i].R = (float)_rand.NextDouble();
                _color[i].G = (float)_rand.NextDouble();
                _color[i].B = (float)_rand.NextDouble();
            }
        }
    }
}
