using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LiveDriverWrapper
{
    public class Point : WrapperBase
    {
        #region PIvokes
        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IPoint_GetValue(IntPtr pObj, ref float x, ref float y);
        #endregion

        public Point(IntPtr pObj) : base(pObj)
        {
        }

        public Vector2 Value
        {
            get
            {
                float x = 0, y = 0;
                IPoint_GetValue(m_pObj, ref x, ref y);
                return new Vector2(x, y);
            }
        }
		
    }

    public class PointList : WrapperBase
    {
        #region PIvokes
        [DllImport("LiveDriverNativeWrapper")]
        static private extern int IPointList_GetCount(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern IntPtr IPointList_GetPoint(IntPtr pObj, int i);
        #endregion


        public PointList(IntPtr pObj) : base(pObj)
        {
        }

        public int Count
        {
            get
            {
                return IPointList_GetCount(m_pObj);
            }
        }

        public Point this[int i]
        {
            get
            {
                return new Point(IPointList_GetPoint(m_pObj, i));
            }
        }
		
		public Point GetPoint(int i) {
			return this[i];
		}
		
		public Point[] ToArray() {
			Point[] array = new Point[Count];
			for (int i = 0; i < Count; i++) {
				array[i] = this[i];
			}
			return array;
		}
    }
}
