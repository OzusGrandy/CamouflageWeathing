using System.Windows.Forms;

namespace CamouflageWeathingApp
{
    public partial class Form1 : Form
    {
        private DataController _dataController;
        private RuntimePaintingBitmap _paintingBitmap;
        private bool _isManualEditing = false;
        private List<ColorNameComboBoxModel> _colors;
        private ColorNameComboBoxModel _color;

        public Form1()
        {
            InitializeComponent();
            _dataController = new DataController();
            _paintingBitmap = new RuntimePaintingBitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            _colors = new List<ColorNameComboBoxModel>();
            _colors.Add(new ColorNameComboBoxModel 
            { 
                Color = button1.BackColor, 
                Id = 0, 
                Name = textBox2.Text , 
                Type = ColorTypeEnum.ColorOne 
            });
            _colors.Add(new ColorNameComboBoxModel 
            { 
                Color = button2.BackColor, 
                Id = 1, 
                Name = textBox3.Text,
                Type = ColorTypeEnum.ColorTwo
            });
            _colors.Add(new ColorNameComboBoxModel 
            { 
                Color = button3.BackColor, 
                Id = 2, 
                Name = textBox4.Text,
                Type = ColorTypeEnum.ColorThree
            });
            comboBox1.DataSource = _colors;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            groupBox1.Visible = _paintingBitmap.IsDrawingScheme;
            label14.Enabled = _isManualEditing;
            comboBox1.Enabled = _isManualEditing;
            button5.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color sameColor = button1.BackColor;
                _dataController.ImageData.ColorOne = colorDialog.Color;
                button1.BackColor = colorDialog.Color;
                _dataController.ChangeColor(sameColor, colorDialog.Color);
                pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
                ChangeListOfColors();
                if (_color.Id == 0)
                {
                    _color.Color = colorDialog.Color;
                    _color.Type = ColorTypeEnum.ColorOne;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color sameColor = button2.BackColor;
                _dataController.ImageData.ColorTwo = colorDialog.Color;
                button2.BackColor = colorDialog.Color;
                _dataController.ChangeColor(sameColor, colorDialog.Color);
                pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
                ChangeListOfColors();
                if (_color.Id == 1)
                {
                    _color.Color = colorDialog.Color;
                    _color.Type = ColorTypeEnum.ColorTwo;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color sameColor = button3.BackColor;
                _dataController.ImageData.ColorThree = colorDialog.Color;
                button3.BackColor = colorDialog.Color;
                _dataController.ChangeColor(sameColor, colorDialog.Color);
                pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
                ChangeListOfColors();
                if (_color.Id == 2)
                {
                    _color.Color = colorDialog.Color;
                    _color.Type = ColorTypeEnum.ColorThree;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _dataController.ImageData.BackgroundColor = colorDialog.Color;
                button4.BackColor = colorDialog.Color;
                pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _dataController.GenerateMazeCells();
            _paintingBitmap.IsDrawingScheme = true;
            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            groupBox1.Visible = _paintingBitmap.IsDrawingScheme;
            numericUpDown1.Enabled = !_paintingBitmap.IsDrawingScheme;
            numericUpDown2.Enabled = !_paintingBitmap.IsDrawingScheme;
            numericUpDown3.Enabled = !_paintingBitmap.IsDrawingScheme;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _dataController = new DataController();
            numericUpDown1.Value = _dataController.ImageData.ColorOnePercentage;
            numericUpDown2.Value = _dataController.ImageData.ColorTwoPercentage;
            numericUpDown3.Value = _dataController.ImageData.ColorThreePercentage;
            textBox1.Text = _dataController.ImageData.TextModel.NameOfScheme;
            textBox2.Text = "Цвет №1";
            textBox3.Text = "Цвет №2";
            textBox4.Text = "Цвет №3";
            button1.BackColor = Color.Empty;
            button2.BackColor = Color.Empty;
            button3.BackColor = Color.Empty;
            button4.BackColor = Color.Empty;
            button10.BackColor = Color.Empty;
            button11.BackColor = Color.Empty;
            _paintingBitmap.IsDrawingScheme = false;
            groupBox1.Visible = _paintingBitmap.IsDrawingScheme;
            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            numericUpDown1.Enabled = !_paintingBitmap.IsDrawingScheme;
            numericUpDown2.Enabled = !_paintingBitmap.IsDrawingScheme;
            numericUpDown3.Enabled = !_paintingBitmap.IsDrawingScheme;
            button5.Enabled = false;
            ChangeListOfColors();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string savePath = string.Empty;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                savePath = dialog.SelectedPath;
            }
            if (savePath != string.Empty)
            {
                if (_dataController.ImageData.TextModel.NameOfScheme == string.Empty)
                {
                    _paintingBitmap.Rendering(_dataController.ImageData).Save($"{savePath}" + $"\\scheme.png", System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                {
                    _paintingBitmap.Rendering(_dataController.ImageData).Save($"{savePath}" + $"\\{_dataController.ImageData.TextModel.NameOfScheme}.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _dataController.ImageData.NetColor = colorDialog.Color;
                button10.BackColor = colorDialog.Color;
                pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _dataController.ImageData.DividingLineColor = colorDialog.Color;
                button11.BackColor = colorDialog.Color;
                pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _dataController.ImageData.TextModel.NameOfScheme = textBox1.Text;
            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            _dataController.ImageData.TextModel.NameOfColorOne = textBox2.Text;
            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            ChangeListOfColors();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            _dataController.ImageData.TextModel.NameOfColorTwo = textBox3.Text;
            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            ChangeListOfColors();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            _dataController.ImageData.TextModel.NameOfColorThree = textBox4.Text;
            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            ChangeListOfColors();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value = _dataController.SetColorTwoPercentage(numericUpDown2.Value);

            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            if (_dataController.ImageData.FreePercentage == 0)
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = _dataController.SetColorOnePercentage(numericUpDown1.Value);

            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            if (_dataController.ImageData.FreePercentage == 0)
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Value = _dataController.SetColorThreePercentage(numericUpDown3.Value);

            pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            if (_dataController.ImageData.FreePercentage == 0)
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _isManualEditing = !_isManualEditing;
            label14.Enabled = _isManualEditing;
            comboBox1.Enabled = _isManualEditing;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isManualEditing)
            {
                var locationX = e.Location.X;
                var locationY = e.Location.Y;

                _dataController.ManualEditing(locationX, locationY, _color);

                numericUpDown1.Value = _dataController.ImageData.ColorOnePercentage;
                numericUpDown2.Value = _dataController.ImageData.ColorTwoPercentage;
                numericUpDown3.Value = _dataController.ImageData.ColorThreePercentage;
                pictureBox1.Image = _paintingBitmap.Rendering(_dataController.ImageData);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _color = (ColorNameComboBoxModel)comboBox1.SelectedItem;
        }
        private void ChangeListOfColors()
        {
            _colors = new List<ColorNameComboBoxModel>();
            _colors.Add(new ColorNameComboBoxModel
            {
                Color = button1.BackColor,
                Id = 0,
                Name = textBox2.Text,
                Type = ColorTypeEnum.ColorOne
            });
            _colors.Add(new ColorNameComboBoxModel
            {
                Color = button2.BackColor,
                Id = 1,
                Name = textBox3.Text,
                Type = ColorTypeEnum.ColorTwo
            });
            _colors.Add(new ColorNameComboBoxModel
            {
                Color = button3.BackColor,
                Id = 2,
                Name = textBox4.Text,
                Type = ColorTypeEnum.ColorThree
            });
            comboBox1.DataSource = _colors;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
        }
    }
}