using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkCorrelation;
using System.Windows.Forms;

namespace MarkCorrelationGUI
{
    class TextBoxLogger : ILogger
    {
        protected TextBox textBox;
        protected Button startButton;
        protected ProgressBar progressBar;

        public TextBoxLogger(TextBox textBox, Button startButton, ProgressBar progressBar)
        {
            this.textBox = textBox;
            this.startButton = startButton;
            this.progressBar = progressBar;
        }

        public void Log(string text)
        {
            this.textBox.Invoke(new Action<string>((string data) =>
            {
                this.textBox.Text += data + Environment.NewLine;
            }), new object[] { text });
        }

        public void LogCorrelation(double correlation)
        {
            string corrString = "Корреляция: " + correlation.ToString("F6") + 
                Environment.NewLine + "(-1 - оценки выше у парней, +1 - у девушек)";

            this.Log(corrString);

            this.textBox.FindForm().Invoke(new Action<string>((string data) =>
            {
                this.startButton.Enabled = true;
                MessageBox.Show(data, "Вычисленная корреляция");
            }), new object[] { corrString });

            // this means finish
        }

        public void SetStepsCount(int count)
        {
            this.progressBar.Invoke(new Action(() =>
            {
                this.progressBar.Value = 0;
                this.progressBar.Maximum = count;
                this.progressBar.Step = 1;
            }));
        }

        public void Step()
        {
            this.progressBar.Invoke(new Action(() =>
            {
                this.progressBar.PerformStep();
            }));
        }
    }
}
