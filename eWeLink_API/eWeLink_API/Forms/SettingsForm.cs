using eWeLink_API.Classes;
using System;
using System.Configuration;
using System.Security;
using System.Windows.Forms;
using static eWeLink_API.LoggerClass;

namespace eWeLink_API.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            DecryptAndShow();
        }

        private void DecryptAndShow()
        {
            try
            {
                SecureString Appid = EncryptionClass.DecryptString(Properties.Settings.Default.Appid);
                VarClass.Appid = EncryptionClass.ToInsecureString(Appid);
                SecureString Apikey = EncryptionClass.DecryptString(Properties.Settings.Default.Apikey);
                VarClass.Apikey = EncryptionClass.ToInsecureString(Apikey);
                SecureString Secret = EncryptionClass.DecryptString(Properties.Settings.Default.Secret);
                VarClass.Secret = EncryptionClass.ToInsecureString(Secret);
                SecureString Email = EncryptionClass.DecryptString(Properties.Settings.Default.Email);
                VarClass.Email = EncryptionClass.ToInsecureString(Email);
                SecureString Password = EncryptionClass.DecryptString(Properties.Settings.Default.Password);
                VarClass.Password = EncryptionClass.ToInsecureString(Password);

                Appid_textBox.Text = VarClass.Appid;
                Apikey_textBox.Text = VarClass.Apikey;
                Secret_textBox.Text = VarClass.Secret;
                Email_textBox.Text = VarClass.Email;
                Password_textBox.Text = VarClass.Password;

                //VarClass.Appid = "";
                //VarClass.Apikey = "";
                //VarClass.Secret = "";
                //VarClass.Email = "";
                //VarClass.Password = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Decrypt And Show Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteLine($" *** Error:{ex.Message} [SettingsForm] ***");
                return;
            }
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Appid = EncryptionClass.EncryptString(EncryptionClass.ToSecureString(Appid_textBox.Text));
                Properties.Settings.Default.Apikey = EncryptionClass.EncryptString(EncryptionClass.ToSecureString(Apikey_textBox.Text));
                Properties.Settings.Default.Secret = EncryptionClass.EncryptString(EncryptionClass.ToSecureString(Secret_textBox.Text));
                Properties.Settings.Default.Email = EncryptionClass.EncryptString(EncryptionClass.ToSecureString(Email_textBox.Text));
                Properties.Settings.Default.Password = EncryptionClass.EncryptString(EncryptionClass.ToSecureString(Password_textBox.Text));
                Properties.Settings.Default.Save();
                MessageBox.Show("Settings Saved", "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Settings Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteLine($" *** Error:{ex.Message} [SettingsForm] ***");
                return;
            }

        }

        private void CheckAndDecrypt()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Appid))
                {
                    SecureString Appid = EncryptionClass.DecryptString(Properties.Settings.Default.Appid);
                    VarClass.Appid = EncryptionClass.ToInsecureString(Appid);
                    SecureString Apikey = EncryptionClass.DecryptString(Properties.Settings.Default.Apikey);
                    VarClass.Apikey = EncryptionClass.ToInsecureString(Apikey);
                    SecureString Secret = EncryptionClass.DecryptString(Properties.Settings.Default.Secret);
                    VarClass.Secret = EncryptionClass.ToInsecureString(Secret);
                    SecureString Email = EncryptionClass.DecryptString(Properties.Settings.Default.Email);
                    VarClass.Email = EncryptionClass.ToInsecureString(Email);
                    SecureString Password = EncryptionClass.DecryptString(Properties.Settings.Default.Password);
                    VarClass.Password = EncryptionClass.ToInsecureString(Password);
                }
                else
                {
                    MessageBox.Show("Please Populate Settings Form", "Credentials Missing!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.WriteLine($" *** Credentials Missing: Please Populate Settings Form [SettingsForm] ***");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Check And Decrypt Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteLine($" *** Error:{ex.Message} [SettingsForm] ***");
                return;
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CheckAndDecrypt();
        }
    }
}
