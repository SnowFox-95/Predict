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

namespace Predictor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void BTNPREDICT_Click(object sender, EventArgs e)
        {
          await  Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    this.Invoke(new Action(() => 
                    {
                        UpdateProgressBar(i);
                    }));
                    
                    Thread.Sleep(20);
                }
            });


            MessageBox.Show("prediction");
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
    }
}
