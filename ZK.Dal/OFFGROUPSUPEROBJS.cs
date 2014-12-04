/**  智客知识管理平台。
* OFFGROUPSUPEROBJS.cs
*
* 功 能： N/A
* 类 名： OFFGROUPSUPEROBJS
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
	/// 数据访问类:OFFGROUPSUPEROBJS
	/// </summary>
	public partial class OFFGROUPSUPEROBJS:IOFFGROUPSUPEROBJS
	{
		public OFFGROUPSUPEROBJS()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long SID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from OFFGROUPSUPEROBJS");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt)
			};
			parameters[0].Value = SID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(ZK.Model.OFFGROUPSUPEROBJS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into OFFGROUPSUPEROBJS(");
			strSql.Append("GROUPID,MSGID,FROMUSERID,SUPEROBJCODE,LOCALFILENAME,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@GROUPID,@MSGID,@FROMUSERID,@SUPEROBJCODE,@LOCALFILENAME,@CREATETIME)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@GROUPID", SqlDbType.Int,4),
					new SqlParameter("@MSGID", SqlDbType.VarChar,50),
					new SqlParameter("@FROMUSERID", SqlDbType.Int,4),
					new SqlParameter("@SUPEROBJCODE", SqlDbType.NVarChar,1024),
					new SqlParameter("@LOCALFILENAME", SqlDbType.NVarChar,1024),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.GROUPID;
			parameters[1].Value = model.MSGID;
			parameters[2].Value = model.FROMUSERID;
			parameters[3].Value = model.SUPEROBJCODE;
			parameters[4].Value = model.LOCALFILENAME;
			parameters[5].Value = model.CREATETIME;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt64(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.OFFGROUPSUPEROBJS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update OFFGROUPSUPEROBJS set ");
			strSql.Append("GROUPID=@GROUPID,");
			strSql.Append("MSGID=@MSGID,");
			strSql.Append("FROMUSERID=@FROMUSERID,");
			strSql.Append("SUPEROBJCODE=@SUPEROBJCODE,");
			strSql.Append("LOCALFILENAME=@LOCALFILENAME,");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@GROUPID", SqlDbType.Int,4),
					new SqlParameter("@MSGID", SqlDbType.VarChar,50),
					new SqlParameter("@FROMUSERID", SqlDbType.Int,4),
					new SqlParameter("@SUPEROBJCODE", SqlDbType.NVarChar,1024),
					new SqlParameter("@LOCALFILENAME", SqlDbType.NVarChar,1024),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@SID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.GROUPID;
			parameters[1].Value = model.MSGID;
			parameters[2].Value = model.FROMUSERID;
			parameters[3].Value = model.SUPEROBJCODE;
			parameters[4].Value = model.LOCALFILENAME;
			parameters[5].Value = model.CREATETIME;
			parameters[6].Value = model.SID;

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
		public bool Delete(long SID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from OFFGROUPSUPEROBJS ");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt)
			};
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
			strSql.Append("delete from OFFGROUPSUPEROBJS ");
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
		public ZK.Model.OFFGROUPSUPEROBJS GetModel(long SID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SID,GROUPID,MSGID,FROMUSERID,SUPEROBJCODE,LOCALFILENAME,CREATETIME from OFFGROUPSUPEROBJS ");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt)
			};
			parameters[0].Value = SID;

			ZK.Model.OFFGROUPSUPEROBJS model=new ZK.Model.OFFGROUPSUPEROBJS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SID"]!=null && ds.Tables[0].Rows[0]["SID"].ToString()!="")
				{
					model.SID=long.Parse(ds.Tables[0].Rows[0]["SID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GROUPID"]!=null && ds.Tables[0].Rows[0]["GROUPID"].ToString()!="")
				{
					model.GROUPID=int.Parse(ds.Tables[0].Rows[0]["GROUPID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MSGID"]!=null && ds.Tables[0].Rows[0]["MSGID"].ToString()!="")
				{
					model.MSGID=ds.Tables[0].Rows[0]["MSGID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["FROMUSERID"]!=null && ds.Tables[0].Rows[0]["FROMUSERID"].ToString()!="")
				{
					model.FROMUSERID=int.Parse(ds.Tables[0].Rows[0]["FROMUSERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SUPEROBJCODE"]!=null && ds.Tables[0].Rows[0]["SUPEROBJCODE"].ToString()!="")
				{
					model.SUPEROBJCODE=ds.Tables[0].Rows[0]["SUPEROBJCODE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LOCALFILENAME"]!=null && ds.Tables[0].Rows[0]["LOCALFILENAME"].ToString()!="")
				{
					model.LOCALFILENAME=ds.Tables[0].Rows[0]["LOCALFILENAME"].ToString();
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
			strSql.Append("select SID,GROUPID,MSGID,FROMUSERID,SUPEROBJCODE,LOCALFILENAME,CREATETIME ");
			strSql.Append(" FROM OFFGROUPSUPEROBJS ");
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
			strSql.Append(" SID,GROUPID,MSGID,FROMUSERID,SUPEROBJCODE,LOCALFILENAME,CREATETIME ");
			strSql.Append(" FROM OFFGROUPSUPEROBJS ");
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
			strSql.Append("select count(1) FROM OFFGROUPSUPEROBJS ");
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
			strSql.Append(")AS Row, T.*  from OFFGROUPSUPEROBJS T ");
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
			parameters[1].Value = "OFFGROUPSUPEROBJS";
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

