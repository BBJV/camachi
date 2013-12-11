using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LiveDriverWrapper
{
    public class LiveDriver : WrapperBase
    {
        #region PInvokes
        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_AddLoggingCallback(IntPtr pObj, IntPtr value);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_RemoveLoggingCallback(IntPtr pObj, IntPtr value);
        
        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_ClearLoggingCallback(IntPtr pObj);
                
        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_AddOutputCallback(IntPtr pObj, IntPtr value);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_RemoveOutputCallback(IntPtr pObj, IntPtr value);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_ClearOutputCallback(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_SetImageRotation(IntPtr pObj, int rotation);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern IntPtr ILiveDriver_CreateImage(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_DestroyImage(IntPtr pObj, IntPtr image);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_SetCurrentTrackingModel(IntPtr pObj, string trackingModelFile);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern string ILiveDriver_GetCurrentTrackingModel(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriver_SetFailureThreshold(IntPtr pObj, float value);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern float ILiveDriver_GetFailureThreshold(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern bool ILiveDriver_Start(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern bool ILiveDriver_Stop(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern bool ILiveDriver_ResetTracking(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern bool ILiveDriver_Calibrate(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern bool ILiveDriver_IsCalibrating(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern bool ILiveDriver_IsCalibrated(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern ulong ILiveDriver_AddImage(IntPtr pObj, IntPtr image);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern ulong ILiveDriver_AddImageFile(IntPtr pObj, string imageFile);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern IntPtr ILiveDriver_GetLatestOutput(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern double ILiveDriver_GetVideoFps(IntPtr pObj);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern double ILiveDriver_GetTrackingFps(IntPtr pObj);
        #endregion
		
        public LiveDriver(IntPtr pObj) : base(pObj)
        {

        }
				
        public string CurrentTrackingModel
        {
            set
            {
                ILiveDriver_SetCurrentTrackingModel(m_pObj, value);
            }
            get
            {
                return ILiveDriver_GetCurrentTrackingModel(m_pObj);
            }
        }

        public float FailureThreshold
        {
            set
            {
                ILiveDriver_SetFailureThreshold(m_pObj, value);
            }
            get
            {
                return ILiveDriver_GetFailureThreshold(m_pObj);
            }
        }

        public int ImageRotation
        {
            set
            {
                ILiveDriver_SetImageRotation(m_pObj, value);
            }
        }

        public bool IsCalibrating
        {
            get
            {
                return ILiveDriver_IsCalibrating(m_pObj);
            }
        }

        public bool IsCalibrated
        {
            get
            {
                return ILiveDriver_IsCalibrated(m_pObj);
            }
        }

        public double VideoFPS
        {
            get
            {
                return ILiveDriver_GetVideoFps(m_pObj);
            }
        }

        public double TrackingFPS
        {
            get
            {
                return ILiveDriver_GetTrackingFps(m_pObj);
            }
        }

        public OutputFrame LatestOutput
        {
            get
            {
                return new OutputFrame(ILiveDriver_GetLatestOutput(m_pObj));
            }
        }

        public bool Start()
        {
            return ILiveDriver_Start(m_pObj);
        }

        public bool Stop()
        {
            return ILiveDriver_Stop(m_pObj);
        }

        public bool ResetTracking()
        {
            return ILiveDriver_ResetTracking(m_pObj);
        }

        public bool Calibrate()
        {
            return ILiveDriver_Calibrate(m_pObj);
        }

        public Image CreateImage()
        {
            return new Image(ILiveDriver_CreateImage(m_pObj));
        }

        public void DestoryImage(Image image)
        {
            ILiveDriver_DestroyImage(m_pObj, image.NativePtr);
        }

        public ulong AddImage(Image image)
        {
            return ILiveDriver_AddImage(m_pObj, image.NativePtr);
        }

        public ulong AddImage(string imageFile)
        {
            return ILiveDriver_AddImageFile(m_pObj, imageFile);
        }

    }

	
	
    public delegate void LogCallbackFunc(string message);
    public delegate void FrameCallbackFunc();
	
    class CallbackHandler : WrapperBase, IDisposable
    {
        #region PInvokes
        [DllImport("LiveDriverNativeWrapper")]
        static private extern IntPtr CallbackHandler_Create();

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void CallbackHandler_Dispose(IntPtr pObj);

        #endregion

        public CallbackHandler() : base(CallbackHandler_Create())
        {
			
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            if (m_pObj != IntPtr.Zero)
            {
                CallbackHandler_Dispose(m_pObj);
                m_pObj = IntPtr.Zero;
            }
            if (bDisposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~CallbackHandler()
        {
            Dispose(false);
        }

    }
}
