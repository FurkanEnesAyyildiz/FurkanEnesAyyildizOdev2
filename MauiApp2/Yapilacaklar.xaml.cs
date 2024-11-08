using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace MauiApp2
{
    public partial class Yapilacaklar : ContentPage
    {
        // Yapýlacak notlarýn tutulduðu liste
        private ObservableCollection<string> notes;

        // Notlarýn kaydedileceði dosya yolu
        private readonly string notesFilePath = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

        public Yapilacaklar()
        {
            InitializeComponent();
            notes = new ObservableCollection<string>();
            NotesCollectionView.ItemsSource = notes;

            // Notlarý yükle
            LoadNotes();
        }

        // Yeni not ekleme iþlemi
        private void OnAddNoteClicked(object sender, EventArgs e)
        {
            string newNote = NoteEntry.Text;
            if (!string.IsNullOrWhiteSpace(newNote))
            {
                notes.Add(newNote); // Notu listeye ekle
                NoteEntry.Text = string.Empty; // Giriþ alanýný temizle
                SaveNotes(); // Notlarý kaydet
            }
            else
            {
                DisplayAlert("Hata", "Lütfen bir not girin.", "Tamam");
            }
        }

        // Notlarý dosyaya kaydetme
        private async void SaveNotes()
        {
            var notesData = string.Join("\n", notes);
            await File.WriteAllTextAsync(notesFilePath, notesData);
        }

        // Uygulama açýldýðýnda notlarý yükleme
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
