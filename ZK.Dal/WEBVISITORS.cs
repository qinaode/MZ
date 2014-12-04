/**  智客知识管理平台。
* WEBVISITORS.cs
*
* 功 能： N/A
* 类 名： WEBVISITORS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:41   N/A    初版
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
	/// 数据访问类:WEBVISITORS
	/// </summary>
	public partial class WEBVISITORS:IWEBVISITORS
	{
		public WEBVISITORS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("VISITORID", "WEBVISITORS"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int VISITORID,int USERID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from WEBVISITORS");
			strSql.Append(" where VISITORID=@VISITORID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@VISITORID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = VISITORID;
			parameters[1].Value = USERID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.WEBVISITORS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into WEBVISITORS(");
			strSql.Append("VISITORID,VISITORCODE,USERID,SRCURL,CLIENTIPADDR,CLIENTLOCATION,CLIENTOS,WEBBROWSER,REMARKNAME,REMARKTEXT,FLAG,ISACTIVE,LOGINTIMES,LASTLOGINTIME,SENDMSGS,RECVMSGS,LEAVEMSGS,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@VISITORID,@VISITORCODE,@USERID,@SRCURL,@CLIENTIPADDR,@CLIENTLOCATION,@CLIENTOS,@WEBBROWSER,@REMARKNAME,@REMARKTEXT,@FLAG,@ISACTIVE,@LOGINTIMES,@LASTLOGINTIME,@SENDMSGS,@RECVMSGS,@LEAVEMSGS,@CREATETIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@VISITORID", SqlDbType.Int,4),
					new SqlParameter("@VISITORCODE", SqlDbType.NVarChar,50),
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@SRCURL", SqlDbType.NVarChar,1024),
					new SqlParameter("@CLIENTIPADDR", SqlDbType.NVarChar,50),
					new SqlParameter("@CLIENTLOCATION", SqlDbType.NVarChar,50),
					new SqlParameter("@CLIENTOS", SqlDbType.NVarChar,50),
					new SqlParameter("@WEBBROWSER", SqlDbType.NVarChar,50),
					new SqlParameter("@REMARKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@REMARKTEXT", SqlDbType.NVarChar,1024),
					new SqlParameter("@FLAG", SqlDbType.SmallInt,2),
					new SqlParameter("@ISACTIVE", SqlDbType.SmallInt,2),
					new SqlParameter("@LOGINTIMES", SqlDbType.Int,4),
					new SqlParameter("@LASTLOGINTIME", SqlDbType.DateTime),
					new SqlParameter("@SENDMSGS", SqlDbType.Int,4),
					new SqlParameter("@RECVMSGS", SqlDbType.Int,4),
					new SqlParameter("@LEAVEMSGS", SqlDbType.Int,4),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.VISITORID;
			parameters[1].Value = model.VISITORCODE;
			parameters[2].Value = model.USERID;
			parameters[3].Value = model.SRCURL;
			parameters[4].Value = model.CLIENTIPADDR;
			parameters[5].Value = model.CLIENTLOCATION;
			parameters[6].Value = model.CLIENTOS;
			parameters[7].Value = model.WEBBROWSER;
			parameters[8].Value = model.REMARKNAME;
			parameters[9].Value = model.REMARKTEXT;
			parameters[10].Value = model.FLAG;
			parameters[11].Value = model.ISACTIVE;
			parameters[12].Value = model.LOGINTIMES;
			parameters[13].Value = model.LASTLOGINTIME;
			parameters[14].Value = model.SENDMSGS;
			parameters[15].Value = model.RECVMSGS;
			parameters[16].Value = model.LEAVEMSGS;
			parameters[17].Value = model.CREATETIME;

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
		public bool Update(ZK.Model.WEBVISITORS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WEBVISITORS set ");
			strSql.Append("VISITORCODE=@VISITORCODE,");
			strSql.Append("SRCURL=@SRCURL,");
			strSql.Append("CLIENTIPADDR=@CLIENTIPADDR,");
			strSql.Append("CLIENTLOCATION=@CLIENTLOCATION,");
			strSql.Append("CLIENTOS=@CLIENTOS,");
			strSql.Append("WEBBROWSER=@WEBBROWSER,");
			strSql.Append("REMARKNAME=@REMARKNAME,");
			strSql.Append("REMARKTEXT=@REMARKTEXT,");
			strSql.Append("FLAG=@FLAG,");
			strSql.Append("ISACTIVE=@ISACTIVE,");
			strSql.Append("LOGINTIMES=@LOGINTIMES,");
			strSql.Append("LASTLOGINTIME=@LASTLOGINTIME,");
			strSql.Append("SENDMSGS=@SENDMSGS,");
			strSql.Append("RECVMSGS=@RECVMSGS,");
			strSql.Append("LEAVEMSGS=@LEAVEMSGS,");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where VISITORID=@VISITORID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@VISITORCODE", SqlDbType.NVarChar,50),
					new SqlParameter("@SRCURL", SqlDbType.NVarChar,1024),
					new SqlParameter("@CLIENTIPADDR", SqlDbType.NVarChar,50),
					new SqlParameter("@CLIENTLOCATION", SqlDbType.NVarChar,50),
					new SqlParameter("@CLIENTOS", SqlDbType.NVarChar,50),
					new SqlParameter("@WEBBROWSER", SqlDbType.NVarChar,50),
					new SqlParameter("@REMARKNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@REMARKTEXT", SqlDbType.NVarChar,1024),
					new SqlParameter("@FLAG", SqlDbType.SmallInt,2),
					new SqlParameter("@ISACTIVE", SqlDbType.SmallInt,2),
					new SqlParameter("@LOGINTIMES", SqlDbType.Int,4),
					new SqlParameter("@LASTLOGINTIME", SqlDbType.DateTime),
					new SqlParameter("@SENDMSGS", SqlDbType.Int,4),
					new SqlParameter("@RECVMSGS", SqlDbType.Int,4),
					new SqlParameter("@LEAVEMSGS", SqlDbType.Int,4),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@VISITORID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)};
			parameters[0].Value = model.VISITORCODE;
			parameters[1].Value = model.SRCURL;
			parameters[2].Value = model.CLIENTIPADDR;
			parameters[3].Value = model.CLIENTLOCATION;
			parameters[4].Value = model.CLIENTOS;
			parameters[5].Value = model.WEBBROWSER;
			parameters[6].Value = model.REMARKNAME;
			parameters[7].Value = model.REMARKTEXT;
			parameters[8].Value = model.FLAG;
			parameters[9].Value = model.ISACTIVE;
			parameters[10].Value = model.LOGINTIMES;
			parameters[11].Value = model.LASTLOGINTIME;
			parameters[12].Value = model.SENDMSGS;
			parameters[13].Value = model.RECVMSGS;
			parameters[14].Value = model.LEAVEMSGS;
			parameters[15].Value = model.CREATETIME;
			parameters[16].Value = model.VISITORID;
			parameters[17].Value = model.USERID;

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
		public bool Delete(int VISITORID,int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WEBVISITORS ");
			strSql.Append(" where VISITORID=@VISITORID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@VISITORID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = VISITORID;
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
		public ZK.Model.WEBVISITORS GetModel(int VISITORID,int USERID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 VISITORID,VISITORCODE,USERID,SRCURL,CLIENTIPADDR,CLIENTLOCATION,CLIENTOS,WEBBROWSER,REMARKNAME,REMARKTEXT,FLAG,ISACTIVE,LOGINTIMES,LASTLOGINTIME,SENDMSGS,RECVMSGS,LEAVEMSGS,CREATETIME from WEBVISITORS ");
			strSql.Append(" where VISITORID=@VISITORID and USERID=@USERID ");
			SqlParameter[] parameters = {
					new SqlParameter("@VISITORID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4)			};
			parameters[0].Value = VISITORID;
			parameters[1].Value = USERID;

			ZK.Model.WEBVISITORS model=new ZK.Model.WEBVISITORS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["VISITORID"]!=null && ds.Tables[0].Rows[0]["VISITORID"].ToString()!="")
				{
					model.VISITORID=int.Parse(ds.Tables[0].Rows[0]["VISITORID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["VISITORCODE"]!=null && ds.Tables[0].Rows[0]["VISITORCODE"].ToString()!="")
				{
					model.VISITORCODE=ds.Tables[0].Rows[0]["VISITORCODE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["USERID"]!=null && ds.Tables[0].Rows[0]["USERID"].ToString()!="")
				{
					model.USERID=int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SRCURL"]!=null && ds.Tables[0].Rows[0]["SRCURL"].ToString()!="")
				{
					model.SRCURL=ds.Tables[0].Rows[0]["SRCURL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CLIENTIPADDR"]!=null && ds.Tables[0].Rows[0]["CLIENTIPADDR"].ToString()!="")
				{
					model.CLIENTIPADDR=ds.Tables[0].Rows[0]["CLIENTIPADDR"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CLIENTLOCATION"]!=null && ds.Tables[0].Rows[0]["CLIENTLOCATION"].ToString()!="")
				{
					model.CLIENTLOCATION=ds.Tables[0].Rows[0]["CLIENTLOCATION"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CLIENTOS"]!=null && ds.Tables[0].Rows[0]["CLIENTOS"].ToString()!="")
				{
					model.CLIENTOS=ds.Tables[0].Rows[0]["CLIENTOS"].ToString();
				}
				if(ds.Tables[0].Rows[0]["WEBBROWSER"]!=null && ds.Tables[0].Rows[0]["WEBBROWSER"].ToString()!="")
				{
					model.WEBBROWSER=ds.Tables[0].Rows[0]["WEBBROWSER"].ToString();
				}
				if(ds.Tables[0].Rows[0]["REMARKNAME"]!=null && ds.Tables[0].Rows[0]["REMARKNAME"].ToString()!="")
				{
					model.REMARKNAME=ds.Tables[0].Rows[0]["REMARKNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["REMARKTEXT"]!=null && ds.Tables[0].Rows[0]["REMARKTEXT"].ToString()!="")
				{
					model.REMARKTEXT=ds.Tables[0].Rows[0]["REMARKTEXT"].ToString();
				}
				if(ds.Tables[0].Rows[0]["FLAG"]!=null && ds.Tables[0].Rows[0]["FLAG"].ToString()!="")
				{
					model.FLAG=int.Parse(ds.Tables[0].Rows[0]["FLAG"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ISACTIVE"]!=null && ds.Tables[0].Rows[0]["ISACTIVE"].ToString()!="")
				{
					model.ISACTIVE=int.Parse(ds.Tables[0].Rows[0]["ISACTIVE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LOGINTIMES"]!=null && ds.Tables[0].Rows[0]["LOGINTIMES"].ToString()!="")
				{
					model.LOGINTIMES=int.Parse(ds.Tables[0].Rows[0]["LOGINTIMES"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LASTLOGINTIME"]!=null && ds.Tables[0].Rows[0]["LASTLOGINTIME"].ToString()!="")
				{
					model.LASTLOGINTIME=DateTime.Parse(ds.Tables[0].Rows[0]["LASTLOGINTIME"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SENDMSGS"]!=null && ds.Tables[0].Rows[0]["SENDMSGS"].ToString()!="")
				{
					model.SENDMSGS=int.Parse(ds.Tables[0].Rows[0]["SENDMSGS"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RECVMSGS"]!=null && ds.Tables[0].Rows[0]["RECVMSGS"].ToString()!="")
				{
					model.RECVMSGS=int.Parse(ds.Tables[0].Rows[0]["RECVMSGS"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LEAVEMSGS"]!=null && ds.Tables[0].Rows[0]["LEAVEMSGS"].ToString()!="")
				{
					model.LEAVEMSGS=int.Parse(ds.Tables[0].Rows[0]["LEAVEMSGS"].ToString());
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
			strSql.Append("select VISITORID,VISITORCODE,USERID,SRCURL,CLIENTIPADDR,CLIENTLOCATION,CLIENTOS,WEBBROWSER,REMARKNAME,REMARKTEXT,FLAG,ISACTIVE,LOGINTIMES,LASTLOGINTIME,SENDMSGS,RECVMSGS,LEAVEMSGS,CREATETIME ");
			strSql.Append(" FROM WEBVISITORS ");
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
			strSql.Append(" VISITORID,VISITORCODE,USERID,SRCURL,CLIENTIPADDR,CLIENTLOCATION,CLIENTOS,WEBBROWSER,REMARKNAME,REMARKTEXT,FLAG,ISACTIVE,LOGINTIMES,LASTLOGINTIME,SENDMSGS,RECVMSGS,LEAVEMSGS,CREATETIME ");
			strSql.Append(" FROM WEBVISITORS ");
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
			strSql.Append("select count(1) FROM WEBVISITORS ");
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
			strSql.Append(")AS Row, T.*  from WEBVISITORS T ");
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
			parameters[1].Value = "WEBVISITORS";
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

