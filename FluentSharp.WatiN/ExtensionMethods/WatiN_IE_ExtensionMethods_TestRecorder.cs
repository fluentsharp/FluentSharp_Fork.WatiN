using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;

namespace FluentSharp.Watin
{
    public static class WatiN_IE_ExtensionMethods_TestRecorder
    {
    
        public static WatiN_IE startRecorder(this WatiN_IE watinIe)
        {
            return watinIe.testRecorder(true);
        }
    	
        public static WatiN_IE testRecorder(this WatiN_IE watinIe)
        {
            return watinIe.testRecorder(true);
        }
    	
        public static WatiN_IE testRecorder(this WatiN_IE watinIe, bool executeInNewProcess)
        {
            if (executeInNewProcess)
                Processes.startProcess("Test Recorder");
            else
            {
                O2Thread.staThread(()=>{ "Test Recorder".assembly()
                                                        .type("Program")
                                                        .invokeStatic("Main", new string[] {});
                });
	
            }
            return watinIe;
        }
    }
}