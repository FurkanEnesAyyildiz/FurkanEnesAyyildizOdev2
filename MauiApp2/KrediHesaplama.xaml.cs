using System;
using Microsoft.Maui.Controls;

namespace MauiApp2
{
    public partial class KrediHesaplama : ContentPage
    {
        // kkdf ve bsmv oranlar�
        private const double KKDF_Ihtiyac = 0.15;
        private const double BSMV_Ihtiyac = 0.10;
        private const double KKDF_Tasit = 0.15;
        private const double BSMV_Tasit = 0.05;
        private const double KKDF_Konut = 0.0;
        private const double BSMV_Konut = 0.0;
        private const double KKDF_Ticari = 0.0;
        private const double BSMV_Ticari = 0.05;

        public KrediHesaplama()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            string loanType = loanTypePicker.SelectedItem.ToString();
            double loanAmount = double.Parse(loanAmountEntry.Text);
            int loanTerm = int.Parse(loanTermEntry.Text);
            double monthlyRate = double.Parse(interestRateEntry.Text) / 100; // parse y�zdeyi desimale �evir

            // br�t faiz hesaplama
            double brutFaiz = CalculateBrutFaiz(monthlyRate, loanType);

            // ayl�k taksit hesaplama
            double monthlyPayment = CalculateMonthlyPayment(loanAmount, brutFaiz, loanTerm);
            // toplam �deme hesaplamaa
            double totalPayment = monthlyPayment * loanTerm;
            // toplam faiz hesaplama
            double totalInterest = totalPayment - loanAmount;

            // sonu�lar� g�r�nt�leme
            resultLabel.Text = $"Ayl�k Taksit: {monthlyPayment:F2} TL";
            totalPaymentLabel.Text = $"Toplam �deme: {totalPayment:F2} TL";
            totalInterestLabel.Text = $"Toplam Faiz: {totalInterest:F2} TL";
        }

        private double CalculateBrutFaiz(double monthlyRate, string loanType)
        {
            double KKDF = 0;
            double BSMV = 0;

            switch (loanType)
            {
                case "�htiya� Kredisi":
                    KKDF = KKDF_Ihtiyac;
                    BSMV = BSMV_Ihtiyac;
                    break;
                case "Konut Kredisi":
                    KKDF = KKDF_Konut;
                    BSMV = BSMV_Konut;
                    break;
                case "Ta��t Kredisi":
                    KKDF = KKDF_Tasit;
                    BSMV = BSMV_Tasit;
                    break;
                case "Ticari Kredi":
                    KKDF = KKDF_Ticari;
                    BSMV = BSMV_Ticari;
                    break;
            }   return (monthlyRate + (monthlyRate * BSMV) + (monthlyRate * KKDF));}

        private double CalculateMonthlyPayment(double principal, double brutFaiz, int termMonths)
        {
            double monthlyPayment = (principal * (Math.Pow(1 + brutFaiz, termMonths) * brutFaiz)) /
                (Math.Pow(1 + brutFaiz, termMonths) - 1);
            return monthlyPayment;
        }
    }
}
