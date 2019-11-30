using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DotNetCrud.Web.Data.Models;

namespace DotNetCrud.Web.Data.Services.ADO
{
    public class ProductGroupADODataService : ADODataService<ProductGroup>
    {
        public override string GetInsertQuery(ProductGroup obj)
        {
            throw new NotImplementedException();
        }

        protected override void PopulateObjFromDataReader(SqlDataReader dataReader, ProductGroup obj)
        {
            throw new NotImplementedException();
        }

        protected override ProductGroup GetNewObj()
        {
            throw new NotImplementedException();
        }

        public override string GetSelectByIdQuery(int id)
        {
            return String.Format("select * from ProductGroups where Id={0}", id);
        }

        protected override string GetUpdateQuery(ProductGroup obj)
        {
            throw new NotImplementedException();
        }

        protected override string GetSelectAllQuery()
        {
            throw new NotImplementedException();
        }

        protected override string GetDeleteQuery(int objId)
        {
            throw new NotImplementedException();
        }
    }
}
