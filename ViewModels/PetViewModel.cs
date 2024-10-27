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
        private readonly int _currentUserId;
        private Pet selectedPet;

        public Pet SelectedPet
        {
            get { return selectedPet; }
            set
            {
                selectedPet = value;
                OnPropertyChanged(nameof(SelectedPet));
            }
        }
        //get the userId of logged in user
        
        public string PetName
        {
            get => _petName;
            set
            {
                _petName = Convert.ToString(value);
                OnPropertyChanged(nameof(PetName));
            }
        }

        public DateTime Dob
        {
            get => _dob;
            set
            {
                _dob = Convert.ToDateTime(value);
                OnPropertyChanged(nameof(Dob));
            }
        }

        public string Breed
        {
            get => _breed;
            set
            {
                _breed = Convert.ToString(value);
                OnPropertyChanged(nameof(Breed));
            }
        }

        public int Weight
        {
            get => _weight;
            set
            {
                _weight = Convert.ToInt32(value);
                OnPropertyChanged(nameof(Weight));
            }
        }

        public ObservableCollection<Pet> Pets
        {
            get => _pets;
            set
            {
                _pets = value;
                //OnPropertyChanged(_pets);
                
            }
        }

        public PetViewModel()
        {
            Pets = new ObservableCollection<Pet>();
            Dob = DateTime.Today;  // Set default date
            _currentUserId = GetCurrentUserId();
            LoadPets();
        }

        // Method to get the current user's ID
        private int GetCurrentUserId()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Get the first user (or you could modify this to get the logged-in user)
                    var user = context.Users.FirstOrDefault();
                    if (user != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Found user: {user.Name} with ID: {user.Id}");
                        return user.Id;
                    }
                    else
                    {
                        // If no user exists, create one
                        var newUser = new User("Default User", "default@test.com", "password");
                        context.Users.Add(newUser);
                        context.SaveChanges();
                        System.Diagnostics.Debug.WriteLine($"Created new user with ID: {newUser.Id}");
                        return newUser.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting user ID: {ex.Message}");
                System.Windows.MessageBox.Show("Error getting user information. Please try again.");
                return -1; // Return invalid ID to indicate error
            }
        }

        public void LoadPets() 
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Load only pets for the current user
                    var userPets = context.Pets
                        .Where(p => p.UserId == _currentUserId)
                        .ToList();

                    Pets.Clear();
                    //if no pet, go to addnewpet screen
                    foreach (var pet in userPets)
                    {
                        Pets.Add(pet);
                    }

                    System.Diagnostics.Debug.WriteLine($"Loaded {Pets.Count} pets for user {_currentUserId}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading pets: {ex.Message}");
                System.Windows.MessageBox.Show("Error loading pets. Please try again.");
            }
        }

        public void ShowDetails()
        {
            if (SelectedPet == null)
            {
                System.Windows.MessageBox.Show("Please select a pet to view details.", "Validation Error");
                return;
            }

            System.Windows.MessageBox.Show($"Pet ID: {SelectedPet.Id}\n" +
                                          $"Name: {SelectedPet.PetName}\n" +
                                          $"Breed: {SelectedPet.Breed}\n" +
                                          $"DOB: {SelectedPet.Dob}\n" +
                                          $"Weight: {SelectedPet.Weight}\n" +
                                          $"Owner ID: {SelectedPet.UserId}", "Pet Details");
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
                        UserId = _currentUserId  // get this from logged-in user
                    };
                    System.Diagnostics.Debug.WriteLine($"Adding new pet: {newPet.Id},{newPet.PetName}, {newPet.Breed},{newPet.Dob},{newPet.Weight},{newPet.UserId}");
                    context.Pets.Add(newPet);
                    context.SaveChanges();

                    // Add to observable collection
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        Pets.Add(newPet);
                        SelectedPet = newPet;
                    });

                    // Create new ObservableCollection and notify change
                    
                    System.Windows.MessageBox.Show("Pet added successfully!", "Success");
                  
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding pet: {ex.Message}", "Error");
                if (ex.InnerException != null)
                {
                    System.Windows.MessageBox.Show($"Inner Exception: {ex.InnerException.Message}", "Error");
                }
                return false;
            }
        }
        public void RemovePet()
        {
            try
            {
                if (SelectedPet == null)
                {
                    System.Windows.MessageBox.Show("Please select a pet to remove.", "Validation Error");
                    return;
                }

                using (var context = new AppDbContext())
                {
                    // Remove the selected pet
                    context.Pets.Remove(SelectedPet);
                    context.SaveChanges();

                    // Remove from observable collection
                    Pets.Remove(SelectedPet);
                   
                    System.Windows.MessageBox.Show("Pet removed successfully!", "Success");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error removing pet: {ex.Message}", "Error");
                if (ex.InnerException != null)
                {
                    System.Windows.MessageBox.Show($"Inner Exception: {ex.InnerException.Message}", "Error");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
       
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}