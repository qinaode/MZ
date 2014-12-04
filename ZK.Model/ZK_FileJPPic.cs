/**  智客知识管理平台。
* ZK_FileJPPic.cs
*
* 功 能： N/A
* 类 名： ZK_FileJPPic
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/15 21:49:37   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ZK.Model
{
	/// <summary>
	/// ZK_FileJPPic:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ZK_FileJPPic
	{
		public ZK_FileJPPic()
		{}
		#region Model
		private int _id;
		private int? _filejptypeid;
		private string _imagename;
		private string _imageurl;
		private int? _sortnum;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? fileJPTypeID
		{
			set{ _filejptypeid=value;}
			get{return _filejptypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string imageName
		{
			set{ _imagename=value;}
			get{return _imagename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string imageURL
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sortNum
		{
			set{ _sortnum=value;}
			get{return _sortnum;}
		}
		#endregion Model

	}
}

