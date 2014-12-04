/**  智客知识管理平台。
* miniyun_files.cs
*
* 功 能： N/A
* 类 名： miniyun_files
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/12 8:33:57   N/A    初版
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
using MySql.Data.MySqlClient;
using ZK.IDAL;
using ZK.DBUtility;//Please add references
namespace ZK.Dal
{
	/// <summary>
	/// 数据访问类:miniyun_files
	/// </summary>
	public partial class miniyun_files:Iminiyun_files
	{
		public miniyun_files()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("id", "miniyun_files"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from miniyun_files");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.miniyun_files model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into miniyun_files(");
			strSql.Append("user_id,file_type,parent_file_id,file_create_time,file_update_time,file_name,version_id,file_size,file_path,event_uuid,is_deleted,mime_type,created_at,updated_at,sort)");
			strSql.Append(" values (");
			strSql.Append("@user_id,@file_type,@parent_file_id,@file_create_time,@file_update_time,@file_name,@version_id,@file_size,@file_path,@event_uuid,@is_deleted,@mime_type,@created_at,@updated_at,@sort)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,11),
					new MySqlParameter("@file_type", MySqlDbType.Int32,11),
					new MySqlParameter("@parent_file_id", MySqlDbType.Int32,11),
					new MySqlParameter("@file_create_time", MySqlDbType.Int64,20),
					new MySqlParameter("@file_update_time", MySqlDbType.Int64,20),
					new MySqlParameter("@file_name", MySqlDbType.VarChar,255),
					new MySqlParameter("@version_id", MySqlDbType.Int32,11),
					new MySqlParameter("@file_size", MySqlDbType.Int64,64),
					new MySqlParameter("@file_path", MySqlDbType.VarChar,500),
					new MySqlParameter("@event_uuid", MySqlDbType.VarChar,64),
					new MySqlParameter("@is_deleted", MySqlDbType.Int16,4),
					new MySqlParameter("@mime_type", MySqlDbType.VarChar,64),
					new MySqlParameter("@created_at", MySqlDbType.DateTime),
					new MySqlParameter("@updated_at", MySqlDbType.DateTime),
					new MySqlParameter("@sort", MySqlDbType.Int32,11)};
			parameters[0].Value = model.user_id;
			parameters[1].Value = model.file_type;
			parameters[2].Value = model.parent_file_id;
			parameters[3].Value = model.file_create_time;
			parameters[4].Value = model.file_update_time;
			parameters[5].Value = model.file_name;
			parameters[6].Value = model.version_id;
			parameters[7].Value = model.file_size;
			parameters[8].Value = model.file_path;
			parameters[9].Value = model.event_uuid;
			parameters[10].Value = model.is_deleted;
			parameters[11].Value = model.mime_type;
			parameters[12].Value = model.created_at;
			parameters[13].Value = model.updated_at;
			parameters[14].Value = model.sort;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(ZK.Model.miniyun_files model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update miniyun_files set ");
			strSql.Append("user_id=@user_id,");
			strSql.Append("file_type=@file_type,");
			strSql.Append("parent_file_id=@parent_file_id,");
			strSql.Append("file_create_time=@file_create_time,");
			strSql.Append("file_update_time=@file_update_time,");
			strSql.Append("file_name=@file_name,");
			strSql.Append("version_id=@version_id,");
			strSql.Append("file_size=@file_size,");
			strSql.Append("file_path=@file_path,");
			strSql.Append("event_uuid=@event_uuid,");
			strSql.Append("is_deleted=@is_deleted,");
			strSql.Append("mime_type=@mime_type,");
			strSql.Append("created_at=@created_at,");
			strSql.Append("updated_at=@updated_at,");
			strSql.Append("sort=@sort");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,11),
					new MySqlParameter("@file_type", MySqlDbType.Int32,11),
					new MySqlParameter("@parent_file_id", MySqlDbType.Int32,11),
					new MySqlParameter("@file_create_time", MySqlDbType.Int64,20),
					new MySqlParameter("@file_update_time", MySqlDbType.Int64,20),
					new MySqlParameter("@file_name", MySqlDbType.VarChar,255),
					new MySqlParameter("@version_id", MySqlDbType.Int32,11),
					new MySqlParameter("@file_size", MySqlDbType.Int64,64),
					new MySqlParameter("@file_path", MySqlDbType.VarChar,500),
					new MySqlParameter("@event_uuid", MySqlDbType.VarChar,64),
					new MySqlParameter("@is_deleted", MySqlDbType.Int16,4),
					new MySqlParameter("@mime_type", MySqlDbType.VarChar,64),
					new MySqlParameter("@created_at", MySqlDbType.DateTime),
					new MySqlParameter("@updated_at", MySqlDbType.DateTime),
					new MySqlParameter("@sort", MySqlDbType.Int32,11),
					new MySqlParameter("@id", MySqlDbType.Int32,11)};
			parameters[0].Value = model.user_id;
			parameters[1].Value = model.file_type;
			parameters[2].Value = model.parent_file_id;
			parameters[3].Value = model.file_create_time;
			parameters[4].Value = model.file_update_time;
			parameters[5].Value = model.file_name;
			parameters[6].Value = model.version_id;
			parameters[7].Value = model.file_size;
			parameters[8].Value = model.file_path;
			parameters[9].Value = model.event_uuid;
			parameters[10].Value = model.is_deleted;
			parameters[11].Value = model.mime_type;
			parameters[12].Value = model.created_at;
			parameters[13].Value = model.updated_at;
			parameters[14].Value = model.sort;
			parameters[15].Value = model.id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
			strSql.Append("delete from miniyun_files ");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
			strSql.Append("delete from miniyun_files ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
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
		public ZK.Model.miniyun_files GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,user_id,file_type,parent_file_id,file_create_time,file_update_time,file_name,version_id,file_size,file_path,event_uuid,is_deleted,mime_type,created_at,updated_at,sort from miniyun_files ");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32)
			};
			parameters[0].Value = id;

			ZK.Model.miniyun_files model=new ZK.Model.miniyun_files();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["user_id"]!=null && ds.Tables[0].Rows[0]["user_id"].ToString()!="")
				{
					model.user_id=int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_type"]!=null && ds.Tables[0].Rows[0]["file_type"].ToString()!="")
				{
					model.file_type=int.Parse(ds.Tables[0].Rows[0]["file_type"].ToString());
				}
				if(ds.Tables[0].Rows[0]["parent_file_id"]!=null && ds.Tables[0].Rows[0]["parent_file_id"].ToString()!="")
				{
					model.parent_file_id=int.Parse(ds.Tables[0].Rows[0]["parent_file_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_create_time"]!=null && ds.Tables[0].Rows[0]["file_create_time"].ToString()!="")
				{
					model.file_create_time=long.Parse(ds.Tables[0].Rows[0]["file_create_time"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_update_time"]!=null && ds.Tables[0].Rows[0]["file_update_time"].ToString()!="")
				{
					model.file_update_time=long.Parse(ds.Tables[0].Rows[0]["file_update_time"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_name"]!=null && ds.Tables[0].Rows[0]["file_name"].ToString()!="")
				{
					model.file_name=ds.Tables[0].Rows[0]["file_name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["version_id"]!=null && ds.Tables[0].Rows[0]["version_id"].ToString()!="")
				{
					model.version_id=int.Parse(ds.Tables[0].Rows[0]["version_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_size"]!=null && ds.Tables[0].Rows[0]["file_size"].ToString()!="")
				{
					model.file_size=long.Parse(ds.Tables[0].Rows[0]["file_size"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_path"]!=null && ds.Tables[0].Rows[0]["file_path"].ToString()!="")
				{
					model.file_path=ds.Tables[0].Rows[0]["file_path"].ToString();
				}
				if(ds.Tables[0].Rows[0]["event_uuid"]!=null && ds.Tables[0].Rows[0]["event_uuid"].ToString()!="")
				{
					model.event_uuid=ds.Tables[0].Rows[0]["event_uuid"].ToString();
				}
				if(ds.Tables[0].Rows[0]["is_deleted"]!=null && ds.Tables[0].Rows[0]["is_deleted"].ToString()!="")
				{
					model.is_deleted=int.Parse(ds.Tables[0].Rows[0]["is_deleted"].ToString());
				}
				if(ds.Tables[0].Rows[0]["mime_type"]!=null && ds.Tables[0].Rows[0]["mime_type"].ToString()!="")
				{
					model.mime_type=ds.Tables[0].Rows[0]["mime_type"].ToString();
				}
				if(ds.Tables[0].Rows[0]["created_at"]!=null && ds.Tables[0].Rows[0]["created_at"].ToString()!="")
				{
					model.created_at=DateTime.Parse(ds.Tables[0].Rows[0]["created_at"].ToString());
				}
				if(ds.Tables[0].Rows[0]["updated_at"]!=null && ds.Tables[0].Rows[0]["updated_at"].ToString()!="")
				{
					model.updated_at=DateTime.Parse(ds.Tables[0].Rows[0]["updated_at"].ToString());
				}
				if(ds.Tables[0].Rows[0]["sort"]!=null && ds.Tables[0].Rows[0]["sort"].ToString()!="")
				{
					model.sort=int.Parse(ds.Tables[0].Rows[0]["sort"].ToString());
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
			strSql.Append("select id,user_id,file_type,parent_file_id,file_create_time,file_update_time,file_name,version_id,file_size,file_path,event_uuid,is_deleted,mime_type,created_at,updated_at,sort ");
			strSql.Append(" FROM miniyun_files ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM miniyun_files ");
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
			strSql.Append(")AS Row, T.*  from miniyun_files T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
            //MySqlParameter[] parameters = {
            //        new MySqlParameter("@select_list", MySqlDbType.VarChar, 1000),
            //        new MySqlParameter("@table_name", MySqlDbType.VarChar, 1000),
            //        new MySqlParameter("@where", MySqlDbType.VarChar, 1000),
            //        new MySqlParameter("@primary_key", MySqlDbType.VarChar, 200),
            //        new MySqlParameter("@order_by", MySqlDbType.VarChar, 200),
            //        new MySqlParameter("@page_size", MySqlDbType.Int32),
            //        new MySqlParameter("@page_index", MySqlDbType.Int32),
            //        new MySqlParameter("@bl_page", MySqlDbType.Int32),
            //        };
            //parameters[0].Value = "*";
            //parameters[1].Value = "miniyun_files";
            //parameters[2].Value = strWhere;
            //parameters[3].Value = "id";
            //parameters[4].Value = "id desc";
            //parameters[5].Value = PageSize;
            //parameters[6].Value = PageIndex;
            //parameters[7].Value = "1";
            //return DbHelperMySQL.RunProcedure("Common_PageList",parameters,"ds");
            return new DataSet();
		}

		#endregion  Method
	}
}

