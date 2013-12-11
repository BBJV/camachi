using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace LiveDriverWrapper
{
    public class Image : WrapperBase
    {
        #region PInvokes
        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IImage_SetFromImageFile(IntPtr pObj, string file);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IImage_SetFromImageBuffer(IntPtr pObj, byte[] data, int length);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IImage_SetFromRawBuffer(IntPtr pObj, byte[] data, int length, int width, int height, int depth, bool bgr);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern int IImage_GetLength(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IImage_GetValue(IntPtr pObj, byte[] data, ref int width, ref int height, ref int depth, bool bgr);
        #endregion

        public Image(IntPtr pObj) : base(pObj)
        {
        }

        public void SetFromImageFile(string file)
        {
            IImage_SetFromImageFile(m_pObj, file);
        }

        public void SetFromImageBuffer(byte[] data)
        {
            IImage_SetFromImageBuffer(m_pObj, data, data.Length);
        }

        public void SetFromRawBuffer(byte[] data, int width, int height, int depth, bool bgr)
        {
            IImage_SetFromRawBuffer(m_pObj, data, data.Length, width, height, depth, bgr);
        }

        public int Length
        {
            get
            {
                return IImage_GetLength(m_pObj);
            }
        }

        public void GetValue(out byte[] data, out int width, out int height, out int depth, bool bgr)
        {
            data = new byte[this.Length];
            width = 0;
            height = 0;
            depth = 0;
            IImage_GetValue(m_pObj, data, ref width, ref height, ref depth, bgr);
        }
    }
}
