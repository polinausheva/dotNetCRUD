using System.Collections.Generic;
using System.Data.SqlClient;
using DotNetCrud.Web.Data.Models;

namespace DotNetCrud.Web.Data.Services.ADO
{
    public class PurchaseDAODataService : ADODataService<Purchase>
    {
        public override string GetInsertQuery(Purchase obj)
        {
            throw new System.NotImplementedException();
        }

        protected override void PopulateObjFromDataReader(SqlDataReader dataReader, Purchase obj)
        {
            throw new System.NotImplementedException();
        }

        protected override Purchase GetNewObj()
        {
            throw new System.NotImplementedException();
        }

        public override string GetSelectByIdQuery(int id)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetUpdateQuery(Purchase obj)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetSelectAllQuery()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetDeleteQuery(int objId)
        {
            throw new System.NotImplementedException();
        }
    }
}
