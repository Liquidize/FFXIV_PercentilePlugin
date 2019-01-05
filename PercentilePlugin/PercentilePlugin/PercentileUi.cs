using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using PercentilePlugin.Shared;

namespace PercentilePlugin
{
    public partial class PercentileUi : UserControl
    {
        private CancellationTokenSource cts;
        private Task updateTask;

        public PercentileUi()
        {
            InitializeComponent();
            logView.VirtualListSize = PercentilePlugin.Logger.Logs.Count;
            PercentilePlugin.Logger.Logs.ListChanged += (o, e) =>
            {
                logView.BeginUpdate();
                logView.VirtualListSize = PercentilePlugin.Logger.Logs.Count;
                logView.EnsureVisible(logView.VirtualListSize - 1);
                logView.EndUpdate();
            };
            autoUpdateChbx.Checked = Options.Instance.AutoUpdate;
        }

        private void logView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= PercentilePlugin.Logger.Logs.Count)
            {
                e.Item = new ListViewItem();
                return;
            }

            var log = PercentilePlugin.Logger.Logs[e.ItemIndex];
            e.Item = new ListViewItem(log.Time.ToString());
            e.Item.UseItemStyleForSubItems = true;
            e.Item.SubItems.Add(log.Level.ToString());
            e.Item.SubItems.Add(log.Message);
            e.Item.ForeColor = Color.Black;
            if (log.Level == LogLevel.Warning)
                e.Item.BackColor = Color.LightYellow;
            else if (log.Level == LogLevel.Error)
                e.Item.BackColor = Color.LightPink;
            else
                e.Item.BackColor = Color.White;
        }

        public void UpdateLabelText()
        {
            lastCheckLbl.Text = "Last Updated: " + new DateTime(1970, 1, 1)
                                    .AddMilliseconds(PercentilePlugin.PercentileData.LastUpdated).ToLocalTime()
                                    .ToString("F");
        }

        private async void updateBtn_Click(object sender, EventArgs e)
        {
            updateBtn.Enabled = false;
            try
            {
                var request =
                    WebRequest.Create("https://github.com/Liquidize/FFXIV_PercentilePlugin/raw/master/parsedata.bin");
                var response = await request.GetResponseAsync();
                if (response.GetResponseStream() != null)
                    using (var bsonReader = new BsonReader(response.GetResponseStream()))
                    {
                        var serializer = new JsonSerializer();
                        var data = serializer.Deserialize<PercentileData>(bsonReader);
                        if (data != null)
                        {
                            PercentilePlugin.PercentileData = data;
                            var file = new FileStream("PercentilePlugin/parsedata.bin", FileMode.OpenOrCreate);
                            using (var writer = new BsonWriter(file))
                            {
                                serializer.Serialize(writer, data);
                            }

                            file.Close();
                            PercentilePlugin.Logger.Log(LogLevel.Info, "Percentile Data has been updated.");
                        }
                        else
                        {
                            MessageBox.Show("Error in updating data.");
                        }
                    }
            }
            catch (WebException webEx)
            {
                PercentilePlugin.Logger.Log(LogLevel.Error, "Error in updating data.");
                PercentilePlugin.Logger.Log(LogLevel.Error, webEx.ToString());
            }
            catch (JsonException jsonEx)
            {
                PercentilePlugin.Logger.Log(LogLevel.Error, "Error in updating data.");
                PercentilePlugin.Logger.Log(LogLevel.Error, jsonEx.ToString());
            }

            lastCheckLbl.Text = "Last Updated: " + new DateTime(1970, 1, 1)
                                    .AddMilliseconds(PercentilePlugin.PercentileData.LastUpdated).ToLocalTime()
                                    .ToString("F");
            updateBtn.Enabled = true;
        }

        private void autoUpdateChbx_CheckedChanged(object sender, EventArgs e)
        {
            Options.Instance.AutoUpdate = autoUpdateChbx.Checked;
            Options.Instance.Save();
        }
    }
}