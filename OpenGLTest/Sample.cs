/*
 * 参照設定 : OpenTK System.Drawing
 */

using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenGLTest
{
    class Game : GameWindow
    {
        #region Camera__Field

        bool isCameraRotating;      //カメラが回転状態かどうか
        Vector2 current, previous;  //現在の点、前の点
        Matrix4 rotate;             //回転行列
        float zoom;                 //拡大度
        //float wheelPrevious;        //マウスホイールの前の状態
        #endregion


        //800x600のウィンドウを作る。タイトルは「1-2:Camera」
        public Game()
            : base(800, 600, GraphicsMode.Default, "1-2:Camera")
        {
            #region Camera__Initialize

            isCameraRotating = false;
            current = Vector2.Zero;
            previous = Vector2.Zero;
            rotate = Matrix4.Identity;
            zoom = 1.0f;
            //wheelPrevious = 0.0f;

            #endregion

            #region Camera__Event

            //マウスボタンが押されると発生するイベント
            MouseDown += (sender, e) =>
            {
                //右ボタンが押された場合
                if (e.Button == MouseButton.Left)
                {
                    isCameraRotating = true;
                    current = new Vector2(e.X, e.Y);
                }
            };

            //マウスボタンが離されると発生するイベント
            MouseUp += (sender, e) =>
            {
                //右ボタンが押された場合
                if (e.Button == MouseButton.Left)
                {
                    isCameraRotating = false;
                    previous = Vector2.Zero;
                }
            };

            //マウスが動くと発生するイベント
            MouseMove += (sender, e) =>
            {
                //カメラが回転状態の場合
                if (isCameraRotating)
                {
                    //previous = current;
                    //current = new Vector2(e.X, e.Y);
                    //Vector2 delta = current - previous;
                    var delta = new Vector2(e.XDelta, e.YDelta);

                    delta /= (float)Math.Sqrt(Width * Width + Height * Height);
                    float length = delta.Length;
                    if (length > 0.0)
                    {
                        var rad = length * 5;       // マウスの動かした量に合わせて回転量を決める
                        float theta = (float)Math.Sin(rad) / length;
                        Quaternion after = new Quaternion(delta.Y * theta, delta.X * theta, 0.0f, (float)Math.Cos(rad));
                        //rotate = rotate * Matrix4.Rotate(after);
                        rotate = rotate * Matrix4.CreateFromQuaternion(after);
                    }
                }
            };

            //マウスホイールが回転すると発生するイベント
            MouseWheel += (sender, e) =>
            {
                //float delta = (float)this.Mouse.Wheel - (float)wheelPrevious;
                float delta = e.DeltaPrecise;
                zoom *= (float)Math.Pow(1.2, delta);

                //拡大、縮小の制限
                if (zoom > 2.0f)
                    zoom = 2.0f;
                if (zoom < 0.5f)
                    zoom = 0.5f;
                //wheelPrevious = this.Mouse.Wheel;
            };

            #endregion

            VSync = VSyncMode.On;
        }

        //ウィンドウの起動時に実行される。
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);     // 深度で隠面除去をON
        }

        //ウィンドウのサイズが変更された場合に実行される。
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }

        //画面更新で実行される。
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            var keyboard = Keyboard.GetState();
            //Escapeキーで終了
            if (keyboard[Key.Escape])
            {
                this.Exit();
            }

            #region Camera__Keyboard

            //F1キーで回転をリセット
            if (keyboard[Key.Number1])
            {
                rotate = Matrix4.Identity;
            }

            //F2キーでY軸90度回転
            if (keyboard[Key.Number2])
            {
                rotate = Matrix4.CreateRotationY(MathHelper.PiOver2);
            }

            //F3キーでY軸180度回転
            if (keyboard[Key.Number3])
            {
                rotate = Matrix4.CreateRotationY(MathHelper.Pi);
            }

            //F4キーでY軸270度回転
            if (keyboard[Key.Number4])
            {
                rotate = Matrix4.CreateRotationY(MathHelper.ThreePiOver2);
            }

            //F5キーで拡大をリセット
            if (keyboard[Key.Number5])
            {
                zoom = 1.0f;
            }

            #endregion
        }

        //画面描画で実行される。
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // バッファクリア
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            #region TransFormationMatrix

            // カメラ方向
            Matrix4 modelView = Matrix4.LookAt(Vector3.UnitZ * 10 / zoom, Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelView);       // カメラの方向をセット
            GL.MultMatrix(ref rotate);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Width / Height, 0.1f, 100.3f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            #endregion


            DrawPyramid();

            SwapBuffers();
        }

        //正四角錐を描画する。
        private void DrawPyramid()
        {
            //GL.Begin(BeginMode.Triangles);
            GL.Begin(PrimitiveType.Triangles);

            GL.Color4(Color4.Coral);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(0.0f, 0.0f, 1.0f);

            GL.Color4(Color4.Navy);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(0.0f, 0.0f, 1.0f);

            GL.Color4(Color4.Green);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(0.0f, 0.0f, 1.0f);

            GL.Color4(Color4.LightSkyBlue);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(0.0f, 0.0f, 1.0f);

            GL.Color4(Color4.LightYellow);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.End();
        }
    }
}