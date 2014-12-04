/**  智客知识管理平台。
* ADMINS.cs
*
* 功 能： N/A
* 类 名： ADMINS
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
	/// 数据访问类:ADMINS
	/// </summary>
	public partial class ADMINS:IADMINS
	{
		public ADMINS()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ADMINNAME)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ADMINS");
			strSql.Append(" where ADMINNAME=@ADMINNAME ");
			SqlParameter[] parameters = {
					new SqlParameter("@ADMINNAME", SqlDbType.NVarChar,20)			};
			parameters[0].Value = ADMINNAME;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.ADMINS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ADMINS(");
			strSql.Append("ADMINNAME,ADMINPWD,DESCRIPTION,LOGINED,ISLOCK,LASTLOGINTIME,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@ADMINNAME,@ADMINPWD,@DESCRIPTION,@LOGINED,@ISLOCK,@LASTLOGINTIME,@CREATETIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@ADMINNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@ADMINPWD", SqlDbType.NVarChar,255),
					new SqlParameter("@DESCRIPTION", SqlDbType.NVarChar,255),
					new SqlParameter("@LOGINED", SqlDbType.SmallInt,2),
					new SqlParameter("@ISLOCK", SqlDbType.SmallInt,2),
					new SqlParameter("@LASTLOGINTIME", SqlDbType.DateTime),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.ADMINNAME;
			parameters[1].Value = model.ADMINPWD;
			parameters[2].Value = model.DESCRIPTION;
			parameters[3].Value = model.LOGINED;
			parameters[4].Value = model.ISLOCK;
			parameters[5].Value = model.LASTLOGINTIME;
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
		public bool Update(ZK.Model.ADMINS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ADMINS set ");
			strSql.Append("ADMINPWD=@ADMINPWD,");
			strSql.Append("DESCRIPTION=@DESCRIPTION,");
			strSql.Append("LOGINED=@LOGINED,");
			strSql.Append("ISLOCK=@ISLOCK,");
			strSql.Append("LASTLOGINTIME=@LASTLOGINTIME,");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where ADMINNAME=@ADMINNAME ");
			SqlParameter[] parameters = {
					new SqlParameter("@ADMINPWD", SqlDbType.NVarChar,255),
					new SqlParameter("@DESCRIPTION", SqlDbType.NVarChar,255),
					new SqlParameter("@LOGINED", SqlDbType.SmallInt,2),
					new SqlParameter("@ISLOCK", SqlDbType.SmallInt,2),
					new SqlParameter("@LASTLOGINTIME", SqlDbType.DateTime),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@ADMINNAME", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.ADMINPWD;
			parameters[1].Value = model.DESCRIPTION;
			parameters[2].Value = model.LOGINED;
			parameters[3].Value = model.ISLOCK;
			parameters[4].Value = model.LASTLOGINTIME;
			parameters[5].Value = model.CREATETIME;
			parameters[6].Value = model.ADMINNAME;

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
		public bool Delete(string ADMINNAME)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ADMINS ");
			strSql.Append(" where ADMINNAME=@ADMINNAME ");
			SqlParameter[] parameters = {
					new SqlParameter("@ADMINNAME", SqlDbType.NVarChar,20)			};
			parameters[0].Value = ADMINNAME;

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
		public bool DeleteList(string ADMINNAMElist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ADMINS ");
			strSql.Append(" where ADMINNAME in ("+ADMINNAMElist + ")  ");
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
		public ZK.Model.ADMINS GetModel(string ADMINNAME)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ADMINNAME,ADMINPWD,DESCRIPTION,LOGINED,ISLOCK,LASTLOGINTIME,CREATETIME from ADMINS ");
			strSql.Append(" where ADMINNAME=@ADMINNAME ");
			SqlParameter[] parameters = {
					new SqlParameter("@ADMINNAME", SqlDbType.NVarChar,20)			};
			parameters[0].Value = ADMINNAME;

			ZK.Model.ADMINS model=new ZK.Model.ADMINS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ADMINNAME"]!=null && ds.Tables[0].Rows[0]["ADMINNAME"].ToString()!="")
				{
					model.ADMINNAME=ds.Tables[0].Rows[0]["ADMINNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ADMINPWD"]!=null && ds.Tables[0].Rows[0]["ADMINPWD"].ToString()!="")
				{
					model.ADMINPWD=ds.Tables[0].Rows[0]["ADMINPWD"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DESCRIPTION"]!=null && ds.Tables[0].Rows[0]["DESCRIPTION"].ToString()!="")
				{
					model.DESCRIPTION=ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LOGINED"]!=null && ds.Tables[0].Rows[0]["LOGINED"].ToString()!="")
				{
					model.LOGINED=int.Parse(ds.Tables[0].Rows[0]["LOGINED"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ISLOCK"]!=null && ds.Tables[0].Rows[0]["ISLOCK"].ToString()!="")
				{
					model.ISLOCK=int.Parse(ds.Tables[0].Rows[0]["ISLOCK"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LASTLOGINTIME"]!=null && ds.Tables[0].Rows[0]["LASTLOGINTIME"].ToString()!="")
				{
					model.LASTLOGINTIME=DateTime.Parse(ds.Tables[0].Rows[0]["LASTLOGINTIME"].ToString());
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
			strSql.Append("select ADMINNAME,ADMINPWD,DESCRIPTION,LOGINED,ISLOCK,LASTLOGINTIME,CREATETIME ");
			strSql.Append(" FROM ADMINS ");
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
			strSql.Append(" ADMINNAME,ADMINPWD,DESCRIPTION,LOGINED,ISLOCK,LASTLOGINTIME,CREATETIME ");
			strSql.Append(" FROM ADMINS ");
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
			strSql.Append("select count(1) FROM ADMINS ");
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
				strSql.Append("order by T.ADMINNAME desc");
			}
			strSql.Append(")AS Row, T.*  from ADMINS T ");
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
			parameters[1].Value = "ADMINS";
			parameters[2].Value = strWhere;
			parameters[3].Value = "ADMINNAME";
			parameters[4].Value = "ADMINNAME desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

