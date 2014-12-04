/**  智客知识管理平台。
* sysdiagrams.cs
*
* 功 能： N/A
* 类 名： sysdiagrams
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:38   N/A    初版
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
	/// 数据访问类:sysdiagrams
	/// </summary>
	public partial class sysdiagrams:Isysdiagrams
	{
		public sysdiagrams()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("principal_id", "sysdiagrams"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string name,int principal_id,int diagram_id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from sysdiagrams");
			strSql.Append(" where name=@name and principal_id=@principal_id and diagram_id=@diagram_id ");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,128),
					new SqlParameter("@principal_id", SqlDbType.Int,4),
					new SqlParameter("@diagram_id", SqlDbType.Int,4)			};
			parameters[0].Value = name;
			parameters[1].Value = principal_id;
			parameters[2].Value = diagram_id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZK.Model.sysdiagrams model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into sysdiagrams(");
			strSql.Append("name,principal_id,version,definition)");
			strSql.Append(" values (");
			strSql.Append("@name,@principal_id,@version,@definition)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,128),
					new SqlParameter("@principal_id", SqlDbType.Int,4),
					new SqlParameter("@version", SqlDbType.Int,4),
					new SqlParameter("@definition", SqlDbType.VarBinary,-1)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.principal_id;
			parameters[2].Value = model.version;
			parameters[3].Value = model.definition;

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
		public bool Update(ZK.Model.sysdiagrams model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update sysdiagrams set ");
			strSql.Append("version=@version,");
			strSql.Append("definition=@definition");
			strSql.Append(" where diagram_id=@diagram_id");
			SqlParameter[] parameters = {
					new SqlParameter("@version", SqlDbType.Int,4),
					new SqlParameter("@definition", SqlDbType.VarBinary,-1),
					new SqlParameter("@name", SqlDbType.NVarChar,128),
					new SqlParameter("@principal_id", SqlDbType.Int,4),
					new SqlParameter("@diagram_id", SqlDbType.Int,4)};
			parameters[0].Value = model.version;
			parameters[1].Value = model.definition;
			parameters[2].Value = model.name;
			parameters[3].Value = model.principal_id;
			parameters[4].Value = model.diagram_id;

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
		public bool Delete(int diagram_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sysdiagrams ");
			strSql.Append(" where diagram_id=@diagram_id");
			SqlParameter[] parameters = {
					new SqlParameter("@diagram_id", SqlDbType.Int,4)
			};
			parameters[0].Value = diagram_id;

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
		public bool Delete(string name,int principal_id,int diagram_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sysdiagrams ");
			strSql.Append(" where name=@name and principal_id=@principal_id and diagram_id=@diagram_id ");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,128),
					new SqlParameter("@principal_id", SqlDbType.Int,4),
					new SqlParameter("@diagram_id", SqlDbType.Int,4)			};
			parameters[0].Value = name;
			parameters[1].Value = principal_id;
			parameters[2].Value = diagram_id;

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
		public bool DeleteList(string diagram_idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from sysdiagrams ");
			strSql.Append(" where diagram_id in ("+diagram_idlist + ")  ");
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
		public ZK.Model.sysdiagrams GetModel(int diagram_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 name,principal_id,diagram_id,version,definition from sysdiagrams ");
			strSql.Append(" where diagram_id=@diagram_id");
			SqlParameter[] parameters = {
					new SqlParameter("@diagram_id", SqlDbType.Int,4)
			};
			parameters[0].Value = diagram_id;

			ZK.Model.sysdiagrams model=new ZK.Model.sysdiagrams();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["name"]!=null && ds.Tables[0].Rows[0]["name"].ToString()!="")
				{
					model.name=ds.Tables[0].Rows[0]["name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["principal_id"]!=null && ds.Tables[0].Rows[0]["principal_id"].ToString()!="")
				{
					model.principal_id=int.Parse(ds.Tables[0].Rows[0]["principal_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["diagram_id"]!=null && ds.Tables[0].Rows[0]["diagram_id"].ToString()!="")
				{
					model.diagram_id=int.Parse(ds.Tables[0].Rows[0]["diagram_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["version"]!=null && ds.Tables[0].Rows[0]["version"].ToString()!="")
				{
					model.version=int.Parse(ds.Tables[0].Rows[0]["version"].ToString());
				}
				if(ds.Tables[0].Rows[0]["definition"]!=null && ds.Tables[0].Rows[0]["definition"].ToString()!="")
				{
					model.definition=(byte[])ds.Tables[0].Rows[0]["definition"];
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
			strSql.Append("select name,principal_id,diagram_id,version,definition ");
			strSql.Append(" FROM sysdiagrams ");
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
			strSql.Append(" name,principal_id,diagram_id,version,definition ");
			strSql.Append(" FROM sysdiagrams ");
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
			strSql.Append("select count(1) FROM sysdiagrams ");
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
				strSql.Append("order by T.diagram_id desc");
			}
			strSql.Append(")AS Row, T.*  from sysdiagrams T ");
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
			parameters[1].Value = "sysdiagrams";
			parameters[2].Value = strWhere;
			parameters[3].Value = "diagram_id";
			parameters[4].Value = "diagram_id desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

