/**  智客知识管理平台。
* JOINREQUESTS.cs
*
* 功 能： N/A
* 类 名： JOINREQUESTS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:35   N/A    初版
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
	/// 数据访问类:JOINREQUESTS
	/// </summary>
	public partial class JOINREQUESTS:IJOINREQUESTS
	{
		public JOINREQUESTS()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long SID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from JOINREQUESTS");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt)
			};
			parameters[0].Value = SID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(ZK.Model.JOINREQUESTS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into JOINREQUESTS(");
			strSql.Append("USERID,CONTENT,SENDER,SENDTIME)");
			strSql.Append(" values (");
			strSql.Append("@USERID,@CONTENT,@SENDER,@SENDTIME)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@CONTENT", SqlDbType.NVarChar,255),
					new SqlParameter("@SENDER", SqlDbType.Int,4),
					new SqlParameter("@SENDTIME", SqlDbType.DateTime)};
			parameters[0].Value = model.USERID;
			parameters[1].Value = model.CONTENT;
			parameters[2].Value = model.SENDER;
			parameters[3].Value = model.SENDTIME;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt64(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.JOINREQUESTS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update JOINREQUESTS set ");
			strSql.Append("USERID=@USERID,");
			strSql.Append("CONTENT=@CONTENT,");
			strSql.Append("SENDER=@SENDER,");
			strSql.Append("SENDTIME=@SENDTIME");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@CONTENT", SqlDbType.NVarChar,255),
					new SqlParameter("@SENDER", SqlDbType.Int,4),
					new SqlParameter("@SENDTIME", SqlDbType.DateTime),
					new SqlParameter("@SID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.USERID;
			parameters[1].Value = model.CONTENT;
			parameters[2].Value = model.SENDER;
			parameters[3].Value = model.SENDTIME;
			parameters[4].Value = model.SID;

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
		public bool Delete(long SID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from JOINREQUESTS ");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt)
			};
			parameters[0].Value = SID;

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
		public bool DeleteList(string SIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from JOINREQUESTS ");
			strSql.Append(" where SID in ("+SIDlist + ")  ");
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
		public ZK.Model.JOINREQUESTS GetModel(long SID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SID,USERID,CONTENT,SENDER,SENDTIME from JOINREQUESTS ");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt)
			};
			parameters[0].Value = SID;

			ZK.Model.JOINREQUESTS model=new ZK.Model.JOINREQUESTS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SID"]!=null && ds.Tables[0].Rows[0]["SID"].ToString()!="")
				{
					model.SID=long.Parse(ds.Tables[0].Rows[0]["SID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["USERID"]!=null && ds.Tables[0].Rows[0]["USERID"].ToString()!="")
				{
					model.USERID=int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CONTENT"]!=null && ds.Tables[0].Rows[0]["CONTENT"].ToString()!="")
				{
					model.CONTENT=ds.Tables[0].Rows[0]["CONTENT"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SENDER"]!=null && ds.Tables[0].Rows[0]["SENDER"].ToString()!="")
				{
					model.SENDER=int.Parse(ds.Tables[0].Rows[0]["SENDER"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SENDTIME"]!=null && ds.Tables[0].Rows[0]["SENDTIME"].ToString()!="")
				{
					model.SENDTIME=DateTime.Parse(ds.Tables[0].Rows[0]["SENDTIME"].ToString());
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
			strSql.Append("select SID,USERID,CONTENT,SENDER,SENDTIME ");
			strSql.Append(" FROM JOINREQUESTS ");
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
			strSql.Append(" SID,USERID,CONTENT,SENDER,SENDTIME ");
			strSql.Append(" FROM JOINREQUESTS ");
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
			strSql.Append("select count(1) FROM JOINREQUESTS ");
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
				strSql.Append("order by T.SID desc");
			}
			strSql.Append(")AS Row, T.*  from JOINREQUESTS T ");
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
			parameters[1].Value = "JOINREQUESTS";
			parameters[2].Value = strWhere;
			parameters[3].Value = "SID";
			parameters[4].Value = "SID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

