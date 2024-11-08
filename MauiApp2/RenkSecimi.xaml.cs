using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace MauiApp2
{
    public partial class RenkSecimi : ContentPage
    {
        public RenkSecimi()
        {
            InitializeComponent();
        }

        // renk Karýþtýrýcý Sliders deðer deðiþiminde çaðrýlýr
        private void OnColorSliderValueChanged(object sender, ValueChangedEventArgs e)
        {   // renk deðerlerini alýyoruz
            int red = (int)SliderRed.Value;
            int green = (int)SliderGreen.Value;
            int blue = (int)SliderBlue.Value;

            // yeni rengi ayarla
            Color newColor = Color.FromRgb(red, green, blue);
            ColorPreview.Color = newColor;

            // renk kodu göster
            ColorCodeLabel.Text = $"Renk Kodu: #{red:X2}{green:X2}{blue:X2}"; }


        // renk kodunu kopyalama
        private async void OnCopyColorCodeClicked(object sender, EventArgs e)
        {
          string colorCode = ColorCodeLabel.Text.Replace("Renk Kodu: ", "");
        await Clipboard.Default.SetTextAsync(colorCode);
            await DisplayAlert("Bilgi", "Renk kodu kopyalandý: " + colorCode, "Tamam");
        }
    }
}
