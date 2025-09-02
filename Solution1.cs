using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace practik
{
    public partial class Solution1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Form1 mainForm;

        public Solution1(Form1 form)
        {
            mainForm = form;

            this.ClientSize = new Size(476, 250);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            string imgPath(string fileName) => Path.Combine(Application.StartupPath, "blue", fileName);

            this.BackgroundImage = Image.FromFile(imgPath("solback.png"));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Button CreateButton(string path, int x, int y, int w, int h)
            {
                var btn = new Button();
                btn.Location = new Point(x, y);
                btn.Size = new Size(w, h);
                btn.BackgroundImage = Image.FromFile(imgPath(path));
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.BackColor = Color.Transparent;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                btn.UseVisualStyleBackColor = false;
                return btn;
            }

            PictureBox poleImage = new PictureBox()
            {
                Image = Image.FromFile(imgPath("pole.png")),
                Location = new Point(68, 14),
                Size = new Size(348, 183),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            TextBox inputBox = new TextBox()
            {
                Location = new Point(83, 60),
                Size = new Size(309, 115),
                Multiline = true,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(3, 69, 105),
                ForeColor = Color.White,
                ReadOnly = true,
                BorderStyle = BorderStyle.None
            };

            Button playBtn = CreateButton("Play1.png", 113, 203, 258, 43);
            Button exitBtn = CreateButton("Exit.png", 460, 2, 18, 18);

            exitBtn.Click += (s, e) =>
            {
                mainForm.Show();
                this.Close();
            };

            playBtn.Click += (s, e) =>
            {
                string SolveWithBisection()
                {
                    double f(double x) => x * x * x + 3 * x * x - 24 * x + 10;

                    double Bisection(double a, double b, double eps)
                    {
                        double fa = f(a);
                        double fb = f(b);
                        if (fa * fb > 0)
                            throw new Exception($"f({a}) и f({b}) одного знака. Интервал не подходит.");

                        while (Math.Abs(b - a) > eps)
                        {
                            double c = (a + b) / 2;
                            double fc = f(c);
                            if (Math.Abs(fc) < eps)
                                return c;

                            if (fa * fc < 0)
                            {
                                b = c;
                                fb = fc;
                            }
                            else
                            {
                                a = c;
                                fa = fc;
                            }
                        }
                        return (a + b) / 2;
                    }

                    var results = new StringBuilder();

                    // Найдём корни на разумных интервалах (визуально по графику)
                    double[][] intervals = new double[][] {
                        new double[] { -5, -2 },
                        new double[] { 0, 1 },
                        new double[] { 3, 5 }
                    };

                    int rootNum = 1;
                    foreach (var pair in intervals)
                    {
                        try
                        {
                            double root = Bisection(pair[0], pair[1], 1e-4);  // Точность 0,0001
                            results.AppendLine($"Корень {rootNum}: x ≈ {root:F4}, интервал: [{pair[0]}, {pair[1]}]");  // Показать до 4 знаков и интервал
                            rootNum++;
                        }
                        catch (Exception ex)
                        {
                            results.AppendLine($"На интервале [{pair[0]}, {pair[1]}]: {ex.Message}");
                        }
                    }

                    return results.ToString();
                }

                inputBox.Text = SolveWithBisection();
            };

            this.Controls.Add(poleImage);
            this.Controls.Add(inputBox);
            this.Controls.Add(playBtn);
            this.Controls.Add(exitBtn);
            inputBox.BringToFront();
            exitBtn.BringToFront();

            this.MouseDown += (s, e) =>
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            };

            this.MouseMove += (s, e) =>
            {
                if (dragging)
                {
                    Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    this.Location = Point.Add(dragFormPoint, new Size(diff));
                }
            };

            this.MouseUp += (s, e) => dragging = false;
        }
    }
}
