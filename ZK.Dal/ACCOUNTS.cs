/**  智客知识管理平台。
* ACCOUNTS.cs
*
* 功 能： N/A
* 类 名： ACCOUNTS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:32   N/A    初版
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
	/// 数据访问类:ACCOUNTS
	/// </summary>
	public partial class ACCOUNTS:IACCOUNTS
	{
		public ACCOUNTS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("USERID", "ACCOUNTS"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int USERID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ACCOUNTS");
			strSql.Append(" where USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = USERID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.ACCOUNTS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ACCOUNTS(");
			strSql.Append("USERID,RANDOMVALUE,REGISTED,REGISTERTIME,REGISTERIPADDR,SECTION,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@USERID,@RANDOMVALUE,@REGISTED,@REGISTERTIME,@REGISTERIPADDR,@SECTION,@CREATETIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@RANDOMVALUE", SqlDbType.Float,8),
					new SqlParameter("@REGISTED", SqlDbType.SmallInt,2),
					new SqlParameter("@REGISTERTIME", SqlDbType.DateTime),
					new SqlParameter("@REGISTERIPADDR", SqlDbType.NVarChar,50),
					new SqlParameter("@SECTION", SqlDbType.NVarChar,50),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.USERID;
			parameters[1].Value = model.RANDOMVALUE;
			parameters[2].Value = model.REGISTED;
			parameters[3].Value = model.REGISTERTIME;
			parameters[4].Value = model.REGISTERIPADDR;
			parameters[5].Value = model.SECTION;
			parameters[6].Value = model.CREATETIME;

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
		public bool Update(ZK.Model.ACCOUNTS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ACCOUNTS set ");
			strSql.Append("RANDOMVALUE=@RANDOMVALUE,");
			strSql.Append("REGISTED=@REGISTED,");
			strSql.Append("REGISTERTIME=@REGISTERTIME,");
			strSql.Append("REGISTERIPADDR=@REGISTERIPADDR,");
			strSql.Append("SECTION=@SECTION,");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@RANDOMVALUE", SqlDbType.Float,8),
					new SqlParameter("@REGISTED", SqlDbType.SmallInt,2),
					new SqlParameter("@REGISTERTIME", SqlDbType.DateTime),
					new SqlParameter("@REGISTERIPADDR", SqlDbType.NVarChar,50),
					new SqlParameter("@SECTION", SqlDbType.NVarChar,50),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@USERID", SqlDbType.Int,4)};
			parameters[0].Value = model.RANDOMVALUE;
			parameters[1].Value = model.REGISTED;
			parameters[2].Value = model.REGISTERTIME;
			parameters[3].Value = model.REGISTERIPADDR;
			parameters[4].Value = model.SECTION;
			parameters[5].Value = model.CREATETIME;
			parameters[6].Value = model.USERID;

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
		public bool Delete(int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ACCOUNTS ");
			strSql.Append(" where USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = USERID;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string USERIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ACCOUNTS ");
			strSql.Append(" where USERID in ("+USERIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public ZK.Model.ACCOUNTS GetModel(int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 USERID,RANDOMVALUE,REGISTED,REGISTERTIME,REGISTERIPADDR,SECTION,CREATETIME from ACCOUNTS ");
			strSql.Append(" where USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = USERID;

			ZK.Model.ACCOUNTS model=new ZK.Model.ACCOUNTS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["USERID"]!=null && ds.Tables[0].Rows[0]["USERID"].ToString()!="")
				{
					model.USERID=int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RANDOMVALUE"]!=null && ds.Tables[0].Rows[0]["RANDOMVALUE"].ToString()!="")
				{
					model.RANDOMVALUE=decimal.Parse(ds.Tables[0].Rows[0]["RANDOMVALUE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["REGISTED"]!=null && ds.Tables[0].Rows[0]["REGISTED"].ToString()!="")
				{
					model.REGISTED=int.Parse(ds.Tables[0].Rows[0]["REGISTED"].ToString());
				}
				if(ds.Tables[0].Rows[0]["REGISTERTIME"]!=null && ds.Tables[0].Rows[0]["REGISTERTIME"].ToString()!="")
				{
					model.REGISTERTIME=DateTime.Parse(ds.Tables[0].Rows[0]["REGISTERTIME"].ToString());
				}
				if(ds.Tables[0].Rows[0]["REGISTERIPADDR"]!=null && ds.Tables[0].Rows[0]["REGISTERIPADDR"].ToString()!="")
				{
					model.REGISTERIPADDR=ds.Tables[0].Rows[0]["REGISTERIPADDR"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SECTION"]!=null && ds.Tables[0].Rows[0]["SECTION"].ToString()!="")
				{
					model.SECTION=ds.Tables[0].Rows[0]["SECTION"].ToString();
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
			strSql.Append("select USERID,RANDOMVALUE,REGISTED,REGISTERTIME,REGISTERIPADDR,SECTION,CREATETIME ");
			strSql.Append(" FROM ACCOUNTS ");
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
			strSql.Append(" USERID,RANDOMVALUE,REGISTED,REGISTERTIME,REGISTERIPADDR,SECTION,CREATETIME ");
			strSql.Append(" FROM ACCOUNTS ");
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
			strSql.Append("select count(1) FROM ACCOUNTS ");
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
				strSql.Append("order by T.USERID desc");
			}
			strSql.Append(")AS Row, T.*  from ACCOUNTS T ");
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
			parameters[1].Value = "ACCOUNTS";
			parameters[2].Value = strWhere;
			parameters[3].Value = "USERID";
			parameters[4].Value = "USERID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

