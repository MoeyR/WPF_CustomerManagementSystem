using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CMS.Models;

namespace WPF_CMS.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        //public List<Customer> Customers { get; set; } = new();
        public ObservableCollection<CustomerViewModel> Customers { get; set; } = new();
        public ObservableCollection<DateTime> Appointments { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate { get => _selectedDate; 
            set 
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value; 
                    RaisePropertyChanged(nameof(SelectedDate));
                }   
            } 
        }

        private CustomerViewModel _selectedCustomer;

        public CustomerViewModel SelectedCustomer
        { 
            get => _selectedCustomer;
            set 
            { 
                if(value != _selectedCustomer)
                {
                    _selectedCustomer = value;
                    RaisePropertyChanged(nameof(SelectedCustomer));
                    LoadAppoitemnts(SelectedCustomer.Id);
                }
            } 
        }

        public void LoadCustomers()
        {
            Customers.Clear();
            using (var db = new AppDbContext())
            {
                var customers = db.Customers.ToList();

                foreach(var c in customers)
                {
                    Customers.Add(new CustomerViewModel(c));
                }
            }

            
        }

        public void ClearSelectedCustomer()
        {
            _selectedCustomer = null;
            RaisePropertyChanged(nameof(SelectedCustomer));
        }

        public void SaveCustomer(string name, string idNumber, string address)
        {
            if (SelectedCustomer != null)
            {
                //update customer
                using (var db = new AppDbContext())
                {
                    var customer = db.Customers.Where(c => c.Id == SelectedCustomer.Id).FirstOrDefault();
                    customer.Name = name;
                    customer.IdNumber = idNumber;
                    customer.Address = address;
                }

            }
            else
            {
                //add customer
                using (var db = new AppDbContext())
                {
                    var newCustomer = new Customer()
                    {
                        Name = name,
                        IdNumber = idNumber,
                        Address = address
                    };
                    db.Customers.Add(newCustomer);
                    db.SaveChanges();
                }
                LoadCustomers();
            }
        }

        public void LoadAppoitemnts(int customerId)
        {
            Appointments.Clear();
            using (var db = new AppDbContext())
            {
                var appointments = db.Appointments.Where(a => a.CustomerId == customerId).ToList();

                foreach(var a in appointments)
                {
                    Appointments.Add(a.Time);
                }
            }
        }

        public void AddAppointment()
        {
            if(SelectedCustomer == null)
            {
                return;
            }

            using(var db = new AppDbContext())
            {
                var newAppointment = new Appointment()
                {
                    Time = SelectedDate.Value,
                    CustomerId = SelectedCustomer.Id
                };
                db.Appointments.Add(newAppointment);
                db.SaveChanges();
            }
            SelectedDate = null;
            LoadAppoitemnts(SelectedCustomer.Id);
        }
    }
}
