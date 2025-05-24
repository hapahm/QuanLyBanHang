using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;



namespace QuanLyBanHang.Test
{
    [TestClass]
    public class TestLogin
    {
        private const string WinAppDriverUrl = "http://127.0.0.1:4723";
        private const string AppPath = @"D:\pjKiemThu_dh12c3\setup.exe"; // Đường dẫn tới file .exe ứng dụng WinForms
        private static WindowsDriver<WindowsElement> session;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("deviceName", "WindowsPC");
            session = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), options);
            Assert.IsNotNull(session);
        }

        [TestMethod]
        public void TestLogin_Success()
        {
            // Tìm và nhập username
            var txtUsername = session.FindElementByAccessibilityId("txtUsername");
            txtUsername.Clear();
            txtUsername.SendKeys("admin");

            // Tìm và nhập password
            var txtPassword = session.FindElementByAccessibilityId("txtPassword");
            txtPassword.Clear();
            txtPassword.SendKeys("123456");

            // Nhấn nút đăng nhập
            var btnLogin = session.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

            // Kiểm tra kết quả
            var lblStatus = session.FindElementByAccessibilityId("lblStatus");
            Assert.AreEqual("Đăng nhập thành công", lblStatus.Text);
        }

        [TestMethod]
        public void TestLogin_Failure()
        {
            var txtUsername = session.FindElementByAccessibilityId("txtUsername");
            txtUsername.Clear();
            txtUsername.SendKeys("saiuser");

            var txtPassword = session.FindElementByAccessibilityId("txtPassword");
            txtPassword.Clear();
            txtPassword.SendKeys("saimk");

            var btnLogin = session.FindElementByAccessibilityId("btnLogin");
            btnLogin.Click();

            var lblStatus = session.FindElementByAccessibilityId("lblStatus");
            Assert.AreEqual("Đăng nhập thất bại", lblStatus.Text);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            if (session != null)
            {
                session.CloseApp();
                session.Quit();
                session = null;
            }
        }
    }
}
