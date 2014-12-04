/**  智客知识管理平台。
* ZK_RoleList.cs
*
* 功 能： N/A
* 类 名： ZK_RoleList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/31 10:47:22   N/A    初版
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
	/// 数据访问类:ZK_RoleList
	/// </summary>
	public partial class ZK_RoleList:IZK_RoleList
	{
		public ZK_RoleList()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("roleID", "ZK_RoleList"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int roleID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ZK_RoleList");
			strSql.Append(" where roleID=@roleID");
			SqlParameter[] parameters = {
					new SqlParameter("@roleID", SqlDbType.Int,4)
			};
			parameters[0].Value = roleID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZK.Model.ZK_RoleList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ZK_RoleList(");
			strSql.Append("roleName,roleDesc,roleASC,roleType)");
			strSql.Append(" values (");
			strSql.Append("@roleName,@roleDesc,@roleASC,@roleType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@roleName", SqlDbType.NVarChar,50),
					new SqlParameter("@roleDesc", SqlDbType.NVarChar,50),
					new SqlParameter("@roleASC", SqlDbType.Int,4),
					new SqlParameter("@roleType", SqlDbType.Int,4)};
			parameters[0].Value = model.roleName;
			parameters[1].Value = model.roleDesc;
			parameters[2].Value = model.roleASC;
			parameters[3].Value = model.roleType;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.ZK_RoleList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ZK_RoleList set ");
			strSql.Append("roleName=@roleName,");
			strSql.Append("roleDesc=@roleDesc,");
			strSql.Append("roleASC=@roleASC,");
			strSql.Append("roleType=@roleType");
			strSql.Append(" where roleID=@roleID");
			SqlParameter[] parameters = {
					new SqlParameter("@roleName", SqlDbType.NVarChar,50),
					new SqlParameter("@roleDesc", SqlDbType.NVarChar,50),
					new SqlParameter("@roleASC", SqlDbType.Int,4),
					new SqlParameter("@roleType", SqlDbType.Int,4),
					new SqlParameter("@roleID", SqlDbType.Int,4)};
			parameters[0].Value = model.roleName;
			parameters[1].Value = model.roleDesc;
			parameters[2].Value = model.roleASC;
			parameters[3].Value = model.roleType;
			parameters[4].Value = model.roleID;

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
		public bool Delete(int roleID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_RoleList ");
			strSql.Append(" where roleID=@roleID");
			SqlParameter[] parameters = {
					new SqlParameter("@roleID", SqlDbType.Int,4)
			};
			parameters[0].Value = roleID;

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
		public bool DeleteList(string roleIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_RoleList ");
			strSql.Append(" where roleID in ("+roleIDlist + ")  ");
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
		public ZK.Model.ZK_RoleList GetModel(int roleID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 roleID,roleName,roleDesc,roleASC,roleType from ZK_RoleList ");
			strSql.Append(" where roleID=@roleID");
			SqlParameter[] parameters = {
					new SqlParameter("@roleID", SqlDbType.Int,4)
			};
			parameters[0].Value = roleID;

			ZK.Model.ZK_RoleList model=new ZK.Model.ZK_RoleList();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["roleID"]!=null && ds.Tables[0].Rows[0]["roleID"].ToString()!="")
				{
					model.roleID=int.Parse(ds.Tables[0].Rows[0]["roleID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["roleName"]!=null && ds.Tables[0].Rows[0]["roleName"].ToString()!="")
				{
					model.roleName=ds.Tables[0].Rows[0]["roleName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["roleDesc"]!=null && ds.Tables[0].Rows[0]["roleDesc"].ToString()!="")
				{
					model.roleDesc=ds.Tables[0].Rows[0]["roleDesc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["roleASC"]!=null && ds.Tables[0].Rows[0]["roleASC"].ToString()!="")
				{
					model.roleASC=int.Parse(ds.Tables[0].Rows[0]["roleASC"].ToString());
				}
				if(ds.Tables[0].Rows[0]["roleType"]!=null && ds.Tables[0].Rows[0]["roleType"].ToString()!="")
				{
					model.roleType=int.Parse(ds.Tables[0].Rows[0]["roleType"].ToString());
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
			strSql.Append("select roleID,roleName,roleDesc,roleASC,roleType ");
			strSql.Append(" FROM ZK_RoleList ");
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
			strSql.Append(" roleID,roleName,roleDesc,roleASC,roleType ");
			strSql.Append(" FROM ZK_RoleList ");
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
			strSql.Append("select count(1) FROM ZK_RoleList ");
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
				strSql.Append("order by T.roleID desc");
			}
			strSql.Append(")AS Row, T.*  from ZK_RoleList T ");
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
			parameters[1].Value = "ZK_RoleList";
			parameters[2].Value = strWhere;
			parameters[3].Value = "roleID";
			parameters[4].Value = "roleID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

