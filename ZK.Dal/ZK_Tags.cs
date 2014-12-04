/**  智客知识管理平台。
* ZK_Tags.cs
*
* 功 能： N/A
* 类 名： ZK_Tags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:49   N/A    初版
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
	/// 数据访问类:ZK_Tags
	/// </summary>
	public partial class ZK_Tags:IZK_Tags
	{
		public ZK_Tags()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("tagID", "ZK_Tags"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int tagID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ZK_Tags");
			strSql.Append(" where tagID=@tagID");
			SqlParameter[] parameters = {
					new SqlParameter("@tagID", SqlDbType.Int,4)
			};
			parameters[0].Value = tagID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZK.Model.ZK_Tags model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ZK_Tags(");
			strSql.Append("tagName,ownerID,relevantNum,relevantNumdy,relevantNumjx,relevantNumxz,createTime)");
			strSql.Append(" values (");
			strSql.Append("@tagName,@ownerID,@relevantNum,@relevantNumdy,@relevantNumjx,@relevantNumxz,@createTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@tagName", SqlDbType.NVarChar,50),
					new SqlParameter("@ownerID", SqlDbType.Int,4),
					new SqlParameter("@relevantNum", SqlDbType.Int,4),
					new SqlParameter("@relevantNumdy", SqlDbType.Int,4),
					new SqlParameter("@relevantNumjx", SqlDbType.Int,4),
					new SqlParameter("@relevantNumxz", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime)};
			parameters[0].Value = model.tagName;
			parameters[1].Value = model.ownerID;
			parameters[2].Value = model.relevantNum;
			parameters[3].Value = model.relevantNumdy;
			parameters[4].Value = model.relevantNumjx;
			parameters[5].Value = model.relevantNumxz;
			parameters[6].Value = model.createTime;

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
		public bool Update(ZK.Model.ZK_Tags model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ZK_Tags set ");
			strSql.Append("tagName=@tagName,");
			strSql.Append("ownerID=@ownerID,");
			strSql.Append("relevantNum=@relevantNum,");
			strSql.Append("relevantNumdy=@relevantNumdy,");
			strSql.Append("relevantNumjx=@relevantNumjx,");
			strSql.Append("relevantNumxz=@relevantNumxz,");
			strSql.Append("createTime=@createTime");
			strSql.Append(" where tagID=@tagID");
			SqlParameter[] parameters = {
					new SqlParameter("@tagName", SqlDbType.NVarChar,50),
					new SqlParameter("@ownerID", SqlDbType.Int,4),
					new SqlParameter("@relevantNum", SqlDbType.Int,4),
					new SqlParameter("@relevantNumdy", SqlDbType.Int,4),
					new SqlParameter("@relevantNumjx", SqlDbType.Int,4),
					new SqlParameter("@relevantNumxz", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@tagID", SqlDbType.Int,4)};
			parameters[0].Value = model.tagName;
			parameters[1].Value = model.ownerID;
			parameters[2].Value = model.relevantNum;
			parameters[3].Value = model.relevantNumdy;
			parameters[4].Value = model.relevantNumjx;
			parameters[5].Value = model.relevantNumxz;
			parameters[6].Value = model.createTime;
			parameters[7].Value = model.tagID;

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
		public bool Delete(int tagID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_Tags ");
			strSql.Append(" where tagID=@tagID");
			SqlParameter[] parameters = {
					new SqlParameter("@tagID", SqlDbType.Int,4)
			};
			parameters[0].Value = tagID;

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
		public bool DeleteList(string tagIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_Tags ");
			strSql.Append(" where tagID in ("+tagIDlist + ")  ");
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
		public ZK.Model.ZK_Tags GetModel(int tagID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 tagID,tagName,ownerID,relevantNum,relevantNumdy,relevantNumjx,relevantNumxz,createTime from ZK_Tags ");
			strSql.Append(" where tagID=@tagID");
			SqlParameter[] parameters = {
					new SqlParameter("@tagID", SqlDbType.Int,4)
			};
			parameters[0].Value = tagID;

			ZK.Model.ZK_Tags model=new ZK.Model.ZK_Tags();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["tagID"]!=null && ds.Tables[0].Rows[0]["tagID"].ToString()!="")
				{
					model.tagID=int.Parse(ds.Tables[0].Rows[0]["tagID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["tagName"]!=null && ds.Tables[0].Rows[0]["tagName"].ToString()!="")
				{
					model.tagName=ds.Tables[0].Rows[0]["tagName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ownerID"]!=null && ds.Tables[0].Rows[0]["ownerID"].ToString()!="")
				{
					model.ownerID=int.Parse(ds.Tables[0].Rows[0]["ownerID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["relevantNum"]!=null && ds.Tables[0].Rows[0]["relevantNum"].ToString()!="")
				{
					model.relevantNum=int.Parse(ds.Tables[0].Rows[0]["relevantNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["relevantNumdy"]!=null && ds.Tables[0].Rows[0]["relevantNumdy"].ToString()!="")
				{
					model.relevantNumdy=int.Parse(ds.Tables[0].Rows[0]["relevantNumdy"].ToString());
				}
				if(ds.Tables[0].Rows[0]["relevantNumjx"]!=null && ds.Tables[0].Rows[0]["relevantNumjx"].ToString()!="")
				{
					model.relevantNumjx=int.Parse(ds.Tables[0].Rows[0]["relevantNumjx"].ToString());
				}
				if(ds.Tables[0].Rows[0]["relevantNumxz"]!=null && ds.Tables[0].Rows[0]["relevantNumxz"].ToString()!="")
				{
					model.relevantNumxz=int.Parse(ds.Tables[0].Rows[0]["relevantNumxz"].ToString());
				}
				if(ds.Tables[0].Rows[0]["createTime"]!=null && ds.Tables[0].Rows[0]["createTime"].ToString()!="")
				{
					model.createTime=DateTime.Parse(ds.Tables[0].Rows[0]["createTime"].ToString());
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
			strSql.Append("select tagID,tagName,ownerID,relevantNum,relevantNumdy,relevantNumjx,relevantNumxz,createTime ");
			strSql.Append(" FROM ZK_Tags ");
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
			strSql.Append(" tagID,tagName,ownerID,relevantNum,relevantNumdy,relevantNumjx,relevantNumxz,createTime ");
			strSql.Append(" FROM ZK_Tags ");
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
			strSql.Append("select count(1) FROM ZK_Tags ");
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
				strSql.Append("order by T.tagID desc");
			}
			strSql.Append(")AS Row, T.*  from ZK_Tags T ");
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
			parameters[1].Value = "ZK_Tags";
			parameters[2].Value = strWhere;
			parameters[3].Value = "tagID";
			parameters[4].Value = "tagID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

