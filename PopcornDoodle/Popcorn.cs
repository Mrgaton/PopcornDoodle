using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using PopcornDoodle.Properties;
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopcornDoodle
{
    public partial class Popcorn : Form
    {
        public Popcorn()
        {
            InitializeComponent();

            this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);

            this.TransparencyKey = this.BackColor = Color.White;

            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;

            Size originalSize = new Size(450, 800);
            double mult = 1.10;

            Rectangle screenSize = Screen.PrimaryScreen.Bounds;
            float widthFactor = (float)screenSize.Width / originalSize.Width;
            float heightFactor = (float)screenSize.Height / originalSize.Height;
            float scalingFactor = Math.Min(widthFactor, heightFactor);
            int newWidth = (int)((originalSize.Width * scalingFactor) / mult);
            int newHeight = (int)((originalSize.Height * scalingFactor) / mult);
            this.Size = new Size(newWidth, newHeight);
            this.aspectRatio = (float)this.Width / this.Height;

            this.transparentPanel.BringToFront();
            this.transparentPanel.Size = new Size(0, this.Height / 13);

            AttachMouseEvents(this.Controls);

            InitializeWebView();

            maskPictureBox.Image = Resources.google_frame_mask_cleaned;
            maskPictureBox.Visible = true;
        }

        private static WebView2 webView = null;

        private async Task InitializeWebView()
        {
            maskPictureBox.SendToBack();

            string userDataFolder = Path.Combine(Path.GetTempPath(), "POPCORN");

            if (!Directory.Exists(userDataFolder)) Directory.CreateDirectory(userDataFolder);
            if (!Directory.Exists(Path.Combine(userDataFolder, "runtimes")))
            {
                using (var ms = new MemoryStream(Resources.runtimes))
                {
                    new ZipArchive(ms).ExtractToDirectory(userDataFolder);
                }
            }

            Directory.SetCurrentDirectory(userDataFolder);

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            webView = new WebView2
            {
                Dock = DockStyle.Fill,
                DefaultBackgroundColor = Color.Transparent,
                Size = this.Size,
                TabIndex = 0,
            };

            await webView.EnsureCoreWebView2Async(environment);

            await webView.EnsureCoreWebView2Async(null);

            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            webView.CoreWebView2.NavigationCompleted += webView_NavigationCompleted;

            this.Controls.Add(webView);

            webView.BringToFront();

            transparentPanel.Dock = DockStyle.Top;
            transparentPanel.BringToFront();

            string query = string.Empty;
            string clipboard = Clipboard.GetText();

            if (clipboard.Contains("g.co/doodle"))
            {
                var location = (await (new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false }).SendAsync(new HttpRequestMessage(HttpMethod.Get, clipboard)))).Headers;

                if (location != null) query = location.Location.Query;
            }

            webView.Source = new Uri("https://www.google.com/logos/2024/popcorn/rc4/popcorn.html" + query);
        }

        private async void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.");
            }
        }

        private void webView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                webView.CoreWebView2.ExecuteScriptAsync("function waitForElementToExist(t,e){let l=setInterval((()=>{let n=document.getElementById(t);n&&(clearInterval(l),e(n))}),100)}function clickUntilGone(t){waitForElementToExist(t,(e=>{let l=setInterval((()=>{document.getElementById(t)?e.click():clearInterval(l)}),100)}))}clickUntilGone(\"ctaRoot\");");
            }
            else
            {
                MessageBox.Show("Navigation to the page failed.");
            }
        }

        private void AttachMouseEvents(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.MouseDown += new MouseEventHandler(this.WebView_MouseDown);
                control.MouseMove += new MouseEventHandler(this.WebView_MouseMove);
                control.MouseUp += new MouseEventHandler(this.WebView_MouseUp);

                Console.WriteLine($"Attached events to {control.Name}");

                if (control.Controls.Count > 0)
                {
                    AttachMouseEvents(control.Controls);
                }
            }
        }

        private Point mouseOffset;
        private bool isMouseDown = false;

        private void WebView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = new Point(-e.X, -e.Y);
                isMouseDown = true;
            }
        }

        private void WebView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                this.Location = mousePos;
            }
        }

        private void WebView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private bool isResizing = false;
        private Point lastMousePosition;
        private Size initialSize;
        private float aspectRatio;

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isResizing = true;
                lastMousePosition = e.Location;
                initialSize = this.Size;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                int deltaX = e.X - lastMousePosition.X;
                int deltaY = e.Y - lastMousePosition.Y;

                int newWidth = initialSize.Width + deltaX;
                int newHeight = (int)(newWidth / aspectRatio);

                this.Size = new Size(newWidth, newHeight);
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isResizing = false;
            }
        }
    }
}