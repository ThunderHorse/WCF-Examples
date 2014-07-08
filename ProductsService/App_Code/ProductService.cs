using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ProductsEntityModel;

namespace Products
{
    /* WCF service that implements the service contract
     * This implementation performs minimal error checking and
     * exception handling
     */

    public class ProductsServiceImpl : IProductsService
    {
        public List<string> ListProducts()
        {
            // Create a list for holding product numbers
            var productList = new List<string>();

            try
            {
                using (var db = new AdventureWorksEntities())
                {
                    // Fetch product number of every product
                    var products = from product in db.Products
                        select product.ProductNumber;

                    productList = products.ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry(
                    "ProductsService.ProductsServiceImpl.ListProducts"
                    , "Error: " + ex.StackTrace);
            }

            // Return the list of product numbers
            return productList;
        }

        public ProductData GetProduct(string productNumber)
        {
            ProductData productData = null;

            try
            {
                using (var db = new AdventureWorksEntities())
                {
                    Product matchingProduct = db.Products.First(
                        p => String.Compare(p.ProductNumber, productNumber) == 0);

                    productData = new ProductData()
                    {
                        Name = matchingProduct.Name,
                        ProductNumber = matchingProduct.ProductNumber,
                        Color = matchingProduct.Color,
                        ListPrice = matchingProduct.ListPrice
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry(
                    "ProductsService.ProductsServiceImpl.GetProduct"
                    , "Error: " + ex.StackTrace);
            }

            return productData;
        }

        public int CurrentStockLevel(string productNumber)
        {
            int stockLevel = 0;

            try
            {
                using (var db = new AdventureWorksEntities())
                {
                    /* TODO: Update stockLevel query 
                     * TODO: Remove hard coded value
                     * Current copy of AdventureWorks
                     * does not include tables referenced in query.
                     * 
                     * stockLevel is currently hard coded to 5.
                     */     

                    //stockLevel = (from pi in db.ProductInventories
                                    //join p in db.Products
                                    //on pi.ProductID equals p.ProductID
                                    //where String.Compare(p.ProductNumber, productNumber)
                                    //select (int) pi.Quantity).Sum();

                    stockLevel = 5;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry(
                    "ProductsService.ProductsServiceImpl.CurrentStockLevel"
                    , "Error: " + ex.StackTrace);
            }

            return stockLevel;
        }

        public bool ChangeStockLevel(string productNumber, short newStockLevel, string shelf, int bin)
        {
            try
            {

                /* TODO: Update queries to reflect actual db tables
                 * the tables referenced below are not available in the
                 * AdventureWorks db. You may need to come up with other methods that
                 * perform similar operations on the db
                 */
                //using (var db = new AdventureWorksEntities())
                //{
                //    //Find the ProductID for the specified product
                //    int productID = (from p in db.Products
                //        where String.Compare(p.ProductNumber, productNumber) == 0
                //        select p.ProductID).First();

                //    //Find the ProductInventory object
                //    ProductInventory productInventory = db.ProductInventories.First(
                //        pi => String.Compare(pi.Shelf, shelf) == 0 &&
                //              pi.Bin == bin &&
                //              pi.ProductID == productID);

                //    //Update the stock level for the ProductInventory object
                //    productInventory.Quantity = + newStockLevel;

                //    db.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry(
                    "ProductsService.ProductsServiceImpl.ChangeStockLevel"
                    , "Error: " + ex.StackTrace);
            }

            return false;
        }
    }


}
