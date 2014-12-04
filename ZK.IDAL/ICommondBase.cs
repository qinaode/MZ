using System;
using System.Data;

namespace ZK.IDAL
{

    public interface ICommondBase
    {
        #region  成员方法
        /// <summary>
        /// 执行sql语句
        /// </summary>
        DataSet GetList(string strSelect, string strTable, string strPrimaryKey, string strOrderby, int PageSize, int PageIndex, string strWhere, int intBlPage);

        #endregion  成员方法
    } 
}
