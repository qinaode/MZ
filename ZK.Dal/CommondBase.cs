using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZK.IDAL;
using ZK.DBUtility;//Please add references

namespace ZK.Dal
{
    public partial class CommondBase : ICommondBase
    {
        public CommondBase()
        { }
        public DataSet GetList(string strSelect, string strTable, string strPrimaryKey, string strOrderby, int PageSize, int PageIndex, string strWhere, int intBlPage)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@select_list", SqlDbType.VarChar, 1000),
					new SqlParameter("@table_name", SqlDbType.VarChar, 1000),
					new SqlParameter("@where", SqlDbType.VarChar,1000),
					new SqlParameter("@primary_key", SqlDbType.VarChar,200),
					new SqlParameter("@order_by", SqlDbType.VarChar,200),
					new SqlParameter("@page_size", SqlDbType.SmallInt),
					new SqlParameter("@page_index", SqlDbType.Int),
                    new SqlParameter("@bl_page",SqlDbType.Int),
					};
            parameters[0].Value = strSelect;
            parameters[1].Value = strTable;
            parameters[2].Value = strWhere;
            parameters[3].Value = strPrimaryKey;
            parameters[4].Value = strOrderby;
            parameters[5].Value = PageSize;
            parameters[6].Value = PageIndex;
            parameters[7].Value = intBlPage;
            return DbHelperSQL.RunProcedure("Common_PageList", parameters, "ds");
        }
    }
}
