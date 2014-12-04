/**  智客知识管理平台。
* GROUPS.cs
*
* 功 能： N/A
* 类 名： GROUPS
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
	/// 数据访问类:GROUPS
	/// </summary>
	public partial class GROUPS:IGROUPS
	{
		public GROUPS()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("GROUPID", "GROUPS"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int GROUPID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from GROUPS");
			strSql.Append(" where GROUPID=@GROUPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@GROUPID", SqlDbType.Int,4)			};
			parameters[0].Value = GROUPID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.GROUPS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into GROUPS(");
			strSql.Append("GROUPID,GROUPNAME,INTRODUCTION,NOTICE,JOINSETTING,CREATORID,OWNERID,OWNERUSERTYPE,CREATETIME)");
			strSql.Append(" values (");
			strSql.Append("@GROUPID,@GROUPNAME,@INTRODUCTION,@NOTICE,@JOINSETTING,@CREATORID,@OWNERID,@OWNERUSERTYPE,@CREATETIME)");
			SqlParameter[] parameters = {
					new SqlParameter("@GROUPID", SqlDbType.Int,4),
					new SqlParameter("@GROUPNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@INTRODUCTION", SqlDbType.NVarChar,255),
					new SqlParameter("@NOTICE", SqlDbType.NVarChar,255),
					new SqlParameter("@JOINSETTING", SqlDbType.SmallInt,2),
					new SqlParameter("@CREATORID", SqlDbType.Int,4),
					new SqlParameter("@OWNERID", SqlDbType.Int,4),
					new SqlParameter("@OWNERUSERTYPE", SqlDbType.SmallInt,2),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime)};
			parameters[0].Value = model.GROUPID;
			parameters[1].Value = model.GROUPNAME;
			parameters[2].Value = model.INTRODUCTION;
			parameters[3].Value = model.NOTICE;
			parameters[4].Value = model.JOINSETTING;
			parameters[5].Value = model.CREATORID;
			parameters[6].Value = model.OWNERID;
			parameters[7].Value = model.OWNERUSERTYPE;
			parameters[8].Value = model.CREATETIME;

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
		public bool Update(ZK.Model.GROUPS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update GROUPS set ");
			strSql.Append("GROUPNAME=@GROUPNAME,");
			strSql.Append("INTRODUCTION=@INTRODUCTION,");
			strSql.Append("NOTICE=@NOTICE,");
			strSql.Append("JOINSETTING=@JOINSETTING,");
			strSql.Append("CREATORID=@CREATORID,");
			strSql.Append("OWNERID=@OWNERID,");
			strSql.Append("OWNERUSERTYPE=@OWNERUSERTYPE,");
			strSql.Append("CREATETIME=@CREATETIME");
			strSql.Append(" where GROUPID=@GROUPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@GROUPNAME", SqlDbType.NVarChar,20),
					new SqlParameter("@INTRODUCTION", SqlDbType.NVarChar,255),
					new SqlParameter("@NOTICE", SqlDbType.NVarChar,255),
					new SqlParameter("@JOINSETTING", SqlDbType.SmallInt,2),
					new SqlParameter("@CREATORID", SqlDbType.Int,4),
					new SqlParameter("@OWNERID", SqlDbType.Int,4),
					new SqlParameter("@OWNERUSERTYPE", SqlDbType.SmallInt,2),
					new SqlParameter("@CREATETIME", SqlDbType.DateTime),
					new SqlParameter("@GROUPID", SqlDbType.Int,4)};
			parameters[0].Value = model.GROUPNAME;
			parameters[1].Value = model.INTRODUCTION;
			parameters[2].Value = model.NOTICE;
			parameters[3].Value = model.JOINSETTING;
			parameters[4].Value = model.CREATORID;
			parameters[5].Value = model.OWNERID;
			parameters[6].Value = model.OWNERUSERTYPE;
			parameters[7].Value = model.CREATETIME;
			parameters[8].Value = model.GROUPID;

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
		public bool Delete(int GROUPID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GROUPS ");
			strSql.Append(" where GROUPID=@GROUPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@GROUPID", SqlDbType.Int,4)			};
			parameters[0].Value = GROUPID;

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
		public bool DeleteList(string GROUPIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from GROUPS ");
			strSql.Append(" where GROUPID in ("+GROUPIDlist + ")  ");
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
		public ZK.Model.GROUPS GetModel(int GROUPID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 GROUPID,GROUPNAME,INTRODUCTION,NOTICE,JOINSETTING,CREATORID,OWNERID,OWNERUSERTYPE,CREATETIME from GROUPS ");
			strSql.Append(" where GROUPID=@GROUPID ");
			SqlParameter[] parameters = {
					new SqlParameter("@GROUPID", SqlDbType.Int,4)			};
			parameters[0].Value = GROUPID;

			ZK.Model.GROUPS model=new ZK.Model.GROUPS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["GROUPID"]!=null && ds.Tables[0].Rows[0]["GROUPID"].ToString()!="")
				{
					model.GROUPID=int.Parse(ds.Tables[0].Rows[0]["GROUPID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GROUPNAME"]!=null && ds.Tables[0].Rows[0]["GROUPNAME"].ToString()!="")
				{
					model.GROUPNAME=ds.Tables[0].Rows[0]["GROUPNAME"].ToString();
				}
				if(ds.Tables[0].Rows[0]["INTRODUCTION"]!=null && ds.Tables[0].Rows[0]["INTRODUCTION"].ToString()!="")
				{
					model.INTRODUCTION=ds.Tables[0].Rows[0]["INTRODUCTION"].ToString();
				}
				if(ds.Tables[0].Rows[0]["NOTICE"]!=null && ds.Tables[0].Rows[0]["NOTICE"].ToString()!="")
				{
					model.NOTICE=ds.Tables[0].Rows[0]["NOTICE"].ToString();
				}
				if(ds.Tables[0].Rows[0]["JOINSETTING"]!=null && ds.Tables[0].Rows[0]["JOINSETTING"].ToString()!="")
				{
					model.JOINSETTING=int.Parse(ds.Tables[0].Rows[0]["JOINSETTING"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CREATORID"]!=null && ds.Tables[0].Rows[0]["CREATORID"].ToString()!="")
				{
					model.CREATORID=int.Parse(ds.Tables[0].Rows[0]["CREATORID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OWNERID"]!=null && ds.Tables[0].Rows[0]["OWNERID"].ToString()!="")
				{
					model.OWNERID=int.Parse(ds.Tables[0].Rows[0]["OWNERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OWNERUSERTYPE"]!=null && ds.Tables[0].Rows[0]["OWNERUSERTYPE"].ToString()!="")
				{
					model.OWNERUSERTYPE=int.Parse(ds.Tables[0].Rows[0]["OWNERUSERTYPE"].ToString());
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
			strSql.Append("select GROUPID,GROUPNAME,INTRODUCTION,NOTICE,JOINSETTING,CREATORID,OWNERID,OWNERUSERTYPE,CREATETIME ");
			strSql.Append(" FROM GROUPS ");
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
			strSql.Append(" GROUPID,GROUPNAME,INTRODUCTION,NOTICE,JOINSETTING,CREATORID,OWNERID,OWNERUSERTYPE,CREATETIME ");
			strSql.Append(" FROM GROUPS ");
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
			strSql.Append("select count(1) FROM GROUPS ");
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
			strSql.Append(")AS Row, T.*  from GROUPS T ");
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
			parameters[1].Value = "GROUPS";
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

