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

        this.Controls.AddRange(new Control[] { squareButton, cubeButton, linearButton, sineButton, cosineButton, plotPictureBox });

        this.Size = new Size(450, 500);
        this.Text = "Красивый графический калькулятор";

        SetButtonStyles();
        AttachClickEvents();
    }

    private Button CreateButton(string text, int x, int y)
    {
        return new Button
        {
            Text = text,
            Size = new Size(100, 40),
            Location = new Point(x, y),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(52, 152, 219),
            ForeColor = Color.White,
            Font = new Font("Arial", 12, FontStyle.Bold)
        };
    }

    private void SetButtonStyles()
    {
        SetButtonStyle(squareButton, Color.FromArgb(231, 76, 60));
        SetButtonStyle(cubeButton, Color.FromArgb(46, 204, 113));
        SetButtonStyle(linearButton, Color.FromArgb(241, 196, 15));
        SetButtonStyle(sineButton, Color.FromArgb(155, 89, 182));
        SetButtonStyle(cosineButton, Color.FromArgb(230, 126, 34));
    }

    private void SetButtonStyle(Button button, Color buttonColor)
    {
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseDownBackColor = buttonColor;
        button.FlatAppearance.MouseOverBackColor = buttonColor;
    }

    private void AttachClickEvents()
    {
        squareButton.Click += (sender, e) => PlotFunction(x => x * x, "x^2", Color.Red);
        cubeButton.Click += (sender, e) => PlotFunction(x => x * x * x, "x^3", Color.Green);
        linearButton.Click += (sender, e) => PlotFunction(x => x, "x", Color.Blue);
        sineButton.Click += (sender, e) => PlotFunction(x => (float)Math.Sin(x), "sin(x)", Color.Orange);
        cosineButton.Click += (sender, e) => PlotFunction(x => (float)Math.Cos(x), "cos(x)", Color.Purple);
    }

    private void PlotFunction(Func<float, float> function, string functionName, Color plotColor)
    {
        Bitmap plotBitmap = new Bitmap(plotPictureBox.Width, plotPictureBox.Height);
        using (Graphics g = Graphics.FromImage(plotBitmap))
        {
            DrawGrid(g);
            DrawAxes(g);
            DrawFunction(g, function, plotColor);
            DrawFunctionName(g, functionName);
        }

        plotPictureBox.Image = plotBitmap;
    }

    private void DrawGrid(Graphics g)
    {
        Pen gridPen = new Pen(Color.LightGray);

        // Рисуем вертикальные линии сетки
        for (float x = -5.0f; x <= 5.0f; x += 1.0f)
        {
            float scaledX = x * plotPictureBox.Width / 10 + plotPictureBox.Width / 2;
            g.DrawLine(gridPen, scaledX, 0, scaledX, plotPictureBox.Height);
        }

        // Рисуем горизонтальные линии сетки
        for (float y = -1.0f; y <= 1.0f; y += 0.5f)
        {
            float scaledY = y * plotPictureBox.Height / 2 + plotPictureBox.Height / 2;
            g.DrawLine(gridPen, 0, scaledY, plotPictureBox.Width, scaledY);
        }
    }

    private void DrawAxes(Graphics g)
    {
        Pen axisPen = new Pen(Color.Black);

        DrawAxis(g, 0, plotPictureBox.Height / 2, plotPictureBox.Width, plotPictureBox.Height / 2); // Горизонтальная ось
        DrawAxis(g, plotPictureBox.Width / 2, 0, plotPictureBox.Width / 2, plotPictureBox.Height); // Вертикальная ось
    }

    private void DrawAxis(Graphics g, float x1, float y1, float x2, float y2)
    {
        g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
    }

    private void DrawFunction(Graphics g, Func<float, float> function, Color plotColor)
    {
        Pen plotPen = new Pen(plotColor);

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
        float scaledX1 = x1 * plotPictureBox.Width / 10 + plotPictureBox.Width / 2;
        float scaledY1 = y1 * plotPictureBox.Height / 2 + plotPictureBox.Height / 2;
        float scaledX2 = x2 * plotPictureBox.Width / 10 + plotPictureBox.Width / 2;
        float scaledY2 = y2 * plotPictureBox.Height / 2 + plotPictureBox.Height / 2;

        g.DrawLine(pen, scaledX1, scaledY1, scaledX2, scaledY2);
    }

    private void DrawFunctionName(Graphics g, string functionName)
    {
        Font font = new Font("Arial", 12, FontStyle.Bold);
        SolidBrush brush = new SolidBrush(Color.Black);

        g.DrawString(functionName, font, brush, new Point(10, 10));
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new SimpleGraphicalCalculator());
    }
}
