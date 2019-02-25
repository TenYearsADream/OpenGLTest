using System;

namespace OpenGLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var config = new WindowConfig() { Width = 1280, Height = 720, FullScreen = false };
            //using(var window = new ElffyWindow(config.Width, config.Height, "タイトル", config.FullScreen)){
            //    window.Run(30.0);
            //}

            //using (var window = new LightSample())
            //{
            //    window.Run(30.0);
            //}

            using (var window = new MyLight())
            {
                window.Run(30.0);
            }

            //using (var window = new VBOSample())
            //{
            //    window.Run();
            //}
        }
    }
}
