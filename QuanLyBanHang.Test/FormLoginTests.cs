using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote; // Cần cho DesiredCapabilities
using System;
using System.Threading;

namespace QuanLyBanHang.Test
{
    [TestClass]
    public class FormLoginTests
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string AppPath = @"D:\pjKiemThu_dh12c3\setup.exe"; // Đường dẫn đến ứng dụng của bạn
        private const string FormLoginTitle = "Đăng nhập"; // Tên hiển thị của form đăng nhập

        protected static WindowsDriver<WindowsElement> session;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            if (session == null)
            {
                DesiredCapabilities capabilities = new DesiredCapabilities();
                capabilities.SetCapability("app", AppPath);
                capabilities.SetCapability("deviceName", "WindowsPC");

                try
                {
                    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), capabilities);
                    session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    // Chờ cho form đăng nhập xuất hiện dựa trên tiêu đề
                    Thread.Sleep(2000); // Chờ một chút trước khi tìm cửa sổ
                    try
                    {
                        var loginWindow = session.FindWindow(FormLoginTitle);
                        session.SwitchTo().Window(loginWindow.WindowHandle);
                    }
                    catch (NoSuchWindowException)
                    {
                        Console.WriteLine($"Không tìm thấy cửa sổ '{FormLoginTitle}'. Kiểm tra tiêu đề cửa sổ.");
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi tạo session hoặc chuyển sang form đăng nhập: {ex.Message}");
                    throw;
                }
            }
        }

        [ClassCleanup]
        public static void TearDown()
        {
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }

        [TestMethod]
        public void LoginSuccessTest()
        {
            // Tìm các phần tử trên form đăng nhập
            var usernameField = session.FindElementByAccessibilityId("txbTaiKhoan");
            var passwordField = session.FindElementByAccessibilityId("txbMatKhau");
            var loginButton = session.FindElementByAccessibilityId("btnDangNhap");

            // Nhập thông tin đăng nhập hợp lệ (bạn cần thay thế bằng dữ liệu test thực tế)
            usernameField.SendKeys("admin");
            passwordField.SendKeys("admin");

            // Nhấp vào nút đăng nhập
            loginButton.Click();

            // Kiểm tra xem đã chuyển sang form chính (frmMain.cs) hay chưa
            Thread.Sleep(2000); // Chờ chuyển trang
            try
            {
                var mainWindow = session.FindWindow("Chương trình quản lý bán hàng Laptop [admin]"); // **CHẮC CHẮN TIÊU ĐỀ CỬA SỔ CHÍNH LÀ "Chương trình quản lý bán hàng Laptop [admin]"**
                Assert.IsNotNull(mainWindow, "Đăng nhập thành công nhưng không chuyển sang form chính.");
            }
            catch (NoSuchWindowException)
            {
                Assert.Fail("Đăng nhập thành công nhưng không chuyển sang form chính.");
            }
        }

        [TestMethod]
        public void LoginFailureWrongUsernameTest()
        {
            // Tìm các phần tử trên form đăng nhập
            var usernameField = session.FindElementByAccessibilityId("txbTaiKhoan");
            var passwordField = session.FindElementByAccessibilityId("txbMatKhau");
            var loginButton = session.FindElementByAccessibilityId("btnDangNhap");

            // Nhập tên người dùng sai, mật khẩu đúng
            usernameField.SendKeys("invalid_username");
            passwordField.SendKeys("admin");
            loginButton.Click();

            // Kiểm tra thông báo lỗi
            try
            {
                var errorMessage = session.FindElementByName("Sai tên đăng nhập hoặc mật khẩu");
                Assert.IsNotNull(errorMessage, "Không tìm thấy thông báo lỗi khi nhập sai tên đăng nhập.");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Không tìm thấy thông báo lỗi khi nhập sai tên đăng nhập.");
            }

            // Kiểm tra xem vẫn ở form đăng nhập (tiêu đề vẫn là "Đăng nhập")
            Assert.AreEqual(FormLoginTitle, session.Title, "Sau khi đăng nhập thất bại, form đăng nhập không còn hiển thị.");
        }

        // Thêm các test case khác tương tự...
    }
}