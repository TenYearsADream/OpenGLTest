using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenGLTest
{
    public class FPSManager
    {
        private const int BUF_MAX = 30;
        private Queue<double> _buf = new Queue<double>(BUF_MAX);
        private double _fps;

        /// <summary>
        /// フレーム間の時間を集計
        /// </summary>
        /// <param name="frameSpan">Frame span (sec)</param>
        public void Aggregate(double frameSpan)
        {
            if (_buf.Count >= BUF_MAX) { _buf.Dequeue(); }
            _buf.Enqueue(frameSpan);
            _fps = 1.0 / (_buf.Sum() / _buf.Count);
        }

        /// <summary>
        /// 平均FPSを取得します。スレッドセーフです。
        /// </summary>
        /// <returns>平均FPS</returns>
        public double GetFPS()
        {
            return _fps;
        }
    }
}
