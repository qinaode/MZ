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
using System.Collections.Generic;
using ZK.Common;
using ZK.Model;
using ZK.DALFactory;
using ZK.IDAL;
namespace ZK.BLL
{
	/// <summary>
	/// file_base
	/// </summary>
	public partial class file_base
	{
		private readonly Ifile_base dal=DataAccess.Createfile_base();
		public file_base()
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
		public bool Exists(int file_id)
		{
			return dal.Exists(file_id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZK.Model.file_base model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ZK.Model.file_base model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int file_id)
		{
			
			return dal.Delete(file_id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string file_idlist )
		{
			return dal.DeleteList(file_idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZK.Model.file_base GetModel(int file_id)
		{
			
			return dal.GetModel(file_id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ZK.Model.file_base GetModelByCache(int file_id)
		{
			
			string CacheKey = "file_baseModel-" + file_id;
			object objModel = ZK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(file_id);
					if (objModel != null)
					{
						int ModelCache = ZK.Common.ConfigHelper.GetConfigInt("ModelCache");
						ZK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZK.Model.file_base)objModel;
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
		public List<ZK.Model.file_base> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ZK.Model.file_base> DataTableToList(DataTable dt)
		{
			List<ZK.Model.file_base> modelList = new List<ZK.Model.file_base>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ZK.Model.file_base model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new ZK.Model.file_base();
					if(dt.Rows[n]["file_id"]!=null && dt.Rows[n]["file_id"].ToString()!="")
					{
						model.file_id=int.Parse(dt.Rows[n]["file_id"].ToString());
					}
					if(dt.Rows[n]["file_name"]!=null && dt.Rows[n]["file_name"].ToString()!="")
					{
					model.file_name=dt.Rows[n]["file_name"].ToString();
					}
					if(dt.Rows[n]["file_desc"]!=null && dt.Rows[n]["file_desc"].ToString()!="")
					{
					model.file_desc=dt.Rows[n]["file_desc"].ToString();
					}
					if(dt.Rows[n]["file_type"]!=null && dt.Rows[n]["file_type"].ToString()!="")
					{
						model.file_type=int.Parse(dt.Rows[n]["file_type"].ToString());
					}
					if(dt.Rows[n]["parent_file_id"]!=null && dt.Rows[n]["parent_file_id"].ToString()!="")
					{
						model.parent_file_id=int.Parse(dt.Rows[n]["parent_file_id"].ToString());
					}
					if(dt.Rows[n]["create_time"]!=null && dt.Rows[n]["create_time"].ToString()!="")
					{
						model.create_time=DateTime.Parse(dt.Rows[n]["create_time"].ToString());
					}
					if(dt.Rows[n]["update_time"]!=null && dt.Rows[n]["update_time"].ToString()!="")
					{
						model.update_time=DateTime.Parse(dt.Rows[n]["update_time"].ToString());
					}
					if(dt.Rows[n]["file_size"]!=null && dt.Rows[n]["file_size"].ToString()!="")
					{
						model.file_size=long.Parse(dt.Rows[n]["file_size"].ToString());
					}
					if(dt.Rows[n]["file_path"]!=null && dt.Rows[n]["file_path"].ToString()!="")
					{
					model.file_path=dt.Rows[n]["file_path"].ToString();
					}
					if(dt.Rows[n]["file_ext"]!=null && dt.Rows[n]["file_ext"].ToString()!="")
					{
					model.file_ext=dt.Rows[n]["file_ext"].ToString();
					}
					if(dt.Rows[n]["file_download"]!=null && dt.Rows[n]["file_download"].ToString()!="")
					{
					model.file_download=dt.Rows[n]["file_download"].ToString();
					}
					if(dt.Rows[n]["channel_id"]!=null && dt.Rows[n]["channel_id"].ToString()!="")
					{
						model.channel_id=int.Parse(dt.Rows[n]["channel_id"].ToString());
					}
					if(dt.Rows[n]["is_dir"]!=null && dt.Rows[n]["is_dir"].ToString()!="")
					{
						model.is_dir=int.Parse(dt.Rows[n]["is_dir"].ToString());
					}
					if(dt.Rows[n]["owner_id"]!=null && dt.Rows[n]["owner_id"].ToString()!="")
					{
						model.owner_id=int.Parse(dt.Rows[n]["owner_id"].ToString());
					}
					if(dt.Rows[n]["channel_group_id"]!=null && dt.Rows[n]["channel_group_id"].ToString()!="")
					{
						model.channel_group_id=int.Parse(dt.Rows[n]["channel_group_id"].ToString());
					}
					if(dt.Rows[n]["is_deleted"]!=null && dt.Rows[n]["is_deleted"].ToString()!="")
					{
						model.is_deleted=int.Parse(dt.Rows[n]["is_deleted"].ToString());
					}
					if(dt.Rows[n]["download_count"]!=null && dt.Rows[n]["download_count"].ToString()!="")
					{
						model.download_count=int.Parse(dt.Rows[n]["download_count"].ToString());
					}
					if(dt.Rows[n]["view_count"]!=null && dt.Rows[n]["view_count"].ToString()!="")
					{
						model.view_count=int.Parse(dt.Rows[n]["view_count"].ToString());
					}
					if(dt.Rows[n]["chapter_id"]!=null && dt.Rows[n]["chapter_id"].ToString()!="")
					{
						model.chapter_id=int.Parse(dt.Rows[n]["chapter_id"].ToString());
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

        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        //{
        //    return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //    return dal.GetList(PageSize,PageIndex,strWhere);
        //}

		#endregion  Method
	}
}

