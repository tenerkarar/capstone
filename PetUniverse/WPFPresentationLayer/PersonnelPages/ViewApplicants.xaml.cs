using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataTransferObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace WPFPresentationLayer.PersonnelPages
{
	/// <summary>
	/// CREATED BY: Matt Deaton
	/// DATE: 2020-04-11
	/// CHECKED BY: Steve Coonrod
	/// 
	/// View for handling Applicants
	/// Interaction logic for ViewApplicants.xaml
	/// 
	/// </summary>
	/// <remarks>
	/// UPDATED BY:
	/// UPDATED:
	/// CHANGE:
	/// </remarks>
	public partial class ViewApplicants : Page
	{
		// Class variables used for methods in this class
		private IApplicantManager _applicantManager;
		private ApplicantVM _applicantVM;

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-04-11
		/// CHECKED BY: Steve Coonrod
		/// 
		/// Constructor for initializng an application manager object.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		public ViewApplicants()
		{
			_applicantManager = new ApplicantManager();
			InitializeComponent();
		}// End Constructor

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-04-11
		/// CHECKED BY: Steve Coonrod
		/// 
		/// Method that loads the list of Applicants into the Data Grid on page Load.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			populateApplicantList();
		}// End Page_Loaded()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-04-11
		/// CHECKED BY: Steve Coonrod
		/// 
		/// Method that populates the Data Grid using a method from the Applicant Manager.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		private void populateApplicantList()
		{
			dgViewAllApplicants.ItemsSource = _applicantManager.RetrieveApplicants();
		}// End populateApplicantList()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-04-11
		/// CHECKED BY: Steve Coonrod
		/// 
		/// Method that names the Columns of the Data Grid after page is loaded.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgViewAllApplicants_AutoGeneratedColumns(object sender, EventArgs e)
		{
			dgViewAllApplicants.Columns[0].Header = "Applicant ID";
			dgViewAllApplicants.Columns[1].Header = "First Name";
			dgViewAllApplicants.Columns[2].Header = "Last Name";
			dgViewAllApplicants.Columns.RemoveAt(3); // Remove MiddleName
			dgViewAllApplicants.Columns[3].Header = "Email";
			dgViewAllApplicants.Columns[4].Header = "Phone Number";
			dgViewAllApplicants.Columns.RemoveAt(5); // Remove AddressLineOne
			dgViewAllApplicants.Columns.RemoveAt(5); // Remove AddressLineTwo
			dgViewAllApplicants.Columns.RemoveAt(5); // Remove City
			dgViewAllApplicants.Columns.RemoveAt(5); // Remove State
			dgViewAllApplicants.Columns.RemoveAt(5); // Remove Zipcode
			dgViewAllApplicants.Columns.RemoveAt(5); // Remove Foster
			dgViewAllApplicants.Columns[5].Header = "Application Status";
			dgViewAllApplicants.Columns[6].Header = "Position Applied";

			foreach (var column in this.dgViewAllApplicants.Columns)
			{
				column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}
		}// End dgViewAllApplicants_AutoGeneratedColumns()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-04-11
		/// CHECKED BY: Steve Coonrod
		/// 
		/// Method that to Retrieve applicant details using a Applicant Manager method.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		private void retrieveApplicantDetails()
		{
			Applicant applicant = (Applicant)dgViewAllApplicants.SelectedItem;
			_applicantVM = _applicantManager.RetrieveApplicantForInterview(applicant.ApplicantID);
		}// End retrieveApplicantDetails()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-04-11
		/// CHECKED BY: Steve Coonrod
		/// 
		/// Method that allosw the user to double click a Data Grid Row to open up Applicant Details.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgViewAllApplicants_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			retrieveApplicantDetails();
			this.NavigationService?.Navigate(new InterviewApplicant(_applicantVM, _applicantManager));
		}// End dgViewAllApplicants_MouseDoubleClick()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-04-11
		/// CHECKED BY: Steve Coonrod
		/// 
		/// Method that allosw the user to click the Select Button on a Data Grid Row Selected Item to open up Applicant Details.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectApplicant_Click(object sender, RoutedEventArgs e)
		{
			if (dgViewAllApplicants.SelectedItem != null)
			{
				retrieveApplicantDetails();
				this.NavigationService?.Navigate(new InterviewApplicant(_applicantVM, _applicantManager));
			}
			else
			{
				MessageBox.Show("Please select an applicant first.");
			}

		}// End btnSelectApplicant_Click()

	}// End class ViewApplicants : Page

}// End namespace WPFPresentationLayer.PersonnelPages