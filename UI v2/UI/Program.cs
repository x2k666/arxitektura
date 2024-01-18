using System;
using System.Drawing;
using System.Windows.Forms;

public class SimpleGraphicalCalculator : Form
{
    private Button squareButton;
    private Button cubeButton;
    private Button linearButton;
    private Button sineButton;
    private Button cosineButton;
    private PictureBox plotPictureBox;

    public SimpleGraphicalCalculator()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        squareButton = CreateButton("x^2", 20, 20);
        cubeButton = CreateButton("x^3", 150, 20);
        linearButton = CreateButton("x", 280, 20);
        sineButton = CreateButton("sin(x)", 20, 70);
        cosineButton = CreateButton("cos(x)", 150, 70);

        plotPictureBox = new PictureBox
        {
            Location = new Point(20, 120),
            Size = new Size(400, 300)
        };

        this.Controls.Add(squareButton);
        this.Controls.Add(cubeButton);
        this.Controls.Add(linearButton);
        this.Controls.Add(sineButton);
        this.Controls.Add(cosineButton);
        this.Controls.Add(plotPictureBox);

        this.Size = new Size(450, 500);
        this.Text = "Простой графический калькулятор";

        squareButton.Click += new EventHandler(OnPlotSquare);
        cubeButton.Click += new EventHandler(OnPlotCube);
        linearButton.Click += new EventHandler(OnPlotLinear);
        sineButton.Click += new EventHandler(OnPlotSine);
        cosineButton.Click += new EventHandler(OnPlotCosine);
    }

    private Button CreateButton(string text, int x, int y)
    {
        return new Button
        {
            Text = text,
            Size = new Size(100, 30),
            Location = new Point(x, y)
        };
    }

    private void OnPlotSquare(object sender, EventArgs e)
    {
        PlotFunction(x => x * x);
    }

    private void OnPlotCube(object sender, EventArgs e)
    {
        PlotFunction(x => x * x * x);
    }

    private void OnPlotLinear(object sender, EventArgs e)
    {
        PlotFunction(x => x);
    }

    private void OnPlotSine(object sender, EventArgs e)
    {
        PlotFunction(x => (float)Math.Sin(x));
    }

    private void OnPlotCosine(object sender, EventArgs e)
    {
        PlotFunction(x => (float)Math.Cos(x));
    }

    private void PlotFunction(Func<float, float> function)
    {
        Bitmap plotBitmap = new Bitmap(plotPictureBox.Width, plotPictureBox.Height);
        using (Graphics g = Graphics.FromImage(plotBitmap))
        {
            DrawAxes(g);
            DrawFunction(g, function);
        }

        plotPictureBox.Image = plotBitmap;
    }

    private void DrawAxes(Graphics g)
    {
        Pen axisPen = new Pen(Color.Black);

        // Рисуем горизонтальную ось
        g.DrawLine(axisPen, 0, plotPictureBox.Height / 2, plotPictureBox.Width, plotPictureBox.Height / 2);

        // Рисуем вертикальную ось
        g.DrawLine(axisPen, plotPictureBox.Width / 2, 0, plotPictureBox.Width / 2, plotPictureBox.Height);
    }

    private void DrawFunction(Graphics g, Func<float, float> function)
    {
        Pen plotPen = new Pen(Color.Blue);

        float scaleX = plotPictureBox.Width / 10.0f;
        float scaleY = plotPictureBox.Height / 2.0f;

        float prevX = -5.0f;
        float prevY = function(prevX);

        for (float x = -5.0f; x <= 5.0f; x += 0.1f)
        {
            float y = function(x);
            DrawLine(g, prevX, -prevY, x, -y, plotPen);
            prevX = x;
            prevY = y;
        }
    }

    private void DrawLine(Graphics g, float x1, float y1, float x2, float y2, Pen pen)
    {
        g.DrawLine(pen, x1 * plotPictureBox.Width / 10 + plotPictureBox.Width / 2, y1 * plotPictureBox.Height / 2 + plotPictureBox.Height / 2, x2 * plotPictureBox.Width / 10 + plotPictureBox.Width / 2, y2 * plotPictureBox.Height / 2 + plotPictureBox.Height / 2);
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new SimpleGraphicalCalculator());
    }
}
