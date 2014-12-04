/**  智客知识管理平台。
* GROUPMEMBERS.cs
*
* 功 能： N/A
* 类 名： GROUPMEMBERS
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:34   N/A    初版
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
	/// 数据访问类:GROUPMEMBERS
	/// </summary>
	public partial class GROUPMEMBERS:IGROUPMEMBERS
	{
		public GROUPMEMBERS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("MEMBERID", "GROUPMEMBERS"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MEMBERID,int GROUPID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from GROUPMEMBERS");
			strSql.Append(" where MEMBERID=@MEMBERID and GROUPID=@GROUPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@MEMBERID", SqlDbType.Int,4),
					new SqlParameter("@GROUPID", SqlDbType.Int,4)			};
			parameters[0].Value = MEMBERID;
			parameters[1].Value = GROUPID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.GROUPMEMBERS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GROUPMEMBERS(");
			strSql.Append("MEMBERID,GROUPID,ISMANAGER,LASTRECVMSGSID,MSGHINTSETTING,MODIFYCARDBYMNG,CARD_NAME,CARD_SEX,CARD_NUMBER,CARD_EMAIL,CARD_REMARK,JOINTIME)");
			strSql.Append(" values (");
			strSql.Append("@MEMBERID,@GROUPID,@ISMANAGER,@LASTRECVMSGSID,@MSGHINTSETTING,@MODIFYCARDBYMNG,@CARD_NAME,@CARD_SEX,@CARD_NUMBER,@CARD_EMAIL,@CARD_REMARK,@JOINTIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@MEMBERID", SqlDbType.Int,4),
					new SqlParameter("@GROUPID", SqlDbType.Int,4),
					new SqlParameter("@ISMANAGER", SqlDbType.SmallInt,2),
					new SqlParameter("@LASTRECVMSGSID", SqlDbType.BigInt,8),
					new SqlParameter("@MSGHINTSETTING", SqlDbType.SmallInt,2),
					new SqlParameter("@MODIFYCARDBYMNG", SqlDbType.SmallInt,2),
					new SqlParameter("@CARD_NAME", SqlDbType.NVarChar,20),
					new SqlParameter("@CARD_SEX", SqlDbType.SmallInt,2),
					new SqlParameter("@CARD_NUMBER", SqlDbType.NVarChar,20),
					new SqlParameter("@CARD_EMAIL", SqlDbType.NVarChar,50),
					new SqlParameter("@CARD_REMARK", SqlDbType.NVarChar,255),
					new SqlParameter("@JOINTIME", SqlDbType.DateTime)};
			parameters[0].Value = model.MEMBERID;
			parameters[1].Value = model.GROUPID;
			parameters[2].Value = model.ISMANAGER;
			parameters[3].Value = model.LASTRECVMSGSID;
			parameters[4].Value = model.MSGHINTSETTING;
			parameters[5].Value = model.MODIFYCARDBYMNG;
			parameters[6].Value = model.CARD_NAME;
			parameters[7].Value = model.CARD_SEX;
			parameters[8].Value = model.CARD_NUMBER;
			parameters[9].Value = model.CARD_EMAIL;
			parameters[10].Value = model.CARD_REMARK;
			parameters[11].Value = model.JOINTIME;

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
		public bool Update(ZK.Model.GROUPMEMBERS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update GROUPMEMBERS set ");
			strSql.Append("ISMANAGER=@ISMANAGER,");
			strSql.Append("LASTRECVMSGSID=@LASTRECVMSGSID,");
			strSql.Append("MSGHINTSETTING=@MSGHINTSETTING,");
			strSql.Append("MODIFYCARDBYMNG=@MODIFYCARDBYMNG,");
			strSql.Append("CARD_NAME=@CARD_NAME,");
			strSql.Append("CARD_SEX=@CARD_SEX,");
			strSql.Append("CARD_NUMBER=@CARD_NUMBER,");
			strSql.Append("CARD_EMAIL=@CARD_EMAIL,");
			strSql.Append("CARD_REMARK=@CARD_REMARK,");
			strSql.Append("JOINTIME=@JOINTIME");
			strSql.Append(" where MEMBERID=@MEMBERID and GROUPID=@GROUPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ISMANAGER", SqlDbType.SmallInt,2),
					new SqlParameter("@LASTRECVMSGSID", SqlDbType.BigInt,8),
					new SqlParameter("@MSGHINTSETTING", SqlDbType.SmallInt,2),
					new SqlParameter("@MODIFYCARDBYMNG", SqlDbType.SmallInt,2),
					new SqlParameter("@CARD_NAME", SqlDbType.NVarChar,20),
					new SqlParameter("@CARD_SEX", SqlDbType.SmallInt,2),
					new SqlParameter("@CARD_NUMBER", SqlDbType.NVarChar,20),
					new SqlParameter("@CARD_EMAIL", SqlDbType.NVarChar,50),
					new SqlParameter("@CARD_REMARK", SqlDbType.NVarChar,255),
					new SqlParameter("@JOINTIME", SqlDbType.DateTime),
					new SqlParameter("@MEMBERID", SqlDbType.Int,4),
					new SqlParameter("@GROUPID", SqlDbType.Int,4)};
			parameters[0].Value = model.ISMANAGER;
			parameters[1].Value = model.LASTRECVMSGSID;
			parameters[2].Value = model.MSGHINTSETTING;
			parameters[3].Value = model.MODIFYCARDBYMNG;
			parameters[4].Value = model.CARD_NAME;
			parameters[5].Value = model.CARD_SEX;
			parameters[6].Value = model.CARD_NUMBER;
			parameters[7].Value = model.CARD_EMAIL;
			parameters[8].Value = model.CARD_REMARK;
			parameters[9].Value = model.JOINTIME;
			parameters[10].Value = model.MEMBERID;
			parameters[11].Value = model.GROUPID;

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
		public bool Delete(int MEMBERID,int GROUPID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GROUPMEMBERS ");
			strSql.Append(" where MEMBERID=@MEMBERID and GROUPID=@GROUPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@MEMBERID", SqlDbType.Int,4),
					new SqlParameter("@GROUPID", SqlDbType.Int,4)			};
			parameters[0].Value = MEMBERID;
			parameters[1].Value = GROUPID;

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
		public ZK.Model.GROUPMEMBERS GetModel(int MEMBERID,int GROUPID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 MEMBERID,GROUPID,ISMANAGER,LASTRECVMSGSID,MSGHINTSETTING,MODIFYCARDBYMNG,CARD_NAME,CARD_SEX,CARD_NUMBER,CARD_EMAIL,CARD_REMARK,JOINTIME from GROUPMEMBERS ");
			strSql.Append(" where MEMBERID=@MEMBERID and GROUPID=@GROUPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@MEMBERID", SqlDbType.Int,4),
					new SqlParameter("@GROUPID", SqlDbType.Int,4)			};
			parameters[0].Value = MEMBERID;
			parameters[1].Value = GROUPID;

			ZK.Model.GROUPMEMBERS model=new ZK.Model.GROUPMEMBERS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["MEMBERID"]!=null && ds.Tables[0].Rows[0]["MEMBERID"].ToString()!="")
				{
					model.MEMBERID=int.Parse(ds.Tables[0].Rows[0]["MEMBERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GROUPID"]!=null && ds.Tables[0].Rows[0]["GROUPID"].ToString()!="")
				{
					model.GROUPID=int.Parse(ds.Tables[0].Rows[0]["GROUPID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ISMANAGER"]!=null && ds.Tables[0].Rows[0]["ISMANAGER"].ToString()!="")
				{
					model.ISMANAGER=int.Parse(ds.Tables[0].Rows[0]["ISMANAGER"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LASTRECVMSGSID"]!=null && ds.Tables[0].Rows[0]["LASTRECVMSGSID"].ToString()!="")
				{
					model.LASTRECVMSGSID=long.Parse(ds.Tables[0].Rows[0]["LASTRECVMSGSID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MSGHINTSETTING"]!=null && ds.Tables[0].Rows[0]["MSGHINTSETTING"].ToString()!="")
				{
					model.MSGHINTSETTING=int.Parse(ds.Tables[0].Rows[0]["MSGHINTSETTING"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MODIFYCARDBYMNG"]!=null && ds.Tables[0].Rows[0]["MODIFYCARDBYMNG"].ToString()!="")
				{
					model.MODIFYCARDBYMNG=int.Parse(ds.Tables[0].Rows[0]["MODIFYCARDBYMNG"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CARD_NAME"]!=null && ds.Tables[0].Rows[0]["CARD_NAME"].ToString()!="")
				{
					model.CARD_NAME=ds.Tables[0].Rows[0]["CARD_NAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CARD_SEX"]!=null && ds.Tables[0].Rows[0]["CARD_SEX"].ToString()!="")
				{
					model.CARD_SEX=int.Parse(ds.Tables[0].Rows[0]["CARD_SEX"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CARD_NUMBER"]!=null && ds.Tables[0].Rows[0]["CARD_NUMBER"].ToString()!="")
				{
					model.CARD_NUMBER=ds.Tables[0].Rows[0]["CARD_NUMBER"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CARD_EMAIL"]!=null && ds.Tables[0].Rows[0]["CARD_EMAIL"].ToString()!="")
				{
					model.CARD_EMAIL=ds.Tables[0].Rows[0]["CARD_EMAIL"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CARD_REMARK"]!=null && ds.Tables[0].Rows[0]["CARD_REMARK"].ToString()!="")
				{
					model.CARD_REMARK=ds.Tables[0].Rows[0]["CARD_REMARK"].ToString();
				}
				if(ds.Tables[0].Rows[0]["JOINTIME"]!=null && ds.Tables[0].Rows[0]["JOINTIME"].ToString()!="")
				{
					model.JOINTIME=DateTime.Parse(ds.Tables[0].Rows[0]["JOINTIME"].ToString());
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
			strSql.Append("select MEMBERID,GROUPID,ISMANAGER,LASTRECVMSGSID,MSGHINTSETTING,MODIFYCARDBYMNG,CARD_NAME,CARD_SEX,CARD_NUMBER,CARD_EMAIL,CARD_REMARK,JOINTIME ");
			strSql.Append(" FROM GROUPMEMBERS ");
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
			strSql.Append(" MEMBERID,GROUPID,ISMANAGER,LASTRECVMSGSID,MSGHINTSETTING,MODIFYCARDBYMNG,CARD_NAME,CARD_SEX,CARD_NUMBER,CARD_EMAIL,CARD_REMARK,JOINTIME ");
			strSql.Append(" FROM GROUPMEMBERS ");
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
			strSql.Append("select count(1) FROM GROUPMEMBERS ");
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
				strSql.Append("order by T.GROUPID desc");
			}
			strSql.Append(")AS Row, T.*  from GROUPMEMBERS T ");
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
			parameters[1].Value = "GROUPMEMBERS";
			parameters[2].Value = strWhere;
			parameters[3].Value = "GROUPID";
			parameters[4].Value = "GROUPID desc";
			parameters[5].Value = PageSize;
			parameters[6].Value = PageIndex;
			parameters[7].Value = "1";
			return DbHelperSQL.RunProcedure("Common_PageList",parameters,"ds");
		}

		#endregion  Method
	}
}

