using Assignment_2_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_WPF.ViewModels
{
    public class PetViewModel : INotifyPropertyChanged
    {
        private Pet selectedPet;

        public ObservableCollection<Pet> Pets { get; set; }
        public Pet SelectedPet
        {
            get { return selectedPet; }
            set
            {
                selectedPet = value;
                OnPropertyChanged(nameof(SelectedPet));
            }
        }

        public bool HasUnsavedChanges { get; set; }

        public PetViewModel()
        {
            Pets = new ObservableCollection<Pet>();
            LoadPets();
        }

        public void LoadPets()
        {
            // Load pets from the database or service
            // Example: Pets = new ObservableCollection<Pet>(yourDataContext.Pets.ToList());
        }

        public void AddNewPet()
        {
          
        }

        public void EditPet()
        {
            if (SelectedPet != null)
            {
                // Logic to edit the selected pet
                SelectedPet.editPetDetails();
                HasUnsavedChanges = true;
            }
        }

        public void RemovePet()
        {
            if (SelectedPet != null)
            {
                Pets.Remove(SelectedPet);
                SelectedPet = null;
                HasUnsavedChanges = true;
            }
        }

        public void SaveChanges()
        {
            // Save changes to the database or service
            HasUnsavedChanges = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
