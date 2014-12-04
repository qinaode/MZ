/**  智客知识管理平台。
* file_collection.cs
*
* 功 能： N/A
* 类 名： file_collection
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/4/9 13:53:19   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using ZK.Common;
using ZK.Model;
using ZK.DALFactory;
using ZK.IDAL;
namespace ZK.BLL
{
	/// <summary>
	/// file_collection
	/// </summary>
	public partial class file_collection
	{
		private readonly Ifile_collection dal=DataAccess.Createfile_collection();
		public file_collection()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.file_collection model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.file_collection model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.file_collection GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.file_collection GetModelByCache(int id)
		{
			
			string CacheKey = "file_collectionModel-" + id;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.file_collection)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.file_collection> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.file_collection> DataTableToList(DataTable dt)
		{
			List<ZK.Model.file_collection> modelList = new List<ZK.Model.file_collection>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.file_collection model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.file_collection();
					if(dt.Rows[n]["id"]!=null && dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["user_id"]!=null && dt.Rows[n]["user_id"].ToString()!="")
					{
						model.user_id=int.Parse(dt.Rows[n]["user_id"].ToString());
					}
					if(dt.Rows[n]["file_type"]!=null && dt.Rows[n]["file_type"].ToString()!="")
					{
						model.file_type=int.Parse(dt.Rows[n]["file_type"].ToString());
					}
					if(dt.Rows[n]["parent_file_id"]!=null && dt.Rows[n]["parent_file_id"].ToString()!="")
					{
						model.parent_file_id=int.Parse(dt.Rows[n]["parent_file_id"].ToString());
					}
					if(dt.Rows[n]["file_create_time"]!=null && dt.Rows[n]["file_create_time"].ToString()!="")
					{
						model.file_create_time=long.Parse(dt.Rows[n]["file_create_time"].ToString());
					}
					if(dt.Rows[n]["file_update_time"]!=null && dt.Rows[n]["file_update_time"].ToString()!="")
					{
						model.file_update_time=long.Parse(dt.Rows[n]["file_update_time"].ToString());
					}
					if(dt.Rows[n]["file_name"]!=null && dt.Rows[n]["file_name"].ToString()!="")
					{
					model.file_name=dt.Rows[n]["file_name"].ToString();
					}
					if(dt.Rows[n]["version_id"]!=null && dt.Rows[n]["version_id"].ToString()!="")
					{
						model.version_id=int.Parse(dt.Rows[n]["version_id"].ToString());
					}
					if(dt.Rows[n]["fie_size"]!=null && dt.Rows[n]["fie_size"].ToString()!="")
					{
						model.fie_size=long.Parse(dt.Rows[n]["fie_size"].ToString());
					}
					if(dt.Rows[n]["file_path"]!=null && dt.Rows[n]["file_path"].ToString()!="")
					{
					model.file_path=dt.Rows[n]["file_path"].ToString();
					}
					if(dt.Rows[n]["is_deleted"]!=null && dt.Rows[n]["is_deleted"].ToString()!="")
					{
						model.is_deleted=int.Parse(dt.Rows[n]["is_deleted"].ToString());
					}
					if(dt.Rows[n]["mime_type"]!=null && dt.Rows[n]["mime_type"].ToString()!="")
					{
					model.mime_type=dt.Rows[n]["mime_type"].ToString();
					}
					if(dt.Rows[n]["create_at"]!=null && dt.Rows[n]["create_at"].ToString()!="")
					{
						model.create_at=DateTime.Parse(dt.Rows[n]["create_at"].ToString());
					}
					if(dt.Rows[n]["updated_at"]!=null && dt.Rows[n]["updated_at"].ToString()!="")
					{
						model.updated_at=DateTime.Parse(dt.Rows[n]["updated_at"].ToString());
					}
					if(dt.Rows[n]["sort"]!=null && dt.Rows[n]["sort"].ToString()!="")
					{
						model.sort=int.Parse(dt.Rows[n]["sort"].ToString());
					}
					if(dt.Rows[n]["cuserids"]!=null && dt.Rows[n]["cuserids"].ToString()!="")
					{
					model.cuserids=dt.Rows[n]["cuserids"].ToString();
					}
					if(dt.Rows[n]["cuseridss"]!=null && dt.Rows[n]["cuseridss"].ToString()!="")
					{
					model.cuseridss=dt.Rows[n]["cuseridss"].ToString();
					}
					if(dt.Rows[n]["is_end"]!=null && dt.Rows[n]["is_end"].ToString()!="")
					{
						model.is_end=int.Parse(dt.Rows[n]["is_end"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
		/// <summary>
		/// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
        //}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			return dal.GetList(PageSize,PageIndex,strWhere);
		}

		#endregion  Method
	}
}

