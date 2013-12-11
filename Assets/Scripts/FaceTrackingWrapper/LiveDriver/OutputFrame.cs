using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LiveDriverWrapper
{
    public class OutputFrame : WrapperBase
    {
        #region PInvokes
        [DllImport("LiveDriverNativeWrapper")]
        static private extern bool IOutputFrame_TrackingSucceeded(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern IntPtr IOutputFrame_GetImage(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern int IOutputFrame_GetImageNumber(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern IntPtr IOutputFrame_GetAnimationControls(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IOutputFrame_GetHeadPosition(IntPtr pObj, ref float x, ref float y, ref float z);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IOutputFrame_GetHeadRotation(IntPtr pObj, ref float x, ref float y, ref float z);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IOutputFrame_GetCharacterHeadPosition(IntPtr pObj, ref float x, ref float y, ref float z);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IOutputFrame_GetCharacterHeadRotation(IntPtr pObj, ref float x, ref float y, ref float z);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IOutputFrame_GetCameraPosition(IntPtr pObj, ref float x, ref float y, ref float z);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void IOutputFrame_GetCameraRotation(IntPtr pObj, ref float x, ref float y, ref float z);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern float IOutputFrame_GetCameraFocalLength(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern IntPtr IOutputFrame_GetPoints(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern ulong IOutputFrame_GetTimeStamp(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern double IOutputFrame_GetProcessingTime(IntPtr pObj);
        #endregion


        public OutputFrame(IntPtr pObj) : base(pObj)
        {
        }

        public bool TrackingSucceeded
        {
            get
            {
                return IOutputFrame_TrackingSucceeded(m_pObj);
            }
        }

        public Image Image
        {
            get
            {
                return new Image(IOutputFrame_GetImage(m_pObj));
            }
        }

        public int ImageNumber
        {
            get
            {
                return IOutputFrame_GetImageNumber(m_pObj);
            }
        }

        public AnimationControlList AnimationControls
        {
            get
            {
                return new AnimationControlList(IOutputFrame_GetAnimationControls(m_pObj));
            }
        }

        public Vector3 HeadPosition
        {
            get
            {
                float x=0, y=0, z=0;
                IOutputFrame_GetHeadPosition(m_pObj, ref x, ref y, ref z);
                return new Vector3(x, y, z);
            }
        }

        public Vector3 HeadRotation
        {
            get
            {
                float x = 0, y = 0, z = 0;
                IOutputFrame_GetHeadRotation(m_pObj, ref x, ref y, ref z);
                return new Vector3(x, y, z);
            }
        }

        public Vector3 CharacterHeadPosition
        {
            get
            {
                float x = 0, y = 0, z = 0;
                IOutputFrame_GetCharacterHeadPosition(m_pObj, ref x, ref y, ref z);
                return new Vector3(x, y, z);
            }
        }

        public Vector3 CharacterHeadRotation
        {
            get
            {
                float x = 0, y = 0, z = 0;
                IOutputFrame_GetCharacterHeadRotation(m_pObj, ref x, ref y, ref z);
                return new Vector3(x, y, z);
            }
        }

        public Vector3 CameraPosition
        {
            get
            {
                float x = 0, y = 0, z = 0;
                IOutputFrame_GetCameraPosition(m_pObj, ref x, ref y, ref z);
                return new Vector3(x, y, z);
            }
        }

        public Vector3 CameraRotation
        {
            get
            {
                float x = 0, y = 0, z = 0;
                IOutputFrame_GetCameraRotation(m_pObj, ref x, ref y, ref z);
                return new Vector3(x, y, z);
            }
        }

        public float CameraFocalLength
        {
            get
            {
                return IOutputFrame_GetCameraFocalLength(m_pObj);
            }
        }

        public PointList Points
        {
            get
            {
                return new PointList(IOutputFrame_GetPoints(m_pObj));
            }
        }

        public ulong TimeStamp
        {
            get
            {
                return IOutputFrame_GetTimeStamp(m_pObj);
            }
        }

        public double ProcessingTime
        {
            get
            {
                return IOutputFrame_GetProcessingTime(m_pObj);
            }
        }
    }
}
