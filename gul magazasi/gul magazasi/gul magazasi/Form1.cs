using System;
using System.Globalization;
using System.Windows.Forms;

namespace gul_magazasi
{
    public partial class Form1 : Form
    {
        // Ümumi gəliri saxlayan dəyişən
        double totalIncome = 0;

        public Form1()
        {
            InitializeComponent();

            // Cədvəlin altındakı boş sətir görünməsin
            dataGridView1.AllowUserToAddRows = false;

            // Başlanğıcda toplam gəliri göstər
            lblGelir.Text = totalIncome.ToString("F2", CultureInfo.InvariantCulture);
        }

        // "Satış et" düyməsi
        private void button1_Click(object sender, EventArgs e)
        {
            // 1) Boşluq yoxlaması
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Bütün xanaları doldurun!", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Say düzgün tam ədəd olmalıdır
            int count;
            if (!int.TryParse(textBox2.Text.Trim(), out count))
            {
                MessageBox.Show("Say xanasına düzgün tam ədəd daxil edin!", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            // 3) Qiymət düzgün ədəd olmalıdır (nöqtə və vergül ilə işləyir)
            double price;
            string priceText = textBox3.Text.Trim().Replace(',', '.');
            if (!double.TryParse(priceText, NumberStyles.Any,
                                 CultureInfo.InvariantCulture, out price))
            {
                MessageBox.Show("Qiymət xanasına düzgün ədəd daxil edin!", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }

            // 4) Mənfi və ya sıfır olmaz
            if (count <= 0)
            {
                MessageBox.Show("Say sıfır və ya mənfi ola bilməz!", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show("Qiymət sıfır və ya mənfi ola bilməz!", "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }

            // 5) Yekun qiymətin hesablanması
            double result = count * price;

            // 6) Cədvələ əlavə et
            dataGridView1.Rows.Add(textBox1.Text.Trim(),
                                   count.ToString(),
                                   price.ToString(CultureInfo.InvariantCulture),
                                   result.ToString("F2", CultureInfo.InvariantCulture));

            // 7) Toplam gəliri artır və göstər
            totalIncome += result;
            lblGelir.Text = totalIncome.ToString("F2", CultureInfo.InvariantCulture);

            // 8) Xanaları sıfırla
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
        }

        // "Çıxış" düyməsi
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Çıxış edilsinmi?", "Çıxış",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}