/**  智客知识管理平台。
* ZK_JXCategory.cs
*
* 功 能： N/A
* 类 名： ZK_JXCategory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/3/19 13:37:33   N/A    初版
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
	/// 数据访问类:ZK_JXCategory
	/// </summary>
	public partial class ZK_JXCategory:IZK_JXCategory
	{
		public ZK_JXCategory()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CategoryId", "ZK_JXCategory"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CategoryId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ZK_JXCategory");
			strSql.Append(" where CategoryId=@CategoryId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)			};
			parameters[0].Value = CategoryId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.ZK_JXCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ZK_JXCategory(");
			strSql.Append("CategoryId,CategoryName)");
			strSql.Append(" values (");
			strSql.Append("@CategoryId,@CategoryName)");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CategoryId;
			parameters[1].Value = model.CategoryName;

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
		public bool Update(ZK.Model.ZK_JXCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ZK_JXCategory set ");
			strSql.Append("CategoryName=@CategoryName");
			strSql.Append(" where CategoryId=@CategoryId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,50),
					new SqlParameter("@CategoryId", SqlDbType.Int,4)};
			parameters[0].Value = model.CategoryName;
			parameters[1].Value = model.CategoryId;

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
		public bool Delete(int CategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_JXCategory ");
			strSql.Append(" where CategoryId=@CategoryId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)			};
			parameters[0].Value = CategoryId;

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
		public bool DeleteList(string CategoryIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_JXCategory ");
			strSql.Append(" where CategoryId in ("+CategoryIdlist + ")  ");
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
		public ZK.Model.ZK_JXCategory GetModel(int CategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CategoryId,CategoryName from ZK_JXCategory ");
			strSql.Append(" where CategoryId=@CategoryId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)			};
			parameters[0].Value = CategoryId;

			ZK.Model.ZK_JXCategory model=new ZK.Model.ZK_JXCategory();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CategoryId"]!=null && ds.Tables[0].Rows[0]["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(ds.Tables[0].Rows[0]["CategoryId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CategoryName"]!=null && ds.Tables[0].Rows[0]["CategoryName"].ToString()!="")
				{
					model.CategoryName=ds.Tables[0].Rows[0]["CategoryName"].ToString();
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
			strSql.Append("select CategoryId,CategoryName ");
			strSql.Append(" FROM ZK_JXCategory ");
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
			strSql.Append(" CategoryId,CategoryName ");
			strSql.Append(" FROM ZK_JXCategory ");
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
			strSql.Append("select count(1) FROM ZK_JXCategory ");
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
				strSql.Append("order by T.CategoryId desc");
			}
			strSql.Append(")AS Row, T.*  from ZK_JXCategory T ");
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
			parameters[1].Value = "ZK_JXCategory";
			parameters[2].Value = strWhere;
			parameters[3].Value = "CategoryId";
			parameters[4].Value = "CategoryId desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

