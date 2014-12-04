/**  智客知识管理平台。
* ZK_ChannelGroup.cs
*
* 功 能： N/A
* 类 名： ZK_ChannelGroup
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:42   N/A    初版
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
	/// 数据访问类:ZK_ChannelGroup
	/// </summary>
	public partial class ZK_ChannelGroup:IZK_ChannelGroup
	{
		public ZK_ChannelGroup()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("channelGroupID", "ZK_ChannelGroup"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int channelGroupID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ZK_ChannelGroup");
			strSql.Append(" where channelGroupID=@channelGroupID");
			SqlParameter[] parameters = {
					new SqlParameter("@channelGroupID", SqlDbType.Int,4)
			};
			parameters[0].Value = channelGroupID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZK.Model.ZK_ChannelGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ZK_ChannelGroup(");
			strSql.Append("channelID,channelGroupName,channelGroupDesc,channelGroupParent,channelGroupLevel)");
			strSql.Append(" values (");
			strSql.Append("@channelID,@channelGroupName,@channelGroupDesc,@channelGroupParent,@channelGroupLevel)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@channelID", SqlDbType.Int,4),
					new SqlParameter("@channelGroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@channelGroupDesc", SqlDbType.NVarChar,50),
					new SqlParameter("@channelGroupParent", SqlDbType.Int,4),
					new SqlParameter("@channelGroupLevel", SqlDbType.Int,4)};
			parameters[0].Value = model.channelID;
			parameters[1].Value = model.channelGroupName;
			parameters[2].Value = model.channelGroupDesc;
			parameters[3].Value = model.channelGroupParent;
			parameters[4].Value = model.channelGroupLevel;

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
		public bool Update(ZK.Model.ZK_ChannelGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ZK_ChannelGroup set ");
			strSql.Append("channelID=@channelID,");
			strSql.Append("channelGroupName=@channelGroupName,");
			strSql.Append("channelGroupDesc=@channelGroupDesc,");
			strSql.Append("channelGroupParent=@channelGroupParent,");
			strSql.Append("channelGroupLevel=@channelGroupLevel");
			strSql.Append(" where channelGroupID=@channelGroupID");
			SqlParameter[] parameters = {
					new SqlParameter("@channelID", SqlDbType.Int,4),
					new SqlParameter("@channelGroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@channelGroupDesc", SqlDbType.NVarChar,50),
					new SqlParameter("@channelGroupParent", SqlDbType.Int,4),
					new SqlParameter("@channelGroupLevel", SqlDbType.Int,4),
					new SqlParameter("@channelGroupID", SqlDbType.Int,4)};
			parameters[0].Value = model.channelID;
			parameters[1].Value = model.channelGroupName;
			parameters[2].Value = model.channelGroupDesc;
			parameters[3].Value = model.channelGroupParent;
			parameters[4].Value = model.channelGroupLevel;
			parameters[5].Value = model.channelGroupID;

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
		public bool Delete(int channelGroupID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_ChannelGroup ");
			strSql.Append(" where channelGroupID=@channelGroupID");
			SqlParameter[] parameters = {
					new SqlParameter("@channelGroupID", SqlDbType.Int,4)
			};
			parameters[0].Value = channelGroupID;

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
		public bool DeleteList(string channelGroupIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_ChannelGroup ");
			strSql.Append(" where channelGroupID in ("+channelGroupIDlist + ")  ");
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
		public ZK.Model.ZK_ChannelGroup GetModel(int channelGroupID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 channelGroupID,channelID,channelGroupName,channelGroupDesc,channelGroupParent,channelGroupLevel from ZK_ChannelGroup ");
			strSql.Append(" where channelGroupID=@channelGroupID");
			SqlParameter[] parameters = {
					new SqlParameter("@channelGroupID", SqlDbType.Int,4)
			};
			parameters[0].Value = channelGroupID;

			ZK.Model.ZK_ChannelGroup model=new ZK.Model.ZK_ChannelGroup();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["channelGroupID"]!=null && ds.Tables[0].Rows[0]["channelGroupID"].ToString()!="")
				{
					model.channelGroupID=int.Parse(ds.Tables[0].Rows[0]["channelGroupID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["channelID"]!=null && ds.Tables[0].Rows[0]["channelID"].ToString()!="")
				{
					model.channelID=int.Parse(ds.Tables[0].Rows[0]["channelID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["channelGroupName"]!=null && ds.Tables[0].Rows[0]["channelGroupName"].ToString()!="")
				{
					model.channelGroupName=ds.Tables[0].Rows[0]["channelGroupName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["channelGroupDesc"]!=null && ds.Tables[0].Rows[0]["channelGroupDesc"].ToString()!="")
				{
					model.channelGroupDesc=ds.Tables[0].Rows[0]["channelGroupDesc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["channelGroupParent"]!=null && ds.Tables[0].Rows[0]["channelGroupParent"].ToString()!="")
				{
					model.channelGroupParent=int.Parse(ds.Tables[0].Rows[0]["channelGroupParent"].ToString());
				}
				if(ds.Tables[0].Rows[0]["channelGroupLevel"]!=null && ds.Tables[0].Rows[0]["channelGroupLevel"].ToString()!="")
				{
					model.channelGroupLevel=int.Parse(ds.Tables[0].Rows[0]["channelGroupLevel"].ToString());
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
			strSql.Append("select channelGroupID,channelID,channelGroupName,channelGroupDesc,channelGroupParent,channelGroupLevel ");
			strSql.Append(" FROM ZK_ChannelGroup ");
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
			strSql.Append(" channelGroupID,channelID,channelGroupName,channelGroupDesc,channelGroupParent,channelGroupLevel ");
			strSql.Append(" FROM ZK_ChannelGroup ");
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
			strSql.Append("select count(1) FROM ZK_ChannelGroup ");
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
				strSql.Append("order by T.channelGroupID desc");
			}
			strSql.Append(")AS Row, T.*  from ZK_ChannelGroup T ");
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
			parameters[1].Value = "ZK_ChannelGroup";
			parameters[2].Value = strWhere;
			parameters[3].Value = "channelGroupID";
			parameters[4].Value = "channelGroupID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

