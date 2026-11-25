// @author piercer
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PiercerHostsManager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            string hostsPath = Path.Combine(Environment.SystemDirectory, @"drivers\etc\hosts");
            var f = new Form { Text = "Piercer Hosts File Manager", Size = new Size(760, 520), BackColor = Color.FromArgb(10, 10, 12), ForeColor = Color.White };
            var txt = new TextBox { Dock = DockStyle.Fill, Multiline = true, ScrollBars = ScrollBars.Vertical, Font = new Font("Consolas", 10), BackColor = Color.FromArgb(20, 20, 24), ForeColor = Color.White };
            if (File.Exists(hostsPath)) txt.Text = File.ReadAllText(hostsPath);

            var panel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42, Padding = new Padding(6), BackColor = Color.FromArgb(20, 20, 24) };
            var btnSave = new Button { Text = "Backup + Salvar", Width = 140, Height = 28, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(239, 68, 68), ForeColor = Color.White };
            var btnLocal = new Button { Text = "+ Dev Localhost", Width = 130, Height = 28, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(30, 30, 36), ForeColor = Color.White };

            btnLocal.Click += (s, e) => txt.AppendText(Environment.NewLine + "127.0.0.1 app.local" + Environment.NewLine);
            btnSave.Click += (s, e) =>
            {
                try
                {
                    if (File.Exists(hostsPath)) File.Copy(hostsPath, hostsPath + ".piercer.bak", true);
                    File.WriteAllText(hostsPath, txt.Text);
                    MessageBox.Show("Hosts salvo com backup!");
                }
                catch (Exception ex) { MessageBox.Show("Execute como Administrador: " + ex.Message); }
            };

            panel.Controls.AddRange(new Control[] { btnSave, btnLocal });
            f.Controls.Add(txt);
            f.Controls.Add(panel);
            Application.Run(f);
        }
    }
}
