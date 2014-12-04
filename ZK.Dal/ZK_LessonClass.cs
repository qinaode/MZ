/**  智客知识管理平台。
* ZK_LessonClass.cs
*
* 功 能： N/A
* 类 名： ZK_LessonClass
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
	/// 数据访问类:ZK_LessonClass
	/// </summary>
	public partial class ZK_LessonClass:IZK_LessonClass
	{
		public ZK_LessonClass()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("classID", "ZK_LessonClass"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int classID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ZK_LessonClass");
			strSql.Append(" where classID=@classID");
			SqlParameter[] parameters = {
					new SqlParameter("@classID", SqlDbType.Int,4)
			};
			parameters[0].Value = classID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZK.Model.ZK_LessonClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ZK_LessonClass(");
			strSql.Append("courseID,gradeID,editionID)");
			strSql.Append(" values (");
			strSql.Append("@courseID,@gradeID,@editionID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@courseID", SqlDbType.Int,4),
					new SqlParameter("@gradeID", SqlDbType.Int,4),
					new SqlParameter("@editionID", SqlDbType.Int,4)};
			parameters[0].Value = model.courseID;
			parameters[1].Value = model.gradeID;
			parameters[2].Value = model.editionID;

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
		public bool Update(ZK.Model.ZK_LessonClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ZK_LessonClass set ");
			strSql.Append("courseID=@courseID,");
			strSql.Append("gradeID=@gradeID,");
			strSql.Append("editionID=@editionID");
			strSql.Append(" where classID=@classID");
			SqlParameter[] parameters = {
					new SqlParameter("@courseID", SqlDbType.Int,4),
					new SqlParameter("@gradeID", SqlDbType.Int,4),
					new SqlParameter("@editionID", SqlDbType.Int,4),
					new SqlParameter("@classID", SqlDbType.Int,4)};
			parameters[0].Value = model.courseID;
			parameters[1].Value = model.gradeID;
			parameters[2].Value = model.editionID;
			parameters[3].Value = model.classID;

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
		public bool Delete(int classID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_LessonClass ");
			strSql.Append(" where classID=@classID");
			SqlParameter[] parameters = {
					new SqlParameter("@classID", SqlDbType.Int,4)
			};
			parameters[0].Value = classID;

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
		public bool DeleteList(string classIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ZK_LessonClass ");
			strSql.Append(" where classID in ("+classIDlist + ")  ");
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
		public ZK.Model.ZK_LessonClass GetModel(int classID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 classID,courseID,gradeID,editionID from ZK_LessonClass ");
			strSql.Append(" where classID=@classID");
			SqlParameter[] parameters = {
					new SqlParameter("@classID", SqlDbType.Int,4)
			};
			parameters[0].Value = classID;

			ZK.Model.ZK_LessonClass model=new ZK.Model.ZK_LessonClass();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["classID"]!=null && ds.Tables[0].Rows[0]["classID"].ToString()!="")
				{
					model.classID=int.Parse(ds.Tables[0].Rows[0]["classID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["courseID"]!=null && ds.Tables[0].Rows[0]["courseID"].ToString()!="")
				{
					model.courseID=int.Parse(ds.Tables[0].Rows[0]["courseID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["gradeID"]!=null && ds.Tables[0].Rows[0]["gradeID"].ToString()!="")
				{
					model.gradeID=int.Parse(ds.Tables[0].Rows[0]["gradeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["editionID"]!=null && ds.Tables[0].Rows[0]["editionID"].ToString()!="")
				{
					model.editionID=int.Parse(ds.Tables[0].Rows[0]["editionID"].ToString());
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
			strSql.Append("select classID,courseID,gradeID,editionID ");
			strSql.Append(" FROM ZK_LessonClass ");
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
			strSql.Append(" classID,courseID,gradeID,editionID ");
			strSql.Append(" FROM ZK_LessonClass ");
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
			strSql.Append("select count(1) FROM ZK_LessonClass ");
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
				strSql.Append("order by T.classID desc");
			}
			strSql.Append(")AS Row, T.*  from ZK_LessonClass T ");
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
			parameters[1].Value = "ZK_LessonClass";
			parameters[2].Value = strWhere;
			parameters[3].Value = "classID";
			parameters[4].Value = "classID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

