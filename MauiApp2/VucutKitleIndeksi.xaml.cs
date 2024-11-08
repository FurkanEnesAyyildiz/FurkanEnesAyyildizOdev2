using System;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace MauiApp2
{
    public partial class VucutKitleIndeksi : ContentPage
    {
        public VucutKitleIndeksi()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            // Kilo ve boy giriþlerini oku 
            if (double.TryParse(weightEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double weight) &&
                double.TryParse(heightEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double height) &&
                height > 0)
            {
                // bni hesapla
                double bmi = weight / (height * height);

                // bmi sonucu ve kategoriyi göster
                resultLabel.Text = $"BMI: {bmi:F2}";
                categoryLabel.Text = $"Kategori: {DetermineBMICategory(bmi)}";
            }
            else
            {
                resultLabel.Text = "Geçerli bir kilo ve boy giriniz!";
                categoryLabel.Text = "";
            }
        }

        private string DetermineBMICategory(double bmi)
        {
            if (bmi < 16) return "Ýleri düzeyde zayýf";
            else if (bmi < 17) return "Orta düzeyde zayýf";
            else if (bmi < 18.5) return "Hafif düzeyde zayýf";
            else if (bmi < 25) return "Normal kilolu";
            else if (bmi < 30) return "Hafif þiþman / Fazla kilolu";
            else if (bmi < 35) return "1. Derece obez";
            else if (bmi < 40) return "2. Derece obez";
            else return "3. Derece obez / Morbid obez";
        }
    }
}
