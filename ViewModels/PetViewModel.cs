using Assignment_2_WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Assignment_2_WPF.ViewModels
{
    public class PetViewModel : INotifyPropertyChanged
    {
        private string _petName;
        private DateTime _dob;
        private string _breed;
        private int _weight;
        private ObservableCollection<Pet> _pets;

        public string PetName
        {
            get => _petName;
            set
            {
                _petName = value;
                OnPropertyChanged(nameof(PetName));
            }
        }

        public DateTime Dob
        {
            get => _dob;
            set
            {
                _dob = value;
                OnPropertyChanged(nameof(Dob));
            }
        }

        public string Breed
        {
            get => _breed;
            set
            {
                _breed = value;
                OnPropertyChanged(nameof(Breed));
            }
        }

        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        public ObservableCollection<Pet> Pets
        {
            get => _pets;
            set
            {
                _pets = value;
                OnPropertyChanged(nameof(Pets));
            }
        }

        public PetViewModel()
        {
            Pets = new ObservableCollection<Pet>();
            Dob = DateTime.Today;  // Set default date
            LoadPets();
        }

        public void LoadPets()
        {
            using (var context = new AppDbContext())
            {
                var dbPets = context.Pets.ToList();
                Pets = new ObservableCollection<Pet>(dbPets);
            }
        }

        public bool AddNewPet(string petName, DateTime dob, string breed, string weightStr)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(petName) || string.IsNullOrWhiteSpace(breed))
                {
                    System.Windows.MessageBox.Show("Please fill in all required fields.", "Validation Error");
                    return false;
                }

                if (!int.TryParse(weightStr, out int weight))
                {
                    System.Windows.MessageBox.Show("Please enter a valid number for weight.", "Validation Error");
                    return false;
                }

                using (var context = new AppDbContext())
                {
                    // Create new pet with user input
                    var newPet = new Pet
                    {
                        PetName = petName,
                        Breed = breed,
                        Dob = dob,
                        Weight = weight,
                        UserId = 1  // get this from logged-in user
                    };

                    context.Pets.Add(newPet);
                    context.SaveChanges();

                    // Add to observable collection
                    Pets.Add(newPet);

                    System.Windows.MessageBox.Show("Pet added successfully!", "Success");
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding pet: {ex.Message}", "Error");
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}