/**  智客知识管理平台。
* OFFLINEMESSAGES.cs
*
* 功 能： N/A
* 类 名： OFFLINEMESSAGES
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
	/// 数据访问类:OFFLINEMESSAGES
	/// </summary>
	public partial class OFFLINEMESSAGES:IOFFLINEMESSAGES
	{
		public OFFLINEMESSAGES()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("USERID", "OFFLINEMESSAGES"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string MSGID,int USERID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from OFFLINEMESSAGES");
			strSql.Append(" where MSGID=@MSGID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@MSGID", SqlDbType.VarChar,50),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = MSGID;
			parameters[1].Value = USERID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.OFFLINEMESSAGES model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into OFFLINEMESSAGES(");
			strSql.Append("MSGID,USERID,MSGTYPE,MSGLEVEL,CONTENT,FONT,SENDER_USERID,SENDER_NICKNAME,SENDER_ACTUALNAME,SENDTIME)");
			strSql.Append(" values (");
			strSql.Append("@MSGID,@USERID,@MSGTYPE,@MSGLEVEL,@CONTENT,@FONT,@SENDER_USERID,@SENDER_NICKNAME,@SENDER_ACTUALNAME,@SENDTIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@MSGID", SqlDbType.VarChar,50),
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@MSGTYPE", SqlDbType.SmallInt,2),
					new SqlParameter("@MSGLEVEL", SqlDbType.SmallInt,2),
					new SqlParameter("@CONTENT", SqlDbType.Text),
					new SqlParameter("@FONT", SqlDbType.NVarChar,50),
					new SqlParameter("@SENDER_USERID", SqlDbType.Int,4),
					new SqlParameter("@SENDER_NICKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SENDER_ACTUALNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SENDTIME", SqlDbType.DateTime)};
			parameters[0].Value = model.MSGID;
			parameters[1].Value = model.USERID;
			parameters[2].Value = model.MSGTYPE;
			parameters[3].Value = model.MSGLEVEL;
			parameters[4].Value = model.CONTENT;
			parameters[5].Value = model.FONT;
			parameters[6].Value = model.SENDER_USERID;
			parameters[7].Value = model.SENDER_NICKNAME;
			parameters[8].Value = model.SENDER_ACTUALNAME;
			parameters[9].Value = model.SENDTIME;

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
		public bool Update(ZK.Model.OFFLINEMESSAGES model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update OFFLINEMESSAGES set ");
			strSql.Append("MSGTYPE=@MSGTYPE,");
			strSql.Append("MSGLEVEL=@MSGLEVEL,");
			strSql.Append("CONTENT=@CONTENT,");
			strSql.Append("FONT=@FONT,");
			strSql.Append("SENDER_USERID=@SENDER_USERID,");
			strSql.Append("SENDER_NICKNAME=@SENDER_NICKNAME,");
			strSql.Append("SENDER_ACTUALNAME=@SENDER_ACTUALNAME,");
			strSql.Append("SENDTIME=@SENDTIME");
			strSql.Append(" where MSGID=@MSGID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@MSGTYPE", SqlDbType.SmallInt,2),
					new SqlParameter("@MSGLEVEL", SqlDbType.SmallInt,2),
					new SqlParameter("@CONTENT", SqlDbType.Text),
					new SqlParameter("@FONT", SqlDbType.NVarChar,50),
					new SqlParameter("@SENDER_USERID", SqlDbType.Int,4),
					new SqlParameter("@SENDER_NICKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SENDER_ACTUALNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@SENDTIME", SqlDbType.DateTime),
					new SqlParameter("@MSGID", SqlDbType.VarChar,50),
					new SqlParameter("@USERID", SqlDbType.Int,4)};
			parameters[0].Value = model.MSGTYPE;
			parameters[1].Value = model.MSGLEVEL;
			parameters[2].Value = model.CONTENT;
			parameters[3].Value = model.FONT;
			parameters[4].Value = model.SENDER_USERID;
			parameters[5].Value = model.SENDER_NICKNAME;
			parameters[6].Value = model.SENDER_ACTUALNAME;
			parameters[7].Value = model.SENDTIME;
			parameters[8].Value = model.MSGID;
			parameters[9].Value = model.USERID;

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
		public bool Delete(string MSGID,int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from OFFLINEMESSAGES ");
			strSql.Append(" where MSGID=@MSGID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@MSGID", SqlDbType.VarChar,50),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = MSGID;
			parameters[1].Value = USERID;

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
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.OFFLINEMESSAGES GetModel(string MSGID,int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 MSGID,USERID,MSGTYPE,MSGLEVEL,CONTENT,FONT,SENDER_USERID,SENDER_NICKNAME,SENDER_ACTUALNAME,SENDTIME from OFFLINEMESSAGES ");
			strSql.Append(" where MSGID=@MSGID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@MSGID", SqlDbType.VarChar,50),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = MSGID;
			parameters[1].Value = USERID;

			ZK.Model.OFFLINEMESSAGES model=new ZK.Model.OFFLINEMESSAGES();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["MSGID"]!=null && ds.Tables[0].Rows[0]["MSGID"].ToString()!="")
				{
					model.MSGID=ds.Tables[0].Rows[0]["MSGID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["USERID"]!=null && ds.Tables[0].Rows[0]["USERID"].ToString()!="")
				{
					model.USERID=int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MSGTYPE"]!=null && ds.Tables[0].Rows[0]["MSGTYPE"].ToString()!="")
				{
					model.MSGTYPE=int.Parse(ds.Tables[0].Rows[0]["MSGTYPE"].ToString());
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
			strSql.Append("select MSGID,USERID,MSGTYPE,MSGLEVEL,CONTENT,FONT,SENDER_USERID,SENDER_NICKNAME,SENDER_ACTUALNAME,SENDTIME ");
			strSql.Append(" FROM OFFLINEMESSAGES ");
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
			strSql.Append(" MSGID,USERID,MSGTYPE,MSGLEVEL,CONTENT,FONT,SENDER_USERID,SENDER_NICKNAME,SENDER_ACTUALNAME,SENDTIME ");
			strSql.Append(" FROM OFFLINEMESSAGES ");
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
			strSql.Append("select count(1) FROM OFFLINEMESSAGES ");
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
				strSql.Append("order by T.USERID desc");
			}
			strSql.Append(")AS Row, T.*  from OFFLINEMESSAGES T ");
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
			parameters[1].Value = "OFFLINEMESSAGES";
			parameters[2].Value = strWhere;
			parameters[3].Value = "USERID";
			parameters[4].Value = "USERID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

