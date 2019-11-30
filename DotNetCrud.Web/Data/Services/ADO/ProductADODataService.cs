using System;
using System.Data.SqlClient;
using DotNetCrud.Web.Data.Models;

namespace DotNetCrud.Web.Data.Services.ADO
{
    // http://www.dotnet-stuff.com/tutorials/adonet/crud-operations-using-ado-net-and-C-sharp-in-asp-net
    public class ProductADODataService : ADODataService<Product>
    {
        public override string GetInsertQuery(Product obj)
        {
            return String.Format("Insert into Products (Name, Price, Description, Barcode, ProductGroupId) Values('{0}', '{1}', '{2}', '{3}', '{4}');"
                                 + "Select @@Identity", obj.Name, obj.Price, obj.Description, obj.Barcode, obj.ProductGroupId);
        }

        protected override void PopulateObjFromDataReader(SqlDataReader dataReader, Product obj)
        {
            obj.Id = Convert.ToInt32(dataReader["Id"]);
            obj.Name = dataReader["Name"].ToString();
            obj.Price = Convert.ToInt32(dataReader["Price"]);
            obj.Description = dataReader["Description"].ToString();
            obj.Barcode = dataReader["Barcode"].ToString();
            obj.ProductGroupId = Convert.ToInt32(dataReader["ProductGroupId"]);
        }

        protected override Product GetNewObj()
        {
            return new Product();
        }

        public override string GetSelectByIdQuery(int id)
        {
            return String.Format("select * from Products where Id={0}", id);
        }

        protected override string GetUpdateQuery(Product obj)
        {
            return String.Format("Update Products SET Name='{0}', Price = '{1}', Description ='{2}', Barcode = '{3}', ProductGroupId = '{4}, Where Id = {5};",
                obj.Name, obj.Price, obj.Description, obj.Barcode, obj.ProductGroupId, obj.Id);
        }

        protected override string GetSelectAllQuery()
        {
            return String.Format("select * from Products");
        }

        protected override string GetDeleteQuery(int id)
        {
            return String.Format("Delete from Products where Id = {0}", id);
        }
    }
}
