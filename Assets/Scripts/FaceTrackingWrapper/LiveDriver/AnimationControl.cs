using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace LiveDriverWrapper
{
    public class AnimationControl : WrapperBase
    {        
        #region PInvokes
        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IAnimationControl_GetName(IntPtr pObj, StringBuilder builder);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern float IAnimationControl_GetValue(IntPtr pObj);

        #endregion

        public AnimationControl(IntPtr pObj) : base(pObj)
        {
        }

        public string Name
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                IAnimationControl_GetName(m_pObj, builder);
                return builder.ToString();
            }
        }

        public float Value
        {
            get
            {
                return IAnimationControl_GetValue(m_pObj);
            }
        }
    }

    public class AnimationControlList : WrapperBase
    {
        #region PInvokes
        [DllImport("LiveDriverNativeWrapper")]
        static private extern int IAnimationControlList_GetCount(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern IntPtr IAnimationControlList_GetControl(IntPtr pObj, int i);

        #endregion

        public AnimationControlList(IntPtr pObj) : base(pObj)
        {
        }

        public int Count
        {
            get
            {
                return IAnimationControlList_GetCount(m_pObj);
            }
        }

        public AnimationControl this[int i]
        {
            get
            {
                return new AnimationControl(IAnimationControlList_GetControl(m_pObj, i));
            }
        }
		
		public AnimationControl GetControlPoint(int i) {
			return this[i];
		}
		
		public AnimationControl[] ToArray() {
			AnimationControl[] array = new AnimationControl[Count];
			for (int i = 0; i < Count; i++) {
				array[i] = this[i];
			}
			return array;
		}
    }
}
