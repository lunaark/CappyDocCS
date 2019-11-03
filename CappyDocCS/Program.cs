using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CappyDocCS
{
    public class Form1 : Form
    {
        // instantiate objects
        private NotifyIcon notifyIcon1;

        private ContextMenu contextMenu1;
        private MenuItem menuItem0;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private MenuItem menuItem6;
        private System.ComponentModel.IContainer components;
        private bool isRecording = false;

        private Overwatch Recorder = new Overwatch();
        private Project projHandle = new Project();

        private Icon offIco = Properties.Resources.off;
        private Icon onIco = Properties.Resources.on;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }

        public Form1()
        {
            // run config
            Cappy.Config();

            // init form
            InitializeComponent();

            // add app exit handler
            Application.ApplicationExit += new EventHandler(OnApplicationExit);

            // create context menu
            this.components = new System.ComponentModel.Container(); // specifically declare namespace and class, as Container is also a method declared in Novacode
            this.contextMenu1 = new ContextMenu();
            this.menuItem0 = new MenuItem();
            this.menuItem1 = new MenuItem();
            this.menuItem2 = new MenuItem();
            this.menuItem3 = new MenuItem();
            this.menuItem4 = new MenuItem();
            this.menuItem5 = new MenuItem();
            this.menuItem6 = new MenuItem();

            // Initialize contextMenu1
            this.contextMenu1.MenuItems.AddRange(
                new MenuItem[] { this.menuItem0 });

            this.contextMenu1.MenuItems.AddRange(
               new MenuItem[] { this.menuItem1 });

            this.contextMenu1.MenuItems.AddRange(
               new MenuItem[] { this.menuItem2 });

            this.contextMenu1.MenuItems.AddRange(
                new MenuItem[] { this.menuItem3 });

            this.contextMenu1.MenuItems.AddRange(
                new MenuItem[] { this.menuItem4 });

            this.contextMenu1.MenuItems.AddRange(
                new MenuItem[] { this.menuItem5 });

            this.contextMenu1.MenuItems.AddRange(
                new MenuItem[] { this.menuItem6 });

            // Create the NotifyIcon.
            this.notifyIcon1 = new NotifyIcon(this.components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = offIco;
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipTitle = "CappyDoc";

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon1.ContextMenu = this.contextMenu1;

            // Initialize menuItem0
            this.menuItem0.Index = 0;
            this.menuItem0.Text = "Start Record";
            this.menuItem0.Click += new EventHandler(this.menuItem0_Click);

            // Initialize menuItem1
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "Stop Record";
            this.menuItem1.Click += new EventHandler(this.menuItem1_Click);

            // Initialize menuItem2
            this.menuItem2.Index = 2;
            this.menuItem2.Text = "Create New Project";
            this.menuItem2.Click += new EventHandler(this.menuItem2_Click);

            // Initialize menuItem3
            this.menuItem3.Index = 3;
            this.menuItem3.Text = "Open Project";
            this.menuItem3.Click += new EventHandler(this.menuItem3_Click);

            // Initialize menuItem4
            this.menuItem4.Index = 4;
            this.menuItem4.Text = "Build Current Project";
            this.menuItem4.Click += new EventHandler(this.menuItem4_Click);

            // Initialize menuItem5
            this.menuItem5.Index = 5;
            this.menuItem5.Text = "Config Menu";
            this.menuItem5.Click += new EventHandler(this.menuItem5_Click);

            // Initialize menuItem6
            this.menuItem6.Index = 6;
            this.menuItem6.Text = "Exit";
            this.menuItem6.Click += new EventHandler(this.menuItem6_Click);

            // make sure stuff is greyed out
            menuItem0.Enabled = true;
            menuItem1.Enabled = false;
            menuItem4.Enabled = true;
        }

        // buttons
        private void menuItem0_Click(object Sender, EventArgs e)
        {
            if (!isRecording)
            {
                notifyIcon1.Icon = onIco;
                menuItem0.Enabled = false;
                menuItem1.Enabled = true;
                menuItem4.Enabled = false;

                isRecording = !isRecording;
                Recorder.Start();
            }
        }

        private void menuItem1_Click(object Sender, EventArgs e)
        {
            if (isRecording)
            {
                notifyIcon1.Icon = offIco;
                menuItem0.Enabled = true;
                menuItem1.Enabled = false;
                menuItem4.Enabled = true;

                isRecording = !isRecording;
                Recorder.Stop();
            }
        }

        private void menuItem2_Click(object Sender, EventArgs e)
        {
            projHandle.CreateProject();
        }

        private void menuItem3_Click(object Sender, EventArgs e)
        {
            projHandle.OpenProject();
        }

        private void menuItem4_Click(object Sender, EventArgs e)
        {
            string proj = File.ReadAllText(Cappy.ProjectPath);
            if (String.IsNullOrEmpty(proj))
            {
                MessageBox.Show("Project is empty, you must record some actions first!", "CappyDoc", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                projHandle.BuildProject();
            }
        }

        private void menuItem5_Click(object Sender, EventArgs e)
        {
            ConfigMenu configMenu = new ConfigMenu();
            configMenu.Show();
        }

        private void menuItem6_Click(object Sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void Dispose(bool disposing)
        {
            // Clean up any components being used.
            if (disposing)
                if (components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            notifyIcon1.Icon = null;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // Form1
            //
            this.ClientSize = new System.Drawing.Size(1, 1);
            this.Name = "Form1";
            this.Text = "CappyDoc";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            form.WindowState = FormWindowState.Minimized;
            form.ShowInTaskbar = false;
            form.Opacity = 0;
        }
    }
}