using eWeLink_API.Classes;
using eWeLink_API.Classes.JsonClasses;
using eWeLink_API.Forms;
using System;
using System.IO;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static eWeLink_API.JsonClass;
using static eWeLink_API.LoggerClass;

namespace eWeLink_API
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CreateFolder();
            CheckAndDecrypt();//if u don't want to use encryption uncomment and use VarClass directly
            StatusLabel.Text = "Ready";
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
                    Logger.WriteLine($" *** Credentials Missing: Please Populate Settings Form [MainForm] ***");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Check And Decrypt Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteLine($" *** Error:{ex.Message} [MainForm] ***");
                return;
            }
        }

        private void CreateFolder()
        {
            try
            {
                //Create the folders used by the app
                string path = Application.StartupPath;
                Directory.CreateDirectory(path + @"\Logs");
                //Directory.CreateDirectory(path + @"\Updates");
                Logger.WriteLine(" *** Application Start [SplashForm] ***");
                //richTextBox.AppendText($"[{DateTime.Now}] : Application Start\n");
                Logger.WriteLine(" *** CreateDirectory Success [SplashForm] ***");
                //richTextBox.AppendText($"[{DateTime.Now}] : Logs Create Directory Success\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Create Folder Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteLine($" *** Error:{ex.Message} [SplashForm] ***");
                return;
            }
        }

        private static long GetTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(datetime - sTime).TotalMilliseconds;
        }


        private string calcSign(string payload, string secret)
        {
            // Initialize the keyed hash object using the secret key as the key
            HMACSHA256 hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            // Computes the signature by hashing the salt with the secret key as the key
            byte[] signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(payload));
            // Base 64 Encode
            string encodedSignature = Convert.ToBase64String(signature);
            // URLEncode
            // encodedSignature = System.Web.HttpUtility.UrlEncode(encodedSignature);
            return encodedSignature;
        }

        private async void loginbutton_Click(object sender, EventArgs e)
        {
            LoginPayloadClass.Rootobject payloadclass = new LoginPayloadClass.Rootobject
            {
                appid = VarClass.Appid,
                email = VarClass.Email,
                phoneNumber = "",
                password = VarClass.Password,
                ts = GetTime(DateTime.Now),
                version = 8,
                nonce = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5)
            };
            string payload = JSONSerializer<LoginPayloadClass.Rootobject>.Serialize(payloadclass);
            string sign = "Sign " + calcSign(payload, VarClass.Secret);

            logsrichTextBox.Clear();
            using (HttpClient client = new HttpClient())
            {
                var httpContent = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
                logsrichTextBox.AppendText("SENT => " + Environment.NewLine);
                logsrichTextBox.AppendText(payload + Environment.NewLine);
                //Add Default Request Headers
                client.DefaultRequestHeaders.Add("Authorization", $"{sign}");
                try
                {
                    using (HttpResponseMessage response = await client.PostAsync("https://eu-api.coolkit.cc:8080/api/user/login", httpContent))
                    {
                        using (HttpContent content = response.Content)
                        {
                            //Read the result and display in Textbox
                            string result = await content.ReadAsStringAsync();//Result string JSON
                            string reasonPhrase = response.ReasonPhrase;//Reason OK, FAIL etc.
                            logsrichTextBox.AppendText("RESULT => " + Environment.NewLine);
                            logsrichTextBox.AppendText(result + Environment.NewLine);
                            logsrichTextBox.AppendText(reasonPhrase + Environment.NewLine);
                            JsonLoginSerializer(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Could not test eWeLink API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteLine($" *** Error:{ex.Message} [eWeLink_API_Form] ***");
                    return;
                }
            }
        }

        private void JsonLoginSerializer(string json)
        {
            //JObject _json = JObject.Parse(json);
            //string at = (string)_json["at"];
            //string IDNO = (string)json["devstaus"]["DevIDNO"];//name
            /*foreach (var companyitems in json["status"])
            {
                string id = (string)companyitems["id"];//name
            }*/
            //VarClass.at = at;
            try
            {
                eWelinkLoginJsonClass.Rootobject dr = JsonClass.JSONSerializer<eWelinkLoginJsonClass.Rootobject>.DeSerialize(json);

                string r = dr.at;
                VarClass.At = r;
                logsrichTextBox.AppendText($"at = {r}\n");
                eWelinkLoginJsonClass.Appinfo[] usr = dr.user.appInfos;
                foreach (var i in usr)
                {
                    logsrichTextBox.AppendText($"appVersion = {i.appVersion}, os = {i.os}\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "JsonLoginSerializer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteLine($" *** Error:{ex.Message} [eWeLink_API_Form] ***");
                return;
            }
        }

        private void JsonGetDevicesSerializer(string json)
        {
            try
            {
                DeviceListJsonClass.Rootobject dr = JsonClass.JSONSerializer<DeviceListJsonClass.Rootobject>.DeSerialize(json);

                int r = dr.error;
                if (r == 0)
                {
                    logsrichTextBox.AppendText($"error = {r}\n");
                    Devices_comboBox.Items.Clear();
                    DeviceListJsonClass.Devicelist[] dev = dr.devicelist;
                    foreach (var i in dev)
                    {
                        Devices_comboBox.Items.Add($"{i.name},{i.deviceid},{i.online}");
                        logsrichTextBox.AppendText($"name = {i.name}, deviceid = {i.deviceid}, online = {i.online}\n");
                    }
                    Devices_comboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "JsonGetDevicesSerializer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteLine($" *** Error:{ex.Message} [eWeLink_API_Form] ***");
                return;
            }

        }

        private async void testbutton_Click(object sender, EventArgs e)
        {
            string[] _s = Devices_comboBox.Text.Split(',');
            var token = "Bearer " + VarClass.At;
            DeviceSwitchPayloadClass.Rootobject payloadclass = new DeviceSwitchPayloadClass.Rootobject
            {
                deviceid = _s[1],
                _params = new DeviceSwitchPayloadClass.Params
                {
                    _switch = "on"
                },
                appid = VarClass.Appid,
                nonce = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8),
                ts = GetTime(DateTime.Now),
                version = 8,
            };
            
            string payload = JSONSerializer<DeviceSwitchPayloadClass.Rootobject>.Serialize(payloadclass);

            logsrichTextBox.Clear();
            using (HttpClient client = new HttpClient())
            {
                string pay = payload.Replace("_", string.Empty);
                var httpContent = new StringContent(pay, Encoding.UTF8, "application/json");
                logsrichTextBox.AppendText("SENT => " + Environment.NewLine);
                logsrichTextBox.AppendText(pay + Environment.NewLine);
                //Add Default Request Headers
                client.DefaultRequestHeaders.Add("Authorization", $"{token}");
                try
                {
                    using (HttpResponseMessage response = await client.PostAsync("https://eu-api.coolkit.cc:8080/api/user/device/status", httpContent))
                    {
                        using (HttpContent content = response.Content)
                        {
                            //Read the result and display in Textbox
                            string result = await content.ReadAsStringAsync();//Result string JSON
                            string reasonPhrase = response.ReasonPhrase;//Reason OK, FAIL etc.
                            logsrichTextBox.AppendText("RESULT => " + Environment.NewLine);
                            logsrichTextBox.AppendText(result + Environment.NewLine);
                            logsrichTextBox.AppendText(reasonPhrase + Environment.NewLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Could not test eWeLink API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteLine($" *** Error:{ex.Message} [eWeLink_API_Form] ***");
                    return;
                }
            }
        }

        private async void GetDevbutton_Click(object sender, EventArgs e)
        {
            //double timestamp = GetTime(DateTime.Now);//1635015148080
            string url = $"https://eu-api.coolkit.cc:8080/api/user/device?lang=en&appid={VarClass.Appid}&ts={GetTime(DateTime.Now)}&version=8&getTags=1";
            logsrichTextBox.Clear();
            using (HttpClient client = new HttpClient())
            {
                //var httpContent = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
                logsrichTextBox.AppendText("SENT => " + Environment.NewLine);
                logsrichTextBox.AppendText(url + Environment.NewLine);
                //Add Default Request Headers
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {VarClass.At}");
                try
                {
                    using (HttpResponseMessage response = await client.GetAsync(new Uri(url)))
                    {
                        using (HttpContent content = response.Content)
                        {
                            //Read the result and display in Textbox
                            string result = await content.ReadAsStringAsync();//Result string JSON
                            string reasonPhrase = response.ReasonPhrase;//Reason OK, FAIL etc.
                            logsrichTextBox.AppendText("RESULT => " + Environment.NewLine);
                            logsrichTextBox.AppendText(result + Environment.NewLine);
                            logsrichTextBox.AppendText(reasonPhrase + Environment.NewLine);
                            JsonGetDevicesSerializer(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Could not test eWeLink API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteLine($" *** Error:{ex.Message} [eWeLink_API_Form] ***");
                    return;
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form f = new AboutForm())
            {
                f.ShowDialog();
                f.Activate();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form f = new SettingsForm())
            {
                f.ShowDialog();
                f.Activate();
            }
        }
    }
}
