using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using org.mariuszgromada.math.mxparser;

namespace integral
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}


		private void button1_Click(object sender, EventArgs e)
		{
			string s = tB_function.Text;
			double h = 0.1;
			double x, y;
			double sum = 0.0;
			double a = Convert.ToDouble(tB_a.Text.Replace('.', ','));
			double b = Convert.ToDouble(tB_b.Text.Replace('.', ','));

			this.chart1.Series[0].Points.Clear();
			this.chart1.Series[1].Points.Clear();
			this.chart1.Series[2].Points.Clear();
			Function f = new Function("f(x) = " + tB_function.Text);
			x = a;
			Expression e1 = new Expression("int( " + tB_function.Text + ", x," + Convert.ToString(a) + " ," + Convert.ToString(b) + ")");

			switch (comboBox1.Text)
			{
				case "метод левых прямоугольников":
					while (x <= b - h / 2)
					{
						Expression exp = new Expression("f(" + x.ToString().Replace(',', '.') + ")", f);
						y = Math.Round(exp.calculate(), 3);
						this.chart1.Series[0].Points.AddXY(x, y);
						this.chart1.Series[1].Points.AddXY(Math.Round(x + h * 11 / 24, 6), y);
						sum += y * h;
						x += h;
					}
					break;

				case "метод правых прямоугольников":
					while (x <= b - h / 2) {
						Expression exp = new Expression("f(" + x.ToString().Replace(',', '.') + ")", f);
						y = Math.Round(exp.calculate(), 6);
						this.chart1.Series[0].Points.AddXY(x, y);
						this.chart1.Series[1].Points.AddXY(Math.Round(x - h * 11 / 24, 6), y);
						sum += y * h;
						x += h;
					}
					break;

				case "метод средних прямоугольников":
					while (x <= b - h / 2)
					{
						Expression exp = new Expression("f(" + x.ToString().Replace(',', '.') + ")", f);
						y = Math.Round(exp.calculate(), 6);
						this.chart1.Series[0].Points.AddXY(x, y);
						this.chart1.Series[1].Points.AddXY(x, y);
						sum += y * h;
						x += h;
					}
					break;
			}
			sum = Math.Round(sum, 6);
			bool flazhok = !(double.IsNaN(sum));

            if (flazhok)
			{
				tB_out.Text = Convert.ToString(sum);
				this.BackgroundImage = Properties.Resources.good;
			}

			else
			{
				tB_out.Text = "Kurwa";
                this.BackgroundImage = Properties.Resources.bad;
            }
		}

      
    }
}
