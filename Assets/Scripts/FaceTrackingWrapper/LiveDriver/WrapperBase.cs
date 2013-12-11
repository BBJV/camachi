using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveDriverWrapper
{
    public class WrapperBase
    {
        protected IntPtr m_pObj;
        public IntPtr NativePtr
        {
            get
            {
                return m_pObj;
            }
        }

        public WrapperBase(IntPtr pObj)
        {
            m_pObj = pObj;
        }
    }
}
