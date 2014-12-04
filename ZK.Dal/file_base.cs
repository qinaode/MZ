/**  智客知识管理平台。
* file_base.cs
*
* 功 能： N/A
* 类 名： file_base
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/28 14:00:05   N/A    初版
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
	/// 数据访问类:file_base
	/// </summary>
	public partial class file_base:Ifile_base
	{
		public file_base()
		{}
	#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("file_id", "file_base"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int file_id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from file_base");
			strSql.Append(" where file_id=@file_id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@file_id", MySqlDbType.Int32,11)			};
			parameters[0].Value = file_id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.file_base model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into file_base(");
			strSql.Append("file_id,file_name,file_desc,file_type,parent_file_id,create_time,update_time,file_size,file_path,file_ext,file_download,channel_id,is_dir,owner_id,channel_group_id,is_deleted,download_count,view_count,chapter_id)");
			strSql.Append(" values (");
			strSql.Append("@file_id,@file_name,@file_desc,@file_type,@parent_file_id,@create_time,@update_time,@file_size,@file_path,@file_ext,@file_download,@channel_id,@is_dir,@owner_id,@channel_group_id,@is_deleted,@download_count,@view_count,@chapter_id)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@file_id", MySqlDbType.Int32,11),
					new MySqlParameter("@file_name", MySqlDbType.VarChar,255),
					new MySqlParameter("@file_desc", MySqlDbType.VarChar,500),
					new MySqlParameter("@file_type", MySqlDbType.Int32,11),
					new MySqlParameter("@parent_file_id", MySqlDbType.Int32,11),
					new MySqlParameter("@create_time", MySqlDbType.DateTime),
					new MySqlParameter("@update_time", MySqlDbType.Timestamp),
					new MySqlParameter("@file_size", MySqlDbType.Int64,64),
					new MySqlParameter("@file_path", MySqlDbType.VarChar,500),
					new MySqlParameter("@file_ext", MySqlDbType.VarChar,50),
					new MySqlParameter("@file_download", MySqlDbType.VarChar,500),
					new MySqlParameter("@channel_id", MySqlDbType.Int32,11),
					new MySqlParameter("@is_dir", MySqlDbType.Int16,4),
					new MySqlParameter("@owner_id", MySqlDbType.Int32,11),
					new MySqlParameter("@channel_group_id", MySqlDbType.Int32,11),
					new MySqlParameter("@is_deleted", MySqlDbType.Int16,4),
					new MySqlParameter("@download_count", MySqlDbType.Int32,11),
					new MySqlParameter("@view_count", MySqlDbType.Int32,11),
					new MySqlParameter("@chapter_id", MySqlDbType.Int32,11)};
			parameters[0].Value = model.file_id;
			parameters[1].Value = model.file_name;
			parameters[2].Value = model.file_desc;
			parameters[3].Value = model.file_type;
			parameters[4].Value = model.parent_file_id;
			parameters[5].Value = model.create_time;
			parameters[6].Value = model.update_time;
			parameters[7].Value = model.file_size;
			parameters[8].Value = model.file_path;
			parameters[9].Value = model.file_ext;
			parameters[10].Value = model.file_download;
			parameters[11].Value = model.channel_id;
			parameters[12].Value = model.is_dir;
			parameters[13].Value = model.owner_id;
			parameters[14].Value = model.channel_group_id;
			parameters[15].Value = model.is_deleted;
			parameters[16].Value = model.download_count;
			parameters[17].Value = model.view_count;
			parameters[18].Value = model.chapter_id;

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
		public bool Update(ZK.Model.file_base model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update file_base set ");
			strSql.Append("file_name=@file_name,");
			strSql.Append("file_desc=@file_desc,");
			strSql.Append("file_type=@file_type,");
			strSql.Append("parent_file_id=@parent_file_id,");
			strSql.Append("create_time=@create_time,");
			strSql.Append("file_size=@file_size,");
			strSql.Append("file_path=@file_path,");
			strSql.Append("file_ext=@file_ext,");
			strSql.Append("file_download=@file_download,");
			strSql.Append("channel_id=@channel_id,");
			strSql.Append("is_dir=@is_dir,");
			strSql.Append("owner_id=@owner_id,");
			strSql.Append("channel_group_id=@channel_group_id,");
			strSql.Append("is_deleted=@is_deleted,");
			strSql.Append("download_count=@download_count,");
			strSql.Append("view_count=@view_count,");
			strSql.Append("chapter_id=@chapter_id");
			strSql.Append(" where file_id=@file_id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@file_name", MySqlDbType.VarChar,255),
					new MySqlParameter("@file_desc", MySqlDbType.VarChar,500),
					new MySqlParameter("@file_type", MySqlDbType.Int32,11),
					new MySqlParameter("@parent_file_id", MySqlDbType.Int32,11),
					new MySqlParameter("@create_time", MySqlDbType.DateTime),
					new MySqlParameter("@file_size", MySqlDbType.Int64,64),
					new MySqlParameter("@file_path", MySqlDbType.VarChar,500),
					new MySqlParameter("@file_ext", MySqlDbType.VarChar,50),
					new MySqlParameter("@file_download", MySqlDbType.VarChar,500),
					new MySqlParameter("@channel_id", MySqlDbType.Int32,11),
					new MySqlParameter("@is_dir", MySqlDbType.Int16,4),
					new MySqlParameter("@owner_id", MySqlDbType.Int32,11),
					new MySqlParameter("@channel_group_id", MySqlDbType.Int32,11),
					new MySqlParameter("@is_deleted", MySqlDbType.Int16,4),
					new MySqlParameter("@download_count", MySqlDbType.Int32,11),
					new MySqlParameter("@view_count", MySqlDbType.Int32,11),
					new MySqlParameter("@chapter_id", MySqlDbType.Int32,11),
					new MySqlParameter("@file_id", MySqlDbType.Int32,11)};
			parameters[0].Value = model.file_name;
			parameters[1].Value = model.file_desc;
			parameters[2].Value = model.file_type;
			parameters[3].Value = model.parent_file_id;
			parameters[4].Value = model.create_time;
			parameters[5].Value = model.file_size;
			parameters[6].Value = model.file_path;
			parameters[7].Value = model.file_ext;
			parameters[8].Value = model.file_download;
			parameters[9].Value = model.channel_id;
			parameters[10].Value = model.is_dir;
			parameters[11].Value = model.owner_id;
			parameters[12].Value = model.channel_group_id;
			parameters[13].Value = model.is_deleted;
			parameters[14].Value = model.download_count;
			parameters[15].Value = model.view_count;
			parameters[16].Value = model.chapter_id;
			parameters[17].Value = model.file_id;

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
		public bool Delete(int file_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from file_base ");
			strSql.Append(" where file_id=@file_id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@file_id", MySqlDbType.Int32,11)			};
			parameters[0].Value = file_id;

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
		public bool DeleteList(string file_idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from file_base ");
			strSql.Append(" where file_id in ("+file_idlist + ")  ");
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
		public ZK.Model.file_base GetModel(int file_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select file_id,file_name,file_desc,file_type,parent_file_id,create_time,update_time,file_size,file_path,file_ext,file_download,channel_id,is_dir,owner_id,channel_group_id,is_deleted,download_count,view_count,chapter_id from file_base ");
			strSql.Append(" where file_id=@file_id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@file_id", MySqlDbType.Int32,11)			};
			parameters[0].Value = file_id;

			ZK.Model.file_base model=new ZK.Model.file_base();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["file_id"]!=null && ds.Tables[0].Rows[0]["file_id"].ToString()!="")
				{
					model.file_id=int.Parse(ds.Tables[0].Rows[0]["file_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_name"]!=null && ds.Tables[0].Rows[0]["file_name"].ToString()!="")
				{
					model.file_name=ds.Tables[0].Rows[0]["file_name"].ToString();
				}
				if(ds.Tables[0].Rows[0]["file_desc"]!=null && ds.Tables[0].Rows[0]["file_desc"].ToString()!="")
				{
					model.file_desc=ds.Tables[0].Rows[0]["file_desc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["file_type"]!=null && ds.Tables[0].Rows[0]["file_type"].ToString()!="")
				{
					model.file_type=int.Parse(ds.Tables[0].Rows[0]["file_type"].ToString());
				}
				if(ds.Tables[0].Rows[0]["parent_file_id"]!=null && ds.Tables[0].Rows[0]["parent_file_id"].ToString()!="")
				{
					model.parent_file_id=int.Parse(ds.Tables[0].Rows[0]["parent_file_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["create_time"]!=null && ds.Tables[0].Rows[0]["create_time"].ToString()!="")
				{
					model.create_time=DateTime.Parse(ds.Tables[0].Rows[0]["create_time"].ToString());
				}
				if(ds.Tables[0].Rows[0]["update_time"]!=null && ds.Tables[0].Rows[0]["update_time"].ToString()!="")
				{
					model.update_time=DateTime.Parse(ds.Tables[0].Rows[0]["update_time"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_size"]!=null && ds.Tables[0].Rows[0]["file_size"].ToString()!="")
				{
					model.file_size=long.Parse(ds.Tables[0].Rows[0]["file_size"].ToString());
				}
				if(ds.Tables[0].Rows[0]["file_path"]!=null && ds.Tables[0].Rows[0]["file_path"].ToString()!="")
				{
					model.file_path=ds.Tables[0].Rows[0]["file_path"].ToString();
				}
				if(ds.Tables[0].Rows[0]["file_ext"]!=null && ds.Tables[0].Rows[0]["file_ext"].ToString()!="")
				{
					model.file_ext=ds.Tables[0].Rows[0]["file_ext"].ToString();
				}
				if(ds.Tables[0].Rows[0]["file_download"]!=null && ds.Tables[0].Rows[0]["file_download"].ToString()!="")
				{
					model.file_download=ds.Tables[0].Rows[0]["file_download"].ToString();
				}
				if(ds.Tables[0].Rows[0]["channel_id"]!=null && ds.Tables[0].Rows[0]["channel_id"].ToString()!="")
				{
					model.channel_id=int.Parse(ds.Tables[0].Rows[0]["channel_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["is_dir"]!=null && ds.Tables[0].Rows[0]["is_dir"].ToString()!="")
				{
					model.is_dir=int.Parse(ds.Tables[0].Rows[0]["is_dir"].ToString());
				}
				if(ds.Tables[0].Rows[0]["owner_id"]!=null && ds.Tables[0].Rows[0]["owner_id"].ToString()!="")
				{
					model.owner_id=int.Parse(ds.Tables[0].Rows[0]["owner_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["channel_group_id"]!=null && ds.Tables[0].Rows[0]["channel_group_id"].ToString()!="")
				{
					model.channel_group_id=int.Parse(ds.Tables[0].Rows[0]["channel_group_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["is_deleted"]!=null && ds.Tables[0].Rows[0]["is_deleted"].ToString()!="")
				{
					model.is_deleted=int.Parse(ds.Tables[0].Rows[0]["is_deleted"].ToString());
				}
				if(ds.Tables[0].Rows[0]["download_count"]!=null && ds.Tables[0].Rows[0]["download_count"].ToString()!="")
				{
					model.download_count=int.Parse(ds.Tables[0].Rows[0]["download_count"].ToString());
				}
				if(ds.Tables[0].Rows[0]["view_count"]!=null && ds.Tables[0].Rows[0]["view_count"].ToString()!="")
				{
					model.view_count=int.Parse(ds.Tables[0].Rows[0]["view_count"].ToString());
				}
				if(ds.Tables[0].Rows[0]["chapter_id"]!=null && ds.Tables[0].Rows[0]["chapter_id"].ToString()!="")
				{
					model.chapter_id=int.Parse(ds.Tables[0].Rows[0]["chapter_id"].ToString());
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
			strSql.Append("select file_id,file_name,file_desc,file_type,parent_file_id,create_time,update_time,file_size,file_path,file_ext,file_download,channel_id,is_dir,owner_id,channel_group_id,is_deleted,download_count,view_count,chapter_id ");
			strSql.Append(" FROM file_base ");
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
			strSql.Append("select count(1) FROM file_base ");
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
				strSql.Append("order by T.file_id desc");
			}
			strSql.Append(")AS Row, T.*  from file_base T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperMySQL.Query(strSql.ToString());
		}

        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //    MySqlParameter[] parameters = {
        //            new MySqlParameter("@select_list", MySqlDbType.VarChar, 1000),
        //            new MySqlParameter("@table_name", MySqlDbType.VarChar, 1000),
        //            new MySqlParameter("@where", MySqlDbType.VarChar, 1000),
        //            new MySqlParameter("@primary_key", MySqlDbType.VarChar, 200),
        //            new MySqlParameter("@order_by", MySqlDbType.VarChar, 200),
        //            new MySqlParameter("@page_size", MySqlDbType.Int16),
        //            new MySqlParameter("@page_index", MySqlDbType.Int32),
        //            new MySqlParameter("@bl_page", MySqlDbType.Int32),
        //            };
        //    parameters[0].Value = "*";
        //    parameters[1].Value = "file_base";
        //    parameters[2].Value = strWhere;
        //    parameters[3].Value = "file_id";
        //    parameters[4].Value = "file_id desc";
        //    parameters[5].Value = PageSize;
        //    parameters[6].Value = PageIndex;
        //    parameters[7].Value = "1";
        //    return DbHelperMySQL.RunProcedure("Common_PageList",parameters,"ds");
        //}

		#endregion  Method	
	}
}

