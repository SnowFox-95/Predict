using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Predictor
{
    public partial class Form1 : Form
    {
        private const string APP_NAME = "Шар предсказаний";
        private readonly string PREDICT_CONFIG_PATH = $"{Environment.CurrentDirectory}\\predict_config.json";
        private string[] _predictions;
        private Random _random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private async void BTNPREDICT_Click(object sender, EventArgs e)
        {
            BTNPREDICT.Enabled = false;
                await  Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    this.Invoke(new Action(() => 
                    {
                        UpdateProgressBar(i);
                        Text = $"{i}%"; 
                    }));
                    
                    Thread.Sleep(20);
                }
            });

            var index = _random.Next(_predictions.Length);

            var prediction = _predictions[index];
            MessageBox.Show($"{prediction}!");
            PBpredict.Value = 0;
            Text = APP_NAME;
            BTNPREDICT.Enabled = true;
        }
    private void UpdateProgressBar (int i)
        {
            if (i == PBpredict.Maximum)
            {
                PBpredict.Maximum = i + 1; //увеличить максимум
                PBpredict.Value = i + 1; //Вернуть прошлое значение
                PBpredict.Maximum = i; //Сбросить максимум
            }
            else
            {
                PBpredict.Value = i + 1; //Вернуть прошлое значение
            }
            PBpredict.Value = i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = APP_NAME;
            try
            {
                var data = File.ReadAllText(PREDICT_CONFIG_PATH);
                _predictions = JsonConvert.DeserializeObject<string[]>(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (_predictions == null)
                {
                    Close();
                }
                else if (_predictions.Length == 0)
                {
                    MessageBox.Show("Предсказания закончились :( ");
                    Close();
                }
            }
        }
    }
}
