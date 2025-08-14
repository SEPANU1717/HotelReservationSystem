using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelReservationSystem.Interface.Service.Laundry;
using HotelReservationSystem.Model.Service;

namespace HotelReservationSystem.Presenter
{
    public class LaundryPresenter
    {
        private ILaundryView laundryView;
        private ILaundryServiceRepository repository;
        private BindingSource LaundryBindingSource;
        private IEnumerable<SharedAddServiceModel> laundryList;

        public LaundryPresenter(ILaundryView laundryView, ILaundryServiceRepository repository)
        {
            LaundryBindingSource = new BindingSource();
            this.laundryView = laundryView;
            this.repository = repository;

            this.laundryView.AddEvent += AddLaundry;
            this.laundryView.CompleteEvent += CompleteLaundry;
            this.laundryView.CancelEvent += CancelLaundry;
            this.laundryView.ClearEvent += ClearLaundry;

            this.laundryView.SetLaundryListBindingSource(LaundryBindingSource);
            LoadAllLaundryList();
        }

        private void LoadAllLaundryList()
        {
            try
            {
                laundryList = repository.GetAll();
                LaundryBindingSource.DataSource = laundryList;
                LaundryBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading laundry data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearLaundry(object sender, EventArgs e)
        {
            try
            {
                
                    repository.ClearAll();
                    LoadAllLaundryList(); 
                    MessageBox.Show("All laundry items have been deleted successfully.",
                        "Delete Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                
            }
        
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting laundry items: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CancelLaundry(object sender, EventArgs e) => CleanViewFields();

        private void CompleteLaundry(object sender, EventArgs e)
        {
            MessageBox.Show(@"Complete functionality not implemented yet.");
        }

        private void AddLaundry(object sender, EventArgs e)
        {
            try
            {
                var laundry = new SharedAddServiceModel
                {
                    ItemName = laundryView.LaundryName,
                    Quantity = int.Parse(laundryView.Quantity),
                    Price = decimal.Parse(laundryView.LPrice)
                };

                repository.Add(laundry);
                LoadAllLaundryList(); // Refresh the list
                CleanViewFields();
                MessageBox.Show("Laundry item added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for quantity and price.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding laundry item: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CleanViewFields()
        {
            laundryView.LaundryId = "0";
            laundryView.LaundryName = "";
            laundryView.LPrice = "";
            laundryView.Quantity = "";
        }
    }
}
