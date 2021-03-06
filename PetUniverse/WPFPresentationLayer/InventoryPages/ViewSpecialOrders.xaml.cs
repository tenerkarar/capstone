using DataTransferObjects;
using LogicLayer;
using PresentationUtilityCode;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFPresentationLayer.InventoryPages
{
    /// <summary>
    /// Creator: Jesse Tomash
    /// Created: 3/30/2020
    /// Approver: Brandyn T. Coverdill
    /// Approver: 
    ///
    /// Code-behind file for Viewing ORders.
    /// </summary>
    public partial class ViewSpecialOrders : Page
    {
        private SpecialOrderItemLineManager _orderItemLineManager;
        private SpecialOrderManager _orderManager;
        private SpecialOrder _order;
        private List<SpecialOrder> _orders;
        private List<SpecialOrder> _currentOrders;

        /// <summary>
        /// NAME: Jesse Tomash
        /// DATE: 3/30/2020
        ///
        /// Approver: Brandyn T. Coverdill
        /// Approver: 
        /// 
        /// Iconstructor  for ViewSpecialOrders.xaml
        /// </summary>
        /// /// <remarks>
        /// UPDATED BY:
        /// UPDATE DATE:
        /// WHAT WAS CHANGED:
        /// </remarks>
        /// <returns></returns>
        public ViewSpecialOrders()
        {
            InitializeComponent();
            _orderManager = new SpecialOrderManager();
            _orderItemLineManager = new SpecialOrderItemLineManager();
            dgSpecialOrders.Visibility = Visibility.Visible;
            btnAddOrder.Visibility = Visibility.Visible;
            btnDeleteOrder.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// NAME: Jesse Tomash
        /// DATE: 3/30/2020
        ///
        /// Approver: Brandyn T. Coverdill
        /// Approver: 
        /// 
        /// Opens Add Order on click
        /// </summary>
        /// /// <remarks>
        /// UPDATED BY:
        /// UPDATE DATE:
        /// WHAT WAS CHANGED:
        /// </remarks>
        /// <returns></returns>
        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new ViewAddSpecialOrder());
        }
        /// <summary>
        /// NAME: Jesse Tomash
        /// DATE: 3/30/2020
        ///
        /// Approver: Brandyn T. Coverdill
        /// Approver: 
        /// 
        /// Formats the DG
        /// </summary>
        /// /// <remarks>
        /// UPDATED BY:
        /// UPDATE DATE:
        /// WHAT WAS CHANGED:
        /// </remarks>
        /// <returns></returns>
        private void dgOrders_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgSpecialOrders.Columns[0].Header = "Special Order ID";
            dgSpecialOrders.Columns[1].Header = "Employee ID";
            dgSpecialOrders.Columns[2].Visibility = Visibility.Hidden;
            foreach (var column in this.dgSpecialOrders.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
        /// <summary>
        /// NAME: Jesse Tomash
        /// DATE: 3/30/2020
        ///
        /// Approver: Brandyn T. Coverdill
        /// Approver: 
        /// 
        /// Updates order list on gridload
        /// </summary>
        /// /// <remarks>
        /// UPDATED BY:
        /// UPDATE DATE:
        /// WHAT WAS CHANGED:
        /// </remarks>
        /// <returns></returns>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgSpecialOrders.ItemsSource = _orderManager.RetrieveSpecialOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// NAME: Jesse Tomash
        /// DATE: 3/30/2020
        ///
        /// Approver: 
        /// Approver: 
        /// 
        /// Helper method that sets up view order
        /// </summary>
        /// /// <remarks>
        /// UPDATED BY:
        /// UPDATE DATE:
        /// WHAT WAS CHANGED:
        /// </remarks>
        /// <returns></returns>
        private void SetUpViewOrder()
        {
            if (dgSpecialOrders.SelectedItem != null)
            {
                try
                {
                    _order = (SpecialOrder)dgSpecialOrders.SelectedItem;
                    this.NavigationService?.Navigate(new ViewAddSpecialOrder(_order));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } else
            {
                "An order must be selected".ErrorMessage();
            }
        }
        /// <summary>
        /// NAME: Jesse Tomash
        /// DATE: 4/15/2020
        ///
        /// Approver: 
        /// Approver: 
        /// 
        /// Action to view order when an item on the datagrid is double clicked
        /// </summary>
        /// /// <remarks>
        /// UPDATED BY: Brandyn T. Coverdill
        /// UPDATE DATE: 4/28/2020
        /// WHAT WAS CHANGED: Made it so that an error message displayed if no item was selected.
        /// </remarks>
        /// <returns></returns>
        private void btnViewOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgSpecialOrders.SelectedItem == null)
            {
                "Please Select an Order.".ErrorMessage();
            }
            else
            {
                SetUpViewOrder();
            }
        }
        /// <summary>
        /// NAME: Jesse Tomash
        /// DATE: 3/30/2020
        ///
        /// Approver: Brandyn T. Coverdill
        /// Approver: 
        /// 
        /// Action to view order when an item on the datagrid is double clicked
        /// </summary>
        /// /// <remarks>
        /// UPDATED BY:
        /// UPDATE DATE:
        /// WHAT WAS CHANGED:
        /// </remarks>
        /// <returns></returns>
        private void dgOrders_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetUpViewOrder();
        }
        
        /// <summary>
        /// Creator: Dalton Reierson
        /// Created: 2020/04/24
        /// Approver: Jesse Tomash
        /// Approver: 
        ///
        /// button that takes you to a detailed view of an order
        /// </summary>
        ///
        /// <remarks>
        /// Updated By: 
        /// Updated: 
        /// Update:
        /// </remarks>
        private void btnOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    _order = (SpecialOrder)dgOrders.SelectedItem;
            //    this.NavigationService?.Navigate(new ViewOrderDetails(_order));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        /// <summary>
        /// NAME: Jesse Tomash
        /// DATE: 3/30/2020
        ///
        /// Approver: Brandyn T. Coverdill
        /// Approver: 
        /// 
        /// Action to delete order when an delete order is clicked
        /// </summary>
        /// /// <remarks>
        /// UPDATED BY: Brandyn T. Coverdill
        /// UPDATE DATE: 4/28/2020
        /// WHAT WAS CHANGED: Changed the error message if no order was selected.
        /// </remarks>
        /// <returns></returns>
        private void btnDeleteOrder_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgSpecialOrders.SelectedItem != null)
            {
                try
                {
                    _order = (SpecialOrder)dgSpecialOrders.SelectedItem;
                    _orderManager.DeleteSpecialOrder(_order.SpecialOrderID);
                    foreach (SpecialOrderItemLine line in _orderItemLineManager.SelectSpecialOrderItemLinesByOrderID(_order.SpecialOrderID))
                    {
                        _orderItemLineManager.DeleteSpecialOrderItemLineByItemID(line.ItemID);
                    }
                    dgSpecialOrders.ItemsSource = _orderManager.RetrieveSpecialOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                "Please select an Order.".ErrorMessage();
            }
        }
    }
}
