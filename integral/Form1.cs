using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;

namespace integral
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

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
			Expression e1 = new Expression("int( " + tB_function.Text +", x," + Convert.ToString(a)+ " ," + Convert.ToString(b) + ")");
			while (x <= b)
			{
				Expression exp = new Expression("f(" + x.ToString().Replace(',', '.') + ")", f);
				y = exp.calculate();
				this.chart1.Series[0].Points.AddXY(x, y);
				

				
				if (comboBox1.Text == "метод левых прямоугольников")
				{
					this.chart1.Series[1].Points.AddXY(x + h/2, y);
					sum += y * h;
				}
				
				if (comboBox1.Text == "метод правых прямоугольников")
				{
					x += h;
					Expression exp1 = new Expression("f(" + x.ToString().Replace(',', '.') + ")", f);
					y = exp1.calculate();
					this.chart1.Series[1].Points.AddXY(x - h/2, y);
					sum += y * h;
				}
				if (comboBox1.Text == "метод средних прямоугольников")
				{
					x += h/2;
					Expression exp2 = new Expression("f(" + x.ToString().Replace(',', '.') + ")", f);
					y = exp2.calculate();
					this.chart1.Series[1].Points.AddXY(x, y);
					sum += y * h;
				}
				x += h;
			}
			if (e1.calculate() is double)
                tB_out.Text = Convert.ToString(sum);
			else
				tB_out.Text = Convert.ToString(e1.calculate());
		}
	}
}
