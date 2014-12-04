/**  智客知识管理平台。
* OFFGROUPMSGS.cs
*
* 功 能： N/A
* 类 名： OFFGROUPMSGS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:36   N/A    初版
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
	/// 数据访问类:OFFGROUPMSGS
	/// </summary>
	public partial class OFFGROUPMSGS:IOFFGROUPMSGS
	{
		public OFFGROUPMSGS()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long SID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from OFFGROUPMSGS");
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
		public long Add(ZK.Model.OFFGROUPMSGS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into OFFGROUPMSGS(");
			strSql.Append("GROUPID,MSGID,MSGLEVEL,CONTENT,FONT,SENDER_USERID,SENDER_NICKNAME,SENDER_ACTUALNAME,SENDTIME)");
			strSql.Append(" values (");
			strSql.Append("@GROUPID,@MSGID,@MSGLEVEL,@CONTENT,@FONT,@SENDER_USERID,@SENDER_NICKNAME,@SENDER_ACTUALNAME,@SENDTIME)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@GROUPID", SqlDbType.Int,4),
					new SqlParameter("@MSGID", SqlDbType.VarChar,50),
					new SqlParameter("@MSGLEVEL", SqlDbType.SmallInt,2),
					new SqlParameter("@CONTENT", SqlDbType.Text),
					new SqlParameter("@FONT", SqlDbType.NVarChar,50),
					new SqlParameter("@SENDER_USERID", SqlDbType.Int,4),
					new SqlParameter("@SENDER_NICKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SENDER_ACTUALNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SENDTIME", SqlDbType.DateTime)};
			parameters[0].Value = model.GROUPID;
			parameters[1].Value = model.MSGID;
			parameters[2].Value = model.MSGLEVEL;
			parameters[3].Value = model.CONTENT;
			parameters[4].Value = model.FONT;
			parameters[5].Value = model.SENDER_USERID;
			parameters[6].Value = model.SENDER_NICKNAME;
			parameters[7].Value = model.SENDER_ACTUALNAME;
			parameters[8].Value = model.SENDTIME;

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
		public bool Update(ZK.Model.OFFGROUPMSGS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update OFFGROUPMSGS set ");
			strSql.Append("GROUPID=@GROUPID,");
			strSql.Append("MSGID=@MSGID,");
			strSql.Append("MSGLEVEL=@MSGLEVEL,");
			strSql.Append("CONTENT=@CONTENT,");
			strSql.Append("FONT=@FONT,");
			strSql.Append("SENDER_USERID=@SENDER_USERID,");
			strSql.Append("SENDER_NICKNAME=@SENDER_NICKNAME,");
			strSql.Append("SENDER_ACTUALNAME=@SENDER_ACTUALNAME,");
			strSql.Append("SENDTIME=@SENDTIME");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@GROUPID", SqlDbType.Int,4),
					new SqlParameter("@MSGID", SqlDbType.VarChar,50),
					new SqlParameter("@MSGLEVEL", SqlDbType.SmallInt,2),
					new SqlParameter("@CONTENT", SqlDbType.Text),
					new SqlParameter("@FONT", SqlDbType.NVarChar,50),
					new SqlParameter("@SENDER_USERID", SqlDbType.Int,4),
					new SqlParameter("@SENDER_NICKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SENDER_ACTUALNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SENDTIME", SqlDbType.DateTime),
					new SqlParameter("@SID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.GROUPID;
			parameters[1].Value = model.MSGID;
			parameters[2].Value = model.MSGLEVEL;
			parameters[3].Value = model.CONTENT;
			parameters[4].Value = model.FONT;
			parameters[5].Value = model.SENDER_USERID;
			parameters[6].Value = model.SENDER_NICKNAME;
			parameters[7].Value = model.SENDER_ACTUALNAME;
			parameters[8].Value = model.SENDTIME;
			parameters[9].Value = model.SID;

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
			strSql.Append("delete from OFFGROUPMSGS ");
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
			strSql.Append("delete from OFFGROUPMSGS ");
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
		public ZK.Model.OFFGROUPMSGS GetModel(long SID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SID,GROUPID,MSGID,MSGLEVEL,CONTENT,FONT,SENDER_USERID,SENDER_NICKNAME,SENDER_ACTUALNAME,SENDTIME from OFFGROUPMSGS ");
			strSql.Append(" where SID=@SID");
			SqlParameter[] parameters = {
					new SqlParameter("@SID", SqlDbType.BigInt)
			};
			parameters[0].Value = SID;

			ZK.Model.OFFGROUPMSGS model=new ZK.Model.OFFGROUPMSGS();
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
				if(ds.Tables[0].Rows[0]["MSGLEVEL"]!=null && ds.Tables[0].Rows[0]["MSGLEVEL"].ToString()!="")
				{
					model.MSGLEVEL=int.Parse(ds.Tables[0].Rows[0]["MSGLEVEL"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CONTENT"]!=null && ds.Tables[0].Rows[0]["CONTENT"].ToString()!="")
				{
					model.CONTENT=ds.Tables[0].Rows[0]["CONTENT"].ToString();
				}
				if(ds.Tables[0].Rows[0]["FONT"]!=null && ds.Tables[0].Rows[0]["FONT"].ToString()!="")
				{
					model.FONT=ds.Tables[0].Rows[0]["FONT"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SENDER_USERID"]!=null && ds.Tables[0].Rows[0]["SENDER_USERID"].ToString()!="")
				{
					model.SENDER_USERID=int.Parse(ds.Tables[0].Rows[0]["SENDER_USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SENDER_NICKNAME"]!=null && ds.Tables[0].Rows[0]["SENDER_NICKNAME"].ToString()!="")
				{
					model.SENDER_NICKNAME=ds.Tables[0].Rows[0]["SENDER_NICKNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SENDER_ACTUALNAME"]!=null && ds.Tables[0].Rows[0]["SENDER_ACTUALNAME"].ToString()!="")
				{
					model.SENDER_ACTUALNAME=ds.Tables[0].Rows[0]["SENDER_ACTUALNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SENDTIME"]!=null && ds.Tables[0].Rows[0]["SENDTIME"].ToString()!="")
				{
					model.SENDTIME=DateTime.Parse(ds.Tables[0].Rows[0]["SENDTIME"].ToString());
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
			strSql.Append("select SID,GROUPID,MSGID,MSGLEVEL,CONTENT,FONT,SENDER_USERID,SENDER_NICKNAME,SENDER_ACTUALNAME,SENDTIME ");
			strSql.Append(" FROM OFFGROUPMSGS ");
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
			strSql.Append(" SID,GROUPID,MSGID,MSGLEVEL,CONTENT,FONT,SENDER_USERID,SENDER_NICKNAME,SENDER_ACTUALNAME,SENDTIME ");
			strSql.Append(" FROM OFFGROUPMSGS ");
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
			strSql.Append("select count(1) FROM OFFGROUPMSGS ");
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
			strSql.Append(")AS Row, T.*  from OFFGROUPMSGS T ");
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
			parameters[1].Value = "OFFGROUPMSGS";
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

