using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;

namespace integral
{
	public partial class Form1 : Form
	{
        Random rnd = new Random();
        public Form1()
		{
			InitializeComponent();
		}

		     

        private void leftBorder_KeyPress(object sender, KeyPressEventArgs e)
        {
            // проверка на дебила
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void rightBorder_KeyPress(object sender, KeyPressEventArgs e)
        {
            // проверка на дебила
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
			// проверка на дебила
            if (!char.IsControl(e.KeyChar) && e.KeyChar == ',')
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = functionInput.Text;
            double h = 0.1;
            double x, y;
            double sum = 0.0;
            double a = Convert.ToDouble(leftBorder.Text);
            double b = Convert.ToDouble(rightBorder.Text);

            this.chart2.Series[0].Points.Clear();
            this.chart2.Series[1].Points.Clear();
            this.chart2.Series[2].Points.Clear();
            Function f = new Function("f(x) = " + functionInput.Text);
            x = a;
            Expression e1 = new Expression("int( " + functionInput.Text + ", x," + Convert.ToString(a) + " ," + Convert.ToString(b) + ")");
            while (x <= b)
            {
                Expression exp = new Expression("f(" + x.ToString().Replace(',', '.') + ")", f);
                y = exp.calculate();
                this.chart2.Series[0].Points.AddXY(x, y);



                if (radioButton1.Checked)
                {
                    this.chart2.Series[1].Points.AddXY(x + h / 2, y);
                    sum += y * h;
                }

                if (radioButton2.Checked)
                {
                    x += h;
                    Expression exp1 = new Expression("f(" + x.ToString().Replace(',', '.') + ")", f);
                    y = exp1.calculate();
                    this.chart2.Series[1].Points.AddXY(x - h / 2, y);
                    sum += y * h;
                }
                if (radioButton3.Checked)
                {
                    x += h / 2;
                    Expression exp2 = new Expression("f(" + x.ToString().Replace(',', '.') + ")", f);
                    y = exp2.calculate();
                    this.chart2.Series[1].Points.AddXY(x, y);
                    sum += y * h;
                }

                if(!(radioButton1.Checked || radioButton2.Checked || radioButton3.Checked))
                {
                    this.chart2.Series[0].Points.Clear();
                    this.chart2.Series[1].Points.Clear();
                    this.chart2.Series[2].Points.Clear();
                    MessageBox.Show("Вы не выбрали метод");
                    return;
                }
                x += h;
            }

            /*
            if (Convert.ToString(e1.calculate()) == "не число")
                MessageBox.Show(Convert.ToString(sum));
            else
                MessageBox.Show(Convert.ToString(e1.calculate()));
            */

            // Создаем новое диалоговое окно
            Form messageBox = new Form();
            messageBox.StartPosition = FormStartPosition.CenterParent;

            string path = Path.Combine(
                Directory.GetCurrentDirectory(), 
                "Images", 
                Convert.ToString(rnd.Next(0, 6))+".jpg"
                );


            // Создаем контрол PictureBox
            PictureBox pictureBox = new PictureBox();
            Image image = Image.FromFile(path); // загружаем изображение из файла
            pictureBox.Image = image;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // задаем режим отображения изображения
            
            Label label = new Label();
            Label label1 = new Label();
            label1.Text = "Молодец ты справился, вот тебе за это картинку!!!\n";
            if (Convert.ToString(e1.calculate()) == "не число")
                label.Text += Convert.ToString(sum);
            else
                label.Text += Convert.ToString(e1.calculate());


            pictureBox.Dock = DockStyle.Top;

            label.Font = new Font("Lucida Console", 12, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Dock = DockStyle.Top;

            label1.Font = new Font("Lucida Console", 12);
            //label1.Size = new Size(300, 60);
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Dock = DockStyle.Top;

            // Добавляем PictureBox на диалоговое окно

            messageBox.Controls.Add(label);
            messageBox.Controls.Add(label1);
            messageBox.Controls.Add(pictureBox);

            messageBox.ClientSize = new Size(500, 400);
            pictureBox.Size = new Size(300, 300);
            // Отображаем диалоговое окно
            messageBox.ShowDialog();
        }



        
    }
}
