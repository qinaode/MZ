/**  智客知识管理平台。
* ZK_Lesson.cs
*
* 功 能： N/A
* 类 名： ZK_Lesson
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:48   N/A    初版
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
	/// 数据访问类:ZK_Lesson
	/// </summary>
	public partial class ZK_Lesson:IZK_Lesson
	{
		public ZK_Lesson()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("lessonID", "ZK_Lesson"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int lessonID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ZK_Lesson");
			strSql.Append(" where lessonID=@lessonID");
			SqlParameter[] parameters = {
					new SqlParameter("@lessonID", SqlDbType.Int,4)
			};
			parameters[0].Value = lessonID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZK.Model.ZK_Lesson model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ZK_Lesson(");
			strSql.Append("lessonName,lessonDesc,lessonParent,lessonLevel,classID,teachMB,teachND,teachZD)");
			strSql.Append(" values (");
			strSql.Append("@lessonName,@lessonDesc,@lessonParent,@lessonLevel,@classID,@teachMB,@teachND,@teachZD)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@lessonName", SqlDbType.NVarChar,50),
					new SqlParameter("@lessonDesc", SqlDbType.NVarChar,50),
					new SqlParameter("@lessonParent", SqlDbType.Int,4),
					new SqlParameter("@lessonLevel", SqlDbType.Int,4),
					new SqlParameter("@classID", SqlDbType.Int,4),
					new SqlParameter("@teachMB", SqlDbType.NVarChar,500),
					new SqlParameter("@teachND", SqlDbType.NVarChar,500),
					new SqlParameter("@teachZD", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.lessonName;
			parameters[1].Value = model.lessonDesc;
			parameters[2].Value = model.lessonParent;
			parameters[3].Value = model.lessonLevel;
			parameters[4].Value = model.classID;
			parameters[5].Value = model.teachMB;
			parameters[6].Value = model.teachND;
			parameters[7].Value = model.teachZD;

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
		public bool Update(ZK.Model.ZK_Lesson model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ZK_Lesson set ");
			strSql.Append("lessonName=@lessonName,");
			strSql.Append("lessonDesc=@lessonDesc,");
			strSql.Append("lessonParent=@lessonParent,");
			strSql.Append("lessonLevel=@lessonLevel,");
			strSql.Append("classID=@classID,");
			strSql.Append("teachMB=@teachMB,");
			strSql.Append("teachND=@teachND,");
			strSql.Append("teachZD=@teachZD");
			strSql.Append(" where lessonID=@lessonID");
			SqlParameter[] parameters = {
					new SqlParameter("@lessonName", SqlDbType.NVarChar,50),
					new SqlParameter("@lessonDesc", SqlDbType.NVarChar,50),
					new SqlParameter("@lessonParent", SqlDbType.Int,4),
					new SqlParameter("@lessonLevel", SqlDbType.Int,4),
					new SqlParameter("@classID", SqlDbType.Int,4),
					new SqlParameter("@teachMB", SqlDbType.NVarChar,500),
					new SqlParameter("@teachND", SqlDbType.NVarChar,500),
					new SqlParameter("@teachZD", SqlDbType.NVarChar,500),
					new SqlParameter("@lessonID", SqlDbType.Int,4)};
			parameters[0].Value = model.lessonName;
			parameters[1].Value = model.lessonDesc;
			parameters[2].Value = model.lessonParent;
			parameters[3].Value = model.lessonLevel;
			parameters[4].Value = model.classID;
			parameters[5].Value = model.teachMB;
			parameters[6].Value = model.teachND;
			parameters[7].Value = model.teachZD;
			parameters[8].Value = model.lessonID;

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
		public bool Delete(int lessonID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_Lesson ");
			strSql.Append(" where lessonID=@lessonID");
			SqlParameter[] parameters = {
					new SqlParameter("@lessonID", SqlDbType.Int,4)
			};
			parameters[0].Value = lessonID;

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
		public bool DeleteList(string lessonIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_Lesson ");
			strSql.Append(" where lessonID in ("+lessonIDlist + ")  ");
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
		public ZK.Model.ZK_Lesson GetModel(int lessonID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 lessonID,lessonName,lessonDesc,lessonParent,lessonLevel,classID,teachMB,teachND,teachZD from ZK_Lesson ");
			strSql.Append(" where lessonID=@lessonID");
			SqlParameter[] parameters = {
					new SqlParameter("@lessonID", SqlDbType.Int,4)
			};
			parameters[0].Value = lessonID;

			ZK.Model.ZK_Lesson model=new ZK.Model.ZK_Lesson();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["lessonID"]!=null && ds.Tables[0].Rows[0]["lessonID"].ToString()!="")
				{
					model.lessonID=int.Parse(ds.Tables[0].Rows[0]["lessonID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["lessonName"]!=null && ds.Tables[0].Rows[0]["lessonName"].ToString()!="")
				{
					model.lessonName=ds.Tables[0].Rows[0]["lessonName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["lessonDesc"]!=null && ds.Tables[0].Rows[0]["lessonDesc"].ToString()!="")
				{
					model.lessonDesc=ds.Tables[0].Rows[0]["lessonDesc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["lessonParent"]!=null && ds.Tables[0].Rows[0]["lessonParent"].ToString()!="")
				{
					model.lessonParent=int.Parse(ds.Tables[0].Rows[0]["lessonParent"].ToString());
				}
				if(ds.Tables[0].Rows[0]["lessonLevel"]!=null && ds.Tables[0].Rows[0]["lessonLevel"].ToString()!="")
				{
					model.lessonLevel=int.Parse(ds.Tables[0].Rows[0]["lessonLevel"].ToString());
				}
				if(ds.Tables[0].Rows[0]["classID"]!=null && ds.Tables[0].Rows[0]["classID"].ToString()!="")
				{
					model.classID=int.Parse(ds.Tables[0].Rows[0]["classID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["teachMB"]!=null && ds.Tables[0].Rows[0]["teachMB"].ToString()!="")
				{
					model.teachMB=ds.Tables[0].Rows[0]["teachMB"].ToString();
				}
				if(ds.Tables[0].Rows[0]["teachND"]!=null && ds.Tables[0].Rows[0]["teachND"].ToString()!="")
				{
					model.teachND=ds.Tables[0].Rows[0]["teachND"].ToString();
				}
				if(ds.Tables[0].Rows[0]["teachZD"]!=null && ds.Tables[0].Rows[0]["teachZD"].ToString()!="")
				{
					model.teachZD=ds.Tables[0].Rows[0]["teachZD"].ToString();
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
			strSql.Append("select lessonID,lessonName,lessonDesc,lessonParent,lessonLevel,classID,teachMB,teachND,teachZD ");
			strSql.Append(" FROM ZK_Lesson ");
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
			strSql.Append(" lessonID,lessonName,lessonDesc,lessonParent,lessonLevel,classID,teachMB,teachND,teachZD ");
			strSql.Append(" FROM ZK_Lesson ");
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
			strSql.Append("select count(1) FROM ZK_Lesson ");
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
				strSql.Append("order by T.lessonID desc");
			}
			strSql.Append(")AS Row, T.*  from ZK_Lesson T ");
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
			parameters[1].Value = "ZK_Lesson";
			parameters[2].Value = strWhere;
			parameters[3].Value = "lessonID";
			parameters[4].Value = "lessonID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

