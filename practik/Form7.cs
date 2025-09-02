using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace practik
{
    public partial class Form7 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Form7()
        {
            this.ClientSize = new Size(670, 250);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "green", "background.png"));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Button CreateButton(string path, int x, int y, int w, int h, Color bg)
            {
                var btn = new Button();
                btn.Location = new Point(x, y);
                btn.Size = new Size(w, h);
                btn.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "green", path));
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.BackColor = bg;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = bg;
                btn.FlatAppearance.MouseDownBackColor = bg;
                btn.UseVisualStyleBackColor = false;
                return btn;
            }

            var themeBtn = CreateButton("theme.png", 2, 1, 24, 24, Color.FromArgb(33, 131, 89));
            var playBtn = CreateButton("play.png", 266, 190, 374, 43, Color.FromArgb(0, 175, 100));
            var infoBtn = CreateButton("info.png", 647, 31, 22, 22, Color.FromArgb(33, 131, 89));
            var exitBtn = CreateButton("exit.png", 650, 4, 18, 18, Color.FromArgb(33, 131, 89));

            themeBtn.Click += (s, e) =>
            {
                Form10 form = new Form10();
                form.Show();
                this.Hide();
            };

            infoBtn.Click += (s, e) =>
            {
                Info3 infoForm = new Info3();
                infoForm.Show();
                this.Hide();
            };

            playBtn.Click += (s, e) =>
            {
                Solution3 solForm = new Solution3(this);
                solForm.Show();
                this.Hide();
            };

            exitBtn.Click += (s, e) => Application.Exit();

            PictureBox text = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine(Application.StartupPath, "green", "text.png")),
                Location = new Point(266, 25),
                Size = new Size(367, 149),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            PictureBox image = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine(Application.StartupPath, "green", "image.png")),
                Location = new Point(39, 25),
                Size = new Size(208, 208),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            this.Controls.AddRange(new Control[] { themeBtn, playBtn, infoBtn, exitBtn, text, image });

            this.FormClosed += (s, e) => Application.Exit();

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
