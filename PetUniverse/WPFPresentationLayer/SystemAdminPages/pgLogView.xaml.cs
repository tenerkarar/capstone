using LogicLayer;
using LogicLayerInterfaces;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFPresentationLayer
{
    /// <summary>
    /// Creator: Zach Behrensmeyer
    /// Created: 2/20/2020
    /// Appover: Steven Cardonas
    /// 
    /// This class controls LogView page
    /// 
    /// </summary>
    public partial class LogView : Page
    {

        private ILogManager _logManager;

        /// <summary>
        /// Creator: Zach Behrensmeyer
        /// Created: 2/20/2020
        /// Appover: Steven Cardonas
        /// 
        /// This is a constructor for the LogView Page
        /// 
        /// </summary>
        /// <remarks>
        /// Updater: NA
        /// Updated: NA
        /// Update: NA
        /// 
        /// </remarks>
        /// </summary>
        public LogView()
        {
            _logManager = new LogManager();
            InitializeComponent();
        }

        /// <summary>
        /// Creator: Zach Behrensmeyer
        /// Created: 2/15/2020
        /// Appover: Steven Cardonas
        /// 
        /// Method that generates the columns for the log list.
        /// </summary>
        /// <remarks>
        /// Updater: NA
        /// Updated: NA
        /// Update: NA      
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgLogList_AutoGeneratedColumns_1(object sender, EventArgs e)
        {
            dgLogList.Columns.RemoveAt(6);
            dgLogList.Columns.RemoveAt(4);
            dgLogList.Columns.RemoveAt(2);
            dgLogList.Columns.RemoveAt(0);
            dgLogList.Columns[0].Header = "Log Date";
            dgLogList.Columns[1].Header = "Log Level";
            dgLogList.Columns[2].Header = "Log Message";

            foreach (var column in this.dgLogList.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }


        /// <summary>
        /// Creator: Zach Behrensmeyer
        /// Created: 2/15/2020
        /// Appover: Steven Cardonas
        /// 
        /// When dgLogList is loaded. Adds items into dgLogList.
        /// </summary>
        /// <remarks>
        /// Updater: NA
        /// Updated: NA
        /// Update: NA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgLogList_Loaded(object sender, RoutedEventArgs e)
        {
            dgLogList.ItemsSource = _logManager.RetrieveLoginandOutLogs();
        }
    }
}
