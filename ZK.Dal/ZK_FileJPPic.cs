/**  智客知识管理平台。
* ZK_FileJPPic.cs
*
* 功 能： N/A
* 类 名： ZK_FileJPPic
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/15 21:49:37   N/A    初版
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
	/// 数据访问类:ZK_FileJPPic
	/// </summary>
	public partial class ZK_FileJPPic:IZK_FileJPPic
	{
		public ZK_FileJPPic()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "ZK_FileJPPic"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ZK_FileJPPic");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZK.Model.ZK_FileJPPic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ZK_FileJPPic(");
			strSql.Append("fileJPTypeID,imageName,imageURL,sortNum)");
			strSql.Append(" values (");
			strSql.Append("@fileJPTypeID,@imageName,@imageURL,@sortNum)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@fileJPTypeID", SqlDbType.Int,4),
					new SqlParameter("@imageName", SqlDbType.NVarChar,50),
					new SqlParameter("@imageURL", SqlDbType.NVarChar,50),
					new SqlParameter("@sortNum", SqlDbType.Int,4)};
			parameters[0].Value = model.fileJPTypeID;
			parameters[1].Value = model.imageName;
			parameters[2].Value = model.imageURL;
			parameters[3].Value = model.sortNum;

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
		public bool Update(ZK.Model.ZK_FileJPPic model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ZK_FileJPPic set ");
			strSql.Append("fileJPTypeID=@fileJPTypeID,");
			strSql.Append("imageName=@imageName,");
			strSql.Append("imageURL=@imageURL,");
			strSql.Append("sortNum=@sortNum");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@fileJPTypeID", SqlDbType.Int,4),
					new SqlParameter("@imageName", SqlDbType.NVarChar,50),
					new SqlParameter("@imageURL", SqlDbType.NVarChar,50),
					new SqlParameter("@sortNum", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.fileJPTypeID;
			parameters[1].Value = model.imageName;
			parameters[2].Value = model.imageURL;
			parameters[3].Value = model.sortNum;
			parameters[4].Value = model.id;

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
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_FileJPPic ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_FileJPPic ");
			strSql.Append(" where id in ("+idlist + ")  ");
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
		public ZK.Model.ZK_FileJPPic GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,fileJPTypeID,imageName,imageURL,sortNum from ZK_FileJPPic ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			ZK.Model.ZK_FileJPPic model=new ZK.Model.ZK_FileJPPic();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["fileJPTypeID"]!=null && ds.Tables[0].Rows[0]["fileJPTypeID"].ToString()!="")
				{
					model.fileJPTypeID=int.Parse(ds.Tables[0].Rows[0]["fileJPTypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["imageName"]!=null && ds.Tables[0].Rows[0]["imageName"].ToString()!="")
				{
					model.imageName=ds.Tables[0].Rows[0]["imageName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["imageURL"]!=null && ds.Tables[0].Rows[0]["imageURL"].ToString()!="")
				{
					model.imageURL=ds.Tables[0].Rows[0]["imageURL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["sortNum"]!=null && ds.Tables[0].Rows[0]["sortNum"].ToString()!="")
				{
					model.sortNum=int.Parse(ds.Tables[0].Rows[0]["sortNum"].ToString());
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
			strSql.Append("select id,fileJPTypeID,imageName,imageURL,sortNum ");
			strSql.Append(" FROM ZK_FileJPPic ");
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
			strSql.Append(" id,fileJPTypeID,imageName,imageURL,sortNum ");
			strSql.Append(" FROM ZK_FileJPPic ");
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
			strSql.Append("select count(1) FROM ZK_FileJPPic ");
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
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from ZK_FileJPPic T ");
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
			parameters[1].Value = "ZK_FileJPPic";
			parameters[2].Value = strWhere;
			parameters[3].Value = "id";
			parameters[4].Value = "id desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

