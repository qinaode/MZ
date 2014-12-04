/**  智客知识管理平台。
* OFFLINEFILES.cs
*
* 功 能： N/A
* 类 名： OFFLINEFILES
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:37   N/A    初版
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
	/// 数据访问类:OFFLINEFILES
	/// </summary>
	public partial class OFFLINEFILES:IOFFLINEFILES
	{
		public OFFLINEFILES()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from OFFLINEFILES");
			strSql.Append(" where SID=@SID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.VarChar,50)			};
			parameters[0].Value = SID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.OFFLINEFILES model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into OFFLINEFILES(");
			strSql.Append("SID,SENDER_USERID,RECEIVER_USERID,FILENAMEORDIR,ISDIR,ALLFILECOUNT,ALLFILESIZE,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@SID,@SENDER_USERID,@RECEIVER_USERID,@FILENAMEORDIR,@ISDIR,@ALLFILECOUNT,@ALLFILESIZE,@CREATETIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.VarChar,50),
					new SqlParameter("@SENDER_USERID", SqlDbType.Int,4),
					new SqlParameter("@RECEIVER_USERID", SqlDbType.Int,4),
					new SqlParameter("@FILENAMEORDIR", SqlDbType.NVarChar,255),
					new SqlParameter("@ISDIR", SqlDbType.SmallInt,2),
					new SqlParameter("@ALLFILECOUNT", SqlDbType.Int,4),
					new SqlParameter("@ALLFILESIZE", SqlDbType.BigInt,8),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.SID;
			parameters[1].Value = model.SENDER_USERID;
			parameters[2].Value = model.RECEIVER_USERID;
			parameters[3].Value = model.FILENAMEORDIR;
			parameters[4].Value = model.ISDIR;
			parameters[5].Value = model.ALLFILECOUNT;
			parameters[6].Value = model.ALLFILESIZE;
			parameters[7].Value = model.CREATETIME;

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
		public bool Update(ZK.Model.OFFLINEFILES model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update OFFLINEFILES set ");
			strSql.Append("SENDER_USERID=@SENDER_USERID,");
			strSql.Append("RECEIVER_USERID=@RECEIVER_USERID,");
			strSql.Append("FILENAMEORDIR=@FILENAMEORDIR,");
			strSql.Append("ISDIR=@ISDIR,");
			strSql.Append("ALLFILECOUNT=@ALLFILECOUNT,");
			strSql.Append("ALLFILESIZE=@ALLFILESIZE,");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where SID=@SID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SENDER_USERID", SqlDbType.Int,4),
					new SqlParameter("@RECEIVER_USERID", SqlDbType.Int,4),
					new SqlParameter("@FILENAMEORDIR", SqlDbType.NVarChar,255),
					new SqlParameter("@ISDIR", SqlDbType.SmallInt,2),
					new SqlParameter("@ALLFILECOUNT", SqlDbType.Int,4),
					new SqlParameter("@ALLFILESIZE", SqlDbType.BigInt,8),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@SID", SqlDbType.VarChar,50)};
			parameters[0].Value = model.SENDER_USERID;
			parameters[1].Value = model.RECEIVER_USERID;
			parameters[2].Value = model.FILENAMEORDIR;
			parameters[3].Value = model.ISDIR;
			parameters[4].Value = model.ALLFILECOUNT;
			parameters[5].Value = model.ALLFILESIZE;
			parameters[6].Value = model.CREATETIME;
			parameters[7].Value = model.SID;

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
		public bool Delete(string SID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from OFFLINEFILES ");
			strSql.Append(" where SID=@SID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.VarChar,50)			};
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
			strSql.Append("delete from OFFLINEFILES ");
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
		public ZK.Model.OFFLINEFILES GetModel(string SID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SID,SENDER_USERID,RECEIVER_USERID,FILENAMEORDIR,ISDIR,ALLFILECOUNT,ALLFILESIZE,CREATETIME from OFFLINEFILES ");
			strSql.Append(" where SID=@SID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.VarChar,50)			};
			parameters[0].Value = SID;

			ZK.Model.OFFLINEFILES model=new ZK.Model.OFFLINEFILES();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SID"]!=null && ds.Tables[0].Rows[0]["SID"].ToString()!="")
				{
					model.SID=ds.Tables[0].Rows[0]["SID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SENDER_USERID"]!=null && ds.Tables[0].Rows[0]["SENDER_USERID"].ToString()!="")
				{
					model.SENDER_USERID=int.Parse(ds.Tables[0].Rows[0]["SENDER_USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RECEIVER_USERID"]!=null && ds.Tables[0].Rows[0]["RECEIVER_USERID"].ToString()!="")
				{
					model.RECEIVER_USERID=int.Parse(ds.Tables[0].Rows[0]["RECEIVER_USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FILENAMEORDIR"]!=null && ds.Tables[0].Rows[0]["FILENAMEORDIR"].ToString()!="")
				{
					model.FILENAMEORDIR=ds.Tables[0].Rows[0]["FILENAMEORDIR"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ISDIR"]!=null && ds.Tables[0].Rows[0]["ISDIR"].ToString()!="")
				{
					model.ISDIR=int.Parse(ds.Tables[0].Rows[0]["ISDIR"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ALLFILECOUNT"]!=null && ds.Tables[0].Rows[0]["ALLFILECOUNT"].ToString()!="")
				{
					model.ALLFILECOUNT=int.Parse(ds.Tables[0].Rows[0]["ALLFILECOUNT"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ALLFILESIZE"]!=null && ds.Tables[0].Rows[0]["ALLFILESIZE"].ToString()!="")
				{
					model.ALLFILESIZE=long.Parse(ds.Tables[0].Rows[0]["ALLFILESIZE"].ToString());
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
			strSql.Append("select SID,SENDER_USERID,RECEIVER_USERID,FILENAMEORDIR,ISDIR,ALLFILECOUNT,ALLFILESIZE,CREATETIME ");
			strSql.Append(" FROM OFFLINEFILES ");
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
			strSql.Append(" SID,SENDER_USERID,RECEIVER_USERID,FILENAMEORDIR,ISDIR,ALLFILECOUNT,ALLFILESIZE,CREATETIME ");
			strSql.Append(" FROM OFFLINEFILES ");
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
			strSql.Append("select count(1) FROM OFFLINEFILES ");
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
			strSql.Append(")AS Row, T.*  from OFFLINEFILES T ");
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
			parameters[1].Value = "OFFLINEFILES";
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

