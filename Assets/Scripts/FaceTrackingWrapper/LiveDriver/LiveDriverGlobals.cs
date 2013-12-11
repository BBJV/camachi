using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace LiveDriverWrapper
{
    public class LiveDriverGlobals
    { 
        #region PInvokes
        [DllImport("LiveDriverNativeWrapper")]
        static private extern IntPtr ILiveDriverGlobals_CreateLiveDriver(string licenseKey);

        [DllImport("LiveDriverNativeWrapper")]
        static private extern void ILiveDriverGlobals_DestroyLiveDriver(IntPtr liveDriver);
        #endregion

        static public LiveDriver CreateLiveDriver(string licenseKey) {
            return new LiveDriver(ILiveDriverGlobals_CreateLiveDriver(licenseKey));
        }

        static public void DestoryLiveDriver(LiveDriver liveDriver)
        {
            ILiveDriverGlobals_DestroyLiveDriver(liveDriver.NativePtr);
        }
    }
}
