using FluentSharp.CoreLib;
using FluentSharp.NUnit;
using FluentSharp.Watin;
using FluentSharp.WinForms;
using NUnit.Framework;

namespace UnitTests.FluentSharp_WatiN.APIs
{
    [TestFixture]
    public class Test_IE_UnitTest : NUnitTests
    {
        IE_UnitTest ieUnitTest;      
        WatiN_IE    ie;

        [SetUp] public void setup()
        {                        
            ieUnitTest = new IE_UnitTest();    
            ie = ieUnitTest.ie;   
        }

        [TearDown] public void teardown()
        {
       //     ieUnitTest.close_IE();
        }

        [Test] public void IE_UnitTest_Ctor()
        {            
            Assert.NotNull(ieUnitTest.FormTitle);         
        }       

        [Test] public void openIE()
        {                        
            Assert.IsNull (ieUnitTest.TargetServer);            
            Assert.NotNull(ieUnitTest.ie);
            Assert.AreEqual(ieUnitTest.ie.parentForm().get_Text(), ieUnitTest.FormTitle);
            Assert.NotNull(ieUnitTest.ie);            
        }

        [Test] public void Open_Site_O2Platform()
        {
            this.ignore_If_Offline();
            
            ie.show();

            var o2platform = "http://o2platform.com";            

                     
            ie.open(o2platform);     
            "URL: {0}".info(ie.url());
            Assert.IsTrue(ie.url().contains("o2platform"));

            1000.sleep();
            ie.hide();
            1000.sleep();            
        }        
    }
}
