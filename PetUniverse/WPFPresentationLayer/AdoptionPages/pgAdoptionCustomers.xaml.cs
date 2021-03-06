using DataTransferObjects;
using LogicLayer;
using LogicLayerInterfaces;
using PresentationUtilityCode;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFPresentationLayer.AdoptionPages
{
    /// <summary>
    /// NAME: Austin Gee
    /// DATE: 2/6/2020
    /// CHECKED BY: Mohamed Elamin, 02/07/2020
    /// 
    /// This form displays a list of Adoption customers that can be used to access a
    /// customers profile.
    /// </summary>
    /// <remarks>
    /// UPDATED BY: NA
    /// UPDATE DATE: NA
    /// WHAT WAS CHANGED: NA
    /// 
    /// </remarks>
    public partial class pgAdoptionCustomers : Page
    {
        private IAdoptionCustomerManager _adoptionCustomerManager;
        private IAppointmentTypeManager _appointmentTypeManager;
        private IAdoptionApplicationManager _adoptionApplicationManager;
        private ILocationManager _locationManager;
        private IAdoptionAppointmentManager _adoptionAppointmentManager;


        private List<Location> _locations;
        private AdoptionCustomerVM _adoptionCustomerVM;
        private Location _location;

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 2/6/2020
        /// CHECKED BY: Mohamed Elamin, 02/07/2020
        /// 
        /// This is the standard no argument constructor
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        public pgAdoptionCustomers()
        {
            InitializeComponent();
            _adoptionCustomerManager = new AdoptionCustomerManager();
            _appointmentTypeManager = new AppointmentTypeManager();
            _adoptionApplicationManager = new AdoptionApplicationManager();
            _locationManager = new LocationManager();
            _adoptionAppointmentManager = new AdoptionAppointmentManager();

            canAdoptionCustomerProfile.Visibility = Visibility.Collapsed;
            canAdoptionCustomerProfiles.Visibility = Visibility.Visible;


            populateCustomerDataGrid();
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 2/6/2020
        /// CHECKED BY: Mohamed Elamin, 02/07/2020
        /// 
        /// This helper method populates the Customer data grid with dat when the form is loaded.
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void populateCustomerDataGrid()
        {
            try
            {
                var allCustomers = _adoptionCustomerManager.RetrieveAdoptionCustomersByActive(true);
                var adoptionCustomers = new List<AdoptionCustomerVM>();
                foreach(var c in allCustomers)
                {
                    if(_adoptionApplicationManager.RetrieveAdoptionApplicationsByEmailAndActive(c.Email, true).Count > 0)
                    {
                        adoptionCustomers.Add(c);
                    }
                }
                dgCustomers.ItemsSource = adoptionCustomers;
            }
            catch (Exception)
            {


            }

        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 2/6/2020
        /// CHECKED BY: Mohamed Elamin, 02/07/2020
        /// 
        /// This event handler fires when columns for the dgCustomers data grid are generated. It formats the columns and headers
        /// in such a way that is more human readable.
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgCustomers_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgCustomers.Columns.RemoveAt(10);
            dgCustomers.Columns.RemoveAt(9);
            dgCustomers.Columns.RemoveAt(8);
            dgCustomers.Columns.RemoveAt(7);
            dgCustomers.Columns.RemoveAt(6);
            dgCustomers.Columns.RemoveAt(5);
            dgCustomers.Columns.RemoveAt(0);

            //dgCustomers.Columns.RemoveAt(2);
            //dgCustomers.Columns.RemoveAt(1);
            //dgCustomers.Columns.RemoveAt(0);



            dgCustomers.Columns[0].Header = "Email";
            dgCustomers.Columns[1].Header = "First Name";
            dgCustomers.Columns[2].Header = "Last Name";
            dgCustomers.Columns[3].Header = "Phone Number";



        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 2/6/2020
        /// CHECKED BY: Mohamed Elamin, 02/07/2020
        /// 
        /// This event handler is fired when a dgCustomer data grid selection is double clicked. It then loads
        /// up the chosen customers profile window
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


            _adoptionCustomerVM = (AdoptionCustomerVM)dgCustomers.SelectedItem;

            showCustomerProfile();
            populateTextFields();
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/18/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// shows the customer profile screen
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void showCustomerProfile()
        {
            canAdoptionCustomerProfiles.Visibility = Visibility.Hidden;
            canAdoptionCustomerProfile.Visibility = Visibility.Visible;
            canScheduleAppointment.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 2/6/2020
        /// CHECKED BY: Mohamed Elamin
        /// 
        /// This helper method is used to fill in the various text boxes in this form
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void populateTextFields()
        {
            txtFirstName.Text = _adoptionCustomerVM.FirstName;
            txtLastName.Text = _adoptionCustomerVM.LastName;
            txtCity.Text = _adoptionCustomerVM.City;
            //txtAdoptionApplicationID.Text = _adoptionCustomerVM.AdoptionApplicationID.ToString();
            //txtAdoptionStatus.Text = _adoptionCustomerVM.CustomerAdoptionStatus;
            //txtCustomerID.Text = _adoptionCustomerVM.CustomerID.ToString();
            txtEmail.Text = _adoptionCustomerVM.Email;
            txtPhoneNumber.Text = _adoptionCustomerVM.PhoneNumber;
            txtState.Text = _adoptionCustomerVM.State;
            txtZipcode.Text = _adoptionCustomerVM.ZipCode;
            //dpApplicationRecieved.SelectedDate = _adoptionCustomerVM.AdoptionApplicationRecievedDate;
            //txtAnimalID.Text = _adoptionCustomerVM.AnimalID.ToString();
            //txtAnimalName.Text = _adoptionCustomerVM.AnimalName;
            txtAddressOne.Text = _adoptionCustomerVM.AddressLineOne;
            txtAddressTwo.Text = _adoptionCustomerVM.AddressLineTwo;
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 2/6/2020
        /// CHECKED BY: Mohamed Elamin
        /// 
        /// This is used as an alternate way in which to close this form without using the corner x.
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            canAdoptionCustomerProfile.Visibility = Visibility.Hidden;
            canAdoptionCustomerProfiles.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 2/6/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// This clears the tab when another tab is clicked.
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            showCustomerProfiles();

        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 2/6/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// This clears the tab when another tab is clicked.
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void showCustomerProfiles()
        {
            canAdoptionCustomerProfile.Visibility = Visibility.Hidden;
            canAdoptionCustomerProfiles.Visibility = Visibility.Visible;
            canScheduleAppointment.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/18/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// allows user to schedule a new appointment
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem != null)
            {
                AdoptionCustomerVM adoptionCustomer = (AdoptionCustomerVM)dgCustomers.SelectedItem;
                _adoptionCustomerVM = _adoptionCustomerManager.RetrieveAdoptionCustomerByEmail(adoptionCustomer.Email);
                lblFirstName.Content = _adoptionCustomerVM.FirstName;
                lblLastName.Content = _adoptionCustomerVM.LastName;
                lblEmail.Content = _adoptionCustomerVM.Email;
                txtAppointmentTime.Text = "00h: 00m am/pm";
                showAppointmentScheduling();
                //dgLocations.Visibility = Visibility.Visible;
                grdChooseLocation.Visibility = Visibility.Visible;
                btnAddLocation.Visibility = Visibility.Visible;
                btnLocationSearch.Visibility = Visibility.Visible;
                btnSaveLocation.Visibility = Visibility.Hidden;
                txtLocationSearch.Visibility = Visibility.Visible;
            }
            else
            {
                WPFErrorHandler.ErrorMessage("Please choose a customer from the list");
            }


        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/18/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// shows the scheduling screen
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void showAppointmentScheduling()
        {
            try
            {
                dgAdoptionApplications.ItemsSource = _adoptionApplicationManager.RetrieveAdoptionApplicationsByEmailAndActive(_adoptionCustomerVM.Email);
                cmbAppointmentType.ItemsSource = null;
                cmbAppointmentType.ItemsSource = _appointmentTypeManager.RetrieveAllAppontmentTypes();
            }
            catch (Exception)
            {

            }
            
            populateLocationsDataGrid();
            canAdoptionCustomerProfile.Visibility = Visibility.Hidden;
            canAdoptionCustomerProfiles.Visibility = Visibility.Hidden;
            canScheduleAppointment.Visibility = Visibility.Visible;
            grdLocationDetails.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/21/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// populates the location data grid
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void populateLocationsDataGrid()
        {
            try
            {
                dgLocations.ItemsSource = _locationManager.RetrieveAllLocations();
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/18/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// goes back to the profile list screen from the schedule appointment screen
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void btnCancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            showCustomerProfiles();
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/19/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// formats the dgAdoptionApplications data grid into a human readable format
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void dgAdoptionApplications_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgAdoptionApplications.Columns.RemoveAt(9);
            dgAdoptionApplications.Columns.RemoveAt(8);
            dgAdoptionApplications.Columns.RemoveAt(6);
            dgAdoptionApplications.Columns.RemoveAt(5);
            dgAdoptionApplications.Columns.RemoveAt(4);
            dgAdoptionApplications.Columns.RemoveAt(3);

            dgAdoptionApplications.Columns[0].Header = "Animal Name";
            dgAdoptionApplications.Columns[1].Header = "Species";
            dgAdoptionApplications.Columns[2].Header = "Breed";
            dgAdoptionApplications.Columns[3].Header = "Adoption Status";

        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/19/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// formats the dgLocations data grid into a human readable format
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void dgLocations_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgLocations.Columns.RemoveAt(3);
            dgLocations.Columns.RemoveAt(0);

            dgLocations.Columns[0].Header = "Location Name";
            dgLocations.Columns[1].Header = "Address";
            dgLocations.Columns[2].Header = "City";
            dgLocations.Columns[3].Header = "State";
            dgLocations.Columns[4].Header = "Zip";
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/19/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// clears the time text box when its clicked on
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAppointmentTime_GotFocus(object sender, RoutedEventArgs e)
        {
            //DateTime result;
            if (!DateTime.TryParse(txtAppointmentTime.Text, out var result))
            {
                txtAppointmentTime.Clear();
            }

        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/19/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// adds an appointment
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmitScheduledAppointment_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = new DateTime();
            try
            {
                if (!cmbAppointmentType.SelectedItem.ToString().IsValidString())
                {
                    WPFErrorHandler.ErrorMessage("Must Choose a Valid Appointment Type.");
                    cmbAppointmentType.Focus();
                    return;
                }
            }
            catch (Exception)
            {
                WPFErrorHandler.ErrorMessage("Must Choose a Valid Appointment Type.");
                cmbAppointmentType.Focus();
                return;

            }

            if (!txtScheduledAnimalName.Text.IsValidString())
            {
                WPFErrorHandler.ErrorMessage("Must choose a valid animal from the list.");
                dgAdoptionApplications.Focus();
                return;
            }
            if (!txtScheduledLocationName.Text.IsValidString())
            {
                WPFErrorHandler.ErrorMessage("Must choose a valid location from the list.");
                dgLocations.Focus();
                return;
            }
            try
            {
                DateTime.Parse(txtAppointmentTime.Text);
            }
            catch (Exception)
            {
                txtAppointmentTime.Focus();
                WPFErrorHandler.ErrorMessage("Must enter a valid time");
                return;
            }

            try
            {
                DateTime.Parse(dpAppointmentDate.SelectedDate.ToString());
            }
            catch (Exception)
            {
                cmbAppointmentType.Focus();
                WPFErrorHandler.ErrorMessage("Must enter a valid date");
                return;
            }
            try
            {
                DateTime.TryParse((dpAppointmentDate.SelectedDate.Value.ToShortDateString() + " " + txtAppointmentTime.Text), out dateTime);
                var appointment = new AdoptionAppointment()
                {
                    AdoptionApplicationID = ((ApplicationVM)dgAdoptionApplications.SelectedItem).AdoptionApplicationID,
                    AppointmentTypeID = cmbAppointmentType.SelectedItem.ToString(),
                    AppointmentDateTime = dateTime,
                    LocationID = ((Location)dgLocations.SelectedItem).LocationID,

                };
                _adoptionAppointmentManager.AddAdoptionAppointment(appointment);
                MessageBox.Show("Appointment Added for " + _adoptionCustomerVM.FirstName + " " + _adoptionCustomerVM.LastName
                    + "\non " + dateTime.ToShortDateString() + " at " + dateTime.ToShortTimeString());
                txtAppointmentTime.Text = "00h: 00m am/pm";


            }
            catch (Exception)
            {

                WPFErrorHandler.ErrorMessage("Appointment not added");
            }
            cmbAppointmentType.SelectedItem = null;
            dpAppointmentDate.Text = null;
            txtAppointmentTime.Clear();
            txtScheduledLocationName.Clear();
            txtScheduledAnimalName.Clear();

            showCustomerProfiles();

        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/19/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// adds an appointment
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAppointmentTime_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(txtAppointmentTime.Text, out var result))
            {
                txtAppointmentTime.Clear();
            }
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/20/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// shows a chosen animal in the animal text field
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgAdoptionApplications_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                cmbAppointmentType.SelectedValue = ((ApplicationVM)dgAdoptionApplications.SelectedItem).Status;
                txtScheduledAnimalName.Text = ((ApplicationVM)dgAdoptionApplications.SelectedItem).AnimalName;
            }
            catch (Exception)
            {

                WPFErrorHandler.ErrorMessage("Please select an Animal");
            }
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/20/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// shows a chosen location in the location text field
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgLocations_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                if (null == ((Location)dgLocations.SelectedItem).Name || !((Location)dgLocations.SelectedItem).Name.IsValidString())
                {
                    txtScheduledLocationName.Text = ((Location)dgLocations.SelectedItem).Address1;
                }
                else
                {
                    txtScheduledLocationName.Text = ((Location)dgLocations.SelectedItem).Name;
                }

            }
            catch (Exception)
            {

                WPFErrorHandler.ErrorMessage("Please select a location");
            }
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/20/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// starts the options for creating a new location
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            grdChooseLocation.Visibility = Visibility.Hidden;
            grdLocationDetails.Visibility = Visibility.Visible;
            btnAddLocation.Visibility = Visibility.Hidden;

            txtLocationSearch.Visibility = Visibility.Hidden;
            btnLocationSearch.Visibility = Visibility.Hidden;

            btnSaveLocation.Visibility = Visibility.Visible;


            txtLocationAddress2.IsReadOnly = false;
            txtLocationName.IsReadOnly = false;
            txtLocationAdress1.IsReadOnly = false;
            txtLocationCity.IsReadOnly = false;
            txtLocationState.IsReadOnly = false;
            txtLocationZip.IsReadOnly = false;


        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/21/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// goes back from the Location details
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocationSearchBack_Click(object sender, RoutedEventArgs e)
        {
            closeLocationDetails();
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/21/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// closes location Details
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        private void closeLocationDetails()
        {
            grdLocationDetails.Visibility = Visibility.Hidden;
            grdChooseLocation.Visibility = Visibility.Visible;

            btnAddLocation.Visibility = Visibility.Visible;
            txtLocationSearch.Visibility = Visibility.Visible;
            btnLocationSearch.Visibility = Visibility.Visible;
            btnSaveLocation.Visibility = Visibility.Hidden;

            txtLocationName.Clear();
            txtLocationAddress2.Clear();
            txtLocationAdress1.Clear();
            txtLocationCity.Clear();
            txtLocationState.Clear();
            txtLocationZip.Clear();

            txtLocationAddress2.IsReadOnly = true;
            txtLocationName.IsReadOnly = true;
            txtLocationAdress1.IsReadOnly = true;
            txtLocationCity.IsReadOnly = true;
            txtLocationState.IsReadOnly = true;
            txtLocationZip.IsReadOnly = true;
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/21/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// Saves location Details
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!txtLocationAdress1.Text.IsValidString())
            {
                MessageBox.Show("Please enter a valid Address.");
                txtLocationAdress1.Focus();
                return;
            }
            if (!txtLocationCity.Text.IsValidString())
            {
                MessageBox.Show("Please enter a valid City.");
                txtLocationCity.Focus();
                return;
            }
            if (!txtLocationState.Text.IsValidString(2, 2))
            {
                MessageBox.Show("Please enter a valid two letter State Abreviation.");
                txtLocationState.Focus();
                return;
            }
            if (!txtLocationZip.Text.IsValidString())
            {
                MessageBox.Show("Please enter a valid Zip Code.");
                txtLocationZip.Focus();
                return;
            }

            try
            {
                var location = new Location()
                {
                    Name = txtLocationName.Text,
                    Address1 = txtLocationAdress1.Text,
                    Address2 = txtLocationAddress2.Text,
                    City = txtLocationCity.Text.ToUpper(),
                    State = txtLocationState.Text,
                    Zip = txtLocationZip.Text
                };

                _locationManager.AddLocation(location);
                MessageBox.Show("Location Added");
                populateLocationsDataGrid();
                closeLocationDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/21/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// clears location search text box when clicked and repopulates location data grid from db
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLocationSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            populateLocationsDataGrid();
            txtLocationSearch.Clear();
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 3/21/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// Searches the locations for matching fields
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocationSearch_Click(object sender, RoutedEventArgs e)
        {
            _locations = new List<Location>();
            var allLocations = _locationManager.RetrieveAllLocations();
            foreach (var l in allLocations)
            {
                if (l.Name.ToUpper() == txtLocationSearch.Text.ToUpper())
                {
                    _locations.Add(l);
                }
                else if (l.Address1.ToUpper() == txtLocationSearch.Text.ToUpper())
                {
                    _locations.Add(l);
                }
                else if (l.Address2.ToUpper() == txtLocationSearch.Text.ToUpper())
                {
                    _locations.Add(l);
                }
                else if (l.City.ToUpper() == txtLocationSearch.Text.ToUpper())
                {
                    _locations.Add(l);
                }
                else if (l.State.ToUpper() == txtLocationSearch.Text.ToUpper())
                {
                    _locations.Add(l);
                }
                else if (l.Zip.ToUpper() == txtLocationSearch.Text.ToUpper())
                {
                    _locations.Add(l);
                }
            }
            try
            {
                dgLocations.ItemsSource = _locations;
            }
            catch (Exception)
            {


            }
        }

        /// <summary>
        /// NAME: Austin Gee
        /// DATE: 5/1/2020
        /// CHECKED BY: Michael Thompson
        /// 
        /// Chooses a customer
        /// </summary>
        /// <remarks>
        /// UPDATED BY: NA
        /// UPDATE DATE: NA
        /// WHAT WAS CHANGED: NA
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if(null != dgCustomers.SelectedItem)
            {
                _adoptionCustomerVM = (AdoptionCustomerVM)dgCustomers.SelectedItem;

                showCustomerProfile();
                populateTextFields();
            }
            else
            {
                WPFErrorHandler.ErrorMessage("Please make a customer selection.");
            }
        }
    }
}
