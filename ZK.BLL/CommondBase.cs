using System;
using System.Data;
using System.Collections.Generic;
using ZK.Common;
using ZK.Model;
using ZK.DALFactory;
using ZK.IDAL;

namespace ZK.BLL
{
    public partial class CommondBase
    {
        private readonly ICommondBase dal = DataAccess.CreateCommondBase();
        public CommondBase()
        { }

        #region  Method

        /// <summary>
        /// 制定sql语句
        /// </summary>
        public DataSet GetList(string strSelect, string strTable, string strPrimaryKey, string strOrderby, int PageSize, int PageIndex, string strWhere, int intBlPage)
        {
            return dal.GetList(strSelect, strTable, strPrimaryKey, strOrderby, PageSize, PageIndex, strWhere, intBlPage);
        }

        #endregion
    }


}
