using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenGLTest
{
    public static class UtilGL
    {
        public static Color4 Color4(string color)
        {
            byte a, r, g, b;
            if (color.Length == 9)
            {
                a = Convert.ToByte(color.Substring(1, 2), 16);
                r = Convert.ToByte(color.Substring(3, 2), 16);
                g = Convert.ToByte(color.Substring(5, 2), 16);
                b = Convert.ToByte(color.Substring(7, 2), 16);
            }
            else if (color.Length == 7)
            {
                a = 0xff;
                r = Convert.ToByte(color.Substring(1, 2), 16);
                g = Convert.ToByte(color.Substring(3, 2), 16);
                b = Convert.ToByte(color.Substring(5, 2), 16);
            }
            else { throw new ArgumentException(); }
            return new Color4(r, g, b, a);
        }

        public static void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Normal3(-1.0f, 0.0f, 0.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Normal3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Normal3(0.0f, -1.0f, 0.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Normal3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.Normal3(0.0f, 0.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.End();
        }

        /// <summary>
        /// 球を描画します
        /// </summary>
        public static void DrawSphere()
        {
            int slices = 128, stacks = 128;   //横と縦の分割数
            double r = 1.24;                //半径
            for (int i = 0; i < stacks; i++)
            {
                //輪切り上部
                double upper = Math.PI / stacks * i;
                double upperHeight = Math.Cos(upper);
                double upperWidth = Math.Sin(upper);
                //輪切り下部
                double lower = Math.PI / stacks * (i + 1);
                double lowerHeight = Math.Cos(lower);
                double lowerWidth = Math.Sin(lower);

                GL.Begin(PrimitiveType.QuadStrip);
                for (int j = 0; j <= slices; j++)
                {
                    //輪切りの面を単位円としたときの座標
                    double rotor = 2 * Math.PI / slices * j;
                    double x = Math.Cos(rotor);
                    double y = Math.Sin(rotor);

                    GL.Normal3(x * lowerWidth, lowerHeight, y * lowerWidth);
                    GL.Vertex3(r * x * lowerWidth, r * lowerHeight, r * y * lowerWidth);
                    GL.Normal3(x * upperWidth, upperHeight, y * upperWidth);
                    GL.Vertex3(r * x * upperWidth, r * upperHeight, r * y * upperWidth);
                }
                GL.End();
            }
        }
    }
}
