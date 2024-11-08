using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace MauiApp2
{
    public partial class Yapilacaklar : ContentPage
    {
        // Yap�lacak notlar�n tutuldu�u liste
        private ObservableCollection<string> notes;

        // Notlar�n kaydedilece�i dosya yolu
        private readonly string notesFilePath = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

        public Yapilacaklar()
        {
            InitializeComponent();
            notes = new ObservableCollection<string>();
            NotesCollectionView.ItemsSource = notes;

            // Notlar� y�kle
            LoadNotes();
        }

        // Yeni not ekleme i�lemi
        private void OnAddNoteClicked(object sender, EventArgs e)
        {
            string newNote = NoteEntry.Text;
            if (!string.IsNullOrWhiteSpace(newNote))
            {
                notes.Add(newNote); // Notu listeye ekle
                NoteEntry.Text = string.Empty; // Giri� alan�n� temizle
                SaveNotes(); // Notlar� kaydet
            }
            else
            {
                DisplayAlert("Hata", "L�tfen bir not girin.", "Tamam");
            }
        }

        // Notlar� dosyaya kaydetme
        private async void SaveNotes()
        {
            var notesData = string.Join("\n", notes);
            await File.WriteAllTextAsync(notesFilePath, notesData);
        }

        // Uygulama a��ld���nda notlar� y�kleme
        private async void LoadNotes()
        {
            if (File.Exists(notesFilePath))
            {
                var notesData = await File.ReadAllTextAsync(notesFilePath);
                var loadedNotes = notesData.Split('\n');
                foreach (var note in loadedNotes)
                {
                    if (!string.IsNullOrWhiteSpace(note))
                    {
                        notes.Add(note);
                    }
                }
            }
        }
    }
}
