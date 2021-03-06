using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Creator: Jesse Tomash
    /// Created: 4/27/2020
    /// Approver: Brandyn T. Coverdill
    /// 
    /// This is the interface for order accessor
    /// </summary>
    public interface ISpecialOrderItemLineAccessor
    {
        /// <summary>
        /// Creator: Jesse Tomash
        /// Created: 4/26/2020
        /// Approver: Brandyn T. Coverdill
        /// 
        /// This is the interface method to insert an order item line
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Update:
        /// Updated:
        /// </remarks>
        /// <param name="newLine"></param>
        /// <returns></returns>
        bool InsertSpecialOrderItemLine(SpecialOrderItemLine newLine);

        /// <summary>
        /// Creator: Jesse Tomash
        /// Created: 4/26/2020
        /// Approver:  Brandyn T. Coverdill
        /// 
        /// This is the interface method to select orderitem lines
        /// </summary>
        /// <remarks>
        /// Updater: 
        /// Update:
        /// Updated:
        /// </remarks>
        /// <param name="newLine"></param>
        /// <returns></returns>
        IEnumerable<SpecialOrderItemLine> SelectSpecialOrderItemLinesByOrderID(int orderID);

        /// <summary>
        /// Creator: Jesse Tomash
        /// Created: 4/26/2020
        /// Approver: Brandyn T. Coverdill
        /// 
        /// This is the interface method to delete an orderline
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Update:
        /// Updated:
        /// </remarks>
        /// <param name="itemID"></param>
        /// <returns></returns>
        bool DeleteSpecialOrderItemLineByItemID(int itemID);
    }
}
