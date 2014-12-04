/**  智客知识管理平台。
* ACCOUNTSECTIONS.cs
*
* 功 能： N/A
* 类 名： ACCOUNTSECTIONS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:33   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZK.IDAL;
using ZK.DBUtility;//Please add references
namespace ZK.Dal
{
	/// <summary>
	/// 数据访问类:ACCOUNTSECTIONS
	/// </summary>
	public partial class ACCOUNTSECTIONS:IACCOUNTSECTIONS
	{
		public ACCOUNTSECTIONS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("BEGINUSERID", "ACCOUNTSECTIONS"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int BEGINUSERID,int ENDUSERID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ACCOUNTSECTIONS");
			strSql.Append(" where BEGINUSERID=@BEGINUSERID and ENDUSERID=@ENDUSERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BEGINUSERID", SqlDbType.Int,4),
					new SqlParameter("@ENDUSERID", SqlDbType.Int,4)			};
			parameters[0].Value = BEGINUSERID;
			parameters[1].Value = ENDUSERID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.ACCOUNTSECTIONS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ACCOUNTSECTIONS(");
			strSql.Append("BEGINUSERID,ENDUSERID,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@BEGINUSERID,@ENDUSERID,@CREATETIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@BEGINUSERID", SqlDbType.Int,4),
					new SqlParameter("@ENDUSERID", SqlDbType.Int,4),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.BEGINUSERID;
			parameters[1].Value = model.ENDUSERID;
			parameters[2].Value = model.CREATETIME;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ACCOUNTSECTIONS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ACCOUNTSECTIONS set ");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where BEGINUSERID=@BEGINUSERID and ENDUSERID=@ENDUSERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@BEGINUSERID", SqlDbType.Int,4),
					new SqlParameter("@ENDUSERID", SqlDbType.Int,4)};
			parameters[0].Value = model.CREATETIME;
			parameters[1].Value = model.BEGINUSERID;
			parameters[2].Value = model.ENDUSERID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int BEGINUSERID,int ENDUSERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ACCOUNTSECTIONS ");
			strSql.Append(" where BEGINUSERID=@BEGINUSERID and ENDUSERID=@ENDUSERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BEGINUSERID", SqlDbType.Int,4),
					new SqlParameter("@ENDUSERID", SqlDbType.Int,4)			};
			parameters[0].Value = BEGINUSERID;
			parameters[1].Value = ENDUSERID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.ACCOUNTSECTIONS GetModel(int BEGINUSERID,int ENDUSERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 BEGINUSERID,ENDUSERID,CREATETIME from ACCOUNTSECTIONS ");
			strSql.Append(" where BEGINUSERID=@BEGINUSERID and ENDUSERID=@ENDUSERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@BEGINUSERID", SqlDbType.Int,4),
					new SqlParameter("@ENDUSERID", SqlDbType.Int,4)			};
			parameters[0].Value = BEGINUSERID;
			parameters[1].Value = ENDUSERID;

			ZK.Model.ACCOUNTSECTIONS model=new ZK.Model.ACCOUNTSECTIONS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["BEGINUSERID"]!=null && ds.Tables[0].Rows[0]["BEGINUSERID"].ToString()!="")
				{
					model.BEGINUSERID=int.Parse(ds.Tables[0].Rows[0]["BEGINUSERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ENDUSERID"]!=null && ds.Tables[0].Rows[0]["ENDUSERID"].ToString()!="")
				{
					model.ENDUSERID=int.Parse(ds.Tables[0].Rows[0]["ENDUSERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CREATETIME"]!=null && ds.Tables[0].Rows[0]["CREATETIME"].ToString()!="")
				{
					model.CREATETIME=DateTime.Parse(ds.Tables[0].Rows[0]["CREATETIME"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BEGINUSERID,ENDUSERID,CREATETIME ");
			strSql.Append(" FROM ACCOUNTSECTIONS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" BEGINUSERID,ENDUSERID,CREATETIME ");
			strSql.Append(" FROM ACCOUNTSECTIONS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM ACCOUNTSECTIONS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ENDUSERID desc");
			}
			strSql.Append(")AS Row, T.*  from ACCOUNTSECTIONS T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@select_list", SqlDbType.VarChar, 1000),
					new SqlParameter("@table_name", SqlDbType.VarChar, 1000),
					new SqlParameter("@where", SqlDbType.VarChar, 1000),
					new SqlParameter("@primary_key", SqlDbType.VarChar, 200),
					new SqlParameter("@order_by", SqlDbType.VarChar, 200),
					new SqlParameter("@page_size", SqlDbType.SmallInt),
					new SqlParameter("@page_index", SqlDbType.Int),
					new SqlParameter("@bl_page", SqlDbType.Int),
					};
			parameters[0].Value = "*";
			parameters[1].Value = "ACCOUNTSECTIONS";
			parameters[2].Value = strWhere;
			parameters[3].Value = "ENDUSERID";
			parameters[4].Value = "ENDUSERID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

