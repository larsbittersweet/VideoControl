using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoControl
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint waveOutGetNumDevs();
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutGetDevCaps(IntPtr hwo, ref WAVEOUTCAPS pwoc, uint cbwoc);

        [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
        public struct WAVEOUTCAPS
        {
            public short wMid;
            public short wPid;
            public int vDriverVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public uint dwFormats;
            public short wChannels;
            public short wReserved;
            public uint dwSupport;

            public override string ToString()
            {
                return string.Format("wMid:{0}|wPid:{1}|vDriverVersion:{2}|'szPname:{3}'|dwFormats:{4}|wChannels:{5}|wReserved:{6}|dwSupport:{7}", new object[] { wMid, wPid, vDriverVersion, szPname, dwFormats, wChannels, wReserved, dwSupport });
            }
        }

        public const int C_MAXPNAMELEN = 32;
        public const int WAVE_MAPPER = -1;  //  'Specifies default device ID
        public int iAuto = 0;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int iTurn = 0;

        public MainWindow()
        {
            InitializeComponent();
        }
        List<string> stFiles1 = new List<string>();
        List<string> stFiles2 = new List<string>();
        List<string> stFiles3 = new List<string>();
        List<string> stFiles4 = new List<string>();
        string stLogo1 = "";
        string stLogo2 = "";
        string stLogo3 = "";
        string stLogo4 = "";
        Logo l1;
        Logo l2;
        Logo l3;
        Logo l4;
        const int NONE = 0;
        const int VIDEO = 1;
        const int LOGO = 2;
        private void btnVid1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = true;
            if (of.ShowDialog() == true)
            {
                if (of.FileNames != null)
                {
                    foreach (string st in of.FileNames)
                    {
                        stFiles1.Add(st);
                    }
                }
            }
            RefreshView1();
            SaveFiles1();
        }

        private void btnVid1_Clr_Click(object sender, RoutedEventArgs e)
        {
            stFiles1.Clear();
            RefreshView1();
            SaveFiles1();
        }

        private void btnVid1_Start_Click(object sender, RoutedEventArgs e)
        {
            if (chkEnabled1.IsChecked == true)
            {
                string stProg = "\"C:\\Program Files\\VideoLAN\\VLC\\vlc.exe\"";
                string stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound1.SelectedValue.ToString() + "\" --loop  --fullscreen --qt-fullscreen-screennumber=1";
                if (cmbRot1.Text != "none")
                {
                    stTemp += " :vout-filter=transform --video-filter \"transform{ true}\" --transform-type=" + cmbRot1.Text;
                }
                foreach (string st in stFiles1)
                {
                    stTemp += " \"" + st + "\" \"" + Directory.GetCurrentDirectory() + "\\black.png\"";
                }
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = stProg;
                startInfo.Arguments = stTemp;
                Process.Start(startInfo);
            }
        }

        private void RefreshView1()
        {
            txtFiles1.Text = "";
            foreach (string st in stFiles1)
            {
                txtFiles1.Text += st + "\r\n";
            }
        }
        private void RefreshView3()
        {
            txtFiles3.Text = "";
            foreach (string st in stFiles3)
            {
                txtFiles3.Text += st + "\r\n";
            }
        }
        private void RefreshView4()
        {
            txtFiles4.Text = "";
            foreach (string st in stFiles4)
            {
                txtFiles4.Text += st + "\r\n";
            }
        }
        private void SaveFiles1()
        {
            if (Properties.Settings.Default.Files1 != null)
            {
                Properties.Settings.Default.Files1.Clear();
            }
            foreach (string st in stFiles1)
            {
                Properties.Settings.Default.Files1.Add(st);
            }
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void SaveFiles3()
        {
            if (Properties.Settings.Default.Files3 != null)
            {
                Properties.Settings.Default.Files3.Clear();
            }
            foreach (string st in stFiles3)
            {
                Properties.Settings.Default.Files3.Add(st);
            }
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }
        private void SaveFiles4()
        {
            if (Properties.Settings.Default.Files4 != null)
            {
                Properties.Settings.Default.Files4.Clear();
            }
            foreach (string st in stFiles4)
            {
                Properties.Settings.Default.Files4.Add(st);
            }
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }
        private void SaveFiles2()
        {
            if (Properties.Settings.Default.Files2 != null)
            {
                Properties.Settings.Default.Files2.Clear();
            }
            foreach (string st in stFiles2)
            {
                Properties.Settings.Default.Files2.Add(st);
            }
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }
        private void RefreshView2()
        {
            txtFiles2.Text = "";
            foreach (string st in stFiles2)
            {
                txtFiles2.Text += st + "\r\n";
            }
        }
        private void btnVid2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = true;
            if (of.ShowDialog() == true)
            {
                if (of.FileNames != null)
                {
                    foreach (string st in of.FileNames)
                    {
                        stFiles2.Add(st);
                    }
                }
            }
            RefreshView2();
            SaveFiles2();
        }

        private void btnVid2_Clr_Click(object sender, RoutedEventArgs e)
        {
            stFiles2.Clear();
            RefreshView2();
            SaveFiles2();

        }

        private void btnVid2_Start_Click(object sender, RoutedEventArgs e)
        {
            if (chkEnabled2.IsChecked == true)
            {
                string stProg = "\"C:\\Program Files\\VideoLAN\\VLC\\vlc.exe\"";
                string stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound2.SelectedValue.ToString() + "\" --loop  --fullscreen --qt-fullscreen-screennumber=2";
                if (cmbRot2.Text != "none")
                {
                    stTemp += " :vout-filter=transform --video-filter \"transform{ true}\" --transform-type=" + cmbRot2.Text;
                }
                foreach (string st in stFiles2)
                {
                    stTemp += " \"" + st + "\" \"" + Directory.GetCurrentDirectory() + "\\black.png\"";
                }
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = stProg;
                startInfo.Arguments = stTemp;
                Process.Start(startInfo);
            }
        }

        private void btnLogos_Click(object sender, RoutedEventArgs e)
        {
            if (stLogo1 != "")
            {
                l1 = new Logo(stLogo1, 2);
                l1.Show();
            }
            if (stLogo2 != "")
            {
                l2 = new Logo(stLogo2, 0);
                l2.Show();
            }
            if (stLogo3 != "")
            {
                l3 = new Logo(stLogo3, 0);
                l3.Show();
            }
            if (stLogo4 != "")
            {
                l4 = new Logo(stLogo4, 0);
                l4.Show();
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            StopAll();
        }

        private void StopAll()
        {
            string stProg = "taskkill";
            string stTemp = "/im vlc.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = stProg;
            startInfo.Arguments = stTemp;
            Process.Start(startInfo);

            /*
                        stTemp = "/im slide1.exe";

                        startInfo.Arguments = stTemp;

                        Process.Start(startInfo);

                        stTemp = "/im slide2.exe";

                        startInfo.Arguments = stTemp;

                        Process.Start(startInfo);
            */
            try
            {
                l1.Close();
            }
            catch { }
            try
            {
                l2.Close();
            }
            catch { }
            try
            {
                l3.Close();
            }
            catch { }
            try
            {
                l4.Close();
            }
            catch { }
        }

        private void btnVid2_Start_all_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = TimeSpan.FromSeconds(0);
            if (chkToggle.IsChecked == true)
            {
                int i = 0;
                if (int.TryParse(txtTime.Text, out i))
                {
                    ts = TimeSpan.FromSeconds(i);
                }
                else
                {
                    MessageBox.Show("invalid time");
                }
                if (ts == TimeSpan.FromSeconds(0))
                {
                    MessageBox.Show("invalid time");
                }
                else
                {
                    dispatcherTimer.Interval = ts;
                    dispatcherTimer.Start();
                }
            }
            StartAllVideos();

        }

        private void StartAllVideos()
        {
            string stProg = "\"C:\\Program Files\\VideoLAN\\VLC\\vlc.exe\"";
            string stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound2.Text + "\" --loop  --fullscreen --qt-fullscreen-screennumber=2";
            if ((chkToggle.IsChecked == true) && iTurn % 2 == 1)
            {
                stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound2a.Text + "\" --loop  --fullscreen --qt-fullscreen-screennumber=2";
            }

            if (cmbRot2.Text != "none")
            {
                stTemp += " :vout-filter=transform --video-filter \"transform{ true}\" --transform-type=" + cmbRot2.Text;
            }
            foreach (string st in stFiles2)
            {
                stTemp += " \"" + st + "\" \"" + Directory.GetCurrentDirectory() + "\\black.png\"";
            }

            if (chkEnabled2.IsChecked == true)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = stProg;
                startInfo.Arguments = stTemp;
                Process.Start(startInfo);
            }
            stProg = "\"C:\\Program Files\\VideoLAN\\VLC\\vlc.exe\"";
            stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound1.Text + "\" --loop  --fullscreen --qt-fullscreen-screennumber=1";
            if ((chkToggle.IsChecked == true) && iTurn % 2 == 1)
            {
                stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound1a.Text + "\" --loop  --fullscreen --qt-fullscreen-screennumber=1";
            }

            if (cmbRot1.Text != "none")
            {
                stTemp += " :vout-filter=transform --video-filter \"transform{ true}\" --transform-type=" + cmbRot1.Text;
            }
            foreach (string st in stFiles1)
            {
                stTemp += " \"" + st + "\" \"" + Directory.GetCurrentDirectory() + "\\black.png\"";
            }
            if (chkEnabled1.IsChecked == true)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = stProg;
                startInfo.Arguments = stTemp;
                Process.Start(startInfo);
            }
            stProg = "\"C:\\Program Files\\VideoLAN\\VLC\\vlc.exe\"";
            stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound3.Text + "\" --loop  --fullscreen --qt-fullscreen-screennumber=3";
            if ((chkToggle.IsChecked == true) && iTurn % 2 == 1)
            {
                stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound3a.Text + "\" --loop  --fullscreen --qt-fullscreen-screennumber=3";
            }

            if (cmbRot3.Text != "none")
            {
                stTemp += " :vout-filter=transform --video-filter \"transform{ true}\" --transform-type=" + cmbRot3.Text;
            }
            foreach (string st in stFiles3)
            {
                stTemp += " \"" + st + "\" \"" + Directory.GetCurrentDirectory() + "\\black.png\"";
            }
            if (chkEnabled3.IsChecked == true)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = stProg;
                startInfo.Arguments = stTemp;
                Process.Start(startInfo);
            }
            stProg = "\"C:\\Program Files\\VideoLAN\\VLC\\vlc.exe\"";
            stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound4.Text + "\" --loop  --fullscreen --qt-fullscreen-screennumber=4";
            if ((chkToggle.IsChecked == true) && iTurn % 2 == 1)
            {
                stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound4a.Text + "\" --loop  --fullscreen --qt-fullscreen-screennumber=4";
            }

            if (cmbRot4.Text != "none")
            {
                stTemp += " :vout-filter=transform --video-filter \"transform{ true}\" --transform-type=" + cmbRot4.Text;
            }
            foreach (string st in stFiles4)
            {
                stTemp += " \"" + st + "\" \"" + Directory.GetCurrentDirectory() + "\\black.png\"";
            }
            if (chkEnabled4.IsChecked == true)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = stProg;
                startInfo.Arguments = stTemp;
                Process.Start(startInfo);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WAVEOUTCAPS[] temp;
            temp = GetDevCapsPlayback();
            foreach (WAVEOUTCAPS w in temp)
            {
                cmbSound1.Items.Add(w.szPname + " ($" + w.wMid.ToString("X") + ",$" + w.wPid.ToString("X") + ")");
                cmbSound2.Items.Add(w.szPname + " ($" + w.wMid.ToString("X") + ",$" + w.wPid.ToString("X") + ")");
                cmbSound3.Items.Add(w.szPname + " ($" + w.wMid.ToString("X") + ",$" + w.wPid.ToString("X") + ")");
                cmbSound4.Items.Add(w.szPname + " ($" + w.wMid.ToString("X") + ",$" + w.wPid.ToString("X") + ")");
                cmbSound1a.Items.Add(w.szPname + " ($" + w.wMid.ToString("X") + ",$" + w.wPid.ToString("X") + ")");
                cmbSound2a.Items.Add(w.szPname + " ($" + w.wMid.ToString("X") + ",$" + w.wPid.ToString("X") + ")");
                cmbSound3a.Items.Add(w.szPname + " ($" + w.wMid.ToString("X") + ",$" + w.wPid.ToString("X") + ")");
                cmbSound4a.Items.Add(w.szPname + " ($" + w.wMid.ToString("X") + ",$" + w.wPid.ToString("X") + ")");
            }
            try
            {
                cmbSound1.Text = Properties.Settings.Default.Soundcard1;
                cmbSound2.Text = Properties.Settings.Default.Soundcard2;
                cmbSound3.Text = Properties.Settings.Default.Soundcard3;
                cmbSound4.Text = Properties.Settings.Default.Soundcard4;
                cmbSound1a.Text = Properties.Settings.Default.Soundcard1a;
                cmbSound2a.Text = Properties.Settings.Default.Soundcard2a;
                cmbSound3a.Text = Properties.Settings.Default.Soundcard3a;
                cmbSound4a.Text = Properties.Settings.Default.Soundcard4a;
                cmbRot1.Text = Properties.Settings.Default.rotation1;
                cmbRot2.Text = Properties.Settings.Default.rotation2;
                cmbRot3.Text = Properties.Settings.Default.rotation3;
                cmbRot4.Text = Properties.Settings.Default.rotation4;
                chkToggle.IsChecked = Properties.Settings.Default.togglesound;
                txtTime.Text = Properties.Settings.Default.toggletime.TotalSeconds.ToString();
                if ((Properties.Settings.Default.Files1 != null) && (Properties.Settings.Default.Files1.Count > 0))
                {
                    foreach (string st in Properties.Settings.Default.Files1)
                    {
                        {
                            stFiles1.Add(st);
                        }
                    }
                    RefreshView1();
                }
                else if (Properties.Settings.Default.Files1 == null)
                {
                    Properties.Settings.Default.Files1 = new System.Collections.Specialized.StringCollection();
                }
                if ((Properties.Settings.Default.Files2 != null) && (Properties.Settings.Default.Files2.Count > 0))
                {
                    foreach (string st in Properties.Settings.Default.Files2)
                    {
                        {
                            stFiles2.Add(st);
                        }
                    }
                    RefreshView2();
                }
                else if (Properties.Settings.Default.Files2 == null)
                {
                    Properties.Settings.Default.Files2 = new System.Collections.Specialized.StringCollection();
                }
                if ((Properties.Settings.Default.Files3 != null) && (Properties.Settings.Default.Files3.Count > 0))
                {
                    foreach (string st in Properties.Settings.Default.Files3)
                    {
                        {
                            stFiles3.Add(st);
                        }
                    }
                    RefreshView3();
                }
                else if (Properties.Settings.Default.Files3 == null)
                {
                    Properties.Settings.Default.Files3 = new System.Collections.Specialized.StringCollection();
                }
                if ((Properties.Settings.Default.Files4 != null) && (Properties.Settings.Default.Files4.Count > 0))
                {
                    foreach (string st in Properties.Settings.Default.Files4)
                    {
                        {
                            stFiles4.Add(st);
                        }
                    }
                    RefreshView4();
                }
                else if (Properties.Settings.Default.Files4 == null)
                {
                    Properties.Settings.Default.Files4 = new System.Collections.Specialized.StringCollection();
                }

                chkEnabled1.IsChecked = Properties.Settings.Default.enable1;
                chkEnabled2.IsChecked = Properties.Settings.Default.enable2;
                chkEnabled3.IsChecked = Properties.Settings.Default.enable3;
                chkEnabled4.IsChecked = Properties.Settings.Default.enable4;
                stLogo1 = Properties.Settings.Default.Logo1;
                stLogo2 = Properties.Settings.Default.Logo2;
                stLogo3 = Properties.Settings.Default.Logo3;
                stLogo4 = Properties.Settings.Default.Logo4;
                lblLogo1.Content = stLogo1;
                lblLogo2.Content = stLogo2;
                lblLogo2.Content = stLogo3;
                lblLogo2.Content = stLogo4;
                iAuto = Properties.Settings.Default.Autostart;
                if (iAuto == VIDEO)
                {
                    btnVid2_Start_all_Click(this, null);
                    radVideo.IsChecked = true;
                }
                else if (iAuto == LOGO)
                {
                    btnLogos_Click(this, null);
                    radLogo.IsChecked = true;
                }
                else
                {
                    radNone.IsChecked = true;
                }
            }
            catch { }
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            radNone.Checked += radNone_Checked;
            radVideo.Checked += radVideo_Checked;
            radLogo.Checked += radLogo_Checked;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            StopAll();
            StartAllVideos();
            iTurn++;
        }

        public static WAVEOUTCAPS[] GetDevCapsPlayback()
        {
            uint waveOutDevicesCount = waveOutGetNumDevs();
            if (waveOutDevicesCount > 0)
            {
                WAVEOUTCAPS[] list = new WAVEOUTCAPS[waveOutDevicesCount];
                for (int uDeviceID = 0; uDeviceID < waveOutDevicesCount; uDeviceID++)
                {
                    WAVEOUTCAPS waveOutCaps = new WAVEOUTCAPS();
                    waveOutGetDevCaps((IntPtr)uDeviceID, ref waveOutCaps, Convert.ToUInt32(Marshal.SizeOf(typeof(WAVEOUTCAPS))));
                    System.Diagnostics.Debug.WriteLine("\n" + waveOutCaps.ToString());
                    list[uDeviceID] = waveOutCaps;
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        private void txtFiles1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;
        }
        private void txtFiles2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;
        }

        private void txtFiles2_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string st in files)
            {
                stFiles2.Add(st);
            }
            RefreshView2();
            SaveFiles2();

        }
        private void txtFiles1_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string st in files)
            {
                stFiles1.Add(st);
            }
            RefreshView1();
            SaveFiles1();
        }

        private void cmbSound2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Soundcard2 = cmbSound2.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbSound1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Soundcard1 = cmbSound1.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void chkEnabled1_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.enable1 = Convert.ToBoolean(chkEnabled1.IsChecked);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void chkEnabled2_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.enable2 = Convert.ToBoolean(chkEnabled2.IsChecked);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void btnImage2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == true)
            {
                if (of.FileNames != null)
                {
                    foreach (string st in of.FileNames)
                    {
                        stLogo2 = st;
                    }
                    lblLogo2.Content = stLogo2;
                    Properties.Settings.Default.Logo2= stLogo2;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Upgrade();
                }
            }
        }

        private void btnImage1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == true)
            {
                if (of.FileNames != null)
                {
                    foreach (string st in of.FileNames)
                    {
                        stLogo1 = st;
                    }
                    lblLogo1.Content = stLogo1;
                    Properties.Settings.Default.Logo1 = stLogo1;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Upgrade();
                }
            }
        }

        private void radNone_Checked(object sender, RoutedEventArgs e)
        {
            iAuto = NONE;
            Properties.Settings.Default.Autostart = iAuto;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void radLogo_Checked(object sender, RoutedEventArgs e)
        {
            iAuto = LOGO;
            Properties.Settings.Default.Autostart = iAuto;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void radVideo_Checked(object sender, RoutedEventArgs e)
        {
            iAuto = VIDEO;
            Properties.Settings.Default.Autostart = iAuto;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            btnStop_Click(this, null);
        }

        private void btnVid3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = true;
            if (of.ShowDialog() == true)
            {
                if (of.FileNames != null)
                {
                    foreach (string st in of.FileNames)
                    {
                        stFiles3.Add(st);
                    }
                }
            }
            RefreshView3();
            SaveFiles3();
        }

        private void btnVid3_Clr_Click(object sender, RoutedEventArgs e)
        {
            stFiles3.Clear();
            RefreshView3();
            SaveFiles3();
        }

        private void btnImage3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == true)
            {
                if (of.FileNames != null)
                {
                    foreach (string st in of.FileNames)
                    {
                        stLogo3 = st;
                    }
                    lblLogo1.Content = stLogo3;
                    Properties.Settings.Default.Logo3 = stLogo3;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Upgrade();
                }
            }
        }

        private void btnVid3_Start_Click(object sender, RoutedEventArgs e)
        {
            if (chkEnabled2.IsChecked == true)
            {
                string stProg = "\"C:\\Program Files\\VideoLAN\\VLC\\vlc.exe\"";
                string stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound3.SelectedValue.ToString() + "\" --loop  --fullscreen --qt-fullscreen-screennumber=3";
                if (cmbRot3.Text != "none")
                {
                    stTemp += " :vout-filter=transform --video-filter \"transform{ true}\" --transform-type=" + cmbRot3.Text;
                }
                foreach (string st in stFiles3)
                {
                    stTemp += " \"" + st + "\" \"" + Directory.GetCurrentDirectory() + "\\black.png\"";
                }
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = stProg;
                startInfo.Arguments = stTemp;
                Process.Start(startInfo);
            }
        }

        private void txtFiles3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;

        }

        private void txtFiles3_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string st in files)
            {
                stFiles3.Add(st);
            }
            RefreshView3();
            SaveFiles3();
        }

        private void txtFiles4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;

        }

        private void txtFiles4_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string st in files)
            {
                stFiles4.Add(st);
            }
            RefreshView4();
            SaveFiles4();
        }

        private void btnVid4_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = true;
            if (of.ShowDialog() == true)
            {
                if (of.FileNames != null)
                {
                    foreach (string st in of.FileNames)
                    {
                        stFiles4.Add(st);
                    }
                }
            }
            RefreshView4();
            SaveFiles4();
        }

        private void btnVid4_Clr_Click(object sender, RoutedEventArgs e)
        {
            stFiles4.Clear();
            RefreshView4();
            SaveFiles4();
        }

        private void btnImage4_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == true)
            {
                if (of.FileNames != null)
                {
                    foreach (string st in of.FileNames)
                    {
                        stLogo4 = st;
                    }
                    lblLogo1.Content = stLogo4;
                    Properties.Settings.Default.Logo4 = stLogo4;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Upgrade();
                }
            }
        }

        private void btnVid4_Start_Click(object sender, RoutedEventArgs e)
        {
            if (chkEnabled4.IsChecked == true)
            {
                string stProg = "\"C:\\Program Files\\VideoLAN\\VLC\\vlc.exe\"";
                string stTemp = " --aout=waveout --waveout-audio-device=\"" + cmbSound4.SelectedValue.ToString() + "\" --loop  --fullscreen --qt-fullscreen-screennumber=4";
                if (cmbRot4.Text != "none")
                {
                    stTemp += " :vout-filter=transform --video-filter \"transform{ true}\" --transform-type=" + cmbRot4.Text;
                }
                foreach (string st in stFiles4)
                {
                    stTemp += " \"" + st + "\" \"" + Directory.GetCurrentDirectory() + "\\black.png\"";
                }
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = stProg;
                startInfo.Arguments = stTemp;
                Process.Start(startInfo);
            }
        }

        private void cmbRot3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem ci = (ComboBoxItem)cmbRot2.SelectedItem;
            Properties.Settings.Default.rotation2 = ci.Content.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbRot1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem ci = (ComboBoxItem)cmbRot1.SelectedItem;
                Properties.Settings.Default.rotation1 = ci.Content.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbRot3_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem ci = (ComboBoxItem)cmbRot3.SelectedItem;
            Properties.Settings.Default.rotation3 = ci.Content.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbRot4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem ci = (ComboBoxItem)cmbRot4.SelectedItem;
            Properties.Settings.Default.rotation4 = ci.Content.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbSound2a_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Soundcard2a = cmbSound2a.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbSound4a_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Soundcard4a = cmbSound4a.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbSound4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Soundcard4 = cmbSound4.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbSound3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Soundcard3 = cmbSound3.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbSound3a_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Soundcard3a = cmbSound3a.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void cmbSound1a_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Soundcard1a = cmbSound1a.SelectedValue.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void chkToggle_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.togglesound = true ;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void chkToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.togglesound = true;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void txtTime_LostFocus(object sender, RoutedEventArgs e)
        {
            int i = 0;
            if (int.TryParse(txtTime.Text, out i))
            {
                if (i > 0)
                {
                    
                    Properties.Settings.Default.toggletime = TimeSpan.FromSeconds( i);
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Upgrade();
                }
            }
           
        }

        private void chkEnabled3_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.enable3 = Convert.ToBoolean(chkEnabled3.IsChecked);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();

        }

        private void chkEnabled4_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.enable4 = Convert.ToBoolean(chkEnabled4.IsChecked);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();

        }
    }
}
